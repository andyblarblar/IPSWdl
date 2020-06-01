using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Mono.Options;

namespace IPSWdl
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();
        private static float TotalCount;
        private static float TotalDone;

        static async Task Main(string[] args)
        {
            //CLI args
            string pathToStoreFiles = null;
            string searchTerm = null;
            var showHelp = false;

            var p = new OptionSet() {
                "Usage: ipswdl [OPTIONS]",
                "Downloads Apple IPSW firmware files.",
                "By default, the latest IPSW is downloaded for every device available.",
                "",
                "Options:",
                { "p|path=", "Specifies the {PATH} where files will be downloaded too. If not specified, the CWD is used.",
                    p => pathToStoreFiles = p },
                { "s|search=",
                    "Only downloads for devices matching the {TERM}",
                    s => searchTerm = s},
                { "h|help",  "show this message and exit",
                    v => showHelp = v != null },
            };

            try
            { 
                p.Parse(args);
                //set path to CWD if not defined.
                pathToStoreFiles ??= Directory.GetCurrentDirectory();
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `ipswdl --help' for more information.");
                return;
            }

            if (showHelp)
            {
                p.WriteOptionDescriptions(Console.Out);
                return;
            }

            //Actual program
            Console.WriteLine("Getting devices...");
            var devices = await GetAllDevices();
            TotalCount = devices.Count;
            Console.WriteLine($"Got {TotalCount} devices!");

            var startTime = DateTime.Now;
            Console.WriteLine("Starting Downloads... This may take a very long time. Any files still downloading when the program is closed will be corrupt.");

            //Start a download of most recent IPSW for each device in serial. Downloading in parallel is better only in very high bandwidth scenarios. 
            if (searchTerm is null)
            {
                foreach (var device in devices)
                {
                    var firmware = await GetFirmwaresForDevice(device);
                    await DownloadMostRecentFirmware(firmware, pathToStoreFiles);
                }
            }
            else //only download based on search term if passed
            {
                TotalCount = devices.Count(d => d.name.Contains(searchTerm));
                foreach (var device in devices.Where(d => d.name.Contains(searchTerm)))
                {
                    var firmware = await GetFirmwaresForDevice(device);
                    await DownloadMostRecentFirmware(firmware, pathToStoreFiles);
                }
            }

            Console.WriteLine($"Completed in {(DateTime.Now - startTime).TotalMinutes} minutes");
            Console.WriteLine($"Press any key to quit...");
            Console.ReadKey();
        }


        public static async Task<List<JsonReps.device>> GetAllDevices()
        {
            var res = await Client.GetAsync("https://api.ipsw.me/v4/devices");
            return JsonSerializer.Deserialize<List<JsonReps.device>>(await res.Content.ReadAsStringAsync());
        }

        public static async Task<JsonReps.FirmwareListing> GetFirmwaresForDevice(JsonReps.device device)
        {
            var res = await Client.GetAsync($"https://api.ipsw.me/v4/device/{device.identifier}?type=ipsw");
            var firmware = JsonSerializer.Deserialize<JsonReps.FirmwareListing>(await res.Content.ReadAsStringAsync());

            //sanitize name to avoid directory symbols
            firmware.name = firmware.name.Replace('/', 'z');
            firmware.name = firmware.name.Replace('\\', 'z');

            return firmware;
        }

        public static async Task DownloadMostRecentFirmware(JsonReps.FirmwareListing firmwareListing, string basePathToFolder)
        {
            //firmware[0] is always the newest
            var res = await Client.GetAsync($"https://api.ipsw.me/v4/ipsw/download/{firmwareListing.firmwares[0].identifier}/{firmwareListing.firmwares[0].buildid}");
            var urlToDownload = res.Headers.Location;

            Console.WriteLine($"Beginning to download {firmwareListing.name} {firmwareListing.firmwares[0].version}");

            Directory.CreateDirectory(Path.Join(basePathToFolder, $@"/IPSW/{firmwareListing.name}/"));
            await using var fileStream = File.Create(Path.Join(basePathToFolder, $@"/IPSW/{firmwareListing.name}/{firmwareListing.firmwares[0].version}.ipsw"));
            await using var dlStream = await Client.GetStreamAsync(urlToDownload);

            //download the file
            await dlStream.CopyToAsync(fileStream);

            ++TotalDone;
            Console.Write($"Finished downloading {firmwareListing.name} {firmwareListing.firmwares[0].version}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"                      {(TotalDone/TotalCount) * 100}% complete");
            Console.ForegroundColor = ConsoleColor.White;
        }



    }
}

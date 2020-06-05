using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mono.Options;
using Utf8Json;

namespace IPSWdl
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();
        private static float TotalCount;
        private static float TotalDone;

        static async Task Main(string[] args)
        {
            //Wire up UTF8Json AOT generated class 
            Utf8Json.Resolvers.CompositeResolver.RegisterAndSetAsDefault(
                Utf8Json.Resolvers.GeneratedResolver.Instance,
                Utf8Json.Resolvers.BuiltinResolver.Instance
            );
            
            //CLI args
            string pathToStoreFiles = null;
            string searchTerm = null;
            var showHelp = false;
            var downloadAll = false;

            var p = new OptionSet() {
                "Usage: ipswdl [OPTIONS]",
                "Downloads Apple IPSW firmware files.",
                "The latest firmware is downloaded by default.",
                "",
                "Options:",
                { "p|path=", "Specifies the {PATH} where files will be downloaded too. If not specified, the CWD is used.",
                    p => pathToStoreFiles = p },
                { "s|search=",
                    "Only downloads for devices matching the {TERM}",
                    s => searchTerm = s},
                { "A|All",
                    "Downloads the newest firmware for all devices.",
                    a => downloadAll = a != null
                },
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
                Console.WriteLine("Try `ipswdl --help` for more information.");
                return;
            }

            //quit early if neither -A or -s are passed
            if (showHelp || (!downloadAll && searchTerm is null))
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
            Console.WriteLine("Starting Downloads... This may take a very long time.");

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


        public static async Task<List<JsonReps.Device>> GetAllDevices()
        {
            var res = await Client.GetAsync("https://api.ipsw.me/v4/devices");
            return JsonSerializer.Deserialize<List<JsonReps.Device>>(await res.Content.ReadAsStringAsync());
        }

        public static async Task<JsonReps.FirmwareListing> GetFirmwaresForDevice(JsonReps.Device device)
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
            //leave if no firmware is found
            if (firmwareListing.firmwares.Count == 0)
            {
                ++TotalDone;
                Console.Write($"{firmwareListing.name} has no firmware for download");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"                      {(TotalDone / TotalCount) * 100}% complete");
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }

            //firmware[0] is always the newest
            var res = await Client.GetAsync($"https://api.ipsw.me/v4/ipsw/download/{firmwareListing.firmwares[0].identifier}/{firmwareListing.firmwares[0].buildid}");
            var urlToDownload = res.Headers.Location;

            Console.WriteLine($"Beginning to download {firmwareListing.name} {firmwareListing.firmwares[0].version}");

            //If file has already been downloaded, skip
            if (File.Exists(Path.Join(basePathToFolder, $@"/IPSW/{firmwareListing.name}/{firmwareListing.firmwares[0].version}.ipsw")))
            {
                ++TotalDone;
                Console.Write($"{firmwareListing.name} {firmwareListing.firmwares[0].version} already exists. Skipping download");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"                      {(TotalDone / TotalCount) * 100}% complete");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            //Create file
            Directory.CreateDirectory(Path.Join(basePathToFolder, $@"/IPSW/{firmwareListing.name}/"));
            await using var fileStream = File.Create(Path.Join(basePathToFolder, $@"/IPSW/{firmwareListing.name}/{firmwareListing.firmwares[0].version}.ipsw"));
            await using var dlStream = await Client.GetStreamAsync(urlToDownload);

            using var cts = new CancellationTokenSource();

            //Delete currently downloading files if program is cancelled
            void DeleteCorruptFile(object sender, ConsoleCancelEventArgs args)
            {
                cts.Cancel();
                fileStream.Dispose();
                dlStream.Dispose();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Stop Command recived. Deleting corrupt downloads...");
                Console.ForegroundColor = ConsoleColor.Gray;
                try
                {
                    File.Delete(Path.Join(basePathToFolder, $@"/IPSW/{firmwareListing.name}/{firmwareListing.firmwares[0].version}.ipsw"));
                    Console.WriteLine($"Deleted {Path.Join(basePathToFolder, $@"/IPSW/{firmwareListing.name}/{firmwareListing.firmwares[0].version}.ipsw")}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"There was an error deleting {Path.Join(basePathToFolder, $@"/IPSW/{firmwareListing.name}/{firmwareListing.firmwares[0].version}.ipsw")}");
                }

            }

            Console.CancelKeyPress += DeleteCorruptFile;

            //download the file
            try
            {
                await dlStream.CopyToAsync(fileStream, cts.Token);
            }
            catch (TaskCanceledException)
            {
                //ignore, handled by the delegate above.
            }

            Console.CancelKeyPress -= DeleteCorruptFile;

            ++TotalDone;
            Console.Write($"Finished downloading {firmwareListing.name} {firmwareListing.firmwares[0].version}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"                      {(TotalDone/TotalCount) * 100}% complete");
            Console.ForegroundColor = ConsoleColor.Gray;
        }



    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IPSWdl
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();
        private static float TotalCount;
        private static float TotalDone;

        static async Task Main(string[] args)
        {
            //the base directory the IPSW folder will be made in.
            var pathToStoreFiles = args[0] ??= Directory.GetCurrentDirectory();

            Console.WriteLine("Getting devices...");
            var devices = await GetAllDevices();
            TotalCount = devices.Count;
            Console.WriteLine($"Got {TotalCount} devices!");

            var startTime = DateTime.Now;
            Console.WriteLine("Starting Downloads... This will take a very long time. DO NOT CLOSE THE PROGRAM.");

            //Start a download of most recent IPSW for each device, and await all.
            var dlTasks = new List<Task>();
            foreach (var device in devices)
            {
                var firmware = await GetFirmwaresForDevice(device);
                dlTasks.Add(DownloadMostRecentFirmware(firmware, pathToStoreFiles));
            }

            Task.WaitAll(dlTasks.ToArray());

            Console.WriteLine($"Done in {(DateTime.Now - startTime).TotalMinutes} minutes");
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
            Console.WriteLine($"Finished downloading {firmwareListing.name} {firmwareListing.firmwares[0].version} {(TotalDone/TotalCount) * 100}%");
        }



    }
}

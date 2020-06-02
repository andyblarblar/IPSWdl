using System;
using System.Collections.Generic;
using System.Text;

namespace IPSWdl
{
    public static class JsonReps
    {
        public struct Device
        {
            public string name { get; set; }
            public string identifier { get; set; }
            public string platform { get; set; }
            public int cpid { get; set; }
            public int bdid { get; set; }

        }

        public struct Firmware
        {
            public string identifier { get; set; }
            public string version { get; set; }
            public string buildid { get; set; }
            public string sha1sum { get; set; }
            public string md5sum { get; set; }
            public long filesize { get; set; }
            public string url { get; set; }

            //public DateTime releasedate { get; set; }

            public DateTime uploaddate { get; set; }
        }

        public struct FirmwareListing
        {
            public string name { get; set; }
            public string identifier { get; set; }
            public string boardconfig { get; set; }
            public string platform { get; set; }
            public int cpid { get; set; }
            public int bdid { get; set; }

            public List<Firmware> firmwares { get; set; } //Dates of firmwares are ordered. index 0 is always the newest

        }

        /// <summary>
        /// only for tricking the utf8json genorator into giving me the type i need lol
        /// </summary>
        public struct jank
        {
            public List<Device> devices { get; set; }
        }




    }
}

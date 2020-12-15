# IPSWdl
CLI for downloading Apple's IPSW firmware files.

To run ipswdl from a terminal you need to pass either -s or -A. -s=TERM downloads the most recent firmware for all devices whos name contains 
TERM. -A simply downloads for all devices. 

If you cancel the program while downloading, it should automaticly delete in progress downloads.

# Building from source
If you wish to have a .net core ipswdl, simply compile by any normal .net means.

To build a native image from source, first clone this repo. Now open the 'x64 native tools command prompt' installed with visual studio. If you do not have this installed,
you will need to install it using the visual studio installer. Next, `cd` into the cloned repo. Now run `dotnet publish -c release -r win-x64`. this should generate 
a native exe in the bin/x64/native folder.

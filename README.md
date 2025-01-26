# WolvenKit Serializer

Simple program for uncooking CR2W files into JSON through STDIO using [WolvenKit libraries](https://github.com/WolvenKit/WolvenKit).  
It is supposed to be used by other program.

## Usage

Run the program with the path to Cyberpunk 2077 as an argument.

The communication with this program goes as follows:

1. The program outputs `[LOADING]` after starting.
2. Once ready, it ouputs `[READY]`.
3. After that you can pass realtive game file paths to the STDIN.
4. If everything goes well, output will be `[OUTPUT]` followed by a space and the serialized JSON.  
   If something fails it will be `[ERROR]` followed by a space and a message.
5. Once you're done, enter empty line, close the STDIO or just kill the process.

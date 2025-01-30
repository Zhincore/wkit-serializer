# WolvenKit IPC

Simple program for using a selected funtions of [WolvenKit](https://github.com/WolvenKit/WolvenKit) through STDIO.  
It is supposed to be used by other program.

## Usage

Run the program inside WolvenKit CLI folder with the path to Cyberpunk 2077 as an argument.

The communication with this program goes as follows:

1. The program outputs `[LOADING]` after starting.
2. Once ready, it ouputs `[READY]`.
3. After that you can pass [commands](#commands) to the STDIN.
4. If everything goes well, output will be `[OUTPUT]` followed by a space and the output.  
   If something fails it will be `[ERROR]` followed by a space and a message.
5. Once you're done, enter empty line, close the STDIO or just kill the process.

### Commands

These commands can be passed to the STDIN:

- `search [query]` - lists files containing given query (or all files if you dont pass a query)
  - **Returns** a semicolon separated list of relative paths (or empty string if nothing found).
- `serialize <path>` - Tries to convert file at given path to JSON.
  - **Returns** a JSON

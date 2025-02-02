using System.Text;
using WolvenKit.Common;
using WolvenKit.Common.Conversion;
using WolvenKit.Common.Services;
using WolvenKit.RED4.CR2W;
using WolvenKit.RED4.CR2W.Archive;
using WolvenKit.RED4.CR2W.JSON;

if (args.Length < 1)
{
  WriteError("Missing game path argument.");
  Environment.Exit(1);
}

Console.WriteLine("[LOADING]");
var loggerService = new MockLoggerService();
var hashService = new HashService();
var hookService = new HookService();
var fileService = new Red4ParserService(hashService, loggerService, hookService);
var progressService = new MockProgressService();

var archiveManager = new ArchiveManager(
  hashService,
  fileService,
  loggerService,
  progressService
);

var serializeOptions = new RedJsonSerializerOptions
{
  SkipHeader = true
};
RedJsonSerializer.Options.WriteIndented = false;

var executable = new FileInfo(Path.Combine(args[0], "bin", "x64", "Cyberpunk2077.exe"));
archiveManager.LoadGameArchives(executable);

Console.WriteLine("[READY]");

while (true)
{
  var line = Console.ReadLine();
  if (line == null || line == "") break;
  var command = string.Concat(line.TakeWhile(c => c != ' '));
  var rest = line[command.Length..].Trim();

  try
  {
    switch (command)
    {
      case "search":
        Search(rest);
        break;
      case "serialize":
        await Serialize(rest);
        break;
      default:
        WriteError("Unknown command.");
        break;
    }
  }
  catch (Exception e)
  {
    WriteError($"{e.GetType()}: {e.Message}");
  }
}

void Search(string query)
{
  var files = from file in archiveManager.Search(query, ArchiveManagerScope.Basegame) select file.FileName;
  WriteOutput(string.Join(";", files ?? []));
}

async Task Serialize(string path)
{
  using var file = archiveManager.GetCR2WFile(path, false, false);
  if (file == null)
  {
    WriteError("File not found.");
    return;
  }

  var dto = new RedFileDto(file);

  using var stdout = Console.OpenStandardOutput();
  stdout.Write(Encoding.ASCII.GetBytes("[OUTPUT] "));
  await RedJsonSerializer.SerializeAsync(stdout, dto, serializeOptions);
  stdout.Write(Encoding.ASCII.GetBytes("\n"));
  stdout.Flush();
}

static void WriteError(string message)
{
  Console.WriteLine("[ERROR] " + message);
}

static void WriteOutput(string message)
{
  Console.WriteLine("[OUTPUT] " + message);
}

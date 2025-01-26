using CP77Tools.Tasks;
using WolvenKit.Common.Conversion;
using WolvenKit.Common.Services;
using WolvenKit.Core.Extensions;
using WolvenKit.Interfaces.Extensions;
using WolvenKit.RED4.CR2W;
using WolvenKit.RED4.CR2W.Archive;
using WolvenKit.RED4.CR2W.JSON;

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

if (args.Length < 1)
{
  Console.WriteLine("[ERROR] Missing game path argument.");
  Environment.Exit(1);
}

var executable = new FileInfo(args[0]);
archiveManager.LoadGameArchives(executable);

Console.WriteLine("[READY]");

while (true)
{
  var line = Console.ReadLine();
  if (line == null || line == "") break;

  using var file = archiveManager.GetCR2WFile(line, false, false);
  if (file == null)
  {
    Console.WriteLine("[ERROR] File not found.");
    continue;
  }

  var dto = new RedFileDto(file);
  var json = RedJsonSerializer.Serialize(dto);
  if (json == null)
  {
    Console.WriteLine("[ERROR] Failed to serialize file.");
    continue;
  }

  Console.WriteLine("[OUTPUT] " + json);
}


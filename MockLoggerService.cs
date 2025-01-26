using Microsoft.Build.Framework;
using WolvenKit.Core.Interfaces;

class MockLoggerService : ILoggerService
{
  public LoggerVerbosity LoggerVerbosity => LoggerVerbosity.Quiet;

  public void Debug(string msg)
  {
  }

  public void Error(string msg)
  {
  }

  public void Error(Exception exception)
  {
  }

  public void Info(string s)
  {
  }

  public void SetLoggerVerbosity(LoggerVerbosity verbosity)
  {
  }

  public void Success(string msg)
  {
  }

  public void Warning(string s)
  {
  }
}

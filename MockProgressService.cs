using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.Build.Framework;
using WolvenKit.Core.Interfaces;
using WolvenKit.Core.Services;

class MockProgressService : IProgressService<double>
{
  public bool IsIndeterminate { get => false; set => Expression.Empty(); }
  public EStatus Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

  public event PropertyChangedEventHandler? PropertyChanged = null;
  public event EventHandler<double>? ProgressChanged;

  public void Completed()
  {
  }

  public void Report(double value)
  {
  }
}

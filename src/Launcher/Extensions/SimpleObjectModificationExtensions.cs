#nullable enable
using System;
using System.Linq;
using War3Net.Build.Object;

namespace Launcher.Extensions
{
  public static class SimpleObjectModificationExtensions
  {
    public static SimpleObjectDataModification? GetDataModification(this SimpleObjectModification objectModification, int key) =>
      objectModification.Modifications.FirstOrDefault(x => x.Id == key);

    public static void SetDataModification(this SimpleObjectModification objectModification, int key, object newValue)
    {
      var dataModification = objectModification.Modifications.FirstOrDefault(x => x.Id == key);
      if (dataModification == null)
        throw new InvalidOperationException($"There is no data modification with key {key}.");
      
      dataModification.Value = newValue;
    }
  }
}
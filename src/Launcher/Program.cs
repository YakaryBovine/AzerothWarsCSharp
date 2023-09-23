using System;
using System.CommandLine;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Launcher.Commands;
using Launcher.Settings;
using Microsoft.Extensions.Configuration;

namespace Launcher
{
  internal static class Program
  {
    /// <summary>
    ///   Entry point for the program.
    /// </summary>
    private static int Main(string[] args)
    {
      var rootCommand = new RootCommand();
      rootCommand.RegisterMapDataToW3XCommand();
      rootCommand.RegisterW3XToMapDataCommand();
      return rootCommand.Invoke(args);
    }

    //Todo: shouldn't be in Program.cs
    public static IConfiguration GetAppConfiguration()
    {
      var userAppsettingsFileName = $"appsettings.{Environment.UserName}.json";
      var projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent;
      var projectDirPath = Path.GetDirectoryName(projectDir?.FullName);
      var userAppsettingsFilePath = $"{projectDirPath}\\{userAppsettingsFileName}";

      if (!File.Exists(userAppsettingsFilePath))
        CreateUserAppSettings(userAppsettingsFilePath);

      return new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile(userAppsettingsFilePath, true, true)
        .Build();
    }

    private static void CreateUserAppSettings(string userAppsettingsFilePath)
    {
      var settings = new JsonSerializerOptions
      {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
      };
      File.WriteAllText(userAppsettingsFilePath, JsonSerializer.Serialize(new AppSettings()
      {
        CompilerSettings = new CompilerSettings
        {
          TestingPlayerSlot = 0,
          Warcraft3ExecutablePath = ""
        }
      }, settings));
    }
  }
}
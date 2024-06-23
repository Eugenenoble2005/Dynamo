using Dynamo_Desktop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services;

public class SettingsService
{
    public static Settings GetSettings()
    {
        return JsonSerializer.Deserialize<Settings>(File.ReadAllText(
            AppDomain.CurrentDomain.BaseDirectory+"/settings.json"
            ));
    }

    public static void SetSettings(Settings settings)
    {
        Debug.WriteLine(("attempting change"));
        string dir = AppDomain.CurrentDomain.BaseDirectory + "/settings.json";
        File.WriteAllText(dir,JsonSerializer.Serialize(settings));
    }
    
}

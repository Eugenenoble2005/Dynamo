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
    public static Settings Settings()
    {
        return JsonSerializer.Deserialize<Settings>(File.ReadAllText(
            AppDomain.CurrentDomain.BaseDirectory+"/settings.json"
            ));
    }
}

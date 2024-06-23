using System.Collections.Generic;
using Dynamo_Desktop.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Dynamo_Desktop.ViewModels;

public class SettingsViewModel : ReactiveObject
{
    public List<string> MediaPlayers => new() { "mpv", "vlc" };
    public string CurrentMediaPlayer => SettingsService.GetSettings().media_player;
    
}
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using FluentAvalonia.UI.Controls;

namespace Dynamo_Desktop.Services;

public  class VideoService
{
    public event EventHandler<EventArgs> ProcessExited;

    public VideoService()
    {
        ProcessExited += ProcessExited;
    }
    public void Play(string Source = null)
    {
        string media_player = SettingsService.Settings().media_player;
        switch (media_player)
        {
            case "vlc":
                PLayWithVlc(Source);
                break;
            case "mpv":
                PLayWithMpv(Source);
                break;
            default:
                break;
        }
       
    }
    private  void PLayWithMpv(string source = null)
    {
        //play video with mpv player. Mpv will be bundled for windows, however the user will be required to install mpv on linux
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(new string[] {
                    AppDomain.CurrentDomain.BaseDirectory,
                    "mpv",
                    "mpv.exe"
                }) : "mpv",
                Arguments = $"{source} --force-window=immediate",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true, 
                //play with the bundled mpv binaries on windows. Run mpv anywhere on linux assuming global installation
            },
            EnableRaisingEvents = true
        };
        proc.Exited += (sender, args) =>
        {
            ProcessExited?.Invoke(this,EventArgs.Empty);
        };
        proc.Start();
    }

    private async void PLayWithVlc(string source = null)
    {
        //vlc is assumed to be globally accessible on linux
        string vlcDirOnWindows = "C:\\Program Files (x86)\\VideoLAN\\VLC";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (!Directory.Exists(vlcDirOnWindows))
            {
                vlcDirOnWindows = "C:\\Program Files\\VideoLAN\\VLC";
                if (!Directory.Exists(vlcDirOnWindows))
                {
                    var dialog = new ContentDialog()
                    {
                        Title = "Error",
                        Content = "VLC media player is not installed on this machine. Play with bundled mpv?",
                        PrimaryButtonText = "ok",
                        CloseButtonText = "Dismiss"
                    };
                    var response = await dialog.ShowAsync();
                    if (response == ContentDialogResult.Primary)
                    {
                        PLayWithMpv(source);
                    }
                    else
                    {
                        return;
                    }
                }
            }

        }
        try
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(new string[] {vlcDirOnWindows,"vlc.exe"}) : "vlc",
                    Arguments = $"{source}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true, 
                    //play with the bundled mpv binaries on windows
                    WorkingDirectory = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? vlcDirOnWindows : "",
                },
                EnableRaisingEvents = true
            };
            proc.Exited += (sender, args) =>
            {
                ProcessExited.Invoke(this,EventArgs.Empty);
            };
            proc.Start();
        }
       catch{}
    }
}

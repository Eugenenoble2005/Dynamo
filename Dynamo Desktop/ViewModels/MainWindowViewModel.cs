using Avalonia;
using Avalonia.Themes.Fluent;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Controls;
using LibVLCSharp.Shared;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Dynamo_Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly LibVLC _libVlc = new LibVLC();
    public MediaPlayer MediaPlayer { get; }
    public string Greeting => "Welcome to Avalonia!";
    public MainWindowViewModel()
    {
        MediaPlayer = new MediaPlayer(_libVlc);
    }
    public void Play()
    {
        using var media = new Media(_libVlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
        MediaPlayer.Play(media);
    }


}

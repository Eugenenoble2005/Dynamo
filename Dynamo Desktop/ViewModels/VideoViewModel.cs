using Avalonia.Controls;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAvalonia.UI.Controls;
using System.Net;
using ReactiveUI;

namespace Dynamo_Desktop.ViewModels
{
    internal class VideoViewModel : ViewModelBase
    {

        private readonly LibVLC _libVlc = new LibVLC();
        public string Url { get; set; }
        public MediaPlayer MediaPlayer { get; }
        private string _playButtonIcon = WebUtility.HtmlDecode("&#xF8AE;");
        public string PlayButtonIcon { get => _playButtonIcon; set => this.RaiseAndSetIfChanged(ref _playButtonIcon, value); }
        public void Play()
        {
            using var media = new Media(_libVlc, new Uri(Url));
            MediaPlayer.Play(media);
           
        }
        public void TogglePause()
        {
            MediaPlayer.Pause();
            if (!MediaPlayer.IsPlaying)
            {
                PlayButtonIcon = WebUtility.HtmlDecode("&#xF8AE;");
            }
            else
            {
                PlayButtonIcon = WebUtility.HtmlDecode("&#xF5B0;");
            }
        }
        public void StopPlayer()
        {
            MediaPlayer.Stop();
        }
        public VideoViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
           
        }
    }
}

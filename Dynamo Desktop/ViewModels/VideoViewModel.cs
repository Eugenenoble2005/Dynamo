using Avalonia.Controls;
using LibVLCSharp.Shared;
using System;
using System.Net;
using ReactiveUI;
using System.Diagnostics;

namespace Dynamo_Desktop.ViewModels
{
    public class VideoViewModel : ViewModelBase
    {

        private readonly LibVLC _libVlc = new LibVLC();
        private string _videoTimestamp = "00:00";
        private long _videoMillSecs = 0;
        private long _currentMillSecs = 0;
        private long _previousMillSecs = 0;
        private string _currentTimestamp = "00:00";
        public string Url { get; set; }
        public MediaPlayer MediaPlayer { get; }
        public string _playButtonIcon = WebUtility.HtmlDecode("&#xF8AE;");
        public string VideoTimeStamp { get => _videoTimestamp; set => this.RaiseAndSetIfChanged(ref _videoTimestamp, value); }
        public long VideoMillSecs { get => _videoMillSecs; set => this.RaiseAndSetIfChanged(ref _videoMillSecs, value); } 
        public long CurrentMillSecs { get => _currentMillSecs; set { this.RaiseAndSetIfChanged(ref _currentMillSecs, value);SeekToTime(); } }
        public string CurrentTimeStamp { get => _currentTimestamp; set => this.RaiseAndSetIfChanged(ref _currentTimestamp, value); }
        public string PlayButtonIcon { get => _playButtonIcon; set => this.RaiseAndSetIfChanged(ref _playButtonIcon, value); }
        private bool isSeeking = false;

        public void Play()
        {
            using var media = new Media(_libVlc, new Uri(Url));
            MediaPlayer.Play(media);
            MediaPlayer.TimeChanged += (sender, args) =>
            {
                TimeSpan timespan = TimeSpan.FromMilliseconds(args.Time);
                CurrentTimeStamp = timespan.ToString("mm\\:ss");
                if(isSeeking == false)
                {
                    CurrentMillSecs = args.Time;
                }
            
            };
            media.DurationChanged += (sender, args) =>
            {
                TimeSpan timespan = TimeSpan.FromMilliseconds(args.Duration);
                VideoTimeStamp = timespan.ToString("mm\\:ss");
                VideoMillSecs = args.Duration;
            };
           
        }
        public void SeekToTime()
        {
            if (CurrentMillSecs - _previousMillSecs > 1000 || _previousMillSecs - CurrentMillSecs > 1000)
            {
                isSeeking = true;
                TimeSpan timespan = TimeSpan.FromMilliseconds(CurrentMillSecs);
                    MediaPlayer.SeekTo(timespan);
                isSeeking = false;
        
            }
            _previousMillSecs = CurrentMillSecs;

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

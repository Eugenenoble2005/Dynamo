using Avalonia.Controls;
using LibVLCSharp.Shared;
using System;
using System.Net;
using ReactiveUI;
using System.Diagnostics;
using FluentAvalonia.UI.Controls;
namespace Dynamo_Desktop.ViewModels
{
    public class VideoViewModel : ViewModelBase
    {

        private readonly LibVLC _libVlc = new LibVLC(enableDebugLogs:true);
        private string _videoTimestamp = "00:00";
        private long _videoMillSecs = 0;
        private string _title;
        private long _currentMillSecs = 0;
        private bool _videoBuffering = false;
        private long _previousMillSecs = 0;
        private string _currentTimestamp = "00:00";
        public string Url { get; set; }
        public string Referrer { get; set; }
        public MediaPlayer MediaPlayer { get; }
        public string _playButtonIcon = WebUtility.HtmlDecode("&#xF8AE;");
        public string Title { get => _title; set => this.RaiseAndSetIfChanged(ref _title, value); }
        public string VideoTimeStamp { get => _videoTimestamp; set => this.RaiseAndSetIfChanged(ref _videoTimestamp, value); }
        public long VideoMillSecs { get => _videoMillSecs; set => this.RaiseAndSetIfChanged(ref _videoMillSecs, value); } 
        public long CurrentMillSecs { get => _currentMillSecs; set { this.RaiseAndSetIfChanged(ref _currentMillSecs, value);
                if (isSeeking)
                {
                    Seek();
                }
               
            } }
        public bool VideoBuffering { get => _videoBuffering; set => this.RaiseAndSetIfChanged(ref _videoBuffering, value); }
        public string CurrentTimeStamp { get => _currentTimestamp; set => this.RaiseAndSetIfChanged(ref _currentTimestamp, value); }
        public string PlayButtonIcon { get => _playButtonIcon; set => this.RaiseAndSetIfChanged(ref _playButtonIcon, value); }
        private bool isSeeking = false;

        public async void Play()
        {
            _libVlc.Log += (sender, args) =>
            {
             //   Debug.WriteLine(args.Message);
            };
            using var media = new Media(_libVlc, new Uri(Url));
            string user_agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36";
            Debug.WriteLine(Referrer);
            Debug.WriteLine(Url);
            media.AddOption(":http-referrer="+Referrer);
            media.AddOption(":http-user-agent="+user_agent);
            media.AddOption(":verbose="+2);
            media.AddOption(":no-eit");
            MediaPlayer.Play(media);
           // MediaPlayer.EnableKeyInput = true;
            MediaPlayer.TimeChanged += (sender, args) =>
                {

                    TimeSpan timespan = TimeSpan.FromMilliseconds(args.Time);
                    CurrentTimeStamp = timespan.ToString("mm\\:ss");
                    //do not seek during playback 
                    isSeeking = false;
                        CurrentMillSecs = args.Time;
                    isSeeking = true;
                    

                };
            MediaPlayer.Buffering += (sender, args) =>
            {
                VideoBuffering = true;
                Debug.WriteLine(args.Cache);
                if (args.Cache == 100)
                {
                    VideoBuffering = false;
                }
                Debug.WriteLine(args.Cache);
            };
            media.DurationChanged += (sender, args) =>
                {
                    TimeSpan timespan = TimeSpan.FromMilliseconds(args.Duration);
                    VideoTimeStamp = timespan.ToString("mm\\:ss");
                    VideoMillSecs = args.Duration;
                };
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
        public void Seek()
        {
   
            MediaPlayer.SeekTo(TimeSpan.FromMilliseconds(CurrentMillSecs));
         
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

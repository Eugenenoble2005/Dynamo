using Dynamo_Desktop.Models.Anime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo_Desktop.Services.Anime;
using System.Diagnostics;
using System.Text.Json;
using ReactiveUI;
using Dynamo_Desktop.Services;
using System.Net.Http;
using MessageBox.Avalonia.Models;
using MessageBox.Avalonia.Enums;
using Avalonia;
using FluentAvalonia.UI.Controls;
using System.Windows.Input;
using MessageBox.Avalonia.ViewModels.Commands;

namespace Dynamo_Desktop.ViewModels.Anime
{
    public class IndexViewModel : ViewModelBase
    {
        //anime services;
        private GogoAnimeService GogoAnimeService = new GogoAnimeService();


        //internal properties
        private GogoAnimeRecentEpisodes gogoAnimeRecentEpisodes = new GogoAnimeRecentEpisodes();
        private ICommand RetryCommand { get; }

        //external properties
        public GogoAnimeRecentEpisodes GogoAnimeRecentEpisodes { get => gogoAnimeRecentEpisodes; set => this.RaiseAndSetIfChanged(ref gogoAnimeRecentEpisodes, value); }
    
        public string Name => "Eugene Noble";

        public IndexViewModel()
        {
            GetRecentEpisodes();
           
        }   
        public async void GetRecentEpisodes()
        {
       
                GogoAnimeRecentEpisodes = await GogoAnimeService.RecentEpisodes();
                if(GogoAnimeRecentEpisodes == null)
            {
                var cd = new ContentDialog
                {
                    Title="Error",
                    Content="Could not fetch data. Please check your internet connection",
                    CloseButtonText="Close",
                    PrimaryButtonText="Retry",
                    DefaultButton=ContentDialogButton.Primary
                    
                };
                ContentDialogResult result = await cd.ShowAsync();
                if(result == ContentDialogResult.Primary)
                {
                    GetRecentEpisodes();
                }
            }

        }

    }
}

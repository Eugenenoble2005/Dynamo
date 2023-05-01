using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Services.Anime;
using ReactiveUI;
using FluentAvalonia.UI.Controls;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace Dynamo_Desktop.ViewModels.Anime;

public class IndexViewModel : ViewModelBase
{


    private GogoAnimeRecentEpisodes _gogoAnimeRecentEpisodes = new GogoAnimeRecentEpisodes();
    private int _page = 1;
    private bool _dataLoading = false;
    private string _sort = "Newest";
    //i'm doing this because i'm too lazy to figure out how to write a converter
    private bool _isNewest => false;
    public int Page { get => _page; set => this.RaiseAndSetIfChanged(ref _page, value); }
    public bool DataLoading { get => _dataLoading; set => this.RaiseAndSetIfChanged(ref _dataLoading, value); }
    public GogoAnimeRecentEpisodes GogoAnimeRecentEpisodes { get => _gogoAnimeRecentEpisodes; set => this.RaiseAndSetIfChanged(ref _gogoAnimeRecentEpisodes, value); }

    public List<String> SortOptions { get => new List<string> { "Newest", "Popular" }; }
    public string Sort { get => _sort; set  {this.RaiseAndSetIfChanged(ref _sort, value); SortComboChanged();} }

    public IndexViewModel()
    {
        GetRecentEpisodes();
       
    }   
    public async void GetRecentEpisodes()
    {
            DataLoading = true;
            GogoAnimeRecentEpisodes = await GogoAnimeService.RecentEpisodes(Page:Page);
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
            //ContentDialogResult result = await cd.ShowAsync();
            //if (result == ContentDialogResult.Primary)
            //{
            //    GetRecentEpisodes();
            //}
        }
        DataLoading = false;

    }
    public void PrevPage()
    {
        if (Page > 1)
        {
            Page--;
            GetRecentEpisodes(); 
        }    
    }
    public void NextPage()
    {
        Page++;
        GetRecentEpisodes();
    }
    public void SortComboChanged()
    {
        
    }
}

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


    private GogoAnimeRecentEpisodes? _gogoAnimeRecentEpisodes;
    private GogoAnimePopularAnime? _gogoAnimePopularAnime;
    private GogoAnimeSearch? _gogoAnimeSearch;

    private AnimePaheRecentEpisodes? _animePaheRecentEpisodes;
    private GogoAnimeService GogoAnimeService = new GogoAnimeService();
    private AnimePaheService AnimePaheService = new AnimePaheService();
    private int _page = 1;
    private bool _dataLoading = false;
    private string _searchTerm = "";
    private string _sort = "Newest";

    public int Page { get => _page; set => this.RaiseAndSetIfChanged(ref _page, value); }
    public bool DataLoading { get => _dataLoading; set => this.RaiseAndSetIfChanged(ref _dataLoading, value); }
    public GogoAnimeRecentEpisodes? GogoAnimeRecentEpisodes { get => _gogoAnimeRecentEpisodes; set => this.RaiseAndSetIfChanged(ref _gogoAnimeRecentEpisodes, value); }
    public GogoAnimePopularAnime? GogoAnimePopularAnime { get => _gogoAnimePopularAnime; set => this.RaiseAndSetIfChanged(ref _gogoAnimePopularAnime, value); }
    public string SearchTerm { get => _searchTerm; set { 
            this.RaiseAndSetIfChanged(ref _searchTerm, value);
            SearchTermChanged();
        } }
    public GogoAnimeSearch? GogoAnimeSearch { get => _gogoAnimeSearch; set => this.RaiseAndSetIfChanged(ref _gogoAnimeSearch, value); }

    public AnimePaheRecentEpisodes? AnimePaheRecentEpisodes { get => _animePaheRecentEpisodes; set => this.RaiseAndSetIfChanged(ref _animePaheRecentEpisodes, value); }
    public List<string> SortOptions { get => new List<string> { "Newest", "Popular" }; }
    public string Sort { get => _sort; set  {this.RaiseAndSetIfChanged(ref _sort, value);} }

    public IndexViewModel()
    {
        GetEpisodes();
       
    }   
    public async void GetEpisodes()
    {
            DataLoading = true;
            //Get Episodes for index pages
            GogoAnimeRecentEpisodes = await GogoAnimeService.RecentEpisodes<GogoAnimeRecentEpisodes>(Page:Page);
            GogoAnimePopularAnime = await GogoAnimeService.PopularEpisodes<GogoAnimePopularAnime>(Page:Page);

        AnimePaheRecentEpisodes = await AnimePaheService.RecentEpisodes<AnimePaheRecentEpisodes>(Page: Page);
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
            if (result == ContentDialogResult.Primary)
            {
                GetEpisodes();
            }
        }
        DataLoading = false;

    }
    public void PrevPage()
    {
        if (Page > 1)
        {
            Page--;
            GetEpisodes(); 
        }    
    }
    public void NextPage()
    {
        Page++;
        GetEpisodes();
    }
  
    public void SearchTermChanged()
    {
        Debug.WriteLine(SearchTerm);
        GeneralSearch();
    }
    public async void GeneralSearch()
    {
        DataLoading = true;
        GogoAnimeSearch = await GogoAnimeService.Search<GogoAnimeSearch>(Query: SearchTerm, Page: Page);
        DataLoading = false;
    }
}

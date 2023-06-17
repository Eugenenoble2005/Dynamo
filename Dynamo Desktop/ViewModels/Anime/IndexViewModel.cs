using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Services.Anime;
using ReactiveUI;
using FluentAvalonia.UI.Controls;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.ViewModels.Anime;

public class IndexViewModel : ViewModelBase
{


    private GogoAnimeRecentEpisodes? _gogoAnimeRecentEpisodes;
    private GogoAnimePopularAnime? _gogoAnimePopularAnime;
    private List<KayoAnimeRecentEpisodes>? _kayoAnimeRecentEpisodes;
    private List<KayoAnimeSearch>? _kayoAnimeSearch;
    private ZoroAnimeSearch? _zoroAnimeSearch;
    private GogoAnimeSearch? _gogoAnimeSearch;
    //models are the same. I fucked up with this structure.
	private List<ZoroAnimeRecentEpisodes> _zoroRecentEpisodes;
    private List<ZoroAnimeRecentEpisodes> _zoroPopularAnime;
	private AnimePaheSearch? _animePaheSearch;
    private AnimePaheRecentEpisodes? _animePaheRecentEpisodes;
    private GogoAnimeService GogoAnimeService = new GogoAnimeService();
    private AnimePaheService AnimePaheService = new AnimePaheService();
    private ZoroAnimeService ZoroAnimeService = new ZoroAnimeService();
    private KayoAnimeService KayoAnimeService = new KayoAnimeService();
    private int _page = 1;
    private bool _dataLoading = false;
    private string _searchTerm = "";
    private string _sort = "Newest";

    public int Page { get => _page; set => this.RaiseAndSetIfChanged(ref _page, value); }
    public bool DataLoading { get => _dataLoading; set => this.RaiseAndSetIfChanged(ref _dataLoading, value); }
    public GogoAnimeRecentEpisodes? GogoAnimeRecentEpisodes { get => _gogoAnimeRecentEpisodes; set => this.RaiseAndSetIfChanged(ref _gogoAnimeRecentEpisodes, value); }
    public GogoAnimePopularAnime? GogoAnimePopularAnime { get => _gogoAnimePopularAnime; set => this.RaiseAndSetIfChanged(ref _gogoAnimePopularAnime, value); }
    public ZoroAnimeSearch? ZoroAnimeSearch { get => _zoroAnimeSearch; set => this.RaiseAndSetIfChanged(ref _zoroAnimeSearch, value); }
    public string SearchTerm { get => _searchTerm; set { 
            this.RaiseAndSetIfChanged(ref _searchTerm, value);
            SearchTermChanged();
        } }
    public List<KayoAnimeRecentEpisodes> KayoAnimeRecentEpisodes { get => _kayoAnimeRecentEpisodes; set => this.RaiseAndSetIfChanged(ref _kayoAnimeRecentEpisodes, value); }
    public List<ZoroAnimeRecentEpisodes> ZoroRecentEpisodes { get => _zoroRecentEpisodes; set => this.RaiseAndSetIfChanged(ref _zoroRecentEpisodes, value); }
    public List<ZoroAnimeRecentEpisodes> ZoroPopularAnime { get => _zoroPopularAnime; set => this.RaiseAndSetIfChanged(ref _zoroPopularAnime, value); }
    public GogoAnimeSearch? GogoAnimeSearch { get => _gogoAnimeSearch; set => this.RaiseAndSetIfChanged(ref _gogoAnimeSearch, value); }
    public List<KayoAnimeSearch> KayoAnimeSearch { get => _kayoAnimeSearch; set => this.RaiseAndSetIfChanged(ref _kayoAnimeSearch, value); }
    public AnimePaheRecentEpisodes? AnimePaheRecentEpisodes { get => _animePaheRecentEpisodes; set => this.RaiseAndSetIfChanged(ref _animePaheRecentEpisodes, value); }

    public AnimePaheSearch? AnimePaheSearch { get => _animePaheSearch; set => this.RaiseAndSetIfChanged(ref _animePaheSearch, value); }
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
        var gogoAnimeRecentEpisodesTask =  GogoAnimeService.RecentEpisodes(Page:Page);
        var gogoAnimePopularEpisodesTask =  GogoAnimeService.PopularEpisodes(Page:Page);
        var animePaheRecentEpisodesTask =  AnimePaheService.RecentEpisodes(Page: Page);
        var zoroAnimeRecentEpisodesTask = ZoroAnimeService.RecentEpisodes(Page: Page);
        //var kayoAnimeRecentEpisodesTask = KayoAnimeService.RecentEpisodes();
        var zoroPopularAnimeTask = ZoroAnimeService.PopularAnime(Page: Page);
        await Task.WhenAll(gogoAnimeRecentEpisodesTask, gogoAnimePopularEpisodesTask, animePaheRecentEpisodesTask,zoroAnimeRecentEpisodesTask,zoroPopularAnimeTask);
        GogoAnimeRecentEpisodes = await gogoAnimeRecentEpisodesTask;
        GogoAnimePopularAnime = await gogoAnimePopularEpisodesTask;
        AnimePaheRecentEpisodes = await animePaheRecentEpisodesTask;
        ZoroRecentEpisodes = await zoroAnimeRecentEpisodesTask;
        ZoroPopularAnime = await zoroPopularAnimeTask;
     
        //    if(GogoAnimeRecentEpisodes == null)
        //{
        //    var cd = new ContentDialog
        //    {
        //        Title="Error",
        //        Content="Could not fetch data. Please check your internet connection",
        //        CloseButtonText="Close",
        //        PrimaryButtonText="Retry",
        //        DefaultButton=ContentDialogButton.Primary
                
        //    };
        //    ContentDialogResult result = await cd.ShowAsync();
        //    if (result == ContentDialogResult.Primary)
        //    {
        //        GetEpisodes();
        //    }
        //}
        DataLoading = false;

    }
    public void PrevPage()
    {
        if (Page > 1)
        {
            Page--;
            GetEpisodes();
            GeneralSearch();
        }    
    }
    public void NextPage()
    {
        Page++;
        GetEpisodes();
        GeneralSearch();
    }
  
    public void SearchTermChanged()
    {
        GeneralSearch();
    }
    public async void GeneralSearch()
    {
        DataLoading = true;
        var gogoAnimeSearchTask = GogoAnimeService.Search(Query: SearchTerm, Page: Page);
        var animePaheSearchTask =  AnimePaheService.Search(Query: SearchTerm, Page: Page);
        var zoroAnimeSearchTask = ZoroAnimeService.Search(Query: SearchTerm, Page: Page);
       
        await Task.WhenAll(gogoAnimeSearchTask, animePaheSearchTask,zoroAnimeSearchTask);
        GogoAnimeSearch = await gogoAnimeSearchTask;
        AnimePaheSearch = await animePaheSearchTask;
        ZoroAnimeSearch = await zoroAnimeSearchTask;
        DataLoading = false;
    }
}

using Dynamo_Desktop.Models.Hentai;
using Dynamo_Desktop.Services.Hentai;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.ViewModels.Hentai;

public class IndexViewModel : ViewModelBase
{
  
    public HentaiProviders Provider { get; set; }
    private bool _dataLoading = false;
    private string _searchTerm ="";
    private List<RecentHentai> _hentaiSearch;
    private HanimeService _hanimeService = new HanimeService();
    private List<RecentHentai> _recentHentai;
    private int _page = 1;
    public List<RecentHentai> RecentHentai { get => _recentHentai; set => this.RaiseAndSetIfChanged(ref _recentHentai,value); }
    public int Page { get => _page; set => this.RaiseAndSetIfChanged(ref _page, value); }
    public bool DataLoading { get => _dataLoading; set => this.RaiseAndSetIfChanged(ref _dataLoading, value); }
    public List<RecentHentai> HentaiSearch
    {
        get => _hentaiSearch;
        set => this.RaiseAndSetIfChanged(ref _hentaiSearch, value);
    }
    public string SearchTerm { get => _searchTerm;
        set { this.RaiseAndSetIfChanged(ref _searchTerm, value); GeneralSearch();}
    }
    public async void GetRecentHentai()
    {
        switch (Provider)
        {
            case HentaiProviders.Hanime:
                DataLoading = true;
                RecentHentai = await _hanimeService.Recent();
                DataLoading = false;
                break;
        }
    }

    public async void GeneralSearch()
    {
        DataLoading = true;
        switch (Provider)
        {
            case HentaiProviders.Hanime:
             
                HentaiSearch = await _hanimeService.Search(SearchTerm);
                DataLoading = false;
                break;
        }
    }
    public IndexViewModel()
    {
        GetRecentHentai();
    }
}

public enum HentaiProviders
{
    Hanime
}
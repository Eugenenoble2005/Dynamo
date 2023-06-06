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
    private string _searchTerm = "";
    public HentaiProviders Provider { get; set; }
    private HanimeService _hanimeService = new HanimeService();
    private List<RecentHentai> _recentHentai;
    public List<RecentHentai> RecentHentai { get => _recentHentai; set => this.RaiseAndSetIfChanged(ref _recentHentai,value); }
    public string SearchTerm { get => _searchTerm; set => this.RaiseAndSetIfChanged(ref _searchTerm, value); }
    public async void GetRecentHentai()
    {
        switch (Provider)
        {
            case HentaiProviders.Hanime:
                RecentHentai = await _hanimeService.Recent();
                Debug.WriteLine(JsonSerializer.Serialize(RecentHentai));
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
using System.Diagnostics;
using System.Text.Json;
using Dynamo_Desktop.Models.Hentai;
using Dynamo_Desktop.Services.Hentai;
using Dynamo_Desktop.Views.Hentai.SubViews;
using ReactiveUI;

namespace Dynamo_Desktop.ViewModels.Hentai;

public class DetailsViewModel : ViewModelBase
{
    private HentaiIndexToDetailsRouteParams _routeParams;
    private HanimeService _hanimeService = new HanimeService();
    private HentaiDetails _hentaiDetails;
    public Models.Hentai.HentaiDetails HentaiDetails
    {
        get => _hentaiDetails;
        set => this.RaiseAndSetIfChanged(ref _hentaiDetails, value);
    }

    private bool _dataLoading;

    public bool DataLoading
    {
        get => _dataLoading;
        set => this.RaiseAndSetIfChanged(ref _dataLoading, value);
    }
    public HentaiIndexToDetailsRouteParams RouteParams
    {
        get => _routeParams;
        set { this.RaiseAndSetIfChanged(ref _routeParams, value); GetHentaiDetails(); }
    }

    public async void GetHentaiDetails()
    {
        DataLoading = true;
        switch (RouteParams.Provider)
        {
            case HentaiProviders.Hanime:
                HentaiDetails = await _hanimeService.Info(RouteParams.HentaiId);
                DataLoading = false;
                break;
        }
    }
}
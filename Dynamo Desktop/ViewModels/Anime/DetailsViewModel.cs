using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Services.Anime;
using Dynamo_Desktop.Views.Anime.Subviews.AnimePahe;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace Dynamo_Desktop.ViewModels.Anime;

public class DetailsViewModel : ViewModelBase
{
    
    private AnimeIndexToDetailsRouteParams _routeParams;
    private AnimePaheAnimeInfo _animePaheAnimeInfo;
    private ObservableCollection<PaheResult> _animePaheEpisodes;
    private List<AnimePaheStreamingLinks> _animePaheStreamingLinks;
    private AnimePaheService _animePaheService = new AnimePaheService();
    private ZoroAnimeStreamingLinks _zoroStreamingLinks;
    private ZoroAnimeService _zoroAnimeService = new ZoroAnimeService();
    private GogoAnimeService _gogoAnimeService = new GogoAnimeService();
    private GogoAnimeStreamingLinks _gogoStreamingLinks;
    private GogoAnimeInfo _gogoAnimeInfo;
    private ZoroAnimeInfo _zoroAnimeInfo;
    private bool _dataLoading = true;
    public AnimePaheAnimeInfo AnimePaheAnimeInfo { get => _animePaheAnimeInfo; set => this.RaiseAndSetIfChanged(ref _animePaheAnimeInfo, value); }
    public AnimeIndexToDetailsRouteParams RouteParams { get => _routeParams; set { this.RaiseAndSetIfChanged(ref _routeParams, value); } }
    public ObservableCollection<PaheResult> AnimePaheEpisodes { get => _animePaheEpisodes;set { this.RaiseAndSetIfChanged(ref _animePaheEpisodes, value); } }
    public GogoAnimeStreamingLinks GogoStreamingLinks { get => _gogoStreamingLinks; set => this.RaiseAndSetIfChanged(ref _gogoStreamingLinks, value); }
    public GogoAnimeInfo GogoAnimeInfo { get => _gogoAnimeInfo; set => this.RaiseAndSetIfChanged(ref _gogoAnimeInfo, value); }
    public ZoroAnimeInfo ZoroAnimeInfo { get => _zoroAnimeInfo; set => this.RaiseAndSetIfChanged(ref _zoroAnimeInfo, value); }
    public ZoroAnimeStreamingLinks ZoroStreamingLinks { get => _zoroStreamingLinks; set => this.RaiseAndSetIfChanged(ref _zoroStreamingLinks, value); }
    public List<AnimePaheStreamingLinks> AnimePaheStreamingLinks
    {
        get => _animePaheStreamingLinks;
        set => this.RaiseAndSetIfChanged(ref _animePaheStreamingLinks, value);
    }
    public bool DataLoading
    {
        get => _dataLoading; set => this.RaiseAndSetIfChanged(ref _dataLoading, value);
    }
    public  DetailsViewModel()
    {

    }
    //public async void GetAnimeDetails()
    //{
    //    DataLoading = true;
    //    switch (RouteParams.Provider)
    //    {
    //        case "AnimePahe":
    //            var AnimePaheAnimeInfoTask =  _animePaheService.Info(RouteParams?.AnimeId);
    //            var AnimePaheEpisodesTask =  _animePaheService.AllEpisodes(RouteParams.AnimeId);
    //            var AnimePaheStreamingLinksTask =
    //                 _animePaheService.StreamingLinks(RouteParams.AnimeId, RouteParams?.EpisodeId);
    //            await Task.WhenAll(AnimePaheStreamingLinksTask, AnimePaheEpisodesTask, AnimePaheAnimeInfoTask);
    //            //test
               
    //            AnimePaheAnimeInfo = await AnimePaheAnimeInfoTask;
    //            AnimePaheEpisodes = await AnimePaheEpisodesTask;
    //            AnimePaheStreamingLinks = await AnimePaheStreamingLinksTask;
    //            DataLoading = false;
    //            break;
    //        case "GogoAnime":
    //            //var GogoAnimeInfoTask = _gogoAnimeService.Info(RouteParams?.AnimeId);
    //            //var GogoStreamingLinksTask = _gogoAnimeService.StreamingLinks(EpisodeId: RouteParams.EpisodeId,AnimeId:null);
    //            //await Task.WhenAll(GogoAnimeInfoTask, GogoStreamingLinksTask);
    //            //GogoAnimeInfo = await GogoAnimeInfoTask;
    //            //GogoStreamingLinks = await GogoStreamingLinksTask;
    //            //DataLoading = false;
    //            break;
    //        case "ZoroAnime":
    //            var ZoroAnimeInfoTask = _zoroAnimeService.Info(RouteParams?.AnimeId);
    //            var zoroStreamingLinksTask = _zoroAnimeService.StreamingLinks(RouteParams.EpisodeId);
    //            await Task.WhenAll(ZoroAnimeInfoTask, zoroStreamingLinksTask);
    //            ZoroAnimeInfo = await ZoroAnimeInfoTask;
    //            ZoroStreamingLinks = await zoroStreamingLinksTask;
    //            DataLoading = false;
    //            break;
    //        default:
    //            break;
    //    }
    
    //}

}

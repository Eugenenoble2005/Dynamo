using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Services.Anime;
using Dynamo_Desktop.Views.Anime.Subviews.AnimePahe;
using ReactiveUI;
using System;
using System.Collections.Generic;
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
    private List<PaheResult> _animePaheEpisodes;
    private List<AnimePaheStreamingLinks> _animePaheStreamingLinks;
    private AnimePaheService _animePaheService = new AnimePaheService();
    private bool _dataLoading = true;
    public AnimePaheAnimeInfo AnimePaheAnimeInfo { get => _animePaheAnimeInfo; set => this.RaiseAndSetIfChanged(ref _animePaheAnimeInfo, value); }
    public AnimeIndexToDetailsRouteParams RouteParams { get => _routeParams; set { this.RaiseAndSetIfChanged(ref _routeParams, value);GetAnimeDetails(); } }
    public List<PaheResult> AnimePaheEpisodes { get => _animePaheEpisodes;set { this.RaiseAndSetIfChanged(ref _animePaheEpisodes, value); } }
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
    public async void GetAnimeDetails()
    {
        switch (RouteParams.Provider)
        {
            case "AnimePahe":
                var AnimePaheAnimeInfoTask =  _animePaheService.Info(RouteParams?.AnimeId);
                var AnimePaheEpisodesTask =  _animePaheService.AllEpisodes(RouteParams.AnimeId);
                var AnimePaheStreamingLinksTask =
                     _animePaheService.StreamingLinks(RouteParams.AnimeId, RouteParams?.EpisodeId);
                await Task.WhenAll(AnimePaheStreamingLinksTask, AnimePaheEpisodesTask, AnimePaheAnimeInfoTask);
                //test
                
                DataLoading = false;
                AnimePaheAnimeInfo = await AnimePaheAnimeInfoTask;
                AnimePaheEpisodes = await AnimePaheEpisodesTask;
                AnimePaheStreamingLinks = await AnimePaheStreamingLinksTask;
                foreach (var episode in AnimePaheEpisodes)
                {
                    if (episode.episode.ToString() == RouteParams.EpisodeNumber)
                    {
                        episode.IsCurrentEpisode = true;
                    }
                }
                break;
            default:
                break;
        }
    
    }

}

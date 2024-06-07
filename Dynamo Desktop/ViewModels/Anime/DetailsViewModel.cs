using System.Collections.Generic;
using System.Diagnostics;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Services.Anime;
using FluentAvalonia.UI.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;


namespace Dynamo_Desktop.ViewModels.Anime
{
    public class DetailsViewModel : ReactiveObject
    {
        private AnimeIndexToDetailsRouteParams _routeParams;

        public AnimeIndexToDetailsRouteParams RouteParams
        {
            get => _routeParams;
            set
            {
                this.RaiseAndSetIfChanged(ref _routeParams, value);
                GetInfo();
            }
        }

        [Reactive]
        public AnimeInfo Info { get; set; }
        
        [Reactive]
        public List<AnimeStreamingLinks> Links { get; set; }
        [Reactive]
        public bool DataLoading { get; set; }
        private IAnimeService AnimeService { get; set; }
        public async void GetInfo()
        {
            DataLoading = true;
            switch (RouteParams.Provider)
            {
                case AnimeProviders.GogoAnime:
                    AnimeService = new GogoAnimeService();
                    break;
                case AnimeProviders.AnimePahe:
                    AnimeService = new AnimePaheService();
                    break;
            }
            Info = await AnimeService.Info(RouteParams.AnimeId);
            if (Info == null || Info == default)
            {
                var dialog = new ContentDialog()
                {  
                    Content = "Could Not obtain Information. Please check your internet connection or try another provider.",
                    Title = "Error",
                    DefaultButton = ContentDialogButton.Close,
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            Links = await AnimeService.StreamingLinks(Query: RouteParams.AnimeId, Episode: RouteParams.EpisodeNumber);
            Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(Info));
            DataLoading = false;
        }
        //for ide design
      

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Services.Anime;
using ReactiveUI.Fody.Helpers;

namespace Dynamo_Desktop.ViewModels.Anime
{
    public class DetailsViewModel2 : ViewModelBase
    {
        public AnimeIndexToDetailsRouteParams RouteParams { get; set; }

        [Reactive]
        public AnimeInfo Info { get; set; }
        private IAnimeService AnimeService { get; set; }
        public DetailsViewModel2()
        {
            switch (RouteParams.Provider)
            {
                case AnimeProviders.GogoAnime:
                    AnimeService = new GogoAnimeService();
                    break;
            }
        }
        public async void GetInfo()
        {
            //Info = await AnimeService.Info();
        }
    }
}

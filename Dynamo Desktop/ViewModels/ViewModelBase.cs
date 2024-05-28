using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Dynamo_Desktop.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
       

        [Reactive]
        public int Page { get; set; }

        [Reactive]
        public string Sort { get; set; }

        [Reactive]
        public bool DataLoading { get; set; }
    }
}
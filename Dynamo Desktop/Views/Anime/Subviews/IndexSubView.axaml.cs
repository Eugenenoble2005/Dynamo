using Avalonia.Controls;
using Avalonia.Input;

namespace Dynamo_Desktop.Views.Anime.Subviews
{
    public partial class IndexSubView : UserControl
    {
        public AnimeProviders Provider { get; set; }
        public IndexSubView()
        {
            InitializeComponent();
        }
        public void NavigateToDetails(object sender, TappedEventArgs e)
        {
          
        }
    }
}

using Avalonia.Controls;

namespace Dynamo_Desktop.Views.Anime.Subviews
{
    public partial class IndexSubView : UserControl
    {
        public AnimeProviders Provider { get; set; }
        public IndexSubView()
        {
            InitializeComponent();
        }
    }
}

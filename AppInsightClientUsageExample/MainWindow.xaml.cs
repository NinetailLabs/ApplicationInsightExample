using System.Windows;
using VaraniumSharp.Initiator.Monitoring;

namespace AppInsightClientUsageExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            AppInsightClient.TrackPageView(nameof(MainWindow));
        }
    }
}
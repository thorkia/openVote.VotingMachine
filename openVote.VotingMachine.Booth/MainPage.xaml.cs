using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Messaging;
using MetroLog;
using openVote.VotingMachine.Booth.Settings;
using openVote.VotingMachine.Core.Events;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace openVote.VotingMachine.Booth
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<MainPage>();

		public MainPage()
		{
			this.InitializeComponent();
		}
		
		private void NextNavigationButton(object sender, RoutedEventArgs e)
		{
			Messenger.Default.Send<NextStateEvent>( new NextStateEvent());
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			_logger.Trace(LogStatements.NavigatedLog(LogStatements.NavigatedToAction, "MainPage"));
			base.OnNavigatedTo(e);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			_logger.Trace(LogStatements.NavigatedLog(LogStatements.NavigatedFromAction, "MainPage"));
			base.OnNavigatedFrom(e);
		}
	}
}

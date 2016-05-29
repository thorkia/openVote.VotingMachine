using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Messaging;
using MetroLog;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.PageViewModels;
using openVote.VotingMachine.Booth.Settings;
using openVote.VotingMachine.Core.Events;
using openVote.VotingMachine.Core.States;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace openVote.VotingMachine.Booth.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class LockPage : Page
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<LockPage>();

		public LockPage()
		{
			this.InitializeComponent();

			//TODO: Rmove this once the server is ready
#if DEBUG
			this.DoubleTapped += (sender, args) =>
			{
				_logger.Trace("Received Unlock Signal");
				_logger.Trace("Sending Unlock Message");
				Messenger.Default.Send<UnlockEvent>(new UnlockEvent());
			};
#endif

		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			_logger.Trace(LogStatements.NavigatedLog(LogStatements.NavigatedToAction, "LockPage"));

			var viewModel = ServiceLocator.Current.GetInstance<LockPageViewModel>();
			DataContext = viewModel;

			viewModel.SetState((LockState)e.Parameter);

			base.OnNavigatedTo(e);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			_logger.Trace(LogStatements.NavigatedLog(LogStatements.NavigatedFromAction, "LockPage"));
			base.OnNavigatedFrom(e);
		}

	}
}

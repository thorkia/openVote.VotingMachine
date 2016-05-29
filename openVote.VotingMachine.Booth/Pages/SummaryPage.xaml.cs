using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MetroLog;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.PageViewModels;
using openVote.VotingMachine.Booth.Settings;
using openVote.VotingMachine.Core.States;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace openVote.VotingMachine.Booth.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SummaryPage : Page
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<SummaryPage>();

		public static readonly DependencyProperty ReviewedAllChoicesProperty = DependencyProperty.Register(
			"ReviewedAllChoices", typeof (bool), typeof (SummaryPage), new PropertyMetadata(false));

		public bool ReviewedAllChoices
		{
			get { return (bool) GetValue(ReviewedAllChoicesProperty); }
			set { SetValue(ReviewedAllChoicesProperty, value); }
		}	

		public SummaryPage()
		{
			this.InitializeComponent();

			this.Loaded += (sender, args) =>
			{
				var viewer = ItemsList.GetFirstDescendantOfType<ScrollViewer>();
				if (viewer.VerticalOffset == viewer.ScrollableHeight)
				{
					_logger.Trace("All choices visible.  Enabling [Place Votes] button");
					ReviewedAllChoices = true;
				}
			};
		}

		
		private void ItemsListOnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
		{
			var scroller = e.Container as ScrollViewer;

			if (scroller.ScrollableHeight == scroller.VerticalOffset)
			{
				_logger.Trace("Choices scrolled to end.  Enabling [Place Votes] button");
				ReviewedAllChoices = true;
			}
		}


		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			_logger.Trace(LogStatements.NavigatedLog(LogStatements.NavigatedToAction, "SummaryPage"));

			var viewModel = ServiceLocator.Current.GetInstance<SummaryPageViewModel>();
			DataContext = viewModel;

			viewModel.SetState((SummaryState)e.Parameter);

			base.OnNavigatedTo(e);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			_logger.Trace(LogStatements.NavigatedLog(LogStatements.NavigatedFromAction, "SummaryPage"));
			base.OnNavigatedFrom(e);
		}
	}
}

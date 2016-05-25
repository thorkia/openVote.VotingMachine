using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MetroLog;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.PageViewModels;
using openVote.VotingMachine.Booth.Settings;
using openVote.VotingMachine.Booth.States;
using openVote.VotingMachine.DataAccess.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace openVote.VotingMachine.Booth.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class PlaceVotePage : Page
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<PlaceVotePage>();

		public PlaceVotePage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			_logger.Trace(LogStatements.NavigatedLog(LogStatements.NavigatedToAction, "PlaceVotePage"));

			var viewModel = ServiceLocator.Current.GetInstance<PlaceVoteViewModel>();
			DataContext = viewModel;
			
			viewModel.SetState((VoteState)e.Parameter);

			base.OnNavigatedTo(e);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			_logger.Trace(LogStatements.NavigatedLog(LogStatements.NavigatedFromAction, "PlaceVotePage"));
			base.OnNavigatedFrom(e);
		}
	}
}

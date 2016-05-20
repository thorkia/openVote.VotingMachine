using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.PageViewModels;
using openVote.VotingMachine.Booth.States;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace openVote.VotingMachine.Booth.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SummaryPage : Page
	{
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
					ReviewedAllChoices = true;
				}
			};
		}

		
		private void ItemsListOnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
		{
			var scroller = e.Container as ScrollViewer;

			if (scroller.ScrollableHeight == scroller.VerticalOffset)
			{
				ReviewedAllChoices = true;
			}
		}


		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var viewModel = ServiceLocator.Current.GetInstance<SummaryPageViewModel>();
			DataContext = viewModel;

			viewModel.SetState((SummaryState)e.Parameter);

			base.OnNavigatedTo(e);
		}
	}
}

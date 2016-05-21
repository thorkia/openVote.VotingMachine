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
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.PageViewModels;
using openVote.VotingMachine.Booth.States;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace openVote.VotingMachine.Booth.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class LockPage : Page
	{
		public LockPage()
		{
			this.InitializeComponent();

#if DEBUG
			this.DoubleTapped += (sender, args) =>
			{
				Messenger.Default.Send<UnlockEvent>(new UnlockEvent());
			};
#endif

		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var viewModel = ServiceLocator.Current.GetInstance<LockPageViewModel>();
			DataContext = viewModel;

			viewModel.SetState((LockState)e.Parameter);

			base.OnNavigatedTo(e);
		}


	}
}

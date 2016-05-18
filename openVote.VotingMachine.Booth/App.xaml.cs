using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.Database;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.Pages;
using openVote.VotingMachine.Booth.PageViewModels;
using openVote.VotingMachine.DataAccess;
using openVote.VotingMachine.DataAccess.Api;
using SQLite.Net;

namespace openVote.VotingMachine.Booth
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed partial class App : Application
	{
		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			ServiceLocator.SetLocatorProvider( () => SimpleIoc.Default);

			var nav = new NavigationService();
			SimpleIoc.Default.Register<INavigationService>(() => nav);

			RegisterPages(nav);
			RegisterServices();
			RegisterViewModels();

			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		private void RegisterPages(NavigationService nav)
		{
			nav.Configure("Title", typeof(MainPage));
			nav.Configure("PlaceVote", typeof(PlaceVotePage));
			nav.Configure("ConfirmVote", typeof(VoteConfirmationPage));
		}

		private void RegisterServices()
		{
			SimpleIoc.Default.Register<SQLiteConnection>(() => Database.Database.Connection);
			SimpleIoc.Default.Register<IBallotLoader>(() => new TestBallotLoader());
			SimpleIoc.Default.Register<BallotRepository>();
			SimpleIoc.Default.Register<VoteRepository>(true);
			SimpleIoc.Default.Register<StateManager>(true);
		}

		private void RegisterViewModels()
		{			
			SimpleIoc.Default.Register<PlaceVoteViewModel>();
			SimpleIoc.Default.Register<ConfirmVoteViewModel>();
		}



		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
#if DEBUG
			if (System.Diagnostics.Debugger.IsAttached)
			{
				this.DebugSettings.EnableFrameRateCounter = true;
			}
#endif
			Frame rootFrame = Window.Current.Content as Frame;

			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (rootFrame == null)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
					//TODO: Load state from previously suspended application
				}

				// Place the frame in the current Window
				Window.Current.Content = rootFrame;
			}

			if (e.PrelaunchActivated == false)
			{
				if (rootFrame.Content == null)
				{
					// When the navigation stack isn't restored navigate to the first page,
					// configuring the new page by passing required information as a navigation
					// parameter
					rootFrame.Navigate(typeof(MainPage), e.Arguments);
				}
				// Ensure the current window is active
				Window.Current.Activate();
			}
		}

		/// <summary>
		/// Invoked when Navigation to a certain page fails
		/// </summary>
		/// <param name="sender">The Frame which failed navigation</param>
		/// <param name="e">Details about the navigation failure</param>
		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();

			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}
	}
}

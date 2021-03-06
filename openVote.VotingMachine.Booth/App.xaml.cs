﻿using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MetroLog;
using MetroLog.Layouts;
using MetroLog.Targets;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.DataAccess;
using openVote.VotingMachine.Booth.Database;
using openVote.VotingMachine.Booth.Pages;
using openVote.VotingMachine.Booth.PageViewModels;
using openVote.VotingMachine.Core;
using openVote.VotingMachine.Core.Api;
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
			ConfigureLogger();
			var logger = LogManagerFactory.DefaultLogManager.GetLogger<App>();

			logger.Trace("Initializing Application");
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			var nav = new NavigationService();
			SimpleIoc.Default.Register<INavigationService>(() => nav);

			RegisterPages(nav);
			RegisterServices();
			RegisterViewModels();

			InitializeComponent();
			Suspending += OnSuspending;

			Current.UnhandledException += ApplicationUnhandledException;

			logger.Trace("Initialization Completed");
		}

		private void ApplicationUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			//Write to a temp location
			string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "errors.log");
			using (var appender = File.AppendText(path))
			{
				appender.WriteLine($"{DateTime.Now}||{e.Exception}");
			}
		}

		private void RegisterPages(NavigationService nav)
		{
			nav.Configure("Title", typeof(MainPage));
			nav.Configure("PlaceVote", typeof(PlaceVotePage));
			nav.Configure("ConfirmVote", typeof(VoteConfirmationPage));
			nav.Configure("Summary", typeof(SummaryPage));
			nav.Configure("LockScreen", typeof(LockPage));
		}

		private void RegisterServices()
		{
			SimpleIoc.Default.Register<SQLiteConnection>(() => Database.Database.Connection);
			SimpleIoc.Default.Register<VoteRepository>( () => new VoteRepository(ServiceLocator.Current.GetInstance<SQLiteConnection>()));

			//TODO: add code to check if this is debug, or if there is a server - add config to direct to the server - app.config?
			SimpleIoc.Default.Register<IBallotLoader>(() => new TestBallotLoader());
			
			SimpleIoc.Default.Register<Controller>(() => new Controller(ServiceLocator.Current.GetInstance<IBallotLoader>()), true);

			
			SimpleIoc.Default.Register<StateManager>(true);
		}

		private void RegisterViewModels()
		{			
			SimpleIoc.Default.Register<PlaceVoteViewModel>();
			SimpleIoc.Default.Register<ConfirmVoteViewModel>();
			SimpleIoc.Default.Register<SummaryPageViewModel>();
			SimpleIoc.Default.Register<LockPageViewModel>();
		}

		private void ConfigureLogger()
		{
			GlobalCrashHandler.Configure();
			var target = new StreamingFileTarget(new SingleLineLayout())
			{
				RetainDays = int.MaxValue
			};
			LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, target);
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
				DebugSettings.EnableFrameRateCounter = true;
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
					//Loading from previous state is not needed as this will only run on a Pi
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

			//Close the connection when the application is being suspended
			var connection = ServiceLocator.Current.GetInstance<SQLiteConnection>();
			connection.Close();

			deferral.Complete();
		}
	}
}

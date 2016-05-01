using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace openVote.VotingMachine.Booth
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
		}


		//TODO: Clean this up.  Make navigation make more sense.  Send a null value for the first parameter
		//Then add code to handle passing the ballot and vote choices around by passing the needed data to the system.
		//Create a way to "next" to go the next ballot with out needing to +1 the index
		private void NextNavigationButton(object sender, RoutedEventArgs e)
		{
			var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
			navigationService.NavigateTo("PlaceVote", -1);
		}
	}
}

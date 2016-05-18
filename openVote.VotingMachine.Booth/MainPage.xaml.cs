using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.Events;

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
		
		private void NextNavigationButton(object sender, RoutedEventArgs e)
		{
			Messenger.Default.Send<NextStateEvent>( new NextStateEvent());
		}
	}
}

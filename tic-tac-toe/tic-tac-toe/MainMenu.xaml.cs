using System.Windows;
using System.Windows.Controls;
namespace tic_tac_toe
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MoveToGameBot(object sender, RoutedEventArgs e)
        {
            GameSettingsPage.EnemyTypeValue = true;

            MainFrame.NavigationService.Navigate(new Uri("GameSettingsPage.xaml", UriKind.Relative));
        }

        private void MoveToGameLocal(object sender, RoutedEventArgs e)
        {
            GameSettingsPage.EnemyTypeValue = false;

            MainFrame.NavigationService.Navigate(new Uri("GameSettingsPage.xaml", UriKind.Relative));
        }
    }
}

using System.Windows;
namespace tic_tac_toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
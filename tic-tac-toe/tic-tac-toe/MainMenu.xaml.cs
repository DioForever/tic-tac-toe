using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

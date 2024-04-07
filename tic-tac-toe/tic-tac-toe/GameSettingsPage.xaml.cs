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
    /// Interaction logic for GameSettingsPage.xaml
    /// </summary>
    public partial class GameSettingsPage : Page
    {
        public GameSettings GameSettings;
        public static bool EnemyTypeValue { get; set; } // Static property to hold the value of EnemyType

        public bool EnemyType { get; set; } // Instance property to hold the value of EnemyType

        public GameSettingsPage()
        {
            EnemyType = EnemyTypeValue;

            InitializeComponent();
            GameSettings = new GameSettings(3, EnemyType);
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            ChangeSize(3);

        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            ChangeSize(4);

        }

        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            ChangeSize(5);
        }
        private void ChangeSize(int Size)
        {
            // change size and set other checkboxes to cheked = false
            GameSettings.size = Size;
            if (Size == 3)
            {
                x3.IsChecked = true;
                x4.IsChecked = false;
                x5.IsChecked = false;
            }
            else if (Size == 4)
            {
                x3.IsChecked = false;
                x4.IsChecked = true;
                x5.IsChecked = false;
            }
            else if (Size == 5)
            {
                x3.IsChecked = false;
                x4.IsChecked = false;
                x5.IsChecked = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the GamePage
            GamePage.GameSettings = GameSettings;

            SettingsFrame.NavigationService.Navigate(new Uri("GamePage.xaml", UriKind.Relative));
        }
    }


    public class GameSettings
    {
        public bool _enemyType;
        public bool enemyType
        {
            get { return _enemyType; }
            set { _enemyType = value; }
        }
        public int _size;
        public int size
        {
            get { return _size;}
            set { _size = value; }
        }

        public GameSettings(int Size, bool EnemyType)
        {
            size = Size;
            enemyType = EnemyType;
        }


    }
}

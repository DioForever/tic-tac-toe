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
        public GameSettingsPage()
        {
            InitializeComponent();
            GameSettings = new GameSettings();
            GameSettings.width = 3;
            GameSettings.height = 3;
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }

    public class GameSettings
    {
        public int _width;
        public int width
        {
            get { return _width;}
            set { _width = value; }
        }

        public int _height;
        public int height
        {
            get { return _height;}
            set { _height = value; }
        }
    }
}

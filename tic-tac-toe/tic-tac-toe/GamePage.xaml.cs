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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public static GameSettings GameSettings { get; set; }
        public GameSettings gameSettings { get; set; }
        public bool plaerTurn = true;

        public GamePage()
        {
            gameSettings = GameSettings;

            int size = gameSettings.size;
            bool enemyType = gameSettings.enemyType;

            InitializeComponent();

            GenerateGridOfLists(size);
        }
        private void GenerateGridOfLists(int size)
        {
            // Clear existing grid children
            gameGrid.Children.Clear();
            gameGrid.RowDefinitions.Clear();
            gameGrid.ColumnDefinitions.Clear();

            // Create rows and columns for the main grid
            for (int i = 0; i < size; i++)
            {
                gameGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                gameGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Create and add TextBoxes to the grid
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Spawn border for each cell in the grid
                    Border border = new Border();

                    border.SetValue(Grid.RowProperty, i);
                    border.SetValue(Grid.ColumnProperty, j);
                    border.CornerRadius = new CornerRadius(5);


                    border.Height = gameGrid.Height / size;
                    border.Width = gameGrid.Width / size;

                    border.Background = (Brush)new BrushConverter().ConvertFrom("#456C75");

                    border.Margin = new Thickness(5);

                    // Add on click event to the border so it changes color when clicked
                    border.MouseLeftButtonDown += (sender, e) =>
                    {
                        if (plaerTurn)
                        {
                            /*
                            BitmapImage btm = new BitmapImage(new Uri("pack://application:,,,/Resources/CROSS.png", UriKind.Relative));
                            Image cross = new Image();

                            cross.Source = btm;*/
                            Image Img = new Image();
                            Img.Source = new BitmapImage(new Uri(
                                         "pack://application:,,,/tic-tac-toe;component/Resources/CROSS.png"));

                            plaerTurn = false;

                            border.Child = Img;

                        }
                        else
                        {
                            // put image CIRCLE in the border as child
                            Image Img = new Image();
                            Img.Source = new BitmapImage(new Uri(
                                         "pack://application:,,,/tic-tac-toe;component/Resources/CIRCLE.png"));

                            plaerTurn = true;

                            border.Child = Img;
                        }
                    };


                    // Add the border to the grid
                    gameGrid.Children.Add(border);
                }
            }
        }

    }
}
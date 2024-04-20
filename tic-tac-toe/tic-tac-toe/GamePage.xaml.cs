using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
using tic_tac_toe.Resources;

namespace tic_tac_toe
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public static GameSettings GameSettings { get; set; }
        public GameSettings gameSettings { get; set; }
        public bool playerTurn = true;

        private Border[,] borders;
        private Game game;
        private byte player = 1;
        public GamePage()
        {
            gameSettings = GameSettings;

            int size = gameSettings.size;
            game = new Game(size);

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

            borders = new Border[size, size];
            
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {

                    // Spawn border for each cell in the grid
                    Border border = new Border();
                    borders[y, x] = border;

                    border.SetValue(Grid.RowProperty, y);
                    border.SetValue(Grid.ColumnProperty, x);
                    border.CornerRadius = new CornerRadius(5);


                    border.Height = gameGrid.Height / size;
                    border.Width = gameGrid.Width / size;

                    border.Background = (Brush)new BrushConverter().ConvertFrom("#456C75");

                    border.Margin = new Thickness(5);

                    border.MouseLeftButtonDown += (sender, e) =>
                    {
                        int clickedRowY = Grid.GetRow(border);
                        int clickedColumnX = Grid.GetColumn(border);
                        player = Place(clickedRowY, clickedColumnX, player, border);
                    };


                    // Add the border to the grid
                    gameGrid.Children.Add(border);
                }
            }
        }

        public byte Place(int y, int x, byte player, Border border)
        {
            byte[,] map = game.board;
            bool taken = (map[y, x] != 0);

            int winner = 0;
            bool boardFull = false;

            if(taken) return player;

            if (playerTurn && !taken)
            {

                spawnImageUnderBorder("Resources/CROSS.png", border);
                game.board[y, x] = player;
                byte[,] boardS = game.board;
                player = 2;

                playerTurn = false;

            }
            else if (!gameSettings.enemyType && !taken)
            {

                spawnImageUnderBorder("Resources/CIRCLE.png", border);
                game.board[y, x] = player;

                player = 1;
                playerTurn = true;
            }

            winner = game.GameWon();
            boardFull = game.BoardFull();
            AnnounceWinner(winner, boardFull);

            // AI move if enabled
            if (GameSettings.enemyType)
            {
                MoveAI();
                // use AI to make a move
                player = 1;
                playerTurn = true;

            }

            winner = game.GameWon();
            boardFull = game.BoardFull();
            AnnounceWinner(winner, boardFull);


            byte[,] b = game.board;
            return player;

        }

        private void MoveAI()
        {
            (int, int) move = game.FindBestMove(game);
            game.MakeMove(move.Item1, move.Item2, 2);
            spawnImageUnderBorder("Resources/CIRCLE.png", borders[move.Item1, move.Item2]);
        }

        private void AnnounceWinner(int winner, bool draw)
        {
            Game g = game;
            if(winner != 0)
            {
                MessageBox.Show("Player " + winner + " won!");
                game.InitializeBoard();
                GenerateGridOfLists(borders.GetLength(0));
            }
            else if(draw)
            {
                MessageBox.Show("Draw!");
                game.InitializeBoard();
                GenerateGridOfLists(borders.GetLength(0));
            }

        }

        public bool CheckTaken(int y, int x)
        {
            return game.board[y, x] != 0;
        }

        public static void spawnImageUnderBorder(string path, Border border)
        {
            // put image CIRCLE in the border as child
            Image Img = new Image();
            Img.Source = new BitmapImage(new Uri(
                                        "pack://application:,,,/tic-tac-toe;component/"+path));

            border.Child = Img;
            
        }

    }
}
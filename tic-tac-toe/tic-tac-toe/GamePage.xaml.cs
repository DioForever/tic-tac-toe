using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private char[,] board;
        private const char PlayerSymbol = 'x';
        private const char AISymbol = 'o';
        private bool AITurn = false;
        public GamePage()
        {
            gameSettings = GameSettings;

            board = new char[gameSettings.size, gameSettings.size];
            for(int i = 0; i < gameSettings.size; i++)
            {
                for (int j = 0; j < gameSettings.size; j++)
                {
                    board[i, j] = '-';
                }
            }

            int size = gameSettings.size;

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
                        int clickedY = Grid.GetRow(border);
                        int clickedX = Grid.GetColumn(border);
                        Place(clickedX, clickedY, border);
                    };


                    // Add the border to the grid
                    gameGrid.Children.Add(border);
                }
            }
        }

        public char Place(int y, int x, Border border)
        {
            if (board[y, x] == '-' && !AITurn)
            {
                // Update the game board with the player's symbol
                board[y, x] = playerTurn ? PlayerSymbol : AISymbol;

                // Display the player's symbol on the UI
                string imagePath = playerTurn ? "Resources/CROSS.png" : "Resources/CIRCLE.png";
                spawnImageUnderBorder(imagePath, border);

                // Check for a win or draw
                if (CheckWin(board, playerTurn ? PlayerSymbol : AISymbol))
                {
                    AnnounceWinner(playerTurn ? 1 : -1, false); // Player wins
                }
                else if (IsBoardFull(board))
                {
                    AnnounceWinner(0, true); // Draw
                }
                else
                {
                    // Toggle player turn
                    playerTurn = !playerTurn;

                    if(gameSettings.enemyType) { AITurn = true; MoveAI(); }
                    char[,] map = board;
                }


                // Return the symbol placed
                return playerTurn ? PlayerSymbol : AISymbol;
            }

            // If the cell is already taken, return the existing symbol
            return board[y, x];
        }


        private void MoveAI()
        {
            (int row, int col, int score) bestMove = MinMax(board, AISymbol);

            board[bestMove.row, bestMove.col] = playerTurn ? PlayerSymbol : AISymbol;
            Border border = borders[bestMove.col, bestMove.row];
            string imagePath = "Resources/CIRCLE.png";
            spawnImageUnderBorder(imagePath, border);

            // Check for a win or draw
            if (CheckWin(board, playerTurn ? PlayerSymbol : AISymbol))
            {
                AnnounceWinner(playerTurn ? 1 : -1, false); // Player wins
            }
            else if (IsBoardFull(board))
            {
                AnnounceWinner(0, true); // Draw
            }

            playerTurn = !playerTurn;
            AITurn = false;
        }

        private (int, int, int) MinMax(char[,] currentBoard, char player, int depth = 0)
        {
            int depthLimit = (gameSettings.size == 4) ? 5 : (gameSettings.size == 5) ? 4 : int.MaxValue;
            if (gameSettings.size == 3) depthLimit = 9;

            // If game over or depth limit reached return the score
            if (IsGameOver(currentBoard) || depth >= depthLimit)
            {
                int score = Evaluate(currentBoard);
                return (-1, -1, score);
            }

            List<(int, int, int)> moves = new List<(int, int, int)>();

            for (int row = 0; row < gameSettings.size; row++)
            {
                for (int col = 0; col < gameSettings.size; col++)
                {
                    if (currentBoard[row, col] == '-')
                    {
                        currentBoard[row, col] = player;

                        // Check if the move results in an immediate win or loss
                        int score = Evaluate(currentBoard);
                        if (score == 10 || score == -10)
                        {
                            currentBoard[row, col] = '-';
                            return (row, col, score);
                        }

                        // If not win or loss, use MinMax again
                        (int, int, int) minmax = MinMax(currentBoard, (player == PlayerSymbol) ? AISymbol : PlayerSymbol, depth + 1);

                        // Dont add invalid move (-1, -1)
                        if (minmax.Item1 != -1 && minmax.Item2 != -1)
                        {
                            moves.Add((row, col, minmax.Item3));
                        }

                        currentBoard[row, col] = '-';
                    }
                }
            }

            // Choose the best move based on the player
            if (moves.Count > 0)
            {
                var bestMove = player == AISymbol ? moves.OrderByDescending(m => m.Item3).First() : moves.OrderBy(m => m.Item3).First();
                return (bestMove.Item1, bestMove.Item2, bestMove.Item3);
            }

            // If no valid moves are found, return 0,0 move
            return (0, 0, Evaluate(currentBoard));
        }


        private bool IsGameOver(char[,] board)
        {
            // Check for a win or draw
            return CheckWin(board, PlayerSymbol) || CheckWin(board, AISymbol) || IsBoardFull(board);
        }

        private bool IsBoardFull(char[,] board)
        {
            // Check if the board is full
            for (int row = 0; row < gameSettings.size; row++)
            {
                for (int col = 0; col < gameSettings.size; col++)
                {
                    if (board[row, col] == '-')
                        return false;
                }
            }
            return true;
        }

        private bool CheckWin(char[,] board, char player)
        {
            int size = gameSettings.size; 
            int requiredLineLength = 3; // The required line length to win

            // Check for a win horizontally
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col <= size - requiredLineLength; col++)
                {
                    bool winInRow = true;
                    for (int k = 0; k < requiredLineLength; k++)
                    {
                        if (board[row, col + k] != player)
                        {
                            winInRow = false;
                            break;
                        }
                    }
                    if (winInRow)
                        return true;
                }
            }

            // Check for a win vertically
            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row <= size - requiredLineLength; row++)
                {
                    bool winInCol = true;
                    for (int k = 0; k < requiredLineLength; k++)
                    {
                        if (board[row + k, col] != player)
                        {
                            winInCol = false;
                            break;
                        }
                    }
                    if (winInCol)
                        return true;
                }
            }

            // Check for a win diagonally (from top-left to bottom-right)
            for (int row = 0; row <= size - requiredLineLength; row++)
            {
                for (int col = 0; col <= size - requiredLineLength; col++)
                {
                    bool winInDiagonal1 = true;
                    for (int k = 0; k < requiredLineLength; k++)
                    {
                        if (board[row + k, col + k] != player)
                        {
                            winInDiagonal1 = false;
                            break;
                        }
                    }
                    if (winInDiagonal1)
                        return true;
                }
            }

            // Check for a win diagonally (from top-right to bottom-left)
            for (int row = 0; row <= size - requiredLineLength; row++)
            {
                for (int col = requiredLineLength - 1; col < size; col++)
                {
                    bool winInDiagonal2 = true;
                    for (int k = 0; k < requiredLineLength; k++)
                    {
                        if (board[row + k, col - k] != player)
                        {
                            winInDiagonal2 = false;
                            break;
                        }
                    }
                    if (winInDiagonal2)
                        return true;
                }
            }

            // No win condition found
            return false;
        }


        private int Evaluate(char[,] board)
        {
            // Evaluate the board and return a score
            if (CheckWin(board, AISymbol))
                return 10;
            else if (CheckWin(board, PlayerSymbol))
                return -10;
            else
                return 0; // Draw
        }

        private void AnnounceWinner(int winner, bool draw)
        {
            string message = "";
            if (draw)
            {
                message = "It's a draw!";
            }
            else
            {
                message = winner == 1 ? "PLAYER 1 WON" : "PLAYER 2 WON";
            }

            // Spawn the game over screen on the announcement grid as text and button to move back to the main menu

            // Spawn border on the announcement grid
            Border border = new Border();
            border.CornerRadius = new CornerRadius(5);
            border.Background = (Brush)new BrushConverter().ConvertFrom("#456C75");
            border.Margin = new Thickness(5);
            border.SetValue(Grid.RowProperty, 0);
            border.SetValue(Grid.ColumnProperty, 0);
            border.SetValue(Grid.ColumnSpanProperty, 5);
            border.SetValue(Grid.RowSpanProperty, 5);

            // Spawn text on the border
            TextBlock textBlock = new TextBlock();
            textBlock.Text = message;
            textBlock.FontSize = 24;
            textBlock.Foreground = (Brush)new BrushConverter().ConvertFrom("#ebb134");
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            border.Child = textBlock;

            // Add the border to the grid
            gameGrid.Children.Add(border);

            // Add a button to move back to the main menu
            Button button = new Button();
            button.Content = "CONFIRM";
            button.Click += (sender, e) =>
            {
                GameFrame.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
            };



            button.SetValue(Grid.RowProperty, 4);
            button.SetValue(Grid.ColumnProperty, 0);
            button.SetValue(Grid.RowSpanProperty, 1);
            button.SetValue(Grid.ColumnSpanProperty, 5);
            button.Background = (Brush)new BrushConverter().ConvertFrom("#456C75");
            button.FontSize = 24;
            button.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
            button.Margin = new Thickness(5);

            // Add the button to the grid
            gameGrid.Children.Add(button);




            ResetGame();
        }

        private void ResetGame()
        {
            for (int row = 0; row < gameSettings.size; row++)
            {
                for (int col = 0; col < gameSettings.size; col++)
                {
                    board[row, col] = '-';
                    borders[row, col].Child = null;
                }
            }

            // Reset player turn
            playerTurn = true;
        }


        public static void spawnImageUnderBorder(string path, Border border)
        {
            // put image CIRCLE/CROSS in the border as child
            Image Img = new Image();
            Img.Source = new BitmapImage(new Uri(
                                        "pack://application:,,,/tic-tac-toe;component/"+path));

            border.Child = Img;
            
        }

    }
}
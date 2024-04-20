using System;

namespace tic_tac_toe.Resources
{
    internal class Game
    {
        // The board is represented as a 2D array
        private const int winLength = 3;
        public byte[,] board;
        private int boardSize;

        public Game(int size)
        {
            boardSize = size;
            board = new byte[size, size];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    board[i, j] = 0;
                }
            }
        }



        public void MakeMove(int row, int col, int player)
        {
            board[row, col] = (byte)player;
        }

        public bool BoardFull()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int GameWon()
        {
            // Check rows for a win
            for (int y = 0; y < boardSize; y++)
            {
                if (board[y, 0] != 0 &&
                    board[y, 0] == board[y, 1] &&
                    board[y, 1] == board[y, 2])
                {
                    return board[y, 0]; // Return the winning player
                }
            }

            // Check columns for a win
            for (int x = 0; x < boardSize; x++)
            {
                if (board[0, x] != 0 &&
                    board[0, x] == board[1, x] &&
                    board[1, x] == board[2, x])
                {
                    return board[0, x]; // Return the winning player
                }
            }

            // Check diagonals for a win
            if (board[0, 0] != 0 &&
                board[0, 0] == board[1, 1] &&
                board[1, 1] == board[2, 2])
            {
                return board[0, 0]; // Return the winning player
            }

            if (board[0, 2] != 0 &&
                board[0, 2] == board[1, 1] &&
                board[1, 1] == board[2, 0])
            {
                return board[0, 2]; // Return the winning player
            }

            return 0; // No player has won yet
        }


        private bool CheckRow(int row, int column, int player)
        {
            int count = 0;
            for (int i = column; i < boardSize; i++)
            {
                if (board[row, i] == player)
                {
                    count++;
                }
            }
            if (count == winLength) return true;
            return false;
        }

        private bool CheckColumn(int row, int column, int player)
        {
            int count = 0;
            for (int i = row; i < boardSize; i++)
            {
                if (board[i, column] == player)
                {
                    count++;
                }
            }
            if (count == winLength) return true;
            return false;
        }

        private bool CheckDiagonal(int row, int column, int player)
        {
            int count = 0;
            for (int i = 0; i < boardSize; i++)
            {
                if (board[i, i] == player)
                {
                    count++;
                }
            }
            if (count == winLength) return true;
            return false;
        }

        public bool CheckWin(int row, int column, int player)
        {
            if (CheckRow(row, column, player) || CheckColumn(row, column, player) || CheckDiagonal(row, column, player))
            {
                return true;
            } 
            return false;
        }

        public (int, int) FindBestMove(Game game)
        {
            int bestScore = int.MinValue;
            (int, int) bestMove = (0,0);
            byte[,] bytes = game.board;

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    bool open = game.board[y, x] == 0;
                    if (game.board[y, x] == 0)
                    {
                        game.board[y, x] = 2; // Assuming AI is player 2
                        int score = Minimax(game, 1, 2, true);
                        game.board[y, x] = 0;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (y, x);
                        }
                    }
                }
            }

            return bestMove;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="depth"></param>
        /// <param name="player"></param>
        /// <param name="mode">True if Maximizing, False if Minimazing</param>
        /// <returns></returns>
        static int Minimax(Game game, int depth, byte player, bool isMaximizing)
        {
            if(game.GameWon() == 2)
            {
                return 10 + depth;
            }
            else if(game.GameWon() == 1)
            {
                return 10 - depth;
            }
            else if(game.BoardFull())
            {
                return 10 - depth;
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < game.boardSize; i++)
                {
                    for (int j = 0; j < game.boardSize; j++)
                    {
                        if (game.board[i, j] == 0)
                        {
                            game.board[i, j] = player;
                            int score = Minimax(game, depth + 1, player, false);
                            game.board[i, j] = 0;
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < game.boardSize; i++)
                {
                    for (int j = 0; j < game.boardSize; j++)
                    {
                        if (game.board[i, j] == 0)
                        {
                            game.board[i, j] = player;
                            int score = Minimax(game, depth + 1, player, true);
                            game.board[i, j] = 0;
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;   
            }

        }
    }
}

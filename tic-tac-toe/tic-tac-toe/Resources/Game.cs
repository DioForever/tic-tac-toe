using System;

namespace tic_tac_toe.Resources
{
    internal class Game
    {
        // The board is represented as a 2D array
        public char[,] board;
        public int size;

        public Game(int size)
        {
            board = new char[size, size];

            this.size = size;

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    board[i, j] = '-';
                }
            }


        }

        public bool CheckIfDraw()
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (board[i, j] == '-')
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public int CheckWhoWins(char[,] board, char forWho)
        {
            int size = board.GetLength(0);
            int score = 0;

            // Check rows
            for (int row = 0; row < size; row++)
            {
                bool win = true;
                for (int col = 0; col < size; col++)
                {
                    if (board[row, col] != forWho)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    score++;
                    break; // Only one winning line needed for score
                }
            }

            // Check columns
            for (int col = 0; col < size; col++)
            {
                bool win = true;
                for (int row = 0; row < size; row++)
                {
                    if (board[row, col] != forWho)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    score++;
                    break; // Only one winning line needed for score
                }
            }

            // Check main diagonal
            bool mainDiagonalWin = true;
            for (int i = 0; i < size; i++)
            {
                if (board[i, i] != forWho)
                {
                    mainDiagonalWin = false;
                    break;
                }
            }
            if (mainDiagonalWin) score++; // Player wins

            // Check anti-diagonal
            bool antiDiagonalWin = true;
            for (int i = 0; i < size; i++)
            {
                if (board[i, size - 1 - i] != forWho)
                {
                    antiDiagonalWin = false;
                    break;
                }
            }
            if (antiDiagonalWin) score++; // Player wins

            return score; // Return the score
        }


        public int Minimax(char[,] board, char forWho, int depth = 1)
        {
            var score = CheckWhoWins(board, forWho);
            int depthLimit = 9;
            if (size == 5) depthLimit = 4;
            else if (size == 4) depthLimit = 5;
            if (score != 0 || depth > depthLimit)
            {
                return score;
            }
            depth++;

            var bestScore = forWho == 'o' ? int.MinValue : int.MaxValue;

            int CalcBest(int x, int y) => (forWho == 'o' ? x > y : y > x) ? x : y;

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (board[i, j] == '-')
                    {
                        board[i, j] = forWho;
                        var currentScore = Minimax(board, forWho == 'o' ? 'x' : 'o', depth);
                        board[i, j] = '-';

                        bestScore = CalcBest(bestScore, currentScore);
                    }
                }
            }
            return bestScore;
        }


        public (int, int) GetBestMove()
        {
            int bestScore = int.MinValue;
            int moveX = -1;
            int moveY = -1;

            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    if (board[x, y] == '-')
                    {
                        board[x, y] = 'o';
                        var score = Minimax(board, 'x');
                        board[x, y] = '-';

                        Console.WriteLine("Score: " + score);
                        Console.WriteLine("BestScore: " + bestScore);


                        if (score > bestScore)
                        {
                            bestScore = score;
                            moveX = x;
                            moveY = y;
                        }
                    }
                }
            }

            return (moveX, moveY);
        }

    }
}

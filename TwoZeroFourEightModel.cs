﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        public int score;
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach (int i in range)
            {
                foreach (int j in range)
                {
                    board[i, j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            NotifyAll();
        }

        public int[,] GetBoard()
        {
            return board;
        }

        private bool FullBoard()
        {
            
            int[] buffer = new int[4];
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in rangeX)
            {
                foreach (int j in rangeY)
                {  
                    if (board[i, j] == 0) return false;
                    if (j > 0 && buffer[i] != 0 && buffer[i] == buffer[i - 1])
                    
                        return false;
                }

            }
            GameOver();
            return true;
        }
        private void GameOver()
        {
            bool over = true;
            for(int i=0;i<3;i++)
            {
                for (int j=0; j<3; j++ )
                {
                    if (board[i, j] == board[i, j + 1]) over = false;
                    if (board[j, i] == board[j+1, i]) over = false;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 3] == board[i + 1, 3]) over = false;
                if (board[3, i] == board[3, i + 1]) over = false;
            }
            if (over) MessageBox.Show("GAME OVER " + score); 
        }

        private int[,] Random(int[,] input)
        {
            while (!FullBoard())
            {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                if (board[x, y] == 0)
                {
                    //int random = rand.Next(3) + 1;
                    //double r = Math.Pow(Convert.ToDouble(2), Convert.ToDouble(random));
                    //board[x, y] = Convert.ToInt16(r);
                    board[x, y] = 2;
                    break;
                }
            }
            return input;
        }

        public void PerformDown()
        {
            int[] buffer;
            int pos;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);
            foreach (int i in rangeX)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeX)
                {
                    buffer[k] = 0;
                }
                // shift down
                foreach (int j in rangeY)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        score += buffer[j - 1];
                        buffer[j] = 0;
                    }
                }
                // shift down again
                pos = 3;
                foreach (int j in rangeX)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
            }
            board = Random(board);
            NotifyAll();

        }

        public void PerformUp()
        {
            int[] buffer;
            int pos;

            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                // shift up
                foreach (int j in range)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        score += buffer[j - 1];
                        buffer[j] = 0;
                    }
                }
                // shift up again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos++;
                    }
                }
                // copy back
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
        }

        public void PerformRight()
        {
            int[] buffer;
            int pos;

            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeY)
                {
                    buffer[k] = 0;
                }
                // shift right
                foreach (int j in rangeX)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        score += buffer[j - 1];
                        buffer[j] = 0;
                    }
                }
                // shift right again
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
        }

        public void PerformLeft()
        {
            int[] buffer;
            int pos;
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[boardSize];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                // shift left
                foreach (int j in range)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        score += buffer[j - 1];
                        buffer[j] = 0;
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
        }
    }
}


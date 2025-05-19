using System;
using System.Collections.Generic;
using System.Linq;

//11)  In the question ten extend it to validate a sudoku game. 
// Validate all 9 rows (use int[,] board = new int[9,9])
namespace SudokuGame
{
    internal class SudokuValidator
    {
        // Hardcoded valid Sudoku board
        int[,] board = new int[9, 9]
        {
            { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
            { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
            { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
            { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
            { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
            { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
            { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
            { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
            { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
        };

        bool CheckRows()
        {
            for (int row = 0; row < 9; row++)
            {
                HashSet<int> seen = new HashSet<int>();
                for (int col = 0; col < 9; col++)
                {
                    if (!seen.Add(board[row, col]))
                    {
                        Console.WriteLine(" Duplicate in row");
                        return false;
                    }
                }
            }
            return true;
        }

        bool CheckColumns()
        {
            for (int col = 0; col < 9; col++)
            {
                HashSet<int> seen = new HashSet<int>();
                for (int row = 0; row < 9; row++)
                {
                    if (!seen.Add(board[row, col]))
                    {
                        Console.WriteLine(" Duplicate in columns");
                        return false;
                    }
                }
            }
            return true;
        }

        bool CheckGrids()
        {
            for (int rowStart = 0; rowStart < 9; rowStart += 3)
            {
                for (int colStart = 0; colStart < 9; colStart += 3)
                {
                    HashSet<int> seen = new HashSet<int>();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            int value = board[rowStart + i, colStart + j];
                            if (!seen.Add(value))
                            {
                                Console.WriteLine(" Duplicate in grid ");
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ValidateSudoku()
        {
            Console.WriteLine(" Validating Sudoku board...\n");

            bool rowsValid = CheckRows();
            bool colsValid = CheckColumns();
            bool gridsValid = CheckGrids();

            if (rowsValid && colsValid && gridsValid)
            {
                Console.WriteLine("\n Sudoku board is valid!");
            }
            else
            {
                Console.WriteLine("\n Sudoku board is not valid, Duplicates found");
            }
        }

        public static void Main(string[] args)
        {
            SudokuValidator validator = new SudokuValidator();
            validator.ValidateSudoku();
        }
    }
}

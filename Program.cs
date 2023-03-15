using System;

namespace Magic_Square
{
	internal class Program
	{
		static int[,] OddSquare(int n, string op)
		{
			int[,] square = new int[n, n];
			int row, column;

			row = 0; column = (n - 1) / 2;
			square[row, column] = 1;
			for (int i = 2; i <= n * n; i++)
			{
				row = (row - 1 + n) % n;
				column = (column - 1 + n) % n;

				if (square[row, column] == 0)
					square[row, column] = i;
				else
				{
					row = (row + 1) % n + 1;
					column = (column + 1) % n;
					square[row, column] = i;
				}
			}
			if (op == "draw")
				DrawSquare(square);
			return square;
		}

		static void Even4Square(int n)
		{
			int i, j;
			int[,] square = new int[n, n];
			//define an 2 - D array of order n*n
			// fill array with their index-counting 
			// starting from 1
			for (i = 0; i < n; i++)
			{
				for (j = 0; j < n; j++)
					// filling array with its count value 
					// starting from 1;
					square[i, j] = (n * i) + j + 1;
			}

			// change value of Square elements
			// at certain locations to:
			//   (n*n+1) - arr[i, j]

			// Top Left corner of Square
			// (order (n/4)*(n/4))
			for (i = 0; i < n / 4; i++)
			{
				for (j = 0; j < n / 4; j++)
					square[i, j] = (n * n + 1) - square[i, j];
			}

			// Top Right corner of Square
			// (order (n/4)*(n/4))
			for (i = 0; i < n / 4; i++)
			{
				for (j = 3 * (n / 4); j < n; j++)
					square[i, j] = (n * n + 1) - square[i, j];
			}

			// Bottom Left corner of Square
			// (order (n/4)*(n/4))
			for (i = 3 * n / 4; i < n; i++)
			{
				for (j = 0; j < n / 4; j++)
					square[i, j] = (n * n + 1) - square[i, j];
			}

			// Bottom Right corner of Square 
			// (order (n/4)*(n/4))
			for (i = 3 * n / 4; i < n; i++)
			{
				for (j = 3 * n / 4; j < n; j++)
					square[i, j] = (n * n + 1) - square[i, j];
			}

			// Center of Square (order (n/2)*(n/2))
			for (i = n / 4; i < 3 * n / 4; i++)
			{
				for (j = n / 4; j < 3 * n / 4; j++)
					square[i, j] = (n * n + 1) - square[i, j];
			}
			DrawSquare(square);
		}

		static void Even2Square(int n)
		{
			int halfN = n / 2;
			int[,] subSquare = OddSquare(halfN, "get");
			int[] gridFactors = { 0, 2, 3, 1 };
			int[,] square = new int[n, n];
			int grid, i, j;

			for (i = 0; i < n; i++)
			{
				for (j = 0; j < n; j++)
				{
					grid = (i / halfN) * 2 + (j / halfN);
					square[i, j] = subSquare[i % halfN, j % halfN];
					square[i, j] += gridFactors[grid] * n * n / 4;
				}
			}

			int nColsLeft = halfN / 2;
			int nColsRight = nColsLeft - 1;

			for (i = 0; i < halfN; i++)
				for (j = 0; j < n; j++)
				{
					if (j < nColsLeft || j >= n - nColsRight || (j == nColsLeft && i == nColsLeft))
					{
						if (j == 0 && i == nColsLeft)
							continue;

						(square[i + halfN, j], square[i, j]) = (square[i, j], square[i + halfN, j]);
					}
				}
			DrawSquare(square);
		}

		static void DrawSquare(int[,] square)
		{
			int n = square.GetLength(0);
			int magicNumber = n * (n * n + 1) / 2;

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write($"\n    {n}x{n} magic square\n\n");
			Console.ResetColor();

			for (int i = 0; i < n; i++)
			{
				Console.Write("    ");
				for (int j = 0; j < n; j++)
					Console.Write("--- ");
				Console.Write("\n");
				Console.Write("   ");
				for (int j = 0; j < n; j++)
				{
					Console.Write("|");
					Console.ForegroundColor = ConsoleColor.Blue;
					if (square[i, j] < 10)
						Console.Write($" {square[i, j]} ");
					else if (square[i, j] < 100)
						Console.Write($" {square[i, j]}");
					else
						Console.Write($"{square[i, j]}");
					Console.ResetColor();
				}
				Console.Write("|\n");
			}
			Console.Write("    ");
			for (int i = 0; i < n; i++)
				Console.Write("--- ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("\n\n    magic number: ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write($"{magicNumber}");
			Console.ResetColor();
		}

		static void Main()
		{
			int n;
			bool cont = false;

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("\n  please enter the number for n: ");
			Console.CursorVisible = false;
			while (true)
			{
				try
				{
					n = Convert.ToInt32(Console.ReadLine());
				}
				catch (Exception)
				{
					Console.Clear();
					Console.Write("\n  invalid input\n  try again. n: ");
					continue;
				}
				if (n == 2)
				{
					Console.Clear();
					Console.Write("\n  sorry, there's no magic 2x2 square\n         try again. n: ");
					continue;
				}
				Console.ResetColor();
				if (n % 2 == 1)
					OddSquare(n, "draw");
				else if (n % 4 == 0)
					Even4Square(n);
				else
					Even2Square(n);

				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("\n       (r: retry) \n");
				Console.ResetColor();
				while (true)
				{
					var key = Console.ReadKey();
					if (key.Key == ConsoleKey.R)
					{
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.Write("\n  please enter the number for n: ");
						cont = true;
					}
					else if (key.Key == ConsoleKey.Escape)
						return;
					else
					{
						Console.SetCursorPosition(0, Console.CursorTop);
						Console.Write(new string(' ', 1));
						Console.SetCursorPosition(0, Console.CursorTop);
					}
					if (cont)
					{
						cont = false;
						break;
					}
				}
			}
		}
	}
}

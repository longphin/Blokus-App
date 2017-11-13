using Blokus;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BlokusGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int boardSize = 0;
        private ColumnDefinition[] columns;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard(14, 14);
        }

        private void InitializeBoard(int ncols, int nrows)
        {
            int boardSize = this.boardSize = ncols*nrows;
            // create canvas
            for(int i=0; i<ncols; i++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < nrows; i++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
            }

            // add buttons
            for(int i=0; i<nrows; i++)
            {
                for(int j=0; j<ncols; j++)
                {
                    Button button = new Button();
                    button.Name = "Button_" + i.ToString() + "_" + j.ToString();
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    Board.Children.Add(button);
                }
            }

            // Initialize Board and players
            var b = new Board(ncols, nrows);
            b.PrintBoardText();

            // Create Players
            List<Player> players = new List<Player>();

            var p1 = new Player("Player 1");
            p1.SetCellAsAvailable(new int[] { 0, 0 });

            var p2 = new Player("Player 2");
            p2.SetCellAsAvailable(new int[] { 0, ncols - 1 });

            var p3 = new Player("Player 3");
            p2.SetCellAsAvailable(new int[] { nrows - 1, 0 });

            var p4 = new Player("Player 4");
            p4.SetCellAsAvailable(new int[] { nrows - 1, ncols - 1 });

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);

            // Add pieces to player
            AddPiecesForPlayer(p1);
            CreatePlayerInventoryUI(Inventory, p1);
            AddPiecesForPlayer(p2);
            AddPiecesForPlayer(p3);
            AddPiecesForPlayer(p4);

        }
        
        private static void CreatePlayerInventoryUI(Grid g, Player p)
        {
            var pi = new PlayerInventory(p);

            g.Children.Add(pi);
            /*
            int colsPerRow = 8;
            int nrows = (int)((double)p.pieces.Count / (double)colsPerRow - 1d + 0.5d) + 1; // 8 columns per row. The +0.5d makes sure it rounds down properly to the nearest int.
                                                                     // create canvas
            for (int i = 0; i < colsPerRow; i++)
            {
                g.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < nrows; i++)
            {
                g.RowDefinitions.Add(new RowDefinition());
            }

            for(int i=0; i<p.pieces.Count; i++)
            {
                int nrow = (int)((double)i / (double)colsPerRow);
                int ncol = i%(colsPerRow);
                Button button = new Button();
                button.Name = "Button_" + nrow.ToString() + "_" + ncol.ToString();
                button.Content = i.ToString() + "-" + nrow.ToString() + "-" + ncol.ToString();
                button.FontSize = 9;
                Grid.SetRow(button, nrow);
                Grid.SetColumn(button, ncol);

                g.Children.Add(button);
            }
            */
        }

        private static void AddPiecesForPlayer(Player p)
        {
            p.AddPiece(new Piece("I1",
                new List<int[]> { new int[] { 0, 0 } }));
            p.AddPiece(new Piece("I2",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 } }));
            p.AddPiece(new Piece("I3",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece("I4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 } }));
            p.AddPiece(new Piece("I5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 }, new int[] { 0, 4 } }));
            p.AddPiece(new Piece("L3",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece("L4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece("L5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 }, new int[] { 0, 3 } }));
            p.AddPiece(new Piece("V5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 }, new int[] { 2, 0 } }));
            p.AddPiece(new Piece("Z4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("Z5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 1, 0 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("O4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("T5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece("T4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece("N",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, -1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { 2, 0 } }));
            p.AddPiece(new Piece("P",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece("W",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("U",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, -1 }, new int[] { 1, -1 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("F",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("X",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 } }));
            p.AddPiece(new Piece("R",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 2, 0 } }));
        }
    }
}

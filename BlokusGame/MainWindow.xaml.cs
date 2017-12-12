using Blokus;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private static Random random = new Random(19);

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
            var gameboard = new Board(ncols, nrows);
            gameboard.PrintBoardText();

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
            //players.Add(p2);
            //players.Add(p3);
            players.Add(p4);

            // Add pieces to player
            AddPiecesForPlayer(p1);
            CreatePlayerInventoryUI(Inventory, p1);
            AddPiecesForPlayer(p2);
            AddPiecesForPlayer(p3);
            AddPiecesForPlayer(p4);

            while(true)
            {
                bool gameInPlay = false;
                foreach(var player in players)
                {
                    var moves = gameboard.GetPossibleMovesForPlayer(player);
                    string curname = "";
                    int count = 0;
                    foreach(Move m in moves)
                    {
                        if(m.piece.name==curname)
                        {
                            count += 1;
                        }
                        else
                        {
                            curname = m.piece.name;
                            count = 1;
                        }
                    }
                    if (moves.Any())
                    {
                        // pick a random move
                        int pickedMove = random.Next(0, moves.Count);
                        Move toDoMove = moves[pickedMove];

                        // pick a random center from the selected move
                        int pickedCenter = random.Next(0, toDoMove.validCenters.Count);
                        gameboard.MakeMove(players, player, toDoMove, pickedCenter);
                        
                        gameInPlay = true;
                        // [TO DO] Make player use the toDoMove onto the board
                    }
                }
                if (gameInPlay == false)
                {
                    gameboard.PrintBoardText();
                    break;
                }
            }
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
            p.AddPiece(new Piece(0, "I1",
                new List<int[]> { new int[] { 0, 0 } }));
            p.AddPiece(new Piece(1, "I2",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 } }));
            p.AddPiece(new Piece(2, "I3",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece(3, "I4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 } }));
            p.AddPiece(new Piece(4, "I5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 }, new int[] { 0, 4 } }));
            p.AddPiece(new Piece(5, "L3",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece(6, "L4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece(7, "L5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 }, new int[] { 0, 3 } }));
            p.AddPiece(new Piece(8, "V5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 }, new int[] { 2, 0 } }));
            p.AddPiece(new Piece(9, "Z4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece(10, "Z5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 1, 0 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece(11, "O4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece(12, "T5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece(13, "T4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece(14, "N",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, -1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { 2, 0 } }));
            p.AddPiece(new Piece(15, "P",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece(16, "W",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece(17, "U",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, -1 }, new int[] { 1, -1 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece(18, "F",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece(19, "X",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 } }));
            p.AddPiece(new Piece(20, "R",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 2, 0 } }));
        }
    }
}

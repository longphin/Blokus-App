using Blokus;
using BlokusGame.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BlokusGame
{
    /// <summary>
    /// Interaction logic for PieceController.xaml
    /// </summary>
    public partial class PieceController : UserControl
    {
        public PieceController(Piece p)
        {
            InitializeComponent();

            CreatePieces(p);
        }

        /// <summary>
        /// Creates the UI elements for the piece
        /// </summary>
        private void CreatePieces(Piece p)
        {
            // create the grid for the piece
            for (int i = 0; i < (Piece.allMaxX - Piece.allMinX + 2); i++)
            {
                Container.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < (Piece.allMaxY - Piece.allMinY + 2); i++)
            {
                Container.RowDefinitions.Add(new RowDefinition());
            }

            // fill in the grid with buttons
            foreach (var coord in p.points)
            {
                int x = coord[0] - p.minX;
                int y = coord[1] - p.minY;

                /*
                Button button = new Button();
                button.Name = "Cell_" + x.ToString() + "_" + y.ToString();

                Grid.SetRow(button, x);
                Grid.SetColumn(button, y);

                Container.Children.Add(button);
                */
                var button = new PieceBlockController();
                button.Name = "Cell_" + x.ToString() + "_" + y.ToString();

                Grid.SetRow(button, x);
                Grid.SetColumn(button, y);

                Container.Children.Add(button);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Package the data
                DataObject data = new DataObject();
                data.SetData("Object", this);

                // Initiate the drag-drop operation
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);//, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
    }
}

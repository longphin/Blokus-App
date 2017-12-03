using Blokus;
using BlokusGame.Settings;
using System.Windows;
using System.Windows.Controls;

namespace BlokusGame
{
    /// <summary>
    /// Interaction logic for PlayerInventory.xaml
    /// </summary>
    public partial class PlayerInventory : UserControl
    {
        private static int colsPerRow = 8;
        private bool isVisible = true;

        public PlayerInventory(Player p)
        {
            InitializeComponent();

            CreatePlayerInventoryUI(p);
        }

        public void CreatePlayerInventoryUI(Player p)
        {
            int nrows = (int)((double)p.pieces.Count / (double)colsPerRow - 1d + 0.5d) + 1; // 8 columns per row. The +0.5d makes sure it rounds down properly to the nearest int.
                                                                                            // create canvas
            for (int i = 0; i < colsPerRow; i++)
            {
                var cd = new ColumnDefinition();
                cd.Width = new GridLength(20, GridUnitType.Star);
                Inventory.ColumnDefinitions.Add(cd);
            }
            for (int i = 0; i < nrows; i++)
            {
                var rd = new RowDefinition();
                rd.Height = new GridLength(20, GridUnitType.Star);
                Inventory.RowDefinitions.Add(rd);
            }

            for (int i = 0; i < p.pieces.Count; i++)
            {
                int nrow = (int)((double)i / (double)colsPerRow);
                int ncol = i % (colsPerRow);

                Grid inventoryCell = new Grid();
                inventoryCell.RowDefinitions.Add(new RowDefinition());
                inventoryCell.RowDefinitions.Add(new RowDefinition());

                Button button = new Button();
                button.Name = "Button_" + nrow.ToString() + "_" + ncol.ToString();
                button.Content = p.pieces[i].name + "-" + nrow.ToString() + "-" + ncol.ToString();
                button.FontSize = 9;
                Grid.SetRow(button, 0);
                inventoryCell.Children.Add(button);

                var piece = new PieceController(p.pieces[i]);
                Grid.SetRow(piece, 1);
                inventoryCell.Children.Add(piece);

                Grid.SetRow(inventoryCell, nrow);
                Grid.SetColumn(inventoryCell, ncol);
                
                Inventory.Children.Add(inventoryCell);
            }
        }

        private void MinimizeControl(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = isVisible ? "-" : "+";
            Inventory.Visibility = isVisible ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;

            isVisible = isVisible ? false : true;
        }
    }
}

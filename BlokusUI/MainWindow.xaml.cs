using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BlokusUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private members
        // Holds the current results of cells in the active game
        private MarkType[] mResults = new MarkType[9];
        private bool mPlayer1Turn;
        private bool mGameEnded;
        #endregion

        #region contstructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Starts a new game
        /// </summary>
        private void NewGame()
        {
            mResults = new MarkType[9];

            for(var i = 0; i<mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;

            }

            // Make sure Player 1 starts the game
            mPlayer1Turn = true;
            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White; // background color default
                button.Foreground = Brushes.Blue; // text color default
            });

            // Make sure the game has not finished
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            // cast the sender to a button
            var button = (Button)sender;
            // find the index of the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            // convert column and row to the array index
            var index = column + (row * 3);

            // check if index is valid
            if (mResults[index] != MarkType.Free)
                return;

            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            button.Content = mPlayer1Turn ? "X" : "O";
            button.Foreground = mPlayer1Turn ? Brushes.Blue : Brushes.Red;

            // change player turn
            mPlayer1Turn ^= true;

            CheckForWinner();
        }

        /// <summary>
        /// Checks if there are 3 in a row
        /// </summary>
        private void CheckForWinner()
        {
            #region horizonal wins
            // horizontal wins
            if(mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region vertical wins
            // vertical wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region diagonal wins
            // diagonal wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                // Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Content = string.Empty;
                    button.Background = Brushes.Orange;
                });
            }
        }
    }

}

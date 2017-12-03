using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlokusGame.Controls
{
    /// <summary>
    /// Interaction logic for PieceCellController.xaml
    /// </summary>
    public partial class PieceCellController : UserControl
    {
        private bool FirstClick = true;
        private double
            StartX,
            StartY;

        public PieceCellController()
        {
            InitializeComponent();
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
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if(FirstClick)
                {
                    GeneralTransform transform = this.TransformToAncestor(this.Parent as Visual);
                    Point StartPoint = transform.Transform(new Point(0, 0));
                    StartX = StartPoint.X;
                    StartY = StartPoint.Y;
                    FirstClick = false;
                }
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.LeftButton == MouseButtonState.Released)
            {
                if(!FirstClick)
                {
                    FirstClick = true;
                }
            }
        }
    }
}

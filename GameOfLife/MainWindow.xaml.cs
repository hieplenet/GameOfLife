using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Universe _universe = new Universe();
        Timer _timer = new Timer();
        int _ratio = 8;
        int _relativeHeight = 0;
        int _relativeWidth = 0;

        public MainWindow()
        {
            Initializ Component();
            Random random = new Random();
			
            //Random random = new Random();
            //for (int i = 0; i < 100; i++)
            //    for (int j = 0; j < 100; j++)
            //    {
            //        if (random.Next(5) < 1)
            //        {
            //            _universe.Add(new Cell(i, j));
            //        }
            //    }

            //space gun
            _universe.Add(new Cell(2, 1));
            _universe.Add(new Cell(3, 2));
            _universe.Add(new Cell(1, 3));
            _universe.Add(new Cell(2, 3));
            _universe.Add(new Cell(3, 3));			
			
            _timer.Elapsed += _timer_Elapsed;
            _timer.Interval = 500;
            RefeshUIWithCell();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Action action = () =>
                {
                    _universe.Tick();
                    RefeshUIWithCell();
                };
            Dispatcher.BeginInvoke(action);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }
            else
            {
                _timer.Start();
            }
        }

        private void RefeshUIWithCell()
        {
            canvas.Children.Clear();
            foreach (var cell in _universe.GetAllLivingCells())
            {
                var myRect = new Rectangle();
                myRect.Stroke = System.Windows.Media.Brushes.Black;
                myRect.Fill = System.Windows.Media.Brushes.SkyBlue;
                myRect.HorizontalAlignment = HorizontalAlignment.Left;
                myRect.VerticalAlignment = VerticalAlignment.Center;
                int left = cell.Collumn * _ratio;
                int right = cell.Row * _ratio;
                if (left - _relativeWidth > 300)
                {
                   // _relativeWidth += 300;
                }
                if (right - _relativeHeight > 300)
                {
                    //_relativeHeight += 300;
                }
                Canvas.SetLeft(myRect, left - _relativeWidth);
                Canvas.SetTop(myRect, right - _relativeHeight);
                myRect.Height = _ratio;
                myRect.Width = _ratio;
                canvas.Children.Add(myRect);
            }
        }
    }
}

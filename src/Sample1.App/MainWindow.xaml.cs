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

namespace Sample1.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        mainBorder.PreviewMouseLeftButtonDown += _moveBorder_PreviewMouseLeftButtonDown;
    }

    Point _startPoint;
    bool _isDragging = false;


    private void _moveBorder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (Mouse.Capture(this))
        {
            _isDragging = true;
            _startPoint = PointToScreen(Mouse.GetPosition(this));
        }
    }

    protected override void OnPreviewMouseMove(MouseEventArgs e)
    {
        if (_isDragging)
        {
            Point newPoint = PointToScreen(Mouse.GetPosition(this));
            int diffX = (int)(newPoint.X - _startPoint.X);
            int diffY = (int)(newPoint.Y - _startPoint.Y);
            if (Math.Abs(diffX) > 1 || Math.Abs(diffY) > 1)
            {
                Left += diffX;
                Top += diffY;
                InvalidateVisual();
                _startPoint = newPoint;
            }
        }
    }
    protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        if (_isDragging)
        {
            _isDragging = false;
            Mouse.Capture(null);
        }
    }
}

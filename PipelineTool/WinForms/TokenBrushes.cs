using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.WinForms;
public static class TokenBrushes
{
    public static Brush Text = Brushes.Black;
    public static Brush Comment = Brushes.Gray;
    public static Brush Symbol = Brushes.DarkRed;
    public static Brush Variable = Brushes.Blue;
    public static Brush String = new SolidBrush(Color.FromArgb(0, 100, 100));
    public static Brush Flow = Brushes.DarkMagenta;
    public static Brush Error = Brushes.Red;
}

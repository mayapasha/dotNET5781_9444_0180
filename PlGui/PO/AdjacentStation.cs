using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class AdjacentStation : DependencyObject
    {
        
        static readonly DependencyProperty DistanceProperty = DependencyProperty.Register("Distance", typeof(double), typeof(AdjacentStation));
        public double Distance { get => (double)GetValue(DistanceProperty); set => SetValue(DistanceProperty, value); }

        static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(TimeSpan), typeof(AdjacentStation));
        public TimeSpan Time { get => (TimeSpan)GetValue(TimeProperty); set => SetValue(TimeProperty, value); }
    }
}

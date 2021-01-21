using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
   public class LineStation : AdjacentStation
    {
        static readonly DependencyProperty LineIdProperty = DependencyProperty.Register("LineId", typeof(int), typeof(LineStation));
        public int LineId { get => (int)GetValue(LineIdProperty); set => SetValue(LineIdProperty, value); }

        static readonly DependencyProperty StationProperty = DependencyProperty.Register("Station", typeof(int), typeof(LineStation));
        public int Station { get => (int)GetValue(StationProperty); set => SetValue(StationProperty, value); }

        static readonly DependencyProperty LineStationIndexProperty = DependencyProperty.Register("LineStationIndex", typeof(int), typeof(LineStation));

        public int LineStationIndex { get => (int)GetValue(LineStationIndexProperty); set => SetValue(LineStationIndexProperty, value); }

        static readonly DependencyProperty PrevStationProperty = DependencyProperty.Register("PrevStation", typeof(int), typeof(LineStation));
        public int PrevStation { get => (int)GetValue(PrevStationProperty); set => SetValue(PrevStationProperty, value); }

        static readonly DependencyProperty NextStationProperty = DependencyProperty.Register("NextStation", typeof(int), typeof(LineStation));
        public int NextStation { get => (int)GetValue(NextStationProperty); set => SetValue(NextStationProperty, value); }

        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(LineStation));
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }


    }
}

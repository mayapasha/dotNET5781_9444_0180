using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Line : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }
        private int _code;
        public int Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Code)));
            }
        }
        private BO.Enums.Areas _area;
        public BO.Enums.Areas Area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Area)));
            }
        }
            
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
         public ObservableCollection<PO.LineStation> List_Of_Line_Stations { get; set; } = new ObservableCollection<PO.LineStation>();
        public override string ToString()
        {
            return "line code: "+Code + " last station code: " + LastStation;
        }
        //public ObservableCollection<BO.AdjacentStations> List_Of_AdjacentStation { get; } = new ObservableCollection<BO.AdjacentStations>();
    }
}

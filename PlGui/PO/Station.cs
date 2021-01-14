using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Station:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        private int _longitude;
        public int Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Longitude)));
            }
        }
        private int _lattitude;
        public int Lattitude 
        {
            get
            {
                return _lattitude;
            }
            set
            {
                _lattitude = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Lattitude)));
            }
        }
        public override string ToString()
        {
            return _code + " " + _name;
        }
    }
}

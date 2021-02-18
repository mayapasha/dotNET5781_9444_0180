using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class LineTrip : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _id { get; set; }
        public int Id 
        {
            get 
            { return _id; }
            set {
                _id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof( Id)));
            } 
        }
        private int _lineId { get; set; }
        public int LineId
        {
            get
            { return _lineId; }
            set
            {
                _id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(LineId)));
            }
        }
        private TimeSpan _startAt { get; set; }
        public TimeSpan StartAt
        {
            get
            { return _startAt; }
            set
            {
                _startAt = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(StartAt)));
            }
        }
        public override string ToString()
        {
            return "start at: " + StartAt;
        }
        /* private TimeSpan _frequency { get; set; }
         public TimeSpan Frequency
         {
             get
             { return _frequency; }
             set
             {
                 _frequency = value;
                 if (PropertyChanged != null)
                     PropertyChanged(this, new PropertyChangedEventArgs(nameof(Frequency)));
             }
         }
         private TimeSpan _finishAt { get; set; }
        public TimeSpan FinishAt
         {
             get
             { return _finishAt; }
             set
             {
                 _finishAt = value;
                 if (PropertyChanged != null)
                     PropertyChanged(this, new PropertyChangedEventArgs(nameof(FinishAt)));
             }
         }*/
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet_01_9444_0180
{
    
    public enum Status
    {
        ready, on_drive, on_refeul, on_a_treatment
    }
    public class Buss : BusInfo, INotifyPropertyChanged
    {

        private string _WatchTime;
        public string WatchTime
        {
            get
            {
                return _WatchTime;
            }

            set
            {
                _WatchTime = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(WatchTime)));
            }
        }


        private Status _state;
        public Status state
        {
            get
            {
                return _state;
            }

            set
            {
                this._state = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(state)));
            }

        }
        private bool _On_work;
      

        public bool On_work
        {
            get
            {
                return _On_work;
            }

            set
            {
                _On_work = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("On_work"));
            }
        }
        public DateTime DateOfTreatment { get; set; }
        public DateTime startdate { get; set; }
        public string LicenseNum { get; set; }
        public int Mileage { get; set; }// the main mileage
        public int FuelMileage { get; set; }// counter how much mileage we pass from the last refeul(max 1200)
        public int ThisMileage { get; set; }// counter from the last treatment(max 20000)



        public Buss()// defalt constractor
        { }
        public Buss(string license, DateTime date)// constractor that get arguments
        {
            DateOfTreatment = DateTime.Now;
            LicenseNum = license;
            startdate = date;
            Mileage = 0;
            FuelMileage = 0;
            ThisMileage = 0;
            state = Status.ready;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Prepare()// return true if the buss no dangerous for a drive
        {
            DateTime today = DateTime.Now;
            if (ThisMileage < 20000 && today.AddYears(-1) <= DateOfTreatment)
                return true;
            return false;
        }


        public bool ReadyForDrive(int val)// return true if the buss can go for a drive 
        {
            if (FuelMileage + val > 1200)//if the buss need fuel for the drive
                return false;
            else if (Prepare() == false)//if the buss not preper for drive
                return false;
            else
            {
                FuelMileage += val;
                ThisMileage += val;
                Mileage += val;
                return true;
            }
        }
        public void Refuel()// refeul the buss
        {
            
            FuelMileage = 0;
            

        }

        public void treatment()// give the buss a treatment
        {
            
            ThisMileage = 0;
            DateOfTreatment = DateTime.Now;
            
        }

        public void Print()// print the data of the buss
        {
            Console.WriteLine("License:" + LicenseNum + " Mileage:" + ThisMileage);
        }

        public override string ToString()
        {
            return "license: " + LicenseNum + " status: " + state;
        }
    }


}

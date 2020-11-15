using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_9444_0180
{
    class Ibuslineinumerator : IEnumerator
    {
        public int Busindex = -1;
        public List<BusLine> Lines { get; set; }
        public Ibuslineinumerator(List<BusLine> lines)
        {
            Lines = lines;
        }
        public object Current
        {
            get { return Lines[Busindex]; } 
        }
           
        
        public bool MoveNext()
        {
            if(Lines[++Busindex]!=null)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            Busindex = -1;
        }
    }
    //public interface ICalulator
    //{
    //    int add(int x, int y);
    //}


    //public class SomeCalulator : ICalulator
    //{
    //    public int add(int x, int y)
    //    {
    //        return x + y;
    //    }

    //    public int substract(int x, int y)
    //    {
    //        return x -y;
    //    }


    //}

    //public class SomeCalulator2 : ICalulator
    //{
    //    public int add(int x, int y)
    //    {
    //        return (x * 10) + y;
    //    }
    //}
}

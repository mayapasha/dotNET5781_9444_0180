using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Add_Existing_Item_Exception : Exception
    {
        public Add_Existing_Item_Exception(string message) : base(message)
        {

        }
    }
    public class Item_not_found_Exception : Exception
    {
        public Item_not_found_Exception(string message) : base(message)
        {

        }
    }
}

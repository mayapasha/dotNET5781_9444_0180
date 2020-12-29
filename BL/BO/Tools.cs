using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Tools
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in typeof(T).GetProperties())
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
            return str;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    static class Cloning
    {
        internal static T Clone<T>(this T original) where T : new()// the func get any object and return copy of this object
        {
            T copyToObject = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())// pass all the properties in the object
            {
                propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original, null), null);
                // copy the property to the return object
            }

            return copyToObject;
        }
    }
}

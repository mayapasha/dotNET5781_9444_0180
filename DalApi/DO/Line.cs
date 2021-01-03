using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Line
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public Enums.Areas Area { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public bool Is_Active { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public Enums.Areas Area { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public IEnumerable<LineStation> List_Of_LineStation;
       // public IEnumerable<BO.AdjacentStations> List_of_adjacentStation;
    }
}

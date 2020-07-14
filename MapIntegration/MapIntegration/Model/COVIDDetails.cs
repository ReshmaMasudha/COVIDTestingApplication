using System;
using System.Collections.Generic;
using System.Text;

namespace MapIntegration.Model
{

    public class COVIDDetails 
    {
        public int total_entries { get; set; }
        public TestCenterDetail[] items { get; set; }
    }    

}

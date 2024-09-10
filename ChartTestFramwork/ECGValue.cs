using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal class ECGValue
    {
        private double value;
        private DateTime timeStamp;

        public double Value { get => value; set => this.value = value; }
        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
    }
}

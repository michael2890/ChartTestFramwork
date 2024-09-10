using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal interface IModelEKGDevice
    {
        IViewEKG ViewEKG { set; }
        IModelLocalData ModelLocaldata { set; }
        IControllerEKG ControllerEKG { set; }

        void setPort(string portName);
        
    }
}

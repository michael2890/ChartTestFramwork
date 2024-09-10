using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal interface IViewEKG
    {
        IModelEKGDevice ModelEKGDevice { set; }
        IModelLocalData ModelLocalData {  set; }
        IControllerEKG ControllerEKG { set; }

        double NewValue { set; }
        List<double> Data24h { set; }
    }
}

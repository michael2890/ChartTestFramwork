using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal interface IViewECG
    {
        IModelECGDevice ModelECGDevice { set; }
        IModelLocalData ModelLocalData {  set; }
        IControllerECG ControllerECG { set; }

        double NewValue { set; }
        List<double> Data24h { set; }
    }
}

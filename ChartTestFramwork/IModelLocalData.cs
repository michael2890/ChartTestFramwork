using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal interface IModelLocalData
    {
        IViewEKG ViewEKG { set; }
        IModelEKGDevice ModelEKGDevice { set; }
        IControllerEKG ControllerEKG { set; }

        List<double> getData24h();

    }
}

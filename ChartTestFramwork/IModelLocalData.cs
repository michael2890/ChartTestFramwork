using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal interface IModelLocalData
    {
        IViewECG ViewEKG { set; }
        IModelECGDevice ModelECGDevice { set; }
        IControllerECG ControllerECG { set; }

        List<double> getData24h();

        void saveLiveData(ECGValue aktuellerWert);

    }
}

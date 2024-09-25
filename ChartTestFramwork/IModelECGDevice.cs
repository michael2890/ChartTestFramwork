using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal interface IModelECGDevice
    {
        IViewECG ViewECG { set; }
        IModelLocalData ModelLocaldata { set; }
        IControllerECG ControllerECG { set; }

        void setPort(string portName);

        List<ECGValue> getData24h();

        void deleteData24h();

        void startLiveData();
        void stopLiveData();
        double getValue();


    }
}

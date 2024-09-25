using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal interface IControllerECG
    {
        IViewECG ViewEKG { set; }
        IModelECGDevice ModelEKGDevice { set; }
        IModelLocalData ModelLocaldata { set; }

        void getData24h();

        void startLiveData();

        void stopLiveData();

        void saveLiveData();

        void stopSaveLiveData();

    }
}

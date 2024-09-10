using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal class ControllerEKG : IControllerEKG
    {
        private IModelECGDevice modelEKGDevice;
        private IModelLocalData modelLocalData;
        private IViewEKG viewEKG;
        IViewEKG IControllerEKG.ViewEKG { set => viewEKG=value; }
        IModelECGDevice IControllerEKG.ModelEKGDevice { set => modelEKGDevice=value; }
        IModelLocalData IControllerEKG.ModelLocaldata { set => modelLocalData=value; }

        void IControllerEKG.getData24h()
        {
            viewEKG.Data24h=modelLocalData.getData24h();
        }
    }
}

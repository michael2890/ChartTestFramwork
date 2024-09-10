using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal class ModelLocaldata : IModelLocalData
    {
        private IModelECGDevice modelEKGDevice;
        private IViewEKG viewEKG;
        private IControllerEKG controllerEKG;
        IViewEKG IModelLocalData.ViewEKG { set => viewEKG=value; }
        IModelECGDevice IModelLocalData.ModelEKGDevice { set => modelEKGDevice=value; }
        IControllerEKG IModelLocalData.ControllerEKG { set => controllerEKG=value; }

        List<double> IModelLocalData.getData24h()
        {
            throw new NotImplementedException();
        }
    }
}

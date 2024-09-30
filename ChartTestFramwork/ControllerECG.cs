using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChartTestFramwork.ModelECGDevice;

namespace ChartTestFramwork
{
    internal class ControllerECG : IControllerECG
    {
        private IModelECGDevice modelECGDevice;
        private IModelLocalData modelLocalData;
        private IViewECG viewEKG;
        private bool liveSave=false;
        IViewECG IControllerECG.ViewEKG { set => viewEKG=value; }
        IModelECGDevice IControllerECG.ModelEKGDevice
        {
            set
            {
                modelECGDevice = value;
                modelECGDevice.ECGDataRecieved += new ECGDataRecievedEventHandler(modelECGDevice_ECGDataRecieved);
            }
        }
        IModelLocalData IControllerECG.ModelLocaldata { set => modelLocalData=value; }
        

        private void modelECGDevice_ECGDataRecieved(object sender, ECGDataRecievedEventArgs e)
        {
            if (liveSave)
                modelLocalData.saveLiveData(e.ECGValueRecieved);
        }



        void IControllerECG.getData24h()
        {
            viewEKG.Data24h=modelLocalData.get24hData();
        }

        void IControllerECG.startSaveLiveData()
        {
            liveSave=true;
        }

        void IControllerECG.startLiveData()
        {
           modelECGDevice.startLiveData();
        }

        void IControllerECG.stopLiveData()
        {
            modelECGDevice.stopLiveData();
        }

        void IControllerECG.stopSaveLiveData()
        {
            this.liveSave=false;
        }
    }
}

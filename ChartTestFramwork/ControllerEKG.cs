using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartTestFramwork
{
    internal class ControllerEKG : IControllerECG
    {
        private IModelECGDevice modelEKGDevice;
        private IModelLocalData modelLocalData;
        private IViewECG viewEKG;
        private bool liveSave=false;
        IViewECG IControllerECG.ViewEKG { set => viewEKG=value; }
        IModelECGDevice IControllerECG.ModelEKGDevice { set => modelEKGDevice=value; }
        IModelLocalData IControllerECG.ModelLocaldata { set => modelLocalData=value; }

        void IControllerECG.getData24h()
        {
            viewEKG.Data24h=modelLocalData.getData24h();
        }

        void IControllerECG.saveLiveData()
        {
            //liveSave=true;
            
            //Hier fehlt der Trigger duch den Empfang!

            //while (liveSave == true)
            //{
                ECGValue aktuellerWert = new ECGValue();
                aktuellerWert.TimeStamp = DateTime.Now;
                aktuellerWert.Value = modelEKGDevice.getValue();
                modelLocalData.saveLiveData(aktuellerWert);
            //    Application.DoEvents();
            //}
        }

        void IControllerECG.startLiveData()
        {
           modelEKGDevice.startLiveData();
        }

        void IControllerECG.stopLiveData()
        {
            modelEKGDevice.stopLiveData();
        }

        void IControllerECG.stopSaveLiveData()
        {
            this.liveSave=false;
        }
    }
}

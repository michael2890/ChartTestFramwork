using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartTestFramwork
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// 

        private static IModelLocalData modelLocaldata;
        private static IModelEKGDevice modelEKGDevice;
        private static IViewEKG viewEKG;
        private static IControllerEKG controllerEKG;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            modelLocaldata=new ModelLocaldata();
            modelEKGDevice=new ModelEKGDevice();
            viewEKG=new ViewEKG();
            controllerEKG=new ControllerEKG();

            modelLocaldata.ControllerEKG=controllerEKG;
            modelLocaldata.ViewEKG=viewEKG;
            modelLocaldata.ModelEKGDevice=modelEKGDevice;

            modelEKGDevice.ControllerEKG=controllerEKG;
            modelEKGDevice.ViewEKG = viewEKG;
            modelEKGDevice.ModelLocaldata=modelLocaldata;

            viewEKG.ControllerEKG=controllerEKG;
            viewEKG.ModelEKGDevice=modelEKGDevice;
            viewEKG.ModelLocalData=modelLocaldata;

            controllerEKG.ModelLocaldata=modelLocaldata ;
            controllerEKG.ModelEKGDevice=modelEKGDevice ;
            controllerEKG.ViewEKG=viewEKG ;


            Application.Run((ViewEKG)viewEKG);
        }
    }
}

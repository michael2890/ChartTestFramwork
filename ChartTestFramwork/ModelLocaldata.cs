using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChartTestFramwork
{
    internal class ModelLocaldata : IModelLocalData
    {
        private IModelECGDevice modelEKGDevice;
        private IViewECG viewEKG;
        private IControllerECG controllerEKG;
        private XDocument xmlDoc = new XDocument(new XElement("EKGDaten"));
        IViewECG IModelLocalData.ViewEKG { set => viewEKG=value; }
        IModelECGDevice IModelLocalData.ModelECGDevice { set => modelEKGDevice=value; }
        IControllerECG IModelLocalData.ControllerECG { set => controllerEKG=value; }

        List<double> IModelLocalData.getData24h()
        {
            throw new NotImplementedException();
        }

        void IModelLocalData.saveLiveData(ECGValue aktuellerWert)
        {
            XElement datenElement = new XElement("Messung",
                    new XElement("Zeitstempel", aktuellerWert.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.ffff")),
                    new XElement("Wert", aktuellerWert.Value)
                );
            xmlDoc.Root.Add(datenElement);
           

            string customDirectory = @".\"; // Passe den Pfad an
            string customFileName = $"EKG_Data.xml";

            string fullPath = Path.Combine(customDirectory, customFileName);
            xmlDoc.Save(fullPath);
        }
    }
}

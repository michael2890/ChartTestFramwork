using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal class ModelCSVWerte : IModelLocalData
    {
        private IViewECG view;
        private IModelECGDevice model;
        private IControllerECG controller;
        private FileStream fs;

        IViewECG IModelLocalData.ViewEKG { set => view = value; }
        IModelECGDevice IModelLocalData.ModelECGDevice { set => model = value; }
        IControllerECG IModelLocalData.ControllerECG { set => controller = value; }

        List<double> IModelLocalData.get24hData()
        {
            throw new NotImplementedException();
        }
        
        void IModelLocalData.set24hData(ECGValue aktuellerWert)
        {
            throw new NotImplementedException();
        }
        public ModelCSVWerte()
        {
            string path = @"MyTest" + DateTime.Now.ToString("yyMMdd") + @".csv";
            fs = File.Create(path);
        }


        void IModelLocalData.saveLiveData(ECGValue aktuellerWert)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(
                aktuellerWert.TimeStamp.ToString()
                +";"
                + aktuellerWert.Value.ToString()
                +";\n");
            fs.Write(info, 0, info.Length);
        }

       
    }
}

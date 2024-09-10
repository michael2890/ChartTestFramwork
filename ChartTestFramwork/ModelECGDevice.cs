using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal class ModelECGDevice : IModelECGDevice
    {
        private IModelLocalData modelLocalData;
        private IViewEKG viewEKG;
        private IControllerEKG controllerEKG;

        IViewEKG IModelECGDevice.ViewECG { set => viewEKG=value; }
        IModelLocalData IModelECGDevice.ModelLocaldata { set => modelLocalData=value; }
        IControllerEKG IModelECGDevice.ControllerECG { set => controllerEKG=value; }

        private SerialPort serialPort1=new SerialPort();
        private double recievedDouble;

        public ModelECGDevice()
        {
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.None;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string message = serialPort1.ReadLine();
                recievedDouble = double.Parse(message);
                viewEKG.NewValue = recievedDouble;
            }
            catch (Exception ex)
            {
            }
        }

        void IModelECGDevice.setPort(string portName)
        {
            serialPort1.PortName = portName;
            serialPort1.Open();
        }

        List<ECGValue> IModelECGDevice.getData24h()
        {
            throw new NotImplementedException();
        }

        


        void IModelECGDevice.deleteData24h()
        {
            throw new NotImplementedException();
        }

        void IModelECGDevice.startLiveData()
        {
            throw new NotImplementedException();
        }

        void IModelECGDevice.stopLiveData()
        {
            throw new NotImplementedException();
        }
    }
}

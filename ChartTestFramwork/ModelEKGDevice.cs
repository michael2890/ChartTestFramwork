using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartTestFramwork
{
    internal class ModelEKGDevice : IModelEKGDevice
    {
        private IModelLocalData modelLocalData;
        private IViewEKG viewEKG;
        private IControllerEKG controllerEKG;

        IViewEKG IModelEKGDevice.ViewEKG { set => viewEKG=value; }
        IModelLocalData IModelEKGDevice.ModelLocaldata { set => modelLocalData=value; }
        IControllerEKG IModelEKGDevice.ControllerEKG { set => controllerEKG=value; }

        private SerialPort serialPort1=new SerialPort();
        private double recievedDouble;

        public ModelEKGDevice()
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

        void IModelEKGDevice.setPort(string portName)
        {
            serialPort1.PortName = portName;
            serialPort1.Open();
        }
    }
}

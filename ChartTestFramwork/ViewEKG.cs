using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartTestFramwork
{
    public partial class ViewEKG : Form, IViewEKG
    {
        private IModelEKGDevice modelEKGDevice;
        private IModelLocalData modelLocalData;
        private IControllerEKG controllerEKG;

        IModelEKGDevice IViewEKG.ModelEKGDevice { set => modelEKGDevice=value; }
        IModelLocalData IViewEKG.ModelLocalData { set => modelLocalData=value; }
        IControllerEKG IViewEKG.ControllerEKG { set => controllerEKG=value; }
        double IViewEKG.NewValue { set => this.newValue=value; }
        List<double> IViewEKG.Data24h { set => data24h=value; }

        List<double> xVals = new List<double>();
        List<double> yVals = new List<double>();

        public Random rnd = new Random();
        double tick = 0.0;
        double newValue;
        List<double> data24h;
        
        public ViewEKG()
        {
            InitializeComponent();
            //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

            string[] ports = SerialPort.GetPortNames();
            comboBoxCOMPorts.Items.Clear();
            foreach (string p in ports)
                comboBoxCOMPorts.Items.Add(p);

            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 60;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 255;
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 25;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled=false;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineDashStyle=ChartDashStyle.Dash;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Interval =
                chart1.ChartAreas["ChartArea1"].AxisY.Interval / 2;
            chart1.Legends.Clear();
            chart1.Series["Live EKG"].ChartType=SeriesChartType.Line;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBoxLagetyp.Text = newValue.ToString();
            xVals.Add(tick);
            yVals.Add(newValue);

            if(tick>10.0)
            {
                xVals.RemoveAt(0);
                yVals.RemoveAt(0);
            }

            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = tick;
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = xVals[0];

            chart1.Series["Live EKG"].Points.DataBindXY(xVals,yVals);
            tick = tick + 0.5;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
                buttonStart.Text = "stop";
            }
            else
            {
                timer1.Enabled = false;
                buttonStart.Text = "start";
            }
        }

        private void comboBoxCOMProts_SelectedIndexChanged(object sender, EventArgs e)
        {
            modelEKGDevice.setPort(comboBoxCOMPorts.SelectedItem.ToString());            
        }


        private void buttonLoad_Click(object sender, EventArgs e)
        {
            controllerEKG.getData24h();
        }
    }
}

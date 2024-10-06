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
    public partial class ViewECG : Form, IViewECG
    {
        private IModelECGDevice modelECGDevice;
        private IModelLocalData modelLocalData;
        private IControllerECG controllerECG;
        private List<double> xVals = new List<double>();
        private List<double> yVals = new List<double>();
        private double tick = 0.0;
        private int newValue;
        private List<double> data24h;
        IModelECGDevice IViewECG.ModelECGDevice { set => modelECGDevice=value; }
        IModelLocalData IViewECG.ModelLocalData { set => modelLocalData=value; }
        IControllerECG IViewECG.ControllerECG { set => controllerECG=value; }
        int IViewECG.NewValue { set => this.newValue = value; }
        List<double> IViewECG.Data24h { set => data24h=value; }

        
        
        public ViewECG()
        {
            InitializeComponent();
            
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

        private void buttonStartLiveData_Click(object sender, EventArgs e)
        {

            if (timer1.Enabled == false)
            {
                controllerECG.startLiveData();
                timer1.Enabled = true;
                
                buttonStartLiveData.Text = "stop";

            }
            else
            {
                controllerECG.stopLiveData();
                timer1.Enabled = false;
                buttonStartLiveData.Text = "start";
            }
        }

        private void comboBoxCOMProts_SelectedIndexChanged(object sender, EventArgs e)
        {
            modelECGDevice.setPort(comboBoxCOMPorts.SelectedItem.ToString());            
        }


        private void buttonLoad_Click(object sender, EventArgs e)
        {
            controllerECG.getData24h();
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            if (buttonSaveData.Text == "start save")
            {
                buttonSaveData.Text = "stop save";
                controllerECG.startSaveLiveData();
            }
            else
            {
                buttonSaveData.Text = "start save";
                controllerECG.stopSaveLiveData();
            }
        }
    }
}

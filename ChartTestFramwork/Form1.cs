using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartTestFramwork
{
    public partial class Form1 : Form
    {
        List<double> xVals = new List<double>();
        List<double> yVals = new List<double>();
        
        public Random rnd = new Random();
        double tick = 0.0;

        public Form1()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            comboBoxCOMProts.Items.Clear();
            foreach (string p in ports)
                comboBoxCOMProts.Items.Add(p);

            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 60;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 1024;
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 200;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled=true;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineDashStyle=ChartDashStyle.Dash;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Interval =
                chart1.ChartAreas["ChartArea1"].AxisY.Interval / 2;
            chart1.Legends.Clear();
            chart1.Series["Live EKG"].ChartType=SeriesChartType.Line;
            //chart1.Series["Live EKG"].Points.AddXY(0, 512);
            //chart1.Series["Live EKG"].Points.AddXY(10, 512);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            xVals.Add(tick);
            yVals.Add(1024* rnd.NextDouble());

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
    }
}

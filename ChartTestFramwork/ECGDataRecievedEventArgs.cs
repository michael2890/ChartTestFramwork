using System;

namespace ChartTestFramwork
{
    public class ECGDataRecievedEventArgs : EventArgs
    {
        internal ECGValue ECGValueRecieved { get; set; }
    }
}
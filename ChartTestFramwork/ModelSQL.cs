using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace ChartTestFramwork
{
    internal class ModelSQL : IModelLocalData
    {

       private static String myConnectionString = "server=127.0.0.1;uid=EKG;pwd=1234;database=EKG;";

       private static MySqlConnection conn = new MySqlConnection(myConnectionString);

       private static MySqlCommand mycommand = conn.CreateCommand();
        IViewEKG IModelLocalData.ViewEKG { set => throw new NotImplementedException(); }
        IModelECGDevice IModelLocalData.ModelEKGDevice { set => throw new NotImplementedException(); }
        IControllerEKG IModelLocalData.ControllerEKG { set => throw new NotImplementedException(); }

        List<double> IModelLocalData.getData24h()
        {
            throw new NotImplementedException();
        }

        void IModelLocalData.saveLiveData(ECGValue aktuellerWert)
        {
            mycommand.CommandText = "Insert into ekgtabelle  values(NULL,"+
                aktuellerWert.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:f:ff") + 
                "',"+aktuellerWert.Value +" ')";

            try
            {
                conn.Open();
                mycommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            { 
                conn.Close();
            }
        }


    }
}

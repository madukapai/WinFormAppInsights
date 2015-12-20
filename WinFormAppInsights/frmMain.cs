using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ApplicationInsights;

namespace WinFormAppInsights
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private TelemetryClient objTc = new TelemetryClient();

        private void frmMain_Load(object sender, EventArgs e)
        {
            objTc.InstrumentationKey = "請在這裡放入金鑰";

            objTc.Context.User.Id = Environment.UserName;
            objTc.Context.Session.Id = Guid.NewGuid().ToString();
            objTc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            objTc.TrackPageView("frmMain");
        }

        /// <summary>
        /// 單純的透過Button送出Application Insights的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            objTc.TrackEvent("Button送出的事件");
            MessageBox.Show("已送出事件");
        }

        /// <summary>
        /// 刻意發生意外事件的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnException_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable objDt = new DataTable();
                string strColumnValue = objDt.Rows[0]["Column1"].ToString();
            }
            catch (Exception ex)
            {
                objTc.TrackException(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void btnFlush_Click(object sender, EventArgs e)
        {
            objTc.Flush();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            objTc.Flush();
        }
    }
}

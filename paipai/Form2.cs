using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace paipai
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            this.Location = new Point(0, 0);
            InitializeComponent();
            //webBrowser1.Navigate("https://paimai2.alltobid.com/bid/2015102401/login.htm");
            webBrowser1.Navigate("http://moni.51hupai.org:8081/");
            //webBrowser1.Navigate("http://test.alltobid.com/");
        }

        public void ReFlush()
        {
            webBrowser1.Navigate("http://moni.51hupai.org:8081/");
        
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UrT_pk3_creator
{
    public partial class AddServer : Form
    {
        public AddServer()
        {
            InitializeComponent();
        }

        public String GetName()
        {
            return textBox1.Text;
        }

        public String GetIp()
        {
            return textBox2.Text;
        }
    }
}

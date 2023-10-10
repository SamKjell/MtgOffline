using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtgOffline
{
    public partial class LargeNotice : Form
    {
        public LargeNotice(String title, String message)
        {
            InitializeComponent();
            richTextBox1.Text = message;
            label1.Text = title;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LargeNotice_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
    }
}

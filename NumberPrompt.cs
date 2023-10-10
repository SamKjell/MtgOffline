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
    public partial class NumberPrompt : Form
    {
        public int returnValue = 0;
        public NumberPrompt(string title, string message)
        {
            InitializeComponent();
            Title.Text = title;
            Description.Text = message;
        }
        public NumberPrompt(string title, string message, int lowerLimit, int upperLimit)
        {
            InitializeComponent();
            Title.Text = title;
            Description.Text = message;
            numberValue.Minimum = lowerLimit;
            numberValue.Maximum = upperLimit;
        }

        private void Description_TextChanged(object sender, EventArgs e)
        {

        }

        private void Okay_Click(object sender, EventArgs e)
        {
            returnValue = (int)numberValue.Value;
            Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NumberPrompt_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
    }
}

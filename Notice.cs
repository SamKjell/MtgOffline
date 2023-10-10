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
    public partial class Notice : Form
    {
        public delegate void CloseEvent();
        private CloseEvent closeEvent;
        public Notice(String title, String message)
        {
            InitializeComponent();
            richTextBox1.Text = message;
            label1.Text = title;
        }

        public Notice(string title, string message, string buttonText,CloseEvent closeEvent = null)
        {
            InitializeComponent();
            richTextBox1.Text = message;
            label1.Text = title;
            button1.Text = buttonText;
            this.closeEvent = closeEvent;
        }

        public Notice(string title, string message, bool disableButton)
        {
            InitializeComponent();
            richTextBox1.Text = message;
            label1.Text = title;
            button1.Enabled = !disableButton;
            button1.Visible = !disableButton;
            ShowInTaskbar = false;
        }

        public void SetTitle(string title)
        {
            label1.Text = title;
        }

        public void SetDesc(string desc)
        {
            richTextBox1.Text = desc;
        }

        public void SetButtonText(string text)
        {
            button1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            closeEvent?.Invoke();
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Notice_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
    }
}

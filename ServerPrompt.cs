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
    public partial class ServerPrompt : Form
    {
        public int port;
        public string address;

        public ServerPrompt()
        {
            InitializeComponent();
            Port.Text = Settings.defaultPort.ToString();
        }

        private void CreateCard_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            if (!int.TryParse(Port.Text, out port)) { Close(); return;}
            address = NameBox.Text;
            if (address == "localhost")
                address = "127.0.0.1";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ServerPrompt_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
    }
}

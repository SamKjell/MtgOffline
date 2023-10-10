using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public partial class Settings : Form
    {
        public static string nickname;
        public static bool lightNotifications;
        public static bool loadImages;
        public static int defaultPort;
        public static Bitmap playerIcon = Properties.Resources.DefaultPlayerIcon;

        public Settings()
        {
            ReadSettings();   
            InitializeComponent();
            NickName.Text = nickname;
            LightNotifications.Checked = lightNotifications;
            LoadOnlineCardImages.Checked = loadImages;
            DefaultPortBox.Text = defaultPort.ToString();
        }

        public static void ReadSettings()
        {
            if (!File.Exists(Setup.appDataPath + "settings.es"))
                CreateDefaultSettingsFile();
            if (File.Exists(Setup.appDataPath + "player.png"))
                playerIcon = new Bitmap(Setup.appDataPath + "player.png");
            ESBuilder builder = new ESBuilder(File.ReadAllText(Setup.appDataPath + "settings.es"));
            nickname = builder.Get("nickname", "Player 1");
            lightNotifications = builder.Get("light_notifications", false);
            loadImages = builder.Get("client_images", true);
            defaultPort = builder.Get("default_port", 1313);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //save and exit
            if (!File.Exists(Setup.appDataPath + "settings.es"))
                CreateDefaultSettingsFile();
            ESBuffer buffer = new ESBuffer(new ESBuilder(File.ReadAllText(Setup.appDataPath + "settings.es")));
            
            buffer.Add("nickname", ESUtils.GetFormattedString(NickName.Text));
            buffer.Add("light_notifications", LightNotifications.Checked);
            buffer.Add("client_images", LoadOnlineCardImages.Checked);
            buffer.Add("default_port", (int.TryParse(DefaultPortBox.Text, out int port) ? port : 1313));

            string esBasic = buffer.GetES();
            File.WriteAllText(Setup.appDataPath + "settings.es", esBasic);
            Close();
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        static void CreateDefaultSettingsFile()
        {
            //Create default file
            ESBuffer buffer = new ESBuffer();
            buffer.Add("nickname", "Player 1");
            buffer.Add("light_notifications", false);
            buffer.Add("client_images",true);
            buffer.Add("default_port", 1313);
            string esBasic = buffer.GetES();
            File.WriteAllText(Setup.appDataPath + "settings.es", esBasic);
        }

        private void Information_Click(object sender, EventArgs e)
        {
            LargeNotice notice = new LargeNotice("Mtg Offline",Program.GenerateCredits()+"\n\n\n\n"+Program.GenerateChangelog());
            notice.ShowDialog();
        }

        private void loadImages_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
    }
}

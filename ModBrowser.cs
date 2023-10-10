using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MtgOffline.Mod_Framework;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public partial class ModBrowser : Form
    {
        string websiteLink;

        public ModBrowser()
        {
            InitializeComponent();
            UpdateSettingsButton(false);
            SetDefault();
            PopulateMods();
        }

        private void UpdateSettingsButton(bool active)
        {
            SettingsButton.Visible = active;
            SettingsButton.Enabled = active;
        }

        private void SetDefault()
        {
            ModIconBox.Image = Properties.Resources.ModloaderIcon;
            ModName.Text = "MTGO Modloader";
            ModAuthor.Text = "Sam Kjell";
            websiteLink = "https://github.com/SamKjell/";
            Credits.Text = "Sam Kjell";
            ModIdBox.Text = "";
            Description.Text = "This is the vanilla modloading support. It provides framework to all of the other mods and also allows for their integration into the vanilla game code!";
            UpdateSettingsButton(false);
            ModVersion.Text = $"{Program.GetVersion()}";
        }

        private void SetMod(Mod mod)
        {
            ESBuilder builder = null;
            if (mod.link.Substring(0, 9) == "#META_TAG")
                builder = new ESBuilder(mod.link);
            bool isEgg = builder==null?false: builder.Get("is_egg", false);

            ModIconBox.Image = mod.icon;
            ModName.Text = mod.displayName;
            ModAuthor.Text = mod.author;
            websiteLink = isEgg?builder.Get("link",""):mod.link;
            Credits.Text = mod.credits;
            ModIdBox.Text = isEgg?"":mod.modId;
            Description.Text = mod.description;
            UpdateSettingsButton(mod.modSettings!=null);
            ModVersion.Text = isEgg?"":$"{mod.majorVersion} - {mod.minorVersion}";
        }


        private void ClickOnIcon(object sender, EventArgs e)
        {
            //Open Website
            if (websiteLink == "" || websiteLink == null) return;
            BooleanNotice notice = new BooleanNotice("Confirmation", $"{websiteLink}\n Only open links from sources you trust.", "Cancel", "Continue");
            notice.ShowDialog();
            if(notice.DialogResult == DialogResult.OK)
                System.Diagnostics.Process.Start(websiteLink);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ModAuthor_Click(object sender, EventArgs e)
        {

        }

        private Mod GetSelectedMod()
        {
            int i = ModListBox.SelectedIndex;
            if (i <= 0 || i > ModLoader.mods.Count) return null;
            return ModLoader.mods.ElementAt(i - 1).Value;
        }

        private void PopulateMods()
        {
            ModListBox.BeginUpdate();
            ModListBox.Items.Clear();
            ModListBox.Items.Add("MTGO Modloader");
            foreach (Mod mod in ModLoader.mods.Values)
                ModListBox.Items.Add(mod.displayName);
            ModListBox.EndUpdate();
        }

        private void ModListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = ModListBox.SelectedIndex;
            if (i < 0 || i > ModLoader.mods.Count) return;
            else if (i == 0) SetDefault();
            else
                SetMod(ModLoader.mods.ElementAt(i - 1).Value);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Mod mod = GetSelectedMod();
            if (mod == null || mod.modSettings == null) return;
            mod.modSettings.OnSettingsClicked();
        }

        private void ModBrowser_Load(object sender, EventArgs e)
        {
            EventBus.PostEvent("Load", sender, e);
        }
    }
}

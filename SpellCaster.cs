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
    public partial class SpellCaster : Form
    {
        StackObject effect;
        public SpellCaster(String title, StackObject obj)
        {
            InitializeComponent();
            Title.Text = title;
            effect = obj;
            HandleServerLogic();
        }
        public SpellCaster(string title, StackObject obj, bool canSelectBypassStack)
        {
            InitializeComponent();
            Title.Text = title;
            effect = obj;
            if (!canSelectBypassStack)
            {
                BypassStack.Visible = false;
                BypassStack.Enabled = false;
            }
            HandleServerLogic();
        }

        private void HandleServerLogic()
        {
            if (Program.windowInstance.connectedToServer)
            {
                BypassStack.Visible = false;
                BypassStack.Enabled = false;
            }
        }

        private void CannotBeCountered_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ActivateSpellOrAbility_Click(object sender, EventArgs e)
        {
            if (BypassStack.Checked)
            {
                effect.Resolve();
                Close();
            }
            else
            {
                Program.windowInstance.game.AddToStack(effect);
                Close();
            }
        }

        private void SpellCaster_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
    }
}

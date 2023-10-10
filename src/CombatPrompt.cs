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
    public partial class CombatPrompt : Form
    {
        bool isAttacking;
        List<CreatureBase> defenders = new List<CreatureBase>();
        List<AttackData> attackers = new List<AttackData>();

        public List<AttackData> returnData = new List<AttackData>();
        public CombatPrompt(List<AttackData> attackers, List<CreatureBase>defenders, bool isAttacking)
        {
            this.attackers = attackers;
            this.defenders = defenders;
            this.isAttacking = isAttacking;
            InitializeComponent();
            Text = "Combat: "+ ((isAttacking)?"Attacking":"Blocking");
            UpdateLists();
        }

        void UpdateLists()
        {
            Attackers.BeginUpdate();
            Attackers.Items.Clear();
            foreach (AttackData d in attackers)
            {
                string title = d.creature.name + "   (" + d.creature.GetPower() + "/" + d.creature.GetToughness() + ")" + " is attacking " + d.target.name;
                Attackers.Items.Add(title);
            }
            Attackers.EndUpdate();
            Blockers.BeginUpdate();
            Blockers.Items.Clear();
            foreach (CreatureBase blocker in defenders)
            {
                string title = blocker.name + "   (" + blocker.GetPower() + "/" + blocker.GetToughness() + ")";
                foreach (AttackData aD in attackers)
                {
                    if (aD.blockers.Contains(blocker))
                        title += " blocking " + aD.creature.name + "   (" + aD.creature.GetPower() + "/" + aD.creature.GetToughness() + ")";
                }
                Blockers.Items.Add(title);
            }
            Blockers.EndUpdate();
        }

        private void Done_Click(object sender, EventArgs e)
        {
            returnData = attackers;
            Close();
        }

        private void AssignBlocks_Click(object sender, EventArgs e)
        {
            if (Attackers.SelectedIndex < 0 || Attackers.SelectedIndex >= attackers.Count
                || Blockers.SelectedIndex < 0 || Blockers.SelectedIndex >= defenders.Count) return;
            attackers[Attackers.SelectedIndex].blockers.Clear();
            attackers[Attackers.SelectedIndex].blockers.Add(defenders[Blockers.SelectedIndex]);
            UpdateLists();
        }

        private void RemoveBlocks_Click(object sender, EventArgs e)
        {
            if (Blockers.SelectedIndex < 0 || Blockers.SelectedIndex >= defenders.Count) return;
            foreach (AttackData a in attackers)
            {
                if (a.blockers.Contains(defenders[Blockers.SelectedIndex]))
                    a.blockers.Remove(defenders[Blockers.SelectedIndex]);
            }
            UpdateLists();
        }

        private void Attackers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Attackers.SelectedIndex < 0 || Attackers.SelectedIndex >= attackers.Count) return;
            Program.windowInstance.UpdateInspector(attackers[Attackers.SelectedIndex].creature);
        }

        private void Blockers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Blockers.SelectedIndex < 0 || Blockers.SelectedIndex >= defenders.Count) return;
            Program.windowInstance.UpdateInspector(defenders[Blockers.SelectedIndex]);
        }

        private void CombatPrompt_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
    }
}

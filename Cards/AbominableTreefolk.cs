using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class AbominableTreefolk : CreatureBase //Work in Progress. Reference the card to complete the task.
    {
        public AbominableTreefolk()
        {
            manaCost = "2,G,U";
            name = "Abominable Treefolk";
            superTypes.Add("Snow");
            subTypes.Add("Treefolk");
            staticAbilities.Add("Trample");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464143&type=card";
        }
        public override int GetPower()
        {
            int i = Player.CountBySuperType("Snow", controller.GetBattlefield()) + base.GetPower();
            return i;
        }
        public override int GetToughness()
        {
            return Player.CountBySuperType("Snow", controller.GetBattlefield()) + base.GetToughness();
        }
        public override void ETB()
        {
            Condition con = new FrozenCondition("controller_upkeep",true);
            CardObject target = controller.ChooseTargetCreatureOnBattlefield(true);
            if (target != null)
            {
                ConditionStackObject cso = new ConditionStackObject(new List<CardObject> { target }, con);
                controller.TriggerAbility(cso);
            }
        }

        public override string GetTranscript()
        {
            return @"
Card Name: Abominable Treefolk
Mana Cost:
2GreenBlue
Converted Mana Cost:4
Types:Snow Creature — Treefolk
Card Text:
Trample
Abominable Treefolk's power and toughness are each equal to the number of snow permanents you control.
When Abominable Treefolk enters the battlefield, tap target creature an opponent controls. That creature doesn't untap during its controller's next untap step.
P/T: * / *";
        }
    }
}

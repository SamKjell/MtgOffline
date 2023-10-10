using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class IcebergCancrix : CreatureBase
    {
        public IcebergCancrix()
        {
            manaCost = "1,U";
            name = "Iceberg Cancrix";
            superTypes.Add("Snow");
            subTypes.Add("Crab");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464003&type=card";
            power = 0;
            toughness = 4;
        }
        public override void CardEntersTheBattlefield(CardObject c)
        {
            if(c.superTypes.Contains("Snow") && c.controller == controller && c!=this)
            {
                Player target = controller.ChooseTargetPlayer(true);
                MillStackObject trigger = new MillStackObject(new List<Player>() { target }, 2, controller, this);
                controller.TriggerAbility(trigger);
            }
        }
        public override string GetTranscript()
        {
            return @"Card Name:Iceberg Cancrix
Mana Cost:1Blue
Converted Mana Cost:2
Types:Snow Creature — Crab
Card Text:
Whenever another snow permanent enters the battlefield under your control, you may have target player mill two cards.
P/T:0 / 4";
        }
    }
}

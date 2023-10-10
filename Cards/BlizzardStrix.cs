using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class BlizzardStrix : CreatureBase
    {
        public BlizzardStrix()
        {
            manaCost = "4,U";
            name = "Blizzard Strix";
            superTypes.Add("Snow");
            subTypes.Add("Bird");
            staticAbilities.Add("Flash");
            staticAbilities.Add("Flying");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=463991&type=card";
            power = 3;
            toughness = 2;
        }
        public override void ETB()
        {
            if (Player.CountBySuperType("Snow", controller.battlefield) >= (CardContainsSuperType("Snow") ? 2 : 3))
            {
                //Initiate flicker effect
                CardObject target = controller.ChooseTargetPermanentOnBattlefield(false,new List<CardObject>() {this});
                if (target == null)
                    return;
                Condition condition = new FlickerCondition("endstep", "is being flickered");
                ConditionStackObject cso = new ConditionStackObject(new List<CardObject>() { target }, condition);
                controller.TriggerAbility(cso);
            }
        }

        public override string GetTranscript()
        {
            return @"
Card Name:Blizzard Strix
Mana Cost:4Blue
Converted Mana Cost:5
Types:Snow Creature — Bird
Card Text:
Flash
Flying
When Blizzard Strix enters the battlefield, if you control another snow permanent, exile target permanent other than Blizzard Strix. Return that card to the battlefield under its owner's control at the beginning of the next end step.
P/T:3 / 2";
        }
    }
}

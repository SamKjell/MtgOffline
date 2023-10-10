using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class IcefangCoatl : CreatureBase
    {
        public IcefangCoatl()
        {
            manaCost = "G,U";
            name = "Ice-Fang Coatl";
            superTypes.Add("Snow");
            subTypes.Add("Snake");
            staticAbilities.Add("Flash");
            staticAbilities.Add("Flying");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464152&type=card";
            power = 1;
            toughness = 1;
        }
        public override void ETB()
        {
            DrawStackObject trigger = new DrawStackObject(new List<Player> { controller }, 1);
            controller.TriggerAbility(trigger);
        }
        public override bool HasDeathtouch()
        {
            if (Player.CountBySuperType("Snow", controller.battlefield) >= (CardContainsSuperType("Snow") ? 4 : 3))
                return true;
            return base.HasDeathtouch(); 
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Ice-Fang Coatl
Mana Cost:GreenBlue
Converted Mana Cost:2
Types:Snow Creature — Snake
Card Text:
Flash
Flying
When Ice-Fang Coatl enters the battlefield, draw a card.
Ice-Fang Coatl has deathtouch as long as you control at least three other snow permanents.
P/T:1 / 1";
        }
    }
}

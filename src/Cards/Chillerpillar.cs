using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Chillerpillar : CreatureBase
    {
        public Chillerpillar()
        {
            manaCost = "3,U";
            name = "Chillerpillar";
            superTypes.Add("Snow");
            subTypes.Add("Insect");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=463992&type=card";
            power = 3;
            toughness = 3;
            abilities.Add(new MonstrousAbility(this, 2,"6"));
        }
        public override bool HasFlying()
        {
            if (IsMonsterous())
                return true;
            return base.HasFlying();
        }

        public override CardObject SimulationCloneCard(Player controller)
        {
            Chillerpillar pillar =  base.SimulationCloneCard(controller) as Chillerpillar;
            pillar.abilities.Clear();
            pillar.abilities.Add(new MonstrousAbility(pillar, 2, "6"));
            return pillar;
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Chillerpillar
Mana Cost:3Blue
Converted Mana Cost:4
Types:Snow Creature — Insect
Card Text:
4SnowSnow: Monstrosity 2. (If this creature isn't monstrous, put two +1/+1 counters on it and it becomes monstrous. Snow can be paid with one mana from a snow permanent.)
As long as Chillerpillar is monstrous, it has flying.
P/T:3 / 3";
        }
    }
}

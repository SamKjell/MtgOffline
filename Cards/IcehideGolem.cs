using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class IcehideGolem : CreatureBase
    {
        public IcehideGolem()
        {
            manaCost = "1";
            name = "Icehide Golem";
            superTypes.Add("Snow");
            cardTypes.Insert(0, "Artifact");
            subTypes.Add("Golem");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464173&type=card";
            power = 2;
            toughness = 2;
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Icehide Golem
Mana Cost:Snow
Converted Mana Cost:1
Types:Snow Artifact Creature — Golem
Card Text:
(Snow can be paid with one mana from a snow permanent.)
Flavor Text:
In colder climates, ice is more obedient than stone.
P/T:2 / 2";
        }
    }
}

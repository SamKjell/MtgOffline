using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class ArcumAstrolabe : ArtifactBase
    {
        public ArcumAstrolabe()
        {
            manaCost = "1";
            name = "Arcum's Astrolabe";
            superTypes.Add("Snow");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464169&type=card";
            abilities.Add(new ManaAbility("1,T", "O"));
        }
        public override void ETB()
        {
            DrawStackObject trigger = new DrawStackObject(new List<Player>{controller}, 1);
            controller.TriggerAbility(trigger);
        }
        public override string GetTranscript()
        {
            return @"Card Name:Arcum's Astrolabe
Mana Cost:Snow
Converted Mana Cost:1
Types:Snow Artifact
Card Text:
(Snow can be paid with one mana from a snow permanent.)
When Arcum's Astrolabe enters the battlefield, draw a card.
1, Tap: Add one mana of any color.";
        }
    }
}

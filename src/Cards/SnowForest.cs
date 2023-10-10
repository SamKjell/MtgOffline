using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class SnowForest: LandBase
    {
        public SnowForest()
        {
            name = "Snow-Covered Forest";
            superTypes.Add("Basic");
            superTypes.Add("Snow");
            subTypes.Add("Forest");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464203&type=card";
            abilities.Add(ManaAbility.ForestManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Snow-Covered Forest 
Types:Basic Snow Land — Forest 
Card Text:Tap: add G to your mana pool";
        }
    }
}

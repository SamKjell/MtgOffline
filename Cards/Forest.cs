using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Forest : LandBase
    {
        public Forest()
        {
            name = "Forest";
            superTypes.Add("Basic");
            subTypes.Add("Forest");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=439605&type=card";
            abilities.Add(ManaAbility.ForestManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Forest 
Types:Basic Land — Forest
Card Text:Tap: add G to your mana pool";
        }
    }
}

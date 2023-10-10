using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Mountain : LandBase
    {
        public Mountain()
        {
            name = "Mountain";
            superTypes.Add("Basic");
            subTypes.Add("Mountain");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=439604&type=card";
            abilities.Add(ManaAbility.MountainManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Mountain 
Types:Basic Land — Mountain
Card Text:Tap: add R to your mana pool";
        }
    }
}

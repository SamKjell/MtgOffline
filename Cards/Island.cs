using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Island : LandBase
    {
        public Island()
        {
            name = "Island";
            superTypes.Add("Basic");
            subTypes.Add("Island");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=439602&type=card";
            abilities.Add(ManaAbility.IslandManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Island 
Types:Basic Land — Island 
Card Text:Tap: add U to your mana pool";
        }
    }
}

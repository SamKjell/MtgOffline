using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class SnowIsland : LandBase
    {
        public SnowIsland()
        {
            name = "Snow-Covered Island";
            superTypes.Add("Basic");
            superTypes.Add("Snow");
            subTypes.Add("Island");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464200&type=card";
            abilities.Add(ManaAbility.IslandManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Snow-Covered Island 
Types:Basic Snow Land — Island 
Card Text:Tap: add U to your mana pool";
        }
    }
}

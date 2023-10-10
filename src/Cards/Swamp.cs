using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Swamp : LandBase
    {
        public Swamp()
        {
            name = "Swamp";
            superTypes.Add("Basic");
            subTypes.Add("Swamp");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=439603&type=card";
            abilities.Add(ManaAbility.SwampManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Swamp 
Types:Basic Land — Swamp
Card Text:Tap: add B to your mana pool";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Wastes : LandBase
    {
        public Wastes()
        {
            name = "Wastes";
            superTypes.Add("Basic");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=407696&type=card";
            abilities.Add(ManaAbility.WastesManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Wastes
Types:Basic Land
Card Text:Tap: add colorless to your mana pool";
        }
    }
}

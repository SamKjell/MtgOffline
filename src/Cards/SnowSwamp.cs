using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class SnowSwamp : LandBase
    {
        public SnowSwamp()
        {
            name = "Snow-Covered Swamp";
            superTypes.Add("Basic");
            superTypes.Add("Snow");
            subTypes.Add("Swamp");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464201&type=card";
            abilities.Add(ManaAbility.SwampManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Snow-Covered Swamp 
Types:Basic Snow Land — Swamp
Card Text:Tap: add B to your mana pool";
        }
    }
}

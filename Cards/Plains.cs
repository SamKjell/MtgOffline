using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Plains : LandBase
    {
        public Plains()
        {
            name = "Plains";
            superTypes.Add("Basic");
            subTypes.Add("Plains");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=439601&type=card";
            abilities.Add(ManaAbility.PlainsManaAbility);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Plains
Types:Basic Land — Plains
Card Text:Tap: add W to your mana pool";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class CallTheScions : SorceryBase
    {
        public CallTheScions()
        {
            manaCost = "2,G";
            name = "Call the Scions";
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=401836&type=card";
            staticAbilities.Add("Devoid");
        }

        public override void Resolve()
        {
            for (int i = 0; i < 2; i++)
                controller.InstantiateToken(new EldraziScionToken());

            base.Resolve();
        }

        public override string GetTranscript()
        {
            return @"Card Name:
Call the Scions
Mana Cost:
2Green
Converted Mana Cost:
3
Types:
Sorcery
Card Text:
Devoid (This card has no color.)
Create two 1/1 colorless Eldrazi Scion creature tokens. They have 'Sacrifice this creature: Add Colorless.'";
        }
    }
}

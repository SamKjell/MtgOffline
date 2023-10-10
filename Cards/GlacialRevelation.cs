using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class GlacialRevelation : SorceryBase
    {
        public GlacialRevelation()
        {
            manaCost = "2,G";
            name = "Glacial Revelation";
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464116&type=card";
        }
        public override void Resolve()
        {
            int amount = 6;
            if (controller.library.Count < 6)
                amount = controller.library.Count;
            List<CardObject> topSix = controller.library.GetRange(0, amount);
            List<CardObject> options = new List<CardObject>();
            foreach (CardObject card in topSix)
            {
                controller.RevealCard(card);
                if (card.isPermanent && card.superTypes.Contains("Snow"))
                    options.Add(card);
            }
            foreach (CardObject card in options)
            {
                controller.hand.Add(card);
                topSix.Remove(card);
            }
            controller.graveyard.AddRange(topSix);

            base.Resolve();
        }
        public override string GetTranscript()
        {
            return @"Card Name:Glacial Revelation
Mana Cost:2Green
Converted Mana Cost:3
Types:Sorcery
Card Text:
Reveal the top six cards of your library. You may put any number of snow permanent cards from among them into your hand. Put the rest into your graveyard.";
        }
    }
}

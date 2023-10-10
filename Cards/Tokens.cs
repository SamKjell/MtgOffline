using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Tokens
    {
        public static CardObject CreateTokenFromCard(CardObject card)
        {
            CardObject token = (CardObject)Activator.CreateInstance(card.GetType());
            token.cardTypes.Insert(0, "Token");

            return token;
        }

    }


    public class SquirrelToken : CreatureBase
    {
        public SquirrelToken()
        {
            manaCost = "0";
            name = "Squirrel";
            cardTypes.Insert(0,"Token");
            subTypes.Add("Squirrel");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=5607&type=card";
            power = 1;
            toughness = 1;
        }
    }

    public class EldraziScionToken : CreatureBase
    {
        public EldraziScionToken()
        {
            manaCost = "0";
            name = "Eldrazi Scion";
            cardTypes.Insert(0, "Token");
            subTypes.Add("Eldrazi");
            subTypes.Add("Scion");
            imageURL = "https://tcgplayer-cdn.tcgplayer.com/product/111199_200w.jpg";
            power = 1;
            toughness = 1;
            abilities.Add(new EldraziManaAbility());
        }

        public class EldraziManaAbility : ManaAbility
        {
            public EldraziManaAbility() : base("0","C")
            {

            }

            public override void Activate()
            {
                base.Activate();
                source.Sacrifice();
            }
        }
    }
}

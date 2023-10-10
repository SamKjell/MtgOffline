using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class DeadOfWinter : SorceryBase
    {
        public DeadOfWinter()
        {
            manaCost = "2,B";
            name = "Dead of Winter";
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464034&type=card";
        }
        public override void Resolve()
        {
            List<CardObject> creatures = controller.GetAllCreatures(false);
            int debuffValue = Player.CountBySuperType("Snow", controller.battlefield)*-1;
            Condition condition = new StatModCondition(debuffValue, debuffValue, "endstep");
            foreach (CardObject card in creatures)
            {
                if(!card.CardContainsSuperType("Snow"))
                    card.AddCondition(condition);
            }
            base.Resolve();
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Dead of Winter
Mana Cost:2Black
Converted Mana Cost:3
Types:Sorcery
Card Text:
All nonsnow creatures get -X/-X until end of turn, where X is the number of snow permanents you control.";
        }
    }
}

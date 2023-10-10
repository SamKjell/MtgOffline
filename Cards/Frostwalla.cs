using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class Frostwalla : CreatureBase
    {
        public bool wasAbilityActivatedThisTurn = false;
        public Frostwalla()
        {
            manaCost = "2,G";
            name = "Frostwalla";
            superTypes.Add("Snow");
            subTypes.Add("Lizard");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464114&type=card";
            power = 2;
            toughness = 2;
            abilities.Add(new FrostwallaAblity(this));
            
        }
        public override void OnUpkeep(Player p)
        {
            wasAbilityActivatedThisTurn = false;
        }
        public override CardObject SimulationCloneCard(Player controller)
        {
            Frostwalla card = base.SimulationCloneCard(controller) as Frostwalla;
            card.wasAbilityActivatedThisTurn = wasAbilityActivatedThisTurn;
            card.abilities.Clear();
            card.abilities.Add(new FrostwallaAblity(card));
            return card;
        }
        public override string GetTranscript()
        {
            return @"Card Name:Frostwalla
Mana Cost:2Green
Converted Mana Cost:3
Types:Snow Creature — Lizard
Card Text:
Snow: Frostwalla gets +2/+2 until end of turn. Activate this ability only once each turn. (Snow can be paid with one mana from a snow permanent.)
P/T:2 / 2";
        }
    }
    public class FrostwallaAblity : Ability
    {
        public Frostwalla card;
        public FrostwallaAblity(Frostwalla frostwalla)
        {
            cost = "1";
            card = frostwalla;
            Condition condition = new StatModCondition(2, 2, "endstep");
            effect = new ConditionStackObject(new List<CardObject> { card }, condition);
        }

        public override void AdditionalActions()
        {
            card.wasAbilityActivatedThisTurn = true;
        }

        public override bool CanActivate()
        {
            return !card.wasAbilityActivatedThisTurn;
        }
    }
}

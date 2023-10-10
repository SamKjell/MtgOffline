using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class ScaleUp : SorceryBase, OverloadCastingObject.IOverloadable, ITargetable
    {
        public bool isOverloaded = false;

        public ScaleUp()
        {
            manaCost = "G";
            name = "Scale Up";
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464128&type=card";
        }
        
        public List<object> GetTargets()
        {
            List<object> targets = new List<object>();
            List<CardObject> creatures = AcquireTargets();
            if (creatures == null)
                return null;
            targets.AddRange(creatures);
            return targets;
        }

        public List<CardObject> AcquireTargets()
        {
            if (!isOverloaded)
            {
                CardObject target = controller.ChooseTargetControlledCreatureOnBattlefield(-1);
                if(target != null)
                    return new List<CardObject>() { controller.ChooseTargetControlledCreatureOnBattlefield(-1) };
                return null;
            }
            else
            {
                List<CardObject> l = new List<CardObject>();
                l.AddRange(controller.GetControlledCreatures());
                return l;
            }
        }

        public override void Resolve()
        {
            foreach (CardObject card in AcquireTargets())
            {
                CreatureBase newCard = (CreatureBase)card.SimulationCloneCard(card.controller);
                newCard.power = 6;
                newCard.toughness = 4;
                newCard.subTypes = new List<string>() { "Wurm" };
                Condition condition = new CardAlterationCondition(newCard, "endstep", "becomes a base p/t 6/4 green Wurm until end of turn");
                card.AddCondition(condition);
            }
        }

        public override List<AlternateCastingObject> GetAlternativeCastingObjects()
        {
            OverloadCastingObject overload = new OverloadCastingObject("4,G,G", this);
            return new List<AlternateCastingObject>() {overload};
        }

        public override CardObject SimulationCloneCard(Player newController)
        {
            ScaleUp card = (ScaleUp) base.SimulationCloneCard(newController);
            card.isOverloaded = isOverloaded;
            return card;
        }

        public void SetToOverload()
        {
            isOverloaded = true;
            stackDesc = "(Overloaded) ";
        }

        public override string GetTranscript()
        {
            return @"Card Name:
Scale Up
Mana Cost:
Green
Converted Mana Cost:
1
Types:
Sorcery
Card Text:
Until end of turn, target creature you control becomes a green Wurm with base power and toughness 6/4.
Overload 4GreenGreen (You may cast this spell for its overload cost. If you do, change its text by replacing all instances of 'target' with 'each.')";
        }
    }
}

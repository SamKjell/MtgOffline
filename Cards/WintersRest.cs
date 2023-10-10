using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class WintersRest : AuraBase
    {
        public WintersRest()
        {
            manaCost = "1,U";
            name = "Winter's Rest";
            superTypes.Add("Snow");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464027&type=card";
            effects = new WintersRestCondition(this);
            targetPred = x => x.CardContainsType("Creature") && x.controller!=controller;
            negativeEffects = true;
        }
        public override void ETB()
        {
            TapStackObject trigger = new TapStackObject(new List<CardObject> { host });
            controller.TriggerAbility(trigger);
        }
        public override CardObject SimulationCloneCard(Player newController)
        {
            WintersRest rest = base.SimulationCloneCard(newController) as WintersRest;
            rest.effects = new WintersRestCondition(rest);
            return rest;
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Winter's Rest
Mana Cost:1Blue
Converted Mana Cost:2
Types:Snow Enchantment — Aura
Card Text:
Enchant creature
When Winter's Rest enters the battlefield, tap enchanted creature.
As long as you control another snow permanent, enchanted creature doesn't untap during its controller's untap step.";
        }
    }
    public class WintersRestCondition : FrozenCondition 
    {
        public WintersRest cause;
        public WintersRestCondition(WintersRest reference, string conclusionTime="Special") : base("doesn't untap during its controller's upkeep.", false, conclusionTime)
        {
            cause = reference;
        }
        public override bool CannotUpkeepUntap()
        {
            if (Player.CountBySuperType("Snow", cause.controller.battlefield) > (cause.superTypes.Contains("Snow") ? 2 : 1))
            {
                return true;
            }
            return false;
        }
    }
}

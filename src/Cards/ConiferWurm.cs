using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class ConiferWurm : CreatureBase
    {
        public ConiferWurm()
        {
            manaCost = "4,G";
            name = "Conifer Wurm";
            superTypes.Add("Snow");
            subTypes.Add("Wurm");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464108&type=card";
            power = 4;
            toughness = 4;
            staticAbilities.Add("Trample");
            abilities.Add(new ConiferWurmAbility(this));
        }
        public override CardObject SimulationCloneCard(Player controller)
        {
            ConiferWurm wurm = base.SimulationCloneCard(controller) as ConiferWurm;
            wurm.abilities.Clear();
            wurm.abilities.Add(new ConiferWurmAbility(wurm));
            return wurm;
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Conifer Wurm
Mana Cost:4Green
Converted Mana Cost:5
Types:Snow Creature — Wurm
Card Text:
Trample
3Green: Conifer Wurm gets +X/+X until end of turn, where X is the number of snow permanents you control.
P/T:4 / 4";
        }
    }

    public class ConiferWurmAbility : Ability
    {
        public ConiferWurmAbility(ConiferWurm card)
        {
            cost = "3,G";
            Condition condition = new StatModCondition(0, 0, "endstep");
            effect = new ConditionStackObject(new List<CardObject>() { card }, condition);
        }
        public override void AdditionalActions()
        {
            int mod = Player.CountBySuperType("Snow", source.controller.battlefield);
            Condition condition = new StatModCondition(mod, mod, "endstep");
            effect = new ConditionStackObject(effect.targetCards, condition);
        }
    }
}

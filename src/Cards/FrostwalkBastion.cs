using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class FrostwalkBastion : LandBase
    {
        public FrostwalkBastion()
        {
            name = "Frostwalk Bastion";
            superTypes.Add("Snow");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464189&type=card";
            abilities.Add(ManaAbility.WastesManaAbility);
            abilities.Add(new FrostwalkBastionAbility(this));
        }
        public override CardObject SimulationCloneCard(Player c)
        {
            FrostwalkBastion card = base.SimulationCloneCard(c) as FrostwalkBastion;
            card.abilities.Clear();
            card.abilities.Add(ManaAbility.WastesManaAbility);
            card.abilities.Add(new FrostwalkBastionAbility(card));
            return card;
        }

        public override void OnDealCombatDamageToCreature(List<CardObject> victims)
        {
            FrostwalkBastionTriggerStackObject stackObj = new FrostwalkBastionTriggerStackObject(victims);
            controller.TriggerAbility(stackObj);
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Frostwalk Bastion
Types:Snow Land
Card Text:
Tap: Add Colorless.
1Snow: Until end of turn, Frostwalk Bastion becomes a 2/3 Construct artifact creature. It's still a land. (Snow can be paid with one mana from a snow permanent.)
Whenever Frostwalk Bastion deals combat damage to a creature, tap that creature and it doesn't untap during its controller's next untap step.";
        }
    }

    public class FrostwalkBastionTriggerStackObject : ConditionStackObject
    {
        public FrostwalkBastionTriggerStackObject(List<CardObject> targets) : base(targets, new FrozenCondition("controller_upkeep",true,""))
        {
            description = "is being tapped and doesn't during their controller's next untap step.";
        }
        public override void Resolve()
        {
            foreach (CardObject target in targetCards)
            {
                target.Tap();
            }
            base.Resolve();
        }
    }

    public class FrostwalkBastionAbility : Ability
    {
        public FrostwalkBastion card;
        public FrostwalkBastionAbility(FrostwalkBastion bastion)
        {
            cost = "2";
            card = bastion;
            Condition condition = new CardAlterationCondition(new FrostwalkBastionConstruct(bastion),"endstep","becomes a...");
            effect = new ConditionStackObject(new List<CardObject> { card }, condition);
        }
    }

    public class FrostwalkBastionConstruct : CreatureBase
    {
        public FrostwalkBastionConstruct()
        {
            name = "Frostwalk Bastion";
            power = 2;
            toughness = 3;
            superTypes.Add("Snow");
            cardTypes.Insert(0, "Artifact");
            cardTypes.Add("Land");
            subTypes.Add("Construct");
        }
        public FrostwalkBastionConstruct(FrostwalkBastion card)
        {
            name = "Frostwalk Bastion";
            power = 2;
            toughness = 3;
            superTypes.Add("Snow");
            cardTypes.Insert(0, "Artifact");
            cardTypes.Add("Land");
            subTypes.Add("Construct");
            conditions = card.conditions;
            isTapped = card.isTapped;
        }
        public override void OnDealCombatDamageToCreature(List<CardObject> victims)
        {
            FrostwalkBastionTriggerStackObject stackObj = new FrostwalkBastionTriggerStackObject(victims);
            controller.TriggerAbility(stackObj);
        }
    }
}

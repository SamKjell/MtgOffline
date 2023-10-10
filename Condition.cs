using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class Condition
    {
        public String conclusionTime;
        public String description;
        public bool cannotUpkeepUntap = false;
        public int powerMod = 0;
        public int toughnessMod = 0;
        public string staticAbility;
        public CardObject associatedCard;

        public Condition(String desc, String conclusionTime)
        {
            description = desc;
            this.conclusionTime = conclusionTime;
        }

        public virtual void ApplicationEffects()
        {

        }

        public virtual void RemovalEffects()
        {

        }

        public bool HasConcluded(String stage, CardObject target)
        {
            if (stage == conclusionTime)
                return true;
            return false;
        }

        public virtual bool CannotUpkeepUntap()
        {
            return cannotUpkeepUntap;
        }

        public virtual int GetPowerMod()
        {
            return powerMod;
        }
        public virtual int GetToughnessMod()
        {
            return toughnessMod;
        }
        public virtual string GetGrantedStaticAbility()
        {
            return staticAbility;
        }
        public virtual void OnHostAttack()
        {

        }

        public virtual Condition Clone()
        {
            return new Condition(description,conclusionTime);
        }
    }

    public class FrozenCondition : Condition
    {
        bool tapsTarget;
        public FrozenCondition(string conclusionTime, bool tapsTarget, string desc = "doesn't untap during its controller's next untap step.") : base(desc, conclusionTime)
        {
            cannotUpkeepUntap = true;
            this.tapsTarget = tapsTarget;
        }
        public override void ApplicationEffects()
        {
            if (tapsTarget)
                associatedCard.Tap();
        }
    }
    public class StatModCondition : Condition
    {
        public StatModCondition(int powerMod, int toughnessMod, string conclusionTime) : base("", conclusionTime)
        {
            String p = ((powerMod >= 0) ? "+" : "-") + powerMod;
            String t = ((toughnessMod >= 0) ? "+" : "-") + toughnessMod;
            description = "has " + p + "/" + t+".";
            this.powerMod = powerMod;
            this.toughnessMod = toughnessMod;
        }

        public override Condition Clone()
        {
            return new StatModCondition(powerMod, toughnessMod, conclusionTime);
        }
    }
    public class CardAlterationCondition : Condition
    {
        public CardObject newCard;
        public CardObject originalCard;
        public CardAlterationCondition(CardObject newCard, string conclusionTime, string desc) : base(desc, conclusionTime)
        {
            this.newCard = newCard;
        }
        public override void ApplicationEffects()
        {
            originalCard = associatedCard;
            newCard.controller = associatedCard.controller;
            newCard.conditions = associatedCard.conditions;
            associatedCard.controller.battlefield[associatedCard.controller.battlefield.IndexOf(associatedCard)] = newCard;
            associatedCard = newCard;
            Program.windowInstance.UpdateGameConsole();
        }
        public override void RemovalEffects()
        {
            originalCard.isTapped = associatedCard.isTapped;
            associatedCard.controller.battlefield[associatedCard.controller.battlefield.IndexOf(associatedCard)] = originalCard;
            Program.windowInstance.UpdateGameConsole();
        }
    }
    public class FlickerCondition : Condition
    {
        public FlickerCondition(string conclusionTime,string desc) : base(desc, conclusionTime)
        {
        }
        public override void ApplicationEffects()
        {
            associatedCard.controller.battlefield.Remove(associatedCard);
            associatedCard.owner.exile.Add(associatedCard);
            Program.windowInstance.UpdateGameConsole();
        }
        public override void RemovalEffects()
        {
            CardObject card = (CardObject)Activator.CreateInstance(associatedCard.GetType());
            card.controller = associatedCard.controller;
            card.owner = associatedCard.owner;
            associatedCard.owner.EnterTheBattlefield(card);
        }
    }
    public class MonstrousCondition : Condition
    {
        public MonstrousCondition() : base("is monstrous", "Special")
        {
        }
    }
    public class CounterCondition : Condition
    {
        public int numberOfCounters = 0;
        public string counterName;
        public CounterCondition(string counterName,int numberOfCounters = 1) : base("has "+numberOfCounters+" "+counterName+" counter(s)", "Special")
        {
            this.numberOfCounters = numberOfCounters;
            this.counterName = counterName;
        }

        public void UpdateDescription()
        {
            description = "has " + numberOfCounters + " " + counterName + " counter(s)";
        }
    }
    public class PlusOnePlusOneCounterCondition : CounterCondition
    {
        public PlusOnePlusOneCounterCondition(int numberOfCounters = 1) : base("+1/+1", numberOfCounters)
        {

        }
        public override int GetPowerMod()
        {
            return numberOfCounters;
        }
        public override int GetToughnessMod()
        {
            return numberOfCounters;
        }
    }

    public class TimeCounterCondition : CounterCondition
    {
        public TimeCounterCondition(int numberOfCounters = 1) : base("time", numberOfCounters) { }
    }

    public class AbilityCondition : Condition
    {
        public Ability ability;
        public AbilityCondition(Ability addedAbility,string desc):base($"has '{desc}'","Special")
        {
            ability = addedAbility;
        }
    }

    public class StaticAbilityCondition : Condition
    {
        public StaticAbilityCondition(string staticAbility):base($"has '{staticAbility}'","Special")
        {
            this.staticAbility = staticAbility;
        }
        public override Condition Clone()
        {
            return new StaticAbilityCondition(staticAbility);
        }
    }


    public class StaticConditions
    {
        public static MonstrousCondition monstrous = new MonstrousCondition();
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class Ability
    {

        public String cost; //Annotated via mtg colors and symbols. EX: 2,T = 2 of any colored mana, and tap this permanent.
        public StackObject effect;
        public CardObject source;

        public virtual void Activate() //Usually this should create a StackObject
        {
            AdditionalActions();
            Program.windowInstance.game.AddToStack(effect);
        }
        public virtual void AdditionalActions()//Used to prevent the simulation from adding to the stack while still being able to activate state effects.
        {

        }

        public virtual bool CanActivate()
        {
            return true;
        }

        public virtual bool AdditionalCosts()
        {
            return true;
        }

        public virtual void SimSetController(Player player) //Used to prevent infinite loop in the sim code.
        {

        }
    }
    public class ManaAbility : Ability
    {
        public static ManaAbility IslandManaAbility = new ManaAbility("T","U");
        public static ManaAbility ForestManaAbility = new ManaAbility("T","G");
        public static ManaAbility WastesManaAbility = new ManaAbility("T", "C");
        public static ManaAbility SwampManaAbility = new ManaAbility("T", "B");
        public static ManaAbility PlainsManaAbility = new ManaAbility("T", "W");
        public static ManaAbility MountainManaAbility = new ManaAbility("T", "R");

        public String manaProduced;
        public ManaAbility(string cost,String manaProduced)
        {
            this.manaProduced = manaProduced;
            this.cost = cost;
        }
        public override void Activate()
        {
            //Create mana
            String[] mana = manaProduced.Split(',');
            foreach (String s in mana)
            {
                source.controller.manaPool.Add(s);
            }
            

        }
        public int GetCMC()
        {
            int count = 0;
            String[] temp = manaProduced.Split(',');
            foreach (String s in temp)
            {
                if (int.TryParse(s, out int v))
                {
                    count += v;
                }
                else
                    count++;
            }
            return count;
        }
    }

    public class MonstrousAbility : Ability
    {
        CardObject target;
        public MonstrousAbility(CardObject target,int counterQuantity,string cost)
        {
            this.cost = cost;
            this.target = target;
            effect = new MonstrousStackObject(new List<CardObject>() { target }, counterQuantity);
        }
        public override bool CanActivate()
        {
            return !target.IsMonsterous();
        }
    }
}

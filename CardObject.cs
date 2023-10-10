using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class CardObject
    {
        //Static Card Information
        public String name;
        public List<String> superTypes = new List<string>();
        public List<String> cardTypes = new List<string>();
        public List<String> subTypes = new List<string>();
        public String imageURL;
        public Player owner;
        public bool isPermanent;
        public List<Ability> abilities = new List<Ability>();
        public String manaCost = "0";
        public List<String> staticAbilities = new List<String>();
        public List<Condition> conditions = new List<Condition>();
        public List<EnchantmentBase> auras = new List<EnchantmentBase>();
        public int id;
        static int idCounter = 0;

        //Variable Information;
        public Player controller;
        public Bitmap tempImage;
        public bool isTapped = false;
        public bool hasSummonSickness = false;
        public bool isAttatched = false;

        public CardObject()
        {
            id = idCounter++;
        }

        public virtual void Untap()
        {
            isTapped = false;
        }
        public virtual void Tap()
        {
            isTapped = true;
        }
        public virtual bool CanUntap()
        {
            return true;
        }
        public virtual bool CanTap()
        {
            return !isTapped;
        }
        public virtual string GetTranscript()
        {
            return "This card has not been given a transcript.";
        }
        public virtual bool CanUpkeepUntap()
        {
            foreach (Condition c in conditions)
            {
                if (c.CannotUpkeepUntap())
                    return false;
            }
            return true;
        }

        public virtual List<AlternateCastingObject> GetAlternativeCastingObjects()
        {
            return null;
        }

        public virtual bool CardContainsSuperType(String type)
        {
            return superTypes.Contains(type);
        }

        public virtual bool CardContainsType(string type)
        {
            return cardTypes.Contains(type);
        }

        public virtual bool CardContainsSubType(string type)
        {
            return subTypes.Contains(type);
        }

        public virtual void AddCondition(Condition condition)
        {
            condition.associatedCard = this;
            condition.ApplicationEffects();
            conditions.Add(condition);
        }
        public virtual void RemoveCondition(Condition condition)
        {
            condition.RemovalEffects();
            conditions.Remove(condition);
        }

        public virtual void EquipAura(AuraBase aura)
        {
            aura.host = this;
            auras.Add(aura);
            AddCondition(aura.effects);
        }

        public virtual bool IsMonsterous()
        {
            if (conditions.Contains(StaticConditions.monstrous))
                return true;
            return false;
        }

        public virtual void AddCounter(CounterCondition counter)
        { 
            foreach (Condition c in conditions)
            {
                if(c is CounterCondition)
                {
                    CounterCondition con = c as CounterCondition;
                    if (con.counterName == counter.counterName)
                    {
                        (c as CounterCondition).numberOfCounters += counter.numberOfCounters;
                        (c as CounterCondition).UpdateDescription();
                        return;
                    }
                }
            }
            AddCondition(counter);
        }

        public virtual void RemoveCounter(string counterName, int quantity = 1)
        {
            Condition garbage = null;
            foreach (Condition c in conditions)
            {
                if(c is CounterCondition && (c as CounterCondition).counterName==counterName)
                {
                    (c as CounterCondition).numberOfCounters -= quantity;
                    (c as CounterCondition).UpdateDescription();
                    if ((c as CounterCondition).numberOfCounters <= 0)
                        garbage = c;
                    break;
                }
            }
            if (garbage != null)
            {
                RemoveCondition(garbage);
            }
        }

        public virtual int QuantityOfCounter(string counterName)
        {
            foreach (Condition c in conditions)
            {
                if (c is CounterCondition && (c as CounterCondition).counterName == counterName)
                {
                    return (c as CounterCondition).numberOfCounters;
                }
            }
            return 0;
        }

        public virtual void OnDealCombatDamageToCreature(List<CardObject> victims)
        {

        }

        public virtual void Destroy()
        {
            //Trigger gamewide creature death effects
            if (staticAbilities.Contains("Indestructible"))
                return;
            controller.battlefield.Remove(this);
            if (!cardTypes.Contains("Token"))
            {
                owner.graveyard.Add(this);
            }
            foreach (AuraBase aura in new List<EnchantmentBase>(auras))
            {
                aura.Destroy();
                aura.controller.battlefield.Remove(aura);
            }
            Program.windowInstance.UpdateGameConsole();
        }

        public virtual void Exile()
        {
            //Trigger gamewide creature exile effects
            controller.battlefield.Remove(this);
            if (!cardTypes.Contains("Token"))
            {
                owner.exile.Add(this);
            }
            foreach (AuraBase aura in new List<EnchantmentBase>(auras))
            {
                aura.Destroy();
                aura.controller.battlefield.Remove(aura);
            }
            Program.windowInstance.UpdateGameConsole();
        }

        public virtual void Sacrifice()
        {
            //Trigger gamewide sacrifice effects
            if (staticAbilities.Contains("Indestructible"))
                staticAbilities.Remove("Indestructible");
            Destroy();
        }

        public virtual void ETB()
        {
            //Trigger enter the battlefield effects
        }

        public virtual void CardEntersTheBattlefield(CardObject c)
        {
            //Trigger effects
        }

        public virtual void CardCast(CardObject c)
        {
            //Trigger effects
        }

        public virtual void ReturnToOwnerHand()
        {
            //Trigger return to hand effects
            if (owner == Program.windowInstance.GetLocalPlayer())
                Program.windowInstance.game.NotifyPlayer("Return to Hand", $"Your {name} has been returned to your hand");
            else if (controller == Program.windowInstance.GetLocalPlayer())
                Program.windowInstance.game.NotifyPlayer("Return to Hand", $"Your {name} has been returned to its owner's hand");
            CardObject card = (CardObject)Activator.CreateInstance(this.GetType());
            card.owner = owner;
            card.controller = owner;
            owner.hand.Add(card);
            controller.battlefield.Remove(this);
            Program.windowInstance.UpdateGameConsole();
        }

        public virtual void ReturnToOwnerLibrary(int position,string cardLocation)
        {
            string loc = (position >= 0)?$"{position} card(s) from the top.":$"{-position} card(s) from the bottom.";
            if (owner == Program.windowInstance.GetLocalPlayer())
                Program.windowInstance.game.NotifyPlayer("Return to Library", $"Your {name} is being returned to your library {loc}");
            else if (Program.windowInstance != null && controller == Program.windowInstance.GetLocalPlayer())
                Program.windowInstance.game.NotifyPlayer("Return to Library", $"Your {name} is being returned to its owner's library {loc}");

            CardObject card = (CardObject)Activator.CreateInstance(this.GetType());
            card.owner = owner;
            card.controller = owner;
            if (owner.library.Count == 0)
                owner.library.Add(card);
            else if (position >= 0)
                owner.library.Insert((position >= owner.library.Count ? owner.library.Count - 1 : position), card);
            else
            {
                int index = owner.library.Count + position;
                owner.library.Insert((index < 0 ? 0 : index), card);
            }
            if (cardLocation == "battlefield")
                controller.battlefield.Remove(this);
            else if (cardLocation == "hand")
                owner.hand.Remove(this);
            else if (cardLocation == "graveyard")
                owner.graveyard.Remove(this);
            else if (cardLocation == "exile")
                owner.exile.Remove(this);
            Program.windowInstance.UpdateGameConsole();
        }

        public virtual void MulliganReturnToLibrary()
        {
            owner.library.Insert(0, this);
            owner.hand.Remove(this);
        }

        public virtual void ReturnToOwnerLibrary(int position)
        {
            ReturnToOwnerLibrary(position, "battlefield");
        }

        public virtual CardObject SimulationCloneCard(Player newController)
        {
            CardObject newCard = (CardObject)Activator.CreateInstance(this.GetType());
            newCard.controller = newController;
            newCard.superTypes = Simulation.CloneList(superTypes);
            newCard.subTypes = Simulation.CloneList(subTypes);
            newCard.cardTypes = Simulation.CloneList(cardTypes);
            newCard.name = name;
            newCard.manaCost = manaCost;
            newCard.isTapped = isTapped;
            newCard.isAttatched = isAttatched;
            newCard.abilities = Simulation.CloneList(abilities);
            newCard.staticAbilities = Simulation.CloneList(staticAbilities);
            newCard.conditions = Simulation.CloneList(conditions);
            foreach (AuraBase a in auras)
            {
                newCard.auras.Add(a.SimulationCloneCard(newController) as AuraBase);
                a.SimulationSetHost(newCard);
            }
            newCard.hasSummonSickness = hasSummonSickness;
            newCard.id = id;
            foreach (Ability a in newCard.abilities)
            {
                a.source = newCard;
            }
            return newCard;
        }

        public virtual void OnUpkeep(Player p)
        {
            //Trigger effects
        }



        public virtual List<Ability> GetActivatedAbilities()
        {
            List<Ability> a = abilities;
            foreach (Condition con in conditions)
            {
                if (con is AbilityCondition)
                    a.Add((con as AbilityCondition).ability);
            }
            return a;
        }

        //Testing for Static Abilities:
        public virtual List<String> GetStaticAbilities()
        {
            List<string> staticAbilities = new List<string>(this.staticAbilities);
            foreach (Condition con in conditions)
            {
                string c = con.GetGrantedStaticAbility();
                if(c != null && c != "")
                    staticAbilities.Add(c);
            }
            return staticAbilities;
        }

        public virtual bool HasDeathtouch()
        {
            if (GetStaticAbilities().Contains("Deathtouch"))
                return true;
            return false;
        }
        public virtual bool HasVigilance()
        {
            if (GetStaticAbilities().Contains("Vigilance"))
                return true;
            return false;
        }

        public virtual bool HasHaste()
        {
            if (GetStaticAbilities().Contains("Haste"))
                return true;
            return false;
        }

        public virtual bool HasTrample()
        {
            if (GetStaticAbilities().Contains("Trample"))
                return true;
            return false;
        }

        public virtual bool HasFlying()
        {
            if (GetStaticAbilities().Contains("Flying"))
                return true;
            return false;
        }

        public virtual bool HasIndestructible()
        {
            if (GetStaticAbilities().Contains("Indestructible"))
                return true;
            return false;
        }

        public virtual bool HasLifelink()
        {
            if (GetStaticAbilities().Contains("Lifelink"))
                return true;
            return false;
        }
        
        public virtual bool HasDefender()
        {
            if (GetStaticAbilities().Contains("Defender"))
                return true;
            return false;
        }

        public virtual bool HasFirstStrike()
        {
            if (GetStaticAbilities().Contains("First Strike"))
                return true;
            return false;
        }

        public virtual bool HasDoubleStrike()
        {
            if (GetStaticAbilities().Contains("Double Strike"))
                return true;
            return false;
        }

        public int GetCombatInitiative()
        {
            CombatEventArgs args = new CombatEventArgs();
            args.card = this;

            if (HasDoubleStrike())
                args.modifiedCombatInit = 2;
            else if (HasFirstStrike())
                args.modifiedCombatInit = 1;

            Mod_Framework.EventBus.PostEvent("CombatInit", this, args);
            return args.modifiedCombatInit;
        }

        public class CombatEventArgs : EventArgs
        {
            public CardObject card;
            public int modifiedCombatInit = 0;
        }

        public int CompareTo(CardObject other)
        {
            int score1 = 0;
            int score2 = 0;

            if (cardTypes.Contains("Creature"))
                score1 = 40;
            else if (cardTypes.Contains("Land"))
                score1 = 0;
            else if (cardTypes.Contains("Artifact"))
                score1 = 10;
            else if (cardTypes.Contains("Enchantment"))
                score1 = 20;
            else if (cardTypes.Contains("Planeswalker"))
                score1 = 30;

            if (other.cardTypes.Contains("Creature"))
                score2 = 40;
            else if (other.cardTypes.Contains("Land"))
                score2 = 0;
            else if (other.cardTypes.Contains("Artifact"))
                score2 = 10;
            else if (other.cardTypes.Contains("Enchantment"))
                score2 = 20;
            else if (other.cardTypes.Contains("Planeswalker"))
                score2 = 30;

            if (superTypes.Contains("Legendary"))
                score1++;
            if (other.superTypes.Contains("Legendary"))
                score2++;
            if (name.CompareTo(other.name) > 0)
                score1++;
            else if (name.CompareTo(other.name) < 0)
                score2++;

            if (score1 > score2)
                return 1;
            else if (score1 == score2)
                return 0;
            else
                return -1;
        }
    }
    public class LandBase : CardObject
    {
        public LandBase()
        {
            cardTypes.Add("Land");
            isPermanent = true;
        }
    }
    public class CreatureBase : CardObject
    {
        public int power, toughness = 0;
        public int currentHealth;
        public CreatureBase()
        {
            cardTypes.Add("Creature");
            isPermanent = true;
        }
        public virtual bool HasSummonSickness()
        {
            if (staticAbilities.Contains("Haste"))
                return false;
            return true;
        }
        public virtual int GetPower()
        {
            int p = power;
            foreach (Condition c in conditions)
            {
                p += c.GetPowerMod();
            }
            return p;
        }
        public virtual int GetToughness()
        {
            int t = toughness;
            foreach (Condition c in conditions)
            {
                t += c.GetToughnessMod();
            }
            return t;
        }
        public virtual bool CanAttack()
        {
            return (CanTap()&&GetPower()>0&&!HasDefender());
        }
        public virtual bool CanBlock()
        {
            return !isTapped;
        }
        public virtual bool CanBeBlocked()
        {
            return true;
        }

        public virtual void Attack()
        {
            if (!HasVigilance())
                Tap();
            foreach (Condition con in conditions)
                con.OnHostAttack();
        }

        public virtual int Fight(CreatureBase creature, bool considerDamageTiming)
        {
            int extra = 0;
            if (considerDamageTiming)
            {
                int attacker = GetCombatInitiative();
                int aBonus = attacker - 2;
                int victim = creature.GetCombatInitiative();
                int vBonus = victim - 2;
                //Triple strike and upward...
                for (int i = (aBonus >= vBonus ? aBonus : vBonus); i > 0; i--)
                {
                    if (i <= aBonus)
                        extra = creature.Damage(GetPower(), HasDeathtouch(), HasLifelink(), controller);
                    if (i <= vBonus)
                        Damage(creature.GetPower(), creature.HasDeathtouch(), creature.HasLifelink(), creature.controller);

                    if (creature.ShouldDie() && !ShouldDie())
                    {
                        int v = i * GetPower();
                        extra += v;
                        if (HasLifelink())
                            controller.Heal(v);
                        break;
                    }
                    else if (!creature.ShouldDie() && ShouldDie())
                    {
                        int v = i * creature.GetPower();
                        if (creature.HasLifelink())
                            creature.controller.Heal(v);
                        break;
                    }

                }
                bool aFlag = ShouldDie();
                bool vFlag = creature.ShouldDie();

                //First Strike
                if(attacker > 0 && !aFlag)
                    extra+= creature.Damage(GetPower(), HasDeathtouch(), HasLifelink(), controller);
                if(victim > 0 && !vFlag)
                    Damage(creature.GetPower(), creature.HasDeathtouch(), creature.HasLifelink(), creature.controller);

                aFlag = ShouldDie();
                vFlag = creature.ShouldDie();

                //Normal Damage (and Double Strike if applicable)
                if(attacker>=2 || attacker==0 && !aFlag)
                    extra+= creature.Damage(GetPower(), HasDeathtouch(), HasLifelink(), controller);
                if(victim >= 2 || victim == 0 && !vFlag)
                    Damage(creature.GetPower(), creature.HasDeathtouch(), creature.HasLifelink(), creature.controller);
            }
            else
            {
                extra = creature.Damage(GetPower(), HasDeathtouch(), HasLifelink(), controller);
                Damage(creature.GetPower(), creature.HasDeathtouch(), creature.HasLifelink(), creature.controller);
            }
            return extra;
        }

        public virtual int Fight(CreatureBase creature) { return Fight(creature, false); }

        public virtual int Fight(CreatureBase creature, out bool thisDied, out bool otherDied) {
            return Fight(creature, false, out thisDied, out otherDied);
        }

        public virtual int Fight(CreatureBase creature, bool considerCI, out bool thisDied, out bool otherDied)
        {
            thisDied = false;
            otherDied = false;
            int extra = 0;
            if (considerCI)
            {
                int attacker = GetCombatInitiative();
                int aBonus = attacker - 2;
                int victim = creature.GetCombatInitiative();
                int vBonus = victim - 2;
                //Triple strike and upward...
                for (int i = (aBonus >= vBonus ? aBonus : vBonus); i > 0; i--)
                {
                    if (i <= aBonus)
                        extra = creature.Damage(GetPower(), out otherDied, HasDeathtouch(), HasLifelink(), controller);
                    if (i <= vBonus)
                        Damage(creature.GetPower(), out thisDied, creature.HasDeathtouch(), creature.HasLifelink(), creature.controller);

                    if (creature.ShouldDie() && !ShouldDie())
                    {
                        int v = i * GetPower();
                        extra += v;
                        if (HasLifelink())
                            controller.Heal(v);
                        break;
                    }
                    else if (!creature.ShouldDie() && ShouldDie())
                    {
                        int v = i * creature.GetPower();
                        if (creature.HasLifelink())
                            creature.controller.Heal(v);
                        break;
                    }

                }
                bool aFlag = ShouldDie();
                bool vFlag = creature.ShouldDie();

                //First Strike
                if (attacker > 0 && !aFlag)
                    extra += creature.Damage(GetPower(), out otherDied, HasDeathtouch(), HasLifelink(), controller);
                if (victim > 0 && !vFlag)
                    Damage(creature.GetPower(), out thisDied, creature.HasDeathtouch(), creature.HasLifelink(), creature.controller);

                aFlag = ShouldDie();
                vFlag = creature.ShouldDie();

                //Normal Damage (and Double Strike if applicable)
                if (attacker >= 2 || attacker == 0 && !aFlag)
                    extra += creature.Damage(GetPower(), out otherDied, HasDeathtouch(), HasLifelink(), controller);
                if (victim >= 2 || victim == 0 && !vFlag)
                    Damage(creature.GetPower(), out thisDied, creature.HasDeathtouch(), creature.HasLifelink(), creature.controller);
            }
            else
            {
                extra = creature.Damage(GetPower(), out otherDied, HasDeathtouch(), HasLifelink(), controller);
                Damage(creature.GetPower(), out thisDied, creature.HasDeathtouch(), creature.HasLifelink(), creature.controller);
            }
            return extra;
        }

        public virtual int ServerFight(CreatureBase creature)
        {
            int extra = creature.ServerDamage(GetPower(), HasDeathtouch());
            Damage(creature.GetPower(), creature.HasDeathtouch());
            return extra;
        }

        public override bool CanTap()
        {
            return ((!hasSummonSickness || HasHaste()) && !isTapped);
        }

        public virtual void Heal()
        {
            currentHealth = GetToughness();
        }

        public virtual bool ShouldDie()
        {
            if (currentHealth <= 0)
            {
                if (GetToughness() > 0 && HasIndestructible())
                    return false;
                else return true;
            }
            return false;
        }

        public virtual int Damage(int amount,bool deathtouch = false, bool lifelink = false, Player source = null)
        {
            if (lifelink && source != null)
                source.Heal(amount);

            currentHealth -= amount;
            if (ShouldDie()||(deathtouch && amount > 0 && !HasIndestructible()))
                Destroy();
            return deathtouch?amount-1:(currentHealth<0)?(-currentHealth):0;
        }

        public virtual int Damage(int amount, out bool resultedInDeath,bool deathtouch = false, bool lifelink = false, Player source = null)
        {
            if (lifelink && source != null)
                source.Heal(amount);

            resultedInDeath = false;
            currentHealth -= amount;
            if (ShouldDie() || (deathtouch && amount > 0 && !HasIndestructible()))
            {
                Destroy();
                resultedInDeath = true;
            }
            return deathtouch ? amount - 1 : (currentHealth < 0) ? (-currentHealth) : 0;
        }

        public virtual int ServerDamage(int amount,bool deathtouch = false)
        {
            int h = currentHealth;
            h -= amount;
            Networking.ClientSend.Trigger($"cmd damage null {id} {amount} {deathtouch}");
            return deathtouch ? amount - 1 : (h < 0) ? (-h) : 0;
        }

        public override CardObject SimulationCloneCard(Player controller)
        {
            CreatureBase newCard = base.SimulationCloneCard(controller) as CreatureBase;
            newCard.power = power;
            newCard.toughness = toughness;
            newCard.currentHealth = currentHealth;
            return newCard;
        }
    }
    public class ArtifactBase: CardObject
    {
        public ArtifactBase()
        {
            cardTypes.Add("Artifact");
            isPermanent = true;
        }
    }

    public class EnchantmentBase: CardObject
    {
        public EnchantmentBase()
        {
            cardTypes.Add("Enchantment");
            isPermanent = true;
        }
    }
    public class AuraBase : EnchantmentBase
    {
        public Condition effects;
        public CardObject host;
        public Predicate<CardObject> targetPred;
        public Player.AssessmentModel targetingSystem = new Player.AssessmentModel(Player.AssessCreatureThreat);
        public bool negativeEffects = true;
        public AuraBase()
        {
            subTypes.Add("Aura");
        }
        public override void Destroy()
        {
            RemoveFromPermanent();
            base.Destroy();
        }
        public virtual object GetTarget()
        {
            return controller.ChooseTargetOnBattlefield(targetPred, targetingSystem);
            //Depreciated
            //if (hostType == EnchantTypes.CREATURE)
            //    return controller.ChooseTargetCreatureOnBattlefield(negativeEffects);
            //else if (hostType == EnchantTypes.LAND)
            //    return controller.ChooseTargetLandOnBattlefield(negativeEffects);
            //return null;
        }
        protected void SetToRandom()
        {
            targetingSystem = new Player.AssessmentModel(Player.AssessRandomly);
        }
        void RemoveFromPermanent()
        {
            if (host != null) //Presumably the host is dead, so there is no point in removing the condition from it.
            {
                host.RemoveCondition(effects);
                host.auras.Remove(this);
            }
        }
        //public override CardObject SimulationCloneCard(Player newController)
        //{
        //    AuraBase card = base.SimulationCloneCard(newController) as AuraBase;
            
        //}
        public void SimulationSetHost(CardObject newCard)
        {
            host = newCard;
        }

        //Depreciated
        //public enum EnchantTypes
        //{
        //    CREATURE,
        //    LAND
        //}
    }
    public class SpellBase : CardObject
    {
        public String stackDesc;
        public SpellBase()
        {
            isPermanent = false;
        }
        public virtual void Resolve()
        {
            controller.graveyard.Add(this);
        }
        public virtual void OnCast()
        {

        }
    }
    public class SorceryBase : SpellBase
    {
        public SorceryBase()
        {
            cardTypes.Add("Sorcery");
        }
    }
    public class InstantBase : SpellBase
    {
        public InstantBase()
        {
            cardTypes.Add("Instant");
        }
    }

    public interface ICostReducer
    {
        string GetReducedCost(CardObject card, string cost);
    }

    public interface ITargetable
    {
        List<object> GetTargets();
    }

    public class AlternateLocationObject
    {
        public CardObject card;
        public List<CardObject> location;

        public AlternateLocationObject(CardObject c, List<CardObject> sourceLocation)
        {
            card = c;
            location = sourceLocation;
        }
    }

    public class AlternateCastingObject
    {
        public CardObject source;
        public string cost;

        public AlternateCastingObject(string cost, CardObject source)
        {
            this.cost = cost;
            this.source = source;
        }

        public virtual CardObject GetAlternateCardForPlay()
        {
            return source;
        }
    }

    public class OverloadCastingObject : AlternateCastingObject
    {
        public OverloadCastingObject(string cost, IOverloadable card) : base(cost, card as CardObject) { }

        public override CardObject GetAlternateCardForPlay()
        {
            (source as IOverloadable).SetToOverload();
            return source;
        }

        public interface IOverloadable
        {
            void SetToOverload();
        }
    }
}

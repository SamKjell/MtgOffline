using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class StackObject
    {
        public String name;
        public Player owner;
        public Player controller;
        public String description;
        public bool splitSecond;
        public CardObject source;

        //Generic Details and Targeting Information
        public List<Player> targetPlayers;
        public List<CardObject> targetCards;
        public List<StackObject> targetStackObjects;

        //Server stuff
        public int id;
        private int idCounter = 0;

        public StackObject()
        {
            id = idCounter++;
        }
        public void Targeting(Game g)
        {

        }
        public virtual void Resolve()
        {

        }

        public virtual string GetCMDSyntax()
        {
            return null;
        }
    }

    public class CardStackObject : StackObject
    {
        public CardObject card;
        public override void Resolve()
        {
            controller.EnterTheBattlefield(card);
        }
        public CardStackObject(CardObject card)
        {
            this.card = card;
            name = card.name;
            controller = card.controller;
            owner = card.owner;
        }
    }

    public class SpawnTokenStackObject : StackObject
    {
        public CardObject token;
        public int quantity;
        public SpawnTokenStackObject(CardObject card, int amount =1)
        {
            token = card;
            quantity = amount;
            if (quantity > 1)
                name = $"Creating {quantity} {token.name} tokens.";
            else
                name = $"Creating a {token.name} token.";
            controller = card.controller;
            owner = card.owner;
        }

        public override void Resolve()
        {
            for (int i = 0; i < quantity; i++)
                controller.InstantiateToken(token);
        }
    }

    public class CustomTokenStackObject : SpawnTokenStackObject
    {
        public CustomTokenStackObject(CardObject card): base(card, 1){}

        public override void Resolve()
        {
            controller.EnterTheBattlefield(token);
        }
    }

    public class ImageStackObject : StackObject
    {
        public string link;
        public ImageStackObject(string link)
        {
            this.link = link;
        }
    }

    public class AuraStackObject : CardStackObject
    {
        public object target;
        public AuraStackObject(AuraBase card, object target) : base(card)
        {
            this.target = target;
            if (target is CardObject)
                name = card.name + " enchanting " + (target as CardObject).name;
        }

        public override void Resolve()
        {
            if (target == null)
            {
                card.controller.graveyard.Add(card);
                return;
            }
            card.isAttatched = true;
            if(target is CardObject)
            {
                CardObject targetCard = target as CardObject;
                targetCard.EquipAura(card as AuraBase);
                controller.EnterTheBattlefield(card);
            }
            
        }
    }

    public class SpellStackObject : StackObject
    {
        public SpellBase card;
        public SpellStackObject(SpellBase card)
        {
            this.card = card;
            name = card.stackDesc + card.name;
            controller = card.controller;
            owner = card.owner;
        }
        public override void Resolve()
        {
            card.Resolve();
        }
    }
    public class DestoryStackObject : StackObject
    {
        public DestoryStackObject(List<CardObject> targets)
        {
            targetCards = targets;
            name = "Destroying Multiple Permanents";
        }
        public DestoryStackObject(CardObject target)
        {
            targetCards = new List<CardObject>();
            targetCards.Add(target);
            name = "Destroying " + target.name;
        }
        public override void Resolve()
        {
            foreach (CardObject target in targetCards)
            {
                target.Destroy();
            }
        }

        public override string GetCMDSyntax()
        {
            return $"destroy {Networking.NetworkHandler.ConcatIds(targetCards)}";
        }
    }

    public class RandomDiscardStackObject : StackObject
    {
        int amount;
        public RandomDiscardStackObject(Player target, int quantityOfCards)
        {
            targetPlayers = new List<Player>() { target };
            amount = quantityOfCards;
            name = target.name+" is discarding " + quantityOfCards + " card(s).";
        }
        public override void Resolve()
        {
            targetPlayers[0].RandomlyDiscardCards(amount);
        }
    }

    public class DiscardStackObject : StackObject
    {
        public Dictionary<Player, List<CardObject>> discardValues;
        public DiscardStackObject(Dictionary<Player, List<CardObject>> discardValues)
        {
            this.discardValues = discardValues;
            if (discardValues.Count > 1)
                name = "Multiple players are discarding a card(s).";
            else
                name = discardValues.Keys.ToList()[0].name + " is discarding a card(s).";
            
        }
        public override void Resolve()
        {
            foreach (Player p in discardValues.Keys)
            {
                foreach (CardObject c in discardValues[p])
                {
                    p.DiscardCard(c);
                }
            }
        }
    }

    public class ExileStackObject : StackObject
    {
        public ExileStackObject(List<CardObject> targets)
        {
            targetCards = targets;
            name = "Exiling Multiple Permanents";
        }
        public ExileStackObject(CardObject target)
        {
            targetCards = new List<CardObject>() { target };
            name = "Exiling " + target.name;
        }
        public override void Resolve()
        {
            foreach (CardObject target in targetCards)
            {
                target.Exile();
            }
        }
        public override string GetCMDSyntax()
        {
            return $"exile {Networking.NetworkHandler.ConcatIds(targetCards)}";
        }
    }

    public class ToBottomOfLibStackObject : StackObject
    {
        int amount;
        public ToBottomOfLibStackObject(Player target, int amountOfCards)
        {
            amount = amountOfCards;
            targetPlayers = new List<Player>() { target };
            name = target.name + " is putting the top " + amountOfCards + " card(s) on the bottom of their library.";
        }
        public override void Resolve()
        {
            for (int i = 0; i < amount; i++)
            {
                CardObject z = targetPlayers[0].library[0];
                targetPlayers[0].library.Remove(z);
                targetPlayers[0].library.Add(z);
            }
        }
    }

    public class ToExileStackObject : StackObject
    {
        string location;
        public ToExileStackObject(List<CardObject> targets, string location)
        {
            this.location = location;
            targetCards = targets;
            name = "Moving multiple cards to exile.";
        }
        public ToExileStackObject(CardObject target, string location)
        {
            this.location = location;
            targetCards = new List<CardObject>() { target };
            name = "Moving " + target.name + " to exile.";
        }
        public override void Resolve()
        {
            foreach (CardObject c in targetCards)
            {
                if (location == "battlefield")
                    c.controller.battlefield.Remove(c);
                else if (location == "hand")
                    c.owner.hand.Remove(c);
                else if (location == "graveyard")
                    c.owner.graveyard.Remove(c);
                else if (location == "library")
                    c.owner.library.Remove(c);
                c.owner.exile.Add(c);
            }
        }
    }

    public class ApplyReplacementActionStackObject : StackObject
    {
        public StackReplacementAction action;
        public ApplyReplacementActionStackObject(string stackName, StackObject target,StackReplacementAction action)
        {
            targetStackObjects = new List<StackObject>() { target };
            name = stackName+" " + target.name;
            this.action = action;
        }
        public override void Resolve()
        {
            foreach (StackObject s in targetStackObjects)
            {
                Program.windowInstance.game.ApplyReplacementAction(s,action);
            }
        }
    }

    public class ToGraveyardStackObject : StackObject
    {
        string location;
        public ToGraveyardStackObject(List<CardObject> targets, string location)
        {
            this.location = location;
            targetCards = targets;
            name = "Moving multiple cards to the graveyard.";
        }
        public ToGraveyardStackObject(CardObject target, string location)
        {
            this.location = location;
            targetCards = new List<CardObject>() { target };
            name = "Moving " + target.name + " to the graveyard.";
        }
        public override void Resolve()
        {
            foreach (CardObject c in targetCards)
            {
                if (location == "battlefield")
                    c.controller.battlefield.Remove(c);
                else if (location == "hand")
                    c.owner.hand.Remove(c);
                else if (location == "exile")
                    c.owner.exile.Remove(c);
                else if (location == "library")
                    c.owner.library.Remove(c);
                c.owner.graveyard.Add(c);
            }
        }
    }
    public class ToHandStackObject : StackObject
    {
        string location;
        public ToHandStackObject(List<CardObject> targets, string location)
        {
            this.location = location;
            targetCards = targets;
            name = "Moving multiple cards to their owner's hand.";
        }
        public ToHandStackObject(CardObject target, string location)
        {
            this.location = location;
            targetCards = new List<CardObject>() { target };
            name = "Moving " + target.name + " to its owner's hand.";
        }
        public override void Resolve()
        {
            foreach (CardObject c in targetCards)
            {
                if (location == "battlefield")
                    c.controller.battlefield.Remove(c);
                else if (location == "exile")
                    c.owner.exile.Remove(c);
                else if (location == "graveyard")
                    c.owner.graveyard.Remove(c);
                else if (location == "library")
                    c.owner.library.Remove(c);
                c.owner.hand.Add(c);
            }
        }
    }

    public class DamageTargetStackObject : StackObject
    {
        int amount = 0;
        public DamageTargetStackObject(List<CardObject> targetCards, List<Player> targetPlayers, int amountOfDamage)
        {
            amount = amountOfDamage;
            this.targetCards = targetCards;
            this.targetPlayers = targetPlayers;
            if (targetCards.Count > 0 && targetPlayers.Count > 0)
                name = "Multiple targets are taking " + amountOfDamage + " damage.";
            else if (targetCards.Count == 0 && targetPlayers.Count == 1)
                name = targetPlayers[0].name + " is taking " + amountOfDamage + " damage.";
            else if (targetCards.Count == 1 && targetPlayers.Count == 0)
                name = targetCards[0].name + " is taking " + amountOfDamage + " damage.";
            else if (targetCards.Count == 0 && targetPlayers.Count > 1)
                name = "Multiple players are taking " + amountOfDamage + " damage.";
            else if (targetCards.Count > 1 && targetPlayers.Count == 0)
                name = "Multiple creatures are taking " + amountOfDamage + " damage.";
        }

        public override void Resolve()
        {
            foreach (CardObject card in targetCards)
            {
                if (card is CreatureBase)
                    (card as CreatureBase).Damage(amount);
            }
            foreach (Player p in targetPlayers)
            {
                p.TakeDamage(amount);
            }
        }

        public override string GetCMDSyntax()
        {
            string tC = (targetCards != null && targetCards.Count > 0)? Networking.NetworkHandler.ConcatIds(targetCards):"null";
            string tP = (targetPlayers != null && targetPlayers.Count > 0) ? Networking.NetworkHandler.ConcatIds(targetPlayers) : "null";
            return $"damage {tP} {tC} {amount}";
        }
    }

    public class HealingStackObject : StackObject
    {
        int amount = 0;
        public HealingStackObject(List<Player> players, int amount)
        {   
            this.amount = amount;
            targetPlayers = players;
            if (players.Count > 1)
                name = "Multiple players are being healed " + amount + " health.";
            else
                name = players[0].name + " is being healed " + amount + " health.";
        }
        public override void Resolve()
        {
            foreach (Player  p in targetPlayers)
            {
                p.Heal(amount);
            }
        }
        public override string GetCMDSyntax()
        {
            return $"heal {Networking.NetworkHandler.ConcatIds(targetPlayers)} {amount}";
        }
    }

    public class LoseStackObject : StackObject
    {
        public LoseStackObject(List<Player> targets)
        {
            targetPlayers = targets;
            name = "Making Several Players lose";
        }
        public LoseStackObject(Player target)
        {
            targetPlayers = new List<Player>();
            targetPlayers.Add(target);
            name = "Making " + target.name + " lose";
        }
        public override void Resolve()
        {
            foreach(Player target in targetPlayers)
            {
                Program.windowInstance.game.RemovePlayer(target.name + " has lost", target);
            }
        }

        public override string GetCMDSyntax()
        {
            return $"lose {Networking.NetworkHandler.ConcatIds(targetPlayers)} \"Has lost.\"";
        }
    }
    public class ScryStackObject : StackObject
    {
        public int amount;
        public Player target;
        public ScryStackObject(Player target, int amount)
        {
            this.amount = amount;
            this.target = target;
            name = target.name + " is scrying " + amount;
        }
        public override void Resolve()
        {
            target.Scry(amount);
        }
    }
    public class DrawStackObject : StackObject
    {
        public int amount;
        public DrawStackObject(List<Player> targets, int amount)
        {
            this.amount = amount;
            targetPlayers = targets;
            if (targetPlayers.Count > 1)
                name = "Multiple players are drawing "+ amount+ " card(s).";
            else
                name = targetPlayers[0].name + " is drawing " + amount + " card(s).";
        }
        public override void Resolve()
        {
            foreach (Player p in targetPlayers)
            {
                p.DrawCard(amount);
            }
        }
    }
    public class SearchStackObject : StackObject
    {
        List<CardObject> location;
        Predicate<CardObject> pred;
        List<CardObject> destination;
        protected CardObject card;
        public SearchStackObject(List<CardObject> location, List<CardObject> destination, Predicate<CardObject> pred, string title)
        {
            name = title;
            this.location = location;
            this.pred = pred;
            this.destination = destination;
        }

        public override void Resolve()
        {
            CardObject obj = location.Find(pred);
            if (obj != null)
            {
                location.Remove(obj);
                card = obj;
                if(destination!=null)
                    destination.Add(obj);
            }
        }
    }
    public class MillStackObject : StackObject
    {
        public int amount;
        public MillStackObject(List<Player> targets, int amount, Player controller, CardObject source)
        {
            this.amount = amount;
            targetPlayers = targets;
            this.controller = controller;
            this.source = source;
            if (targetPlayers.Count > 1)
                name = "Multiple players are being milled " + amount + " card(s).";
            else
                name = targetPlayers[0].name + " is being milled " + amount + " card(s)";
        }
        public override void Resolve()
        {
            foreach(Player p in targetPlayers)
            {
                if (controller == null || source == null)
                    p.Mill(amount, "");
                else
                    p.Mill(amount,"{"+controller.name+"'s "+source.name+"}");
            }
        }
    }
    public class TapStackObject : StackObject
    {
        public TapStackObject(List<CardObject> targets)
        {
            targetCards = targets;
            if (targetCards.Count > 1)
                name = "Tapping multiple cards.";
            else
                name = "Tapping " + targetCards[0].name;
        }
        public override void Resolve()
        {
            foreach (CardObject c in targetCards)
            {
                c.Tap();
            }
            Program.windowInstance.UpdateGameConsole();
        }
        public override string GetCMDSyntax()
        {
            return $"tap {Networking.NetworkHandler.ConcatIds(targetCards)}";
        }
    }
    public class UntapStackObject : StackObject
    {
        public UntapStackObject(List<CardObject> targets)
        {
            targetCards = targets;
            if (targetCards.Count > 1)
                name = "Untapping multiple cards.";
            else
                name = "Untapping " + targetCards[0].name;
        }
        public override void Resolve()
        {
            foreach (CardObject c in targetCards)
            {
                c.Untap();
            }
            Program.windowInstance.UpdateGameConsole();
        }
        public override string GetCMDSyntax()
        {
            return $"untap {Networking.NetworkHandler.ConcatIds(targetCards)}";
        }
    }

    public class ReturnToHandStackObject: StackObject
    {
        public ReturnToHandStackObject(List<CardObject> targets)
        {
            targetCards = targets;
            if (targetCards.Count > 1)
                name = "Multiple cards are being returned to their owner's hand";
            else
                name = targetCards[0].name + " is being returned to its owner's hand";
        }
        public override void Resolve()
        {
            foreach (CardObject c in targetCards)
            {
                c.ReturnToOwnerHand();
            }
        }

        public override string GetCMDSyntax()
        {
            return $"tohand {Networking.NetworkHandler.ConcatIds(targetCards)}";
        }
    }

    public class ReturnToLibraryStackObject : StackObject
    {
        int valueFromTop = 0;
        string location = null;
        public ReturnToLibraryStackObject(CardObject target,int valueFromTop)
        {
            targetCards = new List<CardObject>() { target };
            this.valueFromTop = valueFromTop;
            name = targetCards[0].name + " is being returned to its owner's libary, " + (valueFromTop>=0?valueFromTop + " card(s) from the top":(-valueFromTop+1) + " card(s) from the bottom.");
        }
        public ReturnToLibraryStackObject(CardObject target, int valueFromTop, string location)
        {
            this.location = location;
            targetCards = new List<CardObject>() { target };
            this.valueFromTop = valueFromTop;
            name = targetCards[0].name + " is being returned to its owner's libary, " + (valueFromTop >= 0 ? valueFromTop + " card(s) from the top" : (-valueFromTop + 1) + " card(s) from the bottom.");
        }
        public override void Resolve()
        {
            if (location == null)
                targetCards[0].ReturnToOwnerLibrary(valueFromTop);
            else
                targetCards[0].ReturnToOwnerLibrary(valueFromTop, location);
        }
        public override string GetCMDSyntax()
        {
            return $"tolibrary {Networking.NetworkHandler.ConcatIds(targetCards)} {valueFromTop}";
        }
    }

    public class ConditionStackObject : StackObject
    {
        public Condition condition;
        public ConditionStackObject(List<CardObject> targets,Condition condition)
        {
            this.condition = condition;
            targetCards = targets;
            if (targetCards.Count > 1)
                name = "Multiple cards " + condition.description;
            else
                name = targetCards[0].name + " " + condition.description;
        }
        public override void Resolve()
        {
            foreach (CardObject card in targetCards)
            {
                card.AddCondition(condition);
            }
            Program.windowInstance.UpdateGameConsole();
        }
    }
    public class MonstrousStackObject : ConditionStackObject
    {
        int counters = 0;
        public MonstrousStackObject(List<CardObject> targets, int counterQuantity) : base(targets, StaticConditions.monstrous)
        {
            counters = counterQuantity;
        }
        public override void Resolve()
        {
            foreach(CardObject card in targetCards)
            {
                card.AddCounter(new PlusOnePlusOneCounterCondition(counters));
            }
            base.Resolve();
        }
    }

    public class CMDStackObject : StackObject
    {
        public delegate void CMDHandler(string syntax);
        public static CMDHandler instance;
        string cmdSyntax;
        public CMDStackObject(string cmdSyntax)
        {
            this.cmdSyntax = cmdSyntax;
        }

        public override void Resolve()
        {
            if (instance == null)
                throw new Exception("CMD Handler has not properly been set. This is a critical error.");
            instance(cmdSyntax);
        }
        public override string GetCMDSyntax()
        {
            return cmdSyntax;
        }
    }
}

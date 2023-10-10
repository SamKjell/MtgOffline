using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class Game
    {
        public List<Player> players;
        public GameMode gameMode;
        public List<StackObject> stack = new List<StackObject>();
        public Dictionary<int, List<StackReplacementAction>> replacementActions = new Dictionary<int, List<StackReplacementAction>>();
        public Dictionary<TypePredicate, List<Condition>> typeBasedConditions = new Dictionary<TypePredicate, List<Condition>>();
        public int turn = 0;
        public static Random rand;

        private Player stackSub;
        protected bool resolvingStack;

        public Game(List<Player> players, GameMode gameMode)
        {
            this.players = players;
            this.gameMode = gameMode;
        }
        public virtual void Start()
        {
            rand = new Random();
            turn = players.Count - 1;
            foreach (Player p in players)
            {
                p.health = p.startingHealth;
                if (!p.isPlayer)
                {
                    p.deck.LoadDeck();
                    foreach (CardObject c in p.deck.cards)
                    {
                        c.owner = p;
                        c.controller = p;
                        p.library.Add(c);
                    }
                    p.ShuffleLibrary();
                    p.DrawCard(gameMode.startingHandSize);
                    int mulligans = 0;
                    while (Player.CountByCardType("Land", p.hand) < p.bot.favorableLands - mulligans)
                    {
                        mulligans++;
                        p.library.AddRange(p.hand);
                        p.hand.Clear();

                        p.ShuffleLibrary();
                        p.DrawCard(gameMode.startingHandSize);
                        int i = rand.Next(0, p.hand.Count);
                        p.hand[i].MulliganReturnToLibrary();
                    }
                }
            }
        }
        public virtual void NextTurn()
        {
            if (turn >= players.Count - 1)
                turn = 0;
            else
                turn++;

            if (!players[turn].isPlayer)
            {
                Player p = players[turn];
                p.UntapStep();
            }
            else
            {
                Player p = players[turn];
                p.PlayerUntapStep();
            }
        }

        public List<Player> GetAttackableOpponents(Player p)
        {
            List<Player> targets = new List<Player>();
            foreach (Player player in players)
            {
                if (player != p && player.CanBeAttacked())
                    targets.Add(player);
            }
            return targets;
        }

        public void DeclareAttacks(List<AttackData> data)
        {
            List<Player> playersClone = Simulation.CloneList(players);
            foreach (Player p in playersClone)
            {
                List<AttackData> attackers = new List<AttackData>();
                foreach (AttackData attack in data)
                {
                    if (attack.target == p)
                        attackers.Add(attack);
                }
                if (attackers.Count > 0)
                    p.DeclareBlocks(attackers);
            }
            Program.windowInstance.UpdateGameConsole();
            Program.windowInstance.inCombat = false;
        }

        public List<CardObject> GetAllCreatures(Player player, bool opponentOnly)
        {
            List<CardObject> creatures = new List<CardObject>();
            foreach (Player p in players)
            {
                if (opponentOnly && p == player)
                    continue;
                foreach (CardObject c in p.battlefield)
                {
                    if (c is CreatureBase)
                    {
                        creatures.Add(c);
                    }
                }
            }
            return creatures;
        }

        public List<CardObject> GetAllCards(Player player, bool opponentsOnly)
        {
            List<CardObject> cards = new List<CardObject>();
            foreach (Player p in players)
            {
                if (opponentsOnly && p == player)
                    continue;
                foreach (CardObject c in p.battlefield)
                    cards.Add(c);
            }
            return cards;
        }

        public void RemovePlayer(String reason, Player p)
        {
            if (p == Program.windowInstance.GetLocalPlayer())
            {
                NotifyPlayer("Defeat!", "You have lost. You will now be brought back to the title screen");
                Program.windowInstance.Close();
                return;
            }
            NotifyPlayer(p.name + "'s Defeat", reason);
            bool flag = false;
            if (players[turn] == p) flag = true;
            players.Remove(p);
            if (players.Count == 1)
            {
                Win(players[0]);
                return;
            }
            if (flag)
                NextTurn();
        }
        public void Win(Player p)
        {
            if (p != Program.windowInstance.GetLocalPlayer())
            {
                NotifyPlayer("Defeat!", "You have lost. You will now be brought back to the title screen");
                Program.windowInstance.Close();
                return;
            }
            NotifyPlayer("Victory!", p.name + ", you have won. You will now be brought back to the title screen.");
            Program.windowInstance.Close();

        }

        public void OnUpkeep(Player player)
        {
            foreach (Player p in players)
            {
                if (!p.isPlayer)
                {
                    List<CardObject> bf = Simulation.CloneList(p.battlefield);
                    foreach (CardObject c in bf)
                    {
                        c.OnUpkeep(player);
                    }
                }
            }
        }

        public void CardEnteredBattlefield(CardObject card)
        {
            //TypePredicate[] preds = typeBasedConditions.Keys.ToArray();
            //foreach (TypePredicate pred in preds)
            //{
            //    if (pred.MatchedPredicate(card))
            //    {
            //        Condition c = pred.condition.Clone();
            //        card.AddCondition(c);
            //        typeBasedConditions[pred].Add(c);
            //    }
            //}

            foreach (Player p in players)
            {
                foreach (CardObject c in p.battlefield)
                {
                    c.CardEntersTheBattlefield(card);
                }
            }
        }

        public void CardCast(CardObject card)
        {
            foreach (Player p in players)
            {
                foreach (CardObject c in p.battlefield)
                {
                    c.CardCast(card);
                }
            }
        }

        public bool IsCardOnBattlefield(CardObject card)
        {
            foreach (Player p in players)
            {
                foreach (CardObject c in p.battlefield)
                    if (c == card) return true;
            }
            return false;
        }

        public void NotifyPlayer(String title, String description)
        {
            Notice n = new Notice(title, description);
            n.ShowDialog();
        }

        public void LargeNotifyPlayer(string title, string description)
        {
            LargeNotice n = new LargeNotice(title, description);
            n.ShowDialog();
        }

        public virtual void AddToStack(StackObject stackObject)
        {
            if (Program.windowInstance.connectedToServer)
            {
                Networking.ClientSend.StackPacket(stackObject);
            }
            else
            {
                stack.Add(stackObject);
                if (resolvingStack)
                    resolvingStack = false;
                Program.windowInstance.UpdateStackConsole();
            }
        }
        public void AddToStack(CMDStackObject stackObject, bool ignoreCheck)
        {
            stack.Add(stackObject);
            if (resolvingStack)
                resolvingStack = false;
            Program.windowInstance.UpdateStackConsole();
        }
        public virtual void ResolveStack()
        {
            if (stack.Count == 0)
                return;
            resolvingStack = true;
            int l = stack.Count - 1;
            for (int i = l; i >= 0; i--)
            {
                bool flag = true;
                if (replacementActions.ContainsKey(i))
                {
                    bool destinationChange = false;
                    replacementActions[i].Reverse();
                    foreach (StackReplacementAction a in replacementActions[i])
                    {
                        if (a.isSuppressed)
                            flag = false;
                        if (a.destination != null && !destinationChange)
                        {
                            destinationChange = true;
                            if (stack[i] is CardStackObject || stack[i] is SpellStackObject)
                            {
                                CardObject card = (stack[i] is CardStackObject) ? (stack[i] as CardStackObject).card : (stack[i] as SpellStackObject).card;
                                HandleStackDestinationChange(card, a.destination);
                            }
                        }
                    }
                }
                if (flag)
                    stack[i].Resolve();
                if (!resolvingStack)
                {
                    stack.RemoveAt(i);
                    Program.windowInstance.UpdateStackConsole();
                    return;
                }
            }
            resolvingStack = false;
            stack.Clear();
            replacementActions.Clear();
            Program.windowInstance.UpdateStackConsole();
            if (stackSub != null)
                stackSub.DoNextAction();
        }

        protected void HandleStackDestinationChange(CardObject c, string destination)
        {
            if (destination == "graveyard")
                c.owner.graveyard.Add(c);
            else if (destination == "exile")
                c.owner.exile.Add(c);
            Player owner = c.owner;
            c = (CardObject)Activator.CreateInstance(c.GetType());
            c.owner = owner;
            c.controller = owner;
            if (destination == "library")
                c.owner.library.Add(c);
            else if (destination == "hand")
                c.owner.hand.Add(c);
        }

        public void DoCombat(List<AttackData> data)
        {
            foreach (AttackData attack in data)
            {
                if (attack.blockers.Count == 0)
                {
                    int cI = attack.creature.GetCombatInitiative();
                    int multi = (cI > 0 ? cI : 1);
                    int damage = attack.creature.GetPower() * multi;
                    attack.target.TakeDamage(damage);
                    if (attack.creature.HasLifelink())
                        attack.creature.controller.Heal(damage);
                }
                else if (attack.blockers.Count == 1)
                {
                    int trampleDamage = attack.creature.Fight(attack.blockers[0], true);
                    if (attack.creature.HasTrample() && trampleDamage > 0)
                    {
                        attack.target.TakeDamage(trampleDamage);
                        if (attack.creature.HasLifelink())
                            attack.creature.controller.Heal(trampleDamage);
                    }
                }
                else if (attack.blockers.Count > 1)
                {
                    //Multi-blocking. Not yet supported.
                }
            }
        }

        public void ServerDoCombat(List<AttackData> data)
        {
            foreach (AttackData attack in data)
            {
                if (attack.blockers.Count == 0)
                {
                    int damage = attack.creature.GetPower();
                    //attack.target.TakeDamage(damage);
                    Networking.ClientSend.Trigger($"cmd damage {(attack.target as Networking.ServerPlayer).id} null {damage}");
                }
                else if (attack.blockers.Count == 1)
                {
                    int trampleDamage = attack.creature.ServerFight(attack.blockers[0]);
                    if (attack.creature.HasTrample() && trampleDamage > 0)
                        //attack.target.TakeDamage(trampleDamage);
                        Networking.ClientSend.Trigger($"cmd damage {(attack.target as Networking.ServerPlayer).id} null {trampleDamage}");
                }
                else if (attack.blockers.Count > 1)
                {
                    //Multi-blocking. Not yet supported.
                }
            }
        }

        public void SubscribeToStack(Player p)
        {
            stackSub = p;
        }
        public void UnsubscribeToStack(Player p)
        {
            stackSub = null;
        }

        public void ApplyReplacementAction(StackObject target, StackReplacementAction action)
        {
            int i = GetStackObjectIndex(target);
            if (i >= 0)
            {
                if (replacementActions.ContainsKey(i))
                    replacementActions[i].Add(action);
                else
                    replacementActions.Add(i, new List<StackReplacementAction>() { action });
            }
            else
            {
                throw new IndexOutOfRangeException("The target Stack Object is not present in the stack. This shouldn't be occurring.");
            }
        }

        public int GetStackObjectIndex(StackObject s)
        {
            return stack.IndexOf(s);
        }

        public void AddTypeBasedCondition(TypePredicate pred, Condition condition)
        {
            pred.condition = condition;
            List<Condition> cards = new List<Condition>();
            foreach (CardObject card in GetAllCards(null, false))
            {
                if (pred.MatchedPredicate(card))
                {
                    Condition c = condition.Clone();
                    card.AddCondition(c);
                    cards.Add(c);
                }
            }
            typeBasedConditions.Add(pred, cards);
            Program.windowInstance.UpdateGameConsole();
        }

        public void RemoveTypeBasedConditions(CardObject source)
        {
            List<TypePredicate> preds = typeBasedConditions.Keys.ToList().FindAll(x => x.source == source);
            if (preds == null || preds.Count == 0) return;

            foreach (TypePredicate p in preds)
            {
                foreach (Condition c in typeBasedConditions[p])
                    c.associatedCard.RemoveCondition(c);
                typeBasedConditions.Remove(p);
            }
            Program.windowInstance.UpdateGameConsole();
        }

        public void HandleTypeBasedConditions()
        {
            TypePredicate[] preds = typeBasedConditions.Keys.ToArray();
            foreach (TypePredicate pred in preds)
            {
                if (!IsCardOnBattlefield(pred.source))
                    RemoveTypeBasedConditions(pred.source);
            }
        }

        public void CheckIfTypeBasedConditionsStillApply()
        {
            HandleTypeBasedConditions();
            RefreshTypeBasedConditions();
            foreach (TypePredicate pred in typeBasedConditions.Keys.ToArray())
            {
                foreach (Condition con in typeBasedConditions[pred].ToArray()) 
                {
                    if (!pred.pred.Invoke(con.associatedCard))
                    {
                        con.associatedCard.RemoveCondition(con);
                        typeBasedConditions[pred].Remove(con);
                    }
                }
            }
        }

        public void RefreshTypeBasedConditions()
        {
            foreach (TypePredicate pred in typeBasedConditions.Keys.ToArray())
            {
                List<CardObject> co = GetAllCards(null, false).FindAll(pred.pred);
                foreach (CardObject item in co)
                {
                    if(typeBasedConditions[pred].FindAll(x=>x.associatedCard == item).Count == 0)
                    {
                        Condition c = pred.condition.Clone();
                        item.AddCondition(c);
                        typeBasedConditions[pred].Add(c);
                    }
                }
            }
        }
    }

    public class TypePredicate
    {
        public Predicate<CardObject> pred;
        public CardObject source;
        public Condition condition;

        public TypePredicate(Predicate<CardObject> predicate, CardObject source)
        {
            pred = predicate;
            this.source = source;
        }

        public bool MatchedPredicate(CardObject card)
        {
            return pred.Invoke(card);
        }
    }

    public class StackReplacementAction
    {
        public static StackReplacementAction COUNTERED = new StackReplacementAction("Countered", true, "graveyard");
        public static StackReplacementAction TO_HAND = new StackReplacementAction("To hand", true, "hand");
        public static StackReplacementAction EXILED = new StackReplacementAction("Exiled", true, "exile");
        public static StackReplacementAction TO_LIBRARY = new StackReplacementAction("To library", true, "library");

        public string name;
        public bool isSuppressed;
        public string destination;
        public StackReplacementAction(string name, bool doesSupress, string destination = null)
        {
            this.name = name;
            this.destination = destination;
            isSuppressed = doesSupress;
        }

        
    }

    public class AttackData
    {
        public CreatureBase creature;
        public List<CreatureBase> blockers = new List<CreatureBase>();
        public Player target;
        public AttackData(CreatureBase card, Player p)
        {
            creature = card;
            target = p;
        }
    }
}

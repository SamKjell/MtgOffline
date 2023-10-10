using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public class Player
    {
        public String name;
        public int startingHealth;
        public int health;
        public List<CardObject> battlefield = new List<CardObject>();
        public List<CardObject> hand = new List<CardObject>();
        public List<CardObject> library = new List<CardObject>();
        public List<CardObject> graveyard = new List<CardObject>();
        public List<CardObject> exile = new List<CardObject>();
        public List<CardObject> commandZone = new List<CardObject>();
        public List<CardObject> companionZone = new List<CardObject>();
        public Deck deck;
        public Bot bot;
        public bool isPlayer;
        public Bitmap icon;
        public List<String> manaPool = new List<String>();
        public int turnLandCap = 1;
        public int landPlayed = 0;
        public String stage;

        private int nodeIndex = 0;
        private List<int> nodes = new List<int>();

        public Player(String name, int startingHealth, Deck deck, bool isPlayer, Bot bot = null)
        {
            this.startingHealth = startingHealth;
            this.deck = deck;
            this.isPlayer = isPlayer;
            this.bot = bot;
            this.name = name;
        }

        public Player()
        {

        }

        //AI Actions
        public void UntapStep()
        {
            SubToStack();
            stage = "untap";
            foreach (CardObject card in battlefield)
            {
                card.hasSummonSickness = false;
                if(card.CanUntap() && card.CanUpkeepUntap())
                    card.Untap();
                List<Condition> garbage = new List<Condition>();
                foreach (Condition c in card.conditions)
                {
                    if (c.HasConcluded("controller_upkeep",card))
                        garbage.Add(c);
                }
                foreach (Condition c in garbage)
                {
                    card.RemoveCondition(c);
                }
            }
            Upkeep();
        }
        public void Upkeep()
        {
            stage = "upkeep";
            turnLandCap = 1;
            landPlayed = 0;
            Program.windowInstance.game.OnUpkeep(this);
            if(StackEmpty())
                DrawStep();
        }
        public void DrawStep()
        {
            stage = "drawstep";
            //Trigger Drawstep effects
            DrawCard(1);
            PrecombatMainPhase();
        }
        public void PrecombatMainPhase()
        {
            ClearManaPool();
            stage = "precombat";
            //Trigger First Main phase effects

            Simulation sim = new Simulation(this, Program.windowInstance.game);
            nodes = sim.StartSimulation();
            nodeIndex = 0;
            List<object> plays = GetPossiblePlays(manaPool, false);
            object move = (nodes.Count > 0 && plays.Count > 0) ? plays[nodes[nodeIndex]] : null;
            if (move == null)
            {
                if(StackEmpty())
                    Combat();
            }
            else
            {
                if (move is AlternateLocationObject)
                {
                    AlternateLocationObject alo = plays[nodes[nodeIndex]] as AlternateLocationObject;
                    hand.Add(alo.card);
                    alo.location.Remove(alo.card);
                    move = alo.card;
                }

                if (move is CardObject)
                {
                    CardObject card = move as CardObject;
                    nodeIndex++;
                    if (card is LandBase)
                    {
                        landPlayed++;
                        EnterTheBattlefield(card);
                        hand.Remove(card);
                        if (StackEmpty())
                            DoNextAction();
                    }
                    else
                        CastSpell(card);
                }
                else if (move is Ability)
                {
                    Ability ability = move as Ability;
                    nodeIndex++;
                    ActivateAbility(ability);
                    if (StackEmpty())
                        DoNextAction();
                }
            }

        }
        
        public void Combat()
        {
            stage = "combat";
            //Trigger Combat phase effects
            //Program.windowInstance.game.CheckIfTypeBasedConditionsStillApply();

            List<CreatureBase> attackers = new List<CreatureBase>();
            foreach (CreatureBase c in GetCreaturesInLocation(battlefield))
            {
                if (c.CanAttack())
                    attackers.Add(c);
            }
            if (attackers.Count > 0)
            {
                CombatSim sim = new CombatSim(this);
                List<AttackData> attacks = sim.GetBestAttacks(attackers, Program.windowInstance.game.GetAttackableOpponents(this));
                if (attacks.Count > 0)
                    foreach (AttackData aD in attacks)
                        aD.creature.Attack();
                    Program.windowInstance.game.DeclareAttacks(attacks);
            }
                

            PostCombatMainPhase();
        }
        public void PostCombatMainPhase()
        {
            ClearManaPool();
            stage = "postcombat";
            //Trigger post-combat main phase effects

            EndStep();
        }

        public void EndStep()
        {
            stage = "endstep";
            //Trigger end-step effects
            foreach (Player p in Program.windowInstance.game.players)
            {
                bool yourEndstep = false;
                if (p == this)//Your endstep
                    yourEndstep = true;
                List<CardObject> battlefieldEnumerator = Simulation.CloneList(p.battlefield);
                foreach (CardObject card in battlefieldEnumerator)
                {
                    if (card is CreatureBase)
                        (card as CreatureBase).Heal();

                    List<Condition> garbage = new List<Condition>();
                    foreach (Condition c in card.conditions)
                    {
                        if (c.HasConcluded("endstep", card))
                            garbage.Add(c);
                        else if (yourEndstep && c.HasConcluded("your_endstep", card))
                            garbage.Add(c);
                    }
                    foreach (Condition c in garbage)
                    {
                        card.RemoveCondition(c);
                    }
                }
                List<CardObject> exileEnumerator = Simulation.CloneList(p.exile);
                foreach (CardObject card in exileEnumerator)
                {
                    List<Condition> garbage = new List<Condition>();
                    foreach (Condition c in card.conditions)
                    {
                        if (c.HasConcluded("endstep", card))
                            garbage.Add(c);
                    }
                    foreach (Condition c in garbage)
                    {
                        card.RemoveCondition(c);
                    }
                }
            }
            if (hand.Count > GetMaxHandSize())
            {
                int dif = hand.Count - GetMaxHandSize();
                RandomlyDiscardCards(dif);
            }
            ClearManaPool();
            Program.windowInstance.UpdateGameConsole();
            Program.windowInstance.game.NextTurn();
        }

        //Stack handling

        public void DoNextAction()
        {
            if (stage == "precombat")
            {
                if(StackEmpty())
                {
                    List<object> plays = GetPossiblePlays(manaPool,false);
                    bool flag = nodeIndex < nodes.Count && plays.Count > 0 && !(plays.Count <= nodes[nodeIndex]);
                    if (flag && !(plays[nodes[nodeIndex]] is string))
                    {
                        if(plays[nodes[nodeIndex]] is AlternateCastingObject)
                        {
                            AlternateCastingObject alt = plays[nodes[nodeIndex]] as AlternateCastingObject;
                            plays[nodes[nodeIndex]] = alt.GetAlternateCardForPlay();
                        }

                        if(plays[nodes[nodeIndex]] is AlternateLocationObject)
                        {
                            AlternateLocationObject alo = plays[nodes[nodeIndex]] as AlternateLocationObject;
                            hand.Add(alo.card);
                            alo.location.Remove(alo.card);
                            plays[nodes[nodeIndex]] = alo.card;
                        }

                        if(plays[nodes[nodeIndex]] is CardObject)
                        {
                            CardObject card = plays[nodes[nodeIndex]] as CardObject;
                            nodeIndex++;
                            if (card is LandBase)
                            {
                                landPlayed++;
                                EnterTheBattlefield(card);
                                hand.Remove(card);
                                if (StackEmpty())
                                    DoNextAction();
                            }
                            else
                                CastSpell(card);
                        }
                        //else if(plays[nodes[nodeIndex]] is AlternateLocationObject)
                        //{
                        //    AlternateLocationObject altLoc = (plays[nodes[nodeIndex]] as AlternateLocationObject);
                        //    CardObject card = altLoc.card;
                        //    nodeIndex++;
                        //    if (card is LandBase)
                        //    {
                        //        landPlayed++;
                        //        EnterTheBattlefield(card);
                        //        altLoc.location.Remove(card);
                        //        if (StackEmpty())
                        //            DoNextAction();
                        //    }
                        //    else
                        //        CastSpell(card, altLoc.location);
                        //}
                        else if(plays[nodes[nodeIndex]] is Ability)
                        {
                            Ability ability = plays[nodes[nodeIndex]] as Ability;
                            nodeIndex++;
                            ActivateAbility(ability);
                            if (StackEmpty())
                                DoNextAction();
                        }
                    }
                    else if(flag && plays[nodes[nodeIndex]] is string)
                    {
                        string cmd = plays[nodes[nodeIndex]] as string;
                        nodeIndex++;
                        if (cmd == "Do Nothing")
                            DoNextAction();
                    }
                    else
                    {
                        Combat();
                    }
                }
            }
            else if(stage == "upkeep")
            {
                if (StackEmpty())
                {
                    DrawStep();
                }
            }
        }

        public bool StackEmpty()
        {
            if (Program.windowInstance.game.stack.Count == 0)
                return true;
            return false;
        }

        public void SubToStack()
        {
            Program.windowInstance.game.SubscribeToStack(this);
        }


        //Actual Player gameUtils:
        public void PlayerUntapStep()
        {
            foreach (CardObject card in battlefield)
            {
                card.hasSummonSickness = false;
                if (card.CanUntap() && card.CanUpkeepUntap())
                    card.Untap();
                List<Condition> garbage = new List<Condition>();
                foreach (Condition c in card.conditions)
                {
                    if (c.HasConcluded("controller_upkeep", card))
                        garbage.Add(c);
                }
                foreach (Condition c in garbage)
                {
                    card.RemoveCondition(c);
                }
            }
            if(!Settings.lightNotifications && this == Program.windowInstance.GetLocalPlayer())
                Program.windowInstance.game.NotifyPlayer("Your Turn", "It is your turn!");
        }

        public void PlayerEndstep()
        {
            //Trigger end-step effects
            foreach (Player p in Program.windowInstance.game.players)
            {
                bool yourEndstep = false;
                if (p == this)//Your endstep
                    yourEndstep = true;
                List<CardObject> battlefieldEnumerator = Simulation.CloneList(p.battlefield);
                foreach (CardObject card in battlefieldEnumerator)
                {
                    if (card is CreatureBase)
                        (card as CreatureBase).Heal();

                    List<Condition> garbage = new List<Condition>();
                    foreach (Condition c in card.conditions)
                    {
                        if (c.HasConcluded("endstep", card))
                            garbage.Add(c);
                        else if (yourEndstep && c.HasConcluded("your_endstep", card))
                            garbage.Add(c);
                    }
                    foreach (Condition c in garbage)
                    {
                        card.RemoveCondition(c);
                    }
                }
                List<CardObject> exileEnumerator = Simulation.CloneList(p.exile);
                foreach (CardObject card in exileEnumerator)
                {
                    List<Condition> garbage = new List<Condition>();
                    foreach (Condition c in card.conditions)
                    {
                        if (c.HasConcluded("endstep", card))
                            garbage.Add(c);
                    }
                    foreach (Condition c in garbage)
                    {
                        card.RemoveCondition(c);
                    }
                }
            }
            Program.windowInstance.UpdateGameConsole();
        }

        //Utils
        public virtual int GetMaxHandSize()
        {
            return 7;
        }

        public virtual bool CanBeAttacked()
        {
            return true;
        }

        private static Notice blockingNotice;
        private static List<AttackData> receivedBlocks = new List<AttackData>();
        private static int attackCount = 0;
        protected void AddBlock(AttackData block)
        {
            receivedBlocks.Add(block);
            if (receivedBlocks.Count == attackCount)
                blockingNotice.Close();
        }

        public void DeclareBlocks(List<AttackData> attacks)
        {
            List<AttackData> blocks = new List<AttackData>();
            if (isPlayer)
            {
                //Player determines blocks
                if (!Program.windowInstance.connectedToServer)
                {
                    List<CreatureBase> creatures = new List<CreatureBase>();
                    foreach (CreatureBase c in GetCreaturesInLocation(battlefield))
                        creatures.Add(c);
                    CombatPrompt prompt = new CombatPrompt(attacks, creatures, false);
                    prompt.ShowDialog();
                    blocks = prompt.returnData;
                }
                else
                {
                    Notice notice = new Notice("Blocks","Waiting for opponent to declare blocks.",true);
                    blockingNotice = notice;
                    attackCount = attacks.Count;
                    List<string> attackStrings = new List<string>();
                    foreach (AttackData attack in attacks)
                        attackStrings.Add(Networking.NetworkHandler.AttackDataToStringBasic(attack));
                    ESBuffer buffer = new ESBuffer();
                    buffer.Add("cmd_identifier", "getblocks");
                    buffer.Add("defending_player", (this as Networking.ServerPlayer).id);
                    buffer.Add("es_basic", string.Join("~", attackStrings));
                    Networking.ClientSend.TriggerES(buffer.GetES());
                    notice.ShowDialog();
                    blocks = receivedBlocks;
                    receivedBlocks.Clear();
                }
            }
            else
            {
                //AI blocks
                List<CreatureBase> possibleBlockers = new List<CreatureBase>();
                foreach (CreatureBase c in GetCreaturesInLocation(battlefield))
                {
                    if (c.CanBlock())
                        possibleBlockers.Add(c);
                }
                //Initiate Simulation
                CombatSim sim = new CombatSim(this);
                blocks = sim.GetBestBlocks(attacks,possibleBlockers,health);
            }
            if (!Program.windowInstance.connectedToServer)
                Program.windowInstance.game.DoCombat(blocks);
            else
                Program.windowInstance.game.ServerDoCombat(blocks);
        }
        //public int GetCreatureAttackValue(CreatureBase card)
        //{
        //    int value = card.GetPower()*2 + card.GetToughness() + card.GetStaticAbilities().Count*2 + bot.aggressionLevel;
        //    return value;
        //}
        //public int GetCreatureDefenseValue(CreatureBase card)
        //{
        //    int value = card.GetPower() + card.GetToughness() * 2 + card.GetStaticAbilities().Count * 2 + card.GetActivatedAbilities().Count;
        //    return value;
        //}
        //public bool ShouldAttack(CreatureBase card)
        //{
        //    return (GetCreatureAttackValue(card) > GetCreatureDefenseValue(card));
        //}

        public virtual void ClearManaPool()
        {
            manaPool.Clear();
        }

        public List<CreatureBase> GetCreaturesInLocation(List<CardObject> location)
        {
            List<CreatureBase> creatures = new List<CreatureBase>();
            foreach (CardObject card in location)
            {
                if (card is CreatureBase)
                    creatures.Add(card as CreatureBase);
            }
            return creatures;
        }

        public virtual List<CardObject> GetAllCreatures(bool opponentsOnly)
        {
            return Program.windowInstance.game.GetAllCreatures(this, opponentsOnly);
        }

        public virtual List<CreatureBase> GetControlledCreatures()
        {
            List<CreatureBase> creatures = new List<CreatureBase>();
            foreach (CardObject c in battlefield)
                if(c is CreatureBase)
                    creatures.Add(c as CreatureBase);
            return creatures;
        }

        public virtual void TriggerAbility(StackObject ability)
        {
            Program.windowInstance.game.AddToStack(ability);
        }

        public static int CountBySuperType(String target, List<CardObject> location)
        {
            int count = 0;
            foreach (CardObject c in location)
            {
                if (c.superTypes.Contains(target))
                    count++;
            }
            return count;
        }
        public static int CountByCardType(String target, List<CardObject> location)
        {
            int count = 0;
            foreach (CardObject c in location)
            {
                if (c.cardTypes.Contains(target))
                    count++;
            }
            return count;
        }
        public static int CountBySubType(String target, List<CardObject> location)
        {
            int count = 0;
            foreach (CardObject c in location)
            {
                if (c.subTypes.Contains(target))
                    count++;
            }
            return count;
        }

        public virtual List<CardObject> GetBattlefield()
        {
            return battlefield;
        }

        public void Mill(int quantity)
        {
            Mill(quantity, "");
        }
        public void Mill(int quantity, String source)
        {
            if (isPlayer)
            {
                Program.windowInstance.game.NotifyPlayer("Mill", "Put the top " + quantity + " cards of your library into your graveyard." + " " + source);
            }
            else
            {
                for (int i = 0; i < quantity; i++)
                {
                    if (library.Count > 0)
                    {
                        graveyard.Add(library[0]);
                        library.RemoveAt(0);
                    }
                }
            }
        }

        public void DiscardCard(CardObject card)
        {
            //trigger discard events
            hand.Remove(card);
            graveyard.Add(card);
        }

        public void RandomlyDiscardCards(int numberOfCards)
        {
            hand = ShuffleCards(hand);
            for (int i = 0; i < numberOfCards; i++)
            {
                if (hand.Count > 0)
                    DiscardCard(hand[0]);
            }
        }

        public virtual Player ChooseTargetPlayer(bool opponent)
        {
            List<Player> p = new List<Player>();
            List<Player> options = Program.windowInstance.game.players;
            foreach (Player option in options)
            {
                if (opponent && option != this)
                    p.Add(option);
                else if (!opponent)
                    p.Add(option);
            }
            return p[Game.rand.Next(0, p.Count)];
        }
        public static CardObject GetCardByID(int id, List<CardObject> location)
        {
            foreach (CardObject c in location)
            {
                if (c.id == id)
                    return c;
            }
            return null;
        }

        public virtual CardObject ChooseTargetLandOnBattlefield(bool opponentsOnly)
        {
            CardObject target = null;
            int bestScore = 0;
            if (opponentsOnly)
            {

            }
            else
            {
                foreach (CardObject c in battlefield)
                {
                    if(c is LandBase)
                    {
                        int score = 100-c.GetActivatedAbilities().Count;
                        if(score > bestScore)
                        {
                            target = c;
                            bestScore = score;
                        }
                    }
                }
            }

            return target;
        }
        public delegate int AssessmentModel(CardObject card);
        public virtual CardObject ChooseTargetOnBattlefield(Predicate<CardObject> targetPred, AssessmentModel model)
        {
            CardObject target = null;
            int bestScore = 0;
            foreach (Player p in Program.windowInstance.game.players)
            {
                foreach (CardObject card in p.battlefield.FindAll(targetPred))
                {
                    int score = model(card);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        target = card;
                    }
                }
            }
            return target;
        }

        public virtual CardObject ChooseTargetCreatureOnBattlefield(bool opponentsOnly)
        {
            CardObject target = null;
            int bestScore = 0;
            if (opponentsOnly) //Targeting opponents
            {
                foreach (Player p in Program.windowInstance.game.players)
                {
                    if (p == this)
                        continue;
                    foreach (CardObject c in p.battlefield)
                    {
                        if(c is CreatureBase)
                        {
                            int score = AssessCreatureThreat(c as CreatureBase);
                            if (score > bestScore) {
                                target = c;
                                bestScore = score;
                            }
                        }
                    }
                }
            }
            return target;
        }

        public virtual CardObject ChooseTargetControlledCreatureOnBattlefield(int byScore)
        {
            CardObject target = null;
            List<CreatureBase> creatures = GetControlledCreatures();
            if (creatures == null || creatures.Count == 0) return null;
            int bestScore = byScore * AssessCreatureThreat(creatures[0]);
            target = creatures[0];
            if (byScore==0)
                return creatures[Game.rand.Next(0, creatures.Count)];
            else
            {
                foreach (CreatureBase c in creatures)
                {
                    int score = byScore * AssessCreatureThreat(c);
                    if (score > bestScore)
                    {
                        target = c;
                        bestScore = score;
                    }
                }
            }
            return target;
        }
        public virtual CardObject ChooseTargetPermanentOnBattlefield(bool opponentOnly,List<CardObject> invalidTargets = null)
        {
            List<CardObject> possibleTargets = new List<CardObject>();
            foreach (Player p in Program.windowInstance.game.players)
            {
                if (opponentOnly && p == this)
                    continue;
                foreach (CardObject c in p.battlefield)
                {
                    if (invalidTargets != null && invalidTargets.Contains(c))
                        continue;
                    possibleTargets.Add(c);
                }
            }
            int index = Game.rand.Next(0, possibleTargets.Count);
            if (possibleTargets.Count == 0)
                return null;
            return possibleTargets[index];
        }

        public static int AssessCreatureThreat(CreatureBase threat)
        {
            int score = 0;
            score += threat.GetPower();
            score += threat.GetToughness();
            score += threat.GetStaticAbilities().Count;
            score += (int)(ManaUtil.CMC(threat.manaCost) * 0.5);
            score += threat.GetActivatedAbilities().Count*3;

            if (threat.superTypes.Contains("Legendary"))
                score += 10;
            return score;
        }
        public static int AssessCreatureThreat(CardObject threat)
        {
            if (!(threat is CreatureBase)) return 0;
            return AssessCreatureThreat(threat as CreatureBase);
        }

        public static int AssessRandomly(CardObject threat)
        {
            return Game.rand.Next(1, 100);
        }
        public void CastSpell(CardObject card) { CastSpell(card, hand); }

        public void CastSpell(CardObject card,List<CardObject> location)
        {
            string newCost = ReduceCost(card, card.manaCost);
            List<String> cost = newCost.Split(',').ToList();
            FillManaPool(newCost);
            if (DrainManaPool(cost))
            {
                if (card.isPermanent)
                {
                    if (card is AuraBase)
                    {
                        object target = (card as AuraBase).GetTarget();
                        if (target == null)
                            return;
                        AuraStackObject aso = new AuraStackObject(card as AuraBase, target);
                        location.Remove(card);
                        Program.windowInstance.game.AddToStack(aso);
                    }
                    else
                    {
                        CardStackObject cso = new CardStackObject(card);
                        location.Remove(card);
                        Program.windowInstance.game.AddToStack(cso);
                    }
                }
                else
                {
                    SpellStackObject sso = new SpellStackObject(card as SpellBase);
                    if(card is ITargetable)
                    {
                        if (sso.targetCards == null) sso.targetCards = new List<CardObject>();
                        if (sso.targetPlayers == null) sso.targetPlayers = new List<Player>();
                        foreach (object o in (card as ITargetable).GetTargets())
                        {
                            if (o is Player)
                                sso.targetPlayers.Add(o as Player);
                            else if (o is CardObject)
                                sso.targetCards.Add(o as CardObject);
                        }
                    }
                    location.Remove(card);
                    Program.windowInstance.game.AddToStack(sso);
                }

                Program.windowInstance.game.CardCast(card);
            }
        }
        public virtual void ActivateAbility(Ability ability)
        {
            List<String> cost = ability.cost.Split(',').ToList();
            //Handle alternative costs such as tapping
            bool canCast = ability.CanActivate();
            if (cost.Contains("T"))
            {
                if (ability.source.CanTap())
                {
                    cost.Remove("T");
                    ability.source.Tap();
                }
                else
                    canCast = false;
            }
            if (canCast)
            {
                bool enoughMana = true;
                if (cost.Count > 0)
                {
                    String manaCost = string.Join(",", cost);
                    FillManaPool(manaCost);
                    if (!DrainManaPool(cost))
                        enoughMana = false;
                }

                //After all costs have been handled, then the ability is actually activated
                if (enoughMana && ability.AdditionalCosts())
                {
                    ability.Activate();
                }
            }
        }

        public bool DrainManaPool(List<String> cost)
        {
            if (cost.Count == 0 || cost[0] == "0" || cost[0] == "") return true;
            List<String> remainder = ManaUtil.DifferenceOfMana(cost, manaPool,out manaPool);
            if (remainder.Count == 0)
            {
                //manaPool.Clear();
                return true;
            }
            return false;
        }

        public void FillManaPool(String amount)
        {
            if (amount == "0" || amount == "") return;
            List<String> amountRemaining = amount.Split(',').ToList();
            List<CardObject> cObs = Simulation.CloneList(battlefield);
            foreach (CardObject c in cObs)
            {
                foreach (Ability a in c.abilities)
                {
                    if (a is ManaAbility)
                    {
                        if (a.cost == "T" && !c.isTapped)
                        {
                            ManaAbility m = a as ManaAbility;
                            if (IsThisManaRequired(amountRemaining, m.manaProduced))
                            {
                                c.Tap();
                                a.source = c;
                                a.Activate();
                                amountRemaining = ManaUtil.DifferenceOfMana(amountRemaining, m.manaProduced);
                            }
                        }
                    }
                }
            }
        }

        public bool IsThisManaRequired(List<String> cost, String mana)
        {
            List<String> c = cost;
            List<String> m = mana.Split(',').ToList();
            foreach (String s in c)
            {
                if (int.TryParse(s, out int x))
                    return true;
            }
            foreach (String s in m)
            {
                if (c.Contains(s))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual string ReduceCost(CardObject card, string cost)
        {
            foreach (CardObject c in Program.windowInstance.game.GetAllCards(null,false).FindAll(x=>x is ICostReducer))
            {
                ICostReducer reducer = c as ICostReducer;
                cost = reducer.GetReducedCost(card, cost);
            }
            return cost;
        }

        public bool EnoughMana(string manaCost, List<string> manaPool,CardObject card)
        {
            if (manaCost == "0")
                return true;
            if(card!=null)
                manaCost = ReduceCost(card,manaCost);
            List<String> mCost = manaCost.Split(',').ToList();
            for (int m = mCost.Count - 1; m >= 0; m--)
            {
                if (int.TryParse(mCost[m], out int v) && manaPool.Count >= v)
                {
                    mCost.RemoveAt(m);
                    for (int i = 0; i < v; i++)
                    {
                        manaPool.RemoveAt(0);
                    }
                }
                else if (!int.TryParse(mCost[m], out int z) && manaPool.Contains(mCost[m]))
                {
                    manaPool.Remove(mCost[m]);
                    mCost.RemoveAt(m);

                }
                else if (!int.TryParse(mCost[m], out int y) && manaPool.Contains("O"))
                {
                    manaPool.Remove("O");
                    mCost.RemoveAt(m);
                }
                else
                    return false;
            }
            return true;
        }

        public List<object> GetPossiblePlays(List<String> manaP, bool simulation)
        {
            return GetPossiblePlays(manaP);
        }

        public List<object> GetPossiblePlays(List<String> manaP)
        {
            List<object> possiblePlays = new List<object>();
            
            //Strange possible plays (Etc. casting cards from graveyard or exile etc.)
            GetPossiblePlaysEventArgs args = new GetPossiblePlaysEventArgs(this);
            Mod_Framework.EventBus.PostEvent("GetPossiblePlays", Program.windowInstance.game, args);
            possiblePlays.AddRange(args.GetPlays());

            foreach (CardObject c in hand)
            {
                List<String> testing = GetManaPotential();
                if (c is LandBase)
                {
                    if (landPlayed < turnLandCap)
                    {
                        possiblePlays.Add(c);
                    }
                    continue;
                }
                else if (c is AuraBase)
                {
                    if ((c as AuraBase).GetTarget() == null)
                        continue;
                }
                else if(c is ITargetable)
                {
                    ITargetable t = c as ITargetable;
                    if (t.GetTargets() == null) continue;
                }

                if (EnoughMana(c.manaCost, testing,c))
                    possiblePlays.Add(c);

                //Alternate Casting Costs
                List<AlternateCastingObject> alts = c.GetAlternativeCastingObjects();
                if (alts == null || alts.Count == 0) continue;
                foreach (AlternateCastingObject alt in alts)
                {
                    testing = GetManaPotential();
                    if (EnoughMana(alt.cost, testing,alt.GetAlternateCardForPlay()))
                    {
                        //Will cause problems because it sets the card into the alternate state before it has decided which cost to use.
                        //CardObject card = alt.GetAlternateCardForPlay();
                        //if(card is ITargetable)
                        //{
                        //    ITargetable t = card as ITargetable;
                        //    if (t.GetTargets() == null) continue;
                        //}
                        possiblePlays.Add(alt);
                    }
                }
            }
            foreach (CardObject card in battlefield)
            {
                foreach (Ability a in card.abilities)
                {
                    a.source = card;
                    List<String> testing = GetManaPotential();
                    if (!(a is ManaAbility && a.cost == "T") && a.CanActivate())
                    {
                        List<String> cost = a.cost.Split(',').ToList();
                        bool flag = true;
                        if (cost.Contains("T") && a.source.isTapped)
                            flag = false;
                        else if (cost.Contains("T"))
                            cost.Remove("T");

                        if (flag && EnoughMana(string.Join(",", cost), testing,null))
                            possiblePlays.Add(a);
                    }
                }
            }

            //Cull out duplicate moves to reduce computations
            List<string> names = new List<string>();
            List<Type> abilityTypes = new List<Type>();
            List<object> pPlays = new List<object>();
            foreach (object o in possiblePlays)
            {
                if (o is CardObject && !names.Contains((o as CardObject).name))
                {
                    pPlays.Add(o);
                    names.Add((o as CardObject).name);
                }
                else if (o is Ability)
                {
                    Ability a = o as Ability;
                    if (a.GetType() == typeof(Ability) || a.GetType() == typeof(ManaAbility))
                        pPlays.Add(o);
                    else if (!abilityTypes.Contains(a.GetType()))
                    {
                        pPlays.Add(o);
                        abilityTypes.Add(a.GetType());
                    }
                }
                else if(o is AlternateLocationObject && !names.Contains((o as AlternateLocationObject).card.name))
                {
                    pPlays.Add(o);
                    names.Add((o as AlternateLocationObject).card.name);
                }
                else if (o is AlternateCastingObject)
                    pPlays.Add(o);
            }

            pPlays.Add("Do Nothing");

            return pPlays;
        }

        public class GetPossiblePlaysEventArgs : EventArgs
        {
            public Player player;
            private List<object> playsToAdd = new List<object>();

            public GetPossiblePlaysEventArgs(Player p)
            {
                player = p;
            }

            public void AddPlay(object play)
            {
                playsToAdd.Add(play);
            }
            public List<object> GetPlays() { return playsToAdd; }
        }

        public List<String> DuplicateManaPool(List<String> manaP)
        {
            List<String> list = new List<string>();
            foreach (String s in manaP)
            {
                list.Add(s);
            }
            return list;
        }

        public virtual void TakeDamage(int amount)
        {
            if (!CanBeDamaged()) return;
            health -= amount;
            if (health <= 0 && !CanHealthBeReducedToZero())
            {
                //Set health based on the effect preventing it from being reduced from zero. (Usually 1)
            }
            if (health <= 0 && CanLose())
                Lose(name + "'s health has been reduced to zero.");
            Program.windowInstance.UpdateGameConsole();
        }

        public virtual void Heal(int amount)
        {
            if (!CanBeHealed()) return;
            health += amount;
            Program.windowInstance.UpdateGameConsole();
        }

        public virtual bool CanBeHealed()
        {
            return true;
        }

        public virtual bool CanLose()
        {
            return true;
        }

        public virtual bool CanHealthBeReducedToZero()
        {
            return true;
        }

        public virtual bool CanBeDamaged()
        {
            return true;
        }

        public List<String> GetManaPotential()
        {
            List<String> manaP = new List<String>();
            manaP.AddRange(manaPool);
            foreach (CardObject c in battlefield)
            {
                foreach (Ability a in c.abilities)
                {
                    if (a is ManaAbility)
                    {
                        if (a.cost == "T" && c.CanTap())
                        {
                            ManaAbility m = a as ManaAbility;
                            foreach (String s in m.manaProduced.Split(','))
                            {
                                if (int.TryParse(s, out int v))
                                {
                                    for (int i = 0; i < v; i++)
                                    {
                                        manaP.Add("C");
                                    }
                                }
                                else
                                {
                                    manaP.Add(s);
                                }
                            }
                        }
                    }
                }
            }
            return manaP;
        }

        public virtual void Lose(String reason)
        {
            Program.windowInstance.game.RemovePlayer(reason, this);
        }

        public void DrawCard(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                if (library.Count == 0 && GameRuleHandler.HandleGameRuleMill(this))
                {
                    Lose(name + " Has milled themselves!");
                    return;
                }
                hand.Add(library[0]);
                library.RemoveAt(0);
            }
        }

        public virtual void RevealCard(CardObject card)
        {
            Program.windowInstance.selectedObject = card;
            Program.windowInstance.UpdateInspector();
            Program.windowInstance.game.NotifyPlayer("Reveal", name + " is revealing " + card.name + ". See the inspector:");
        }

        public void ShuffleLibrary()
        {
            ShuffleCards(library);
        }

        public List<CardObject> ShuffleCards(List<CardObject> cards)
        {
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = Game.rand.Next(n + 1);
                CardObject c = cards[k];
                cards[k] = cards[n];
                cards[n] = c;
            }
            return cards;
        }

        public void PlayLand()
        {
            while (landPlayed < turnLandCap)
            {
                bool flag = false; //If this is still false at the end of this loop, that means there are no land cards in the player's hand.
                foreach (CardObject c in hand)
                {
                    if(c.cardTypes.Contains("Land"))
                    {
                        EnterTheBattlefield(c);
                        hand.Remove(c);
                        landPlayed++;
                        flag = true;
                        break;
                    }
                }
                if(landPlayed>=turnLandCap || !flag)
                    break;
            }
        }

        public void Scry(int amount)
        {
            List<CardObject> viewedCards = new List<CardObject>();
            for (int i = 0; i < amount; i++)
            {
                if (i < library.Count)
                    viewedCards.Add(library[i]);
            }
            library.RemoveRange(0, amount);
            ShuffleCards(viewedCards);
            foreach (CardObject c in viewedCards)
            {
                if (Game.rand.Next(0, 2) == 1)
                    library.Insert(0, c);
                else
                    library.Add(c);
            }
        }

        public void EnterTheBattlefield(CardObject c, bool simulation)
        {
            battlefield.Add(c);
            if(!simulation)
                Program.windowInstance.UpdateGameConsole();
            //Trigger Enter the battlefield effect(s) on the card and Trigger When a ____ enters the battlefield... effects.
            c.ETB();
            if(c is CreatureBase)
            {
                c.hasSummonSickness = (c as CreatureBase).HasSummonSickness();
            }
            Program.windowInstance.game.CardEnteredBattlefield(c);
        }
        public void EnterTheBattlefield(CardObject c)
        {
            EnterTheBattlefield(c, false);
        }

        public void InstantiateToken(CardObject token)
        {
            CardObject t = (CardObject)Activator.CreateInstance(token.GetType());
            t.controller = this;
            t.owner = this;
            EnterTheBattlefield(t);
        }

        public bool IsCardOnBattleField(CardObject c)
        {
            if (battlefield.Contains(c))
                return true;
            return false;
        }
    }
}

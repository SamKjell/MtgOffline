using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class Simulation
    {
        public Game game;
        public SimulantPlayer player;
        public List<SimulantPlayer> opponents = new List<SimulantPlayer>();
        public Player p;
        public int bestScore = -2;
        public List<int> bestNodes = new List<int>();
        public bool isResultLoss = false;
        public Simulation(Player p, Game g)
        {
            game = g;
            this.p = p;
            ResetSimulation();
        }

        public List<int> StartSimulation()
        {
            RunSimulantLayer(new List<int>(), player);
            if (bestScore < 0)
            {
                //Resets the simulation so that no action were performed.
                //Then, tries to simulant the AI doing nothing.
                //If the resulting score results in a higher score, then the AI
                //will not act during it's turn to prevent itself from losing.
                ResetSimulation();
                CalculateScore(new List<int>(),player);
            }
            return bestNodes;
        }
        void ResetSimulation()
        {
            player = new SimulantPlayer(this);//p.name, p.health, p.deck, false,this, p.bot);
            player.battlefield = CloneCards(p.battlefield);
            player.hand = CloneCards(p.hand);
            player.graveyard = CloneCards(p.graveyard);
            player.library = CloneCards(p.library);
            player.health = p.health;
            player.exile = CloneCards(p.exile);

            foreach (Player pl in Program.windowInstance.game.players)
            {
                if (pl != p)
                {
                    SimulantPlayer simulant = new SimulantPlayer(this);// pl.name, pl.health, pl.deck, false, this, pl.bot);
                    simulant.battlefield = CloneCards(pl.battlefield,simulant);
                    simulant.hand = CloneCards(pl.hand, simulant);
                    simulant.graveyard = CloneCards(pl.graveyard, simulant);
                    simulant.library = CloneCards(pl.library, simulant);
                    simulant.health = pl.health;
                    simulant.exile = CloneCards(pl.exile, simulant);

                    opponents.Add(simulant);
                }
            }
        }
        public static List<T> CloneList<T>(List<T> list)
        {
            List<T> l = new List<T>();
            foreach (T t in list)
            {
                l.Add(t);
            }
            return l;
        }
        public List<CardObject> CloneCards(List<CardObject> cards)
        {
            List<CardObject> l = new List<CardObject>();
            foreach (CardObject card in cards)
            {
                CardObject c = card.SimulationCloneCard(player);
                c.owner = player;
                //c.controller = player;
                l.Add(c);
            }
            return l;
        }
        public List<CardObject> CloneCards(List<CardObject> cards, Player controller)
        {
            List<CardObject> l = new List<CardObject>();
            foreach (CardObject card in cards)
            {
                CardObject c = card.SimulationCloneCard(controller);
                c.owner = player;
                //c.controller = controller;
                l.Add(c);
            }
            return l;
        }
        void CalculateScore(List<int> nodes, Player p)
        {
            int currentScore = p.battlefield.Count;
            foreach (CardObject c in p.battlefield)
            {
                if(c is CreatureBase)
                {
                    currentScore += (c as CreatureBase).GetPower();
                }
            }
            foreach(CardObject c in p.graveyard)
            {
                if(c is SorceryBase)
                {
                    currentScore+=ManaUtil.CMC(c.manaCost);
                }
            }
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                bestNodes = nodes;
            }
            
        }
        void CalculateLossScore(List<int> nodes, Player p)
        {
            int currentScore = -1;
            if(currentScore > bestScore)
            {
                bestScore = currentScore;
                bestNodes = nodes;
            }
            //Sets the flag = to false because it has tried to avoid the threat. 
            isResultLoss = false;
        }

        void RunSimulantLayer(List<int> nodeData, Player playerState)
        {
            if (isResultLoss)
            {
                CalculateLossScore(nodeData, playerState);
                return;
            }
            List<object> possiblePlays = playerState.GetPossiblePlays(playerState.manaPool);
            if (possiblePlays.Count == 0)
            {
                CalculateScore(nodeData,playerState);
                return;
            }
            for (int i = 0; i < possiblePlays.Count; i++)
            {
                Player pl = new SimulantPlayer(this);//playerState.name, playerState.health, playerState.deck, false, this);
                List<int> nd = CloneList(nodeData);
                pl.battlefield = CloneCards(playerState.battlefield,pl);
                pl.hand = CloneCards(playerState.hand, pl);
                pl.graveyard = CloneCards(playerState.graveyard, pl);
                pl.library = CloneCards(playerState.library, pl);
                pl.health = playerState.health;
                pl.exile = CloneCards(playerState.exile, pl);
                pl.landPlayed = playerState.landPlayed;
                pl.manaPool = CloneList(playerState.manaPool);

                if(possiblePlays[i] is AlternateCastingObject)
                {
                    AlternateCastingObject alt = possiblePlays[i] as AlternateCastingObject;
                    alt.source = alt.source.SimulationCloneCard(alt.source.controller);
                    possiblePlays[i] = alt.GetAlternateCardForPlay();
                }
                
                if (possiblePlays[i] is string)
                {
                    //Do Nothing
                    CalculateScore(nodeData, playerState);
                    return;
                }

                if(possiblePlays[i] is AlternateLocationObject)
                {
                    AlternateLocationObject a = possiblePlays[i] as AlternateLocationObject;
                    pl.hand.Add(a.card.SimulationCloneCard(pl));
                    possiblePlays[i] = a.card;
                }

                if (possiblePlays[i] is CardObject)
                {
                    CardObject play = possiblePlays[i] as CardObject;
                    play.controller = pl;
                    bool isLand = (play is LandBase);
                    string newCost = play.controller.ReduceCost(play, play.manaCost);
                    if (!isLand)
                        pl.FillManaPool(newCost);
                    if (isLand || ManaUtil.CMC(newCost)==0|| pl.DrainManaPool(newCost.Split(',').ToList()))
                    {
                        if (play.isPermanent)
                        {
                            if (play is AuraBase)
                            {
                                object target = (play as AuraBase).GetTarget();
                                if (target == null)
                                {
                                    play.controller.graveyard.Add(play);
                                }
                                else
                                {
                                    play.isAttatched = true;
                                    if (target is CardObject)
                                    {
                                        CardObject targetCard = target as CardObject;
                                        targetCard.EquipAura(play as AuraBase);
                                        pl.EnterTheBattlefield(play);
                                    }
                                }
                            }
                            else
                            {
                                pl.EnterTheBattlefield(play, false);
                                if (play is LandBase)
                                    pl.landPlayed++;
                            }

                        }
                        else
                            (play as SpellBase).Resolve();
                        pl.hand.Remove(pl.hand.Find(x=>x.id==play.id));
                        nd.Add(i);
                        RunSimulantLayer(nd, pl);
                    }
                }
                else if(possiblePlays[i] is Ability)
                {
                    Ability play = possiblePlays[i] as Ability;
                    play.SimSetController(pl);
                    List<String> cost = play.cost.Split(',').ToList();
                    if (play.CanActivate())
                    {
                        if (cost.Contains("T") && play.source.CanTap())
                        {
                            play.source.Tap();
                            cost.Remove("T");
                        }
                        pl.FillManaPool(play.cost);
                        if (pl.DrainManaPool(cost) && play.AdditionalCosts())
                        {
                            pl.ActivateAbility(play);
                            nd.Add(i);
                            RunSimulantLayer(nd, pl);
                        }
                    }
                }
            }
        }
    }
}

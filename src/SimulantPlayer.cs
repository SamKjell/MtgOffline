using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class SimulantPlayer : Player
    {
        public static int IDCounter;
        public int ID = 0;
        Simulation sim;
        public SimulantPlayer(Simulation sim)
        {
            this.sim = sim;
            ID = IDCounter;
            IDCounter++;
        }
        public override void Lose(string reason)
        {
            sim.isResultLoss = true;
        }
        public override void TriggerAbility(StackObject ability)
        {
            ability.Resolve();
        }

        public override void ActivateAbility(Ability ability)
        {
            if (ability is ManaAbility)
                ability.Activate();
            else
            {
                ability.AdditionalActions();
                ability.effect.Resolve();
            }
        }

        public override List<CardObject> GetAllCreatures(bool opponentsOnly)
        {
            return new List<CardObject>();
        }

        public override void RevealCard(CardObject card)
        {
            //This prevent the simulation from revealing cards to the player while it is iterating through possible plays.
        }

        public override string ReduceCost(CardObject card, string cost)
        {
            foreach (CardObject c in GetAllSimulantCards().FindAll(x => x is ICostReducer))
            {
                ICostReducer reducer = c as ICostReducer;
                cost = reducer.GetReducedCost(card, cost);
            }
            return cost;
        }

        private List<CardObject> GetAllSimulantCards()
        {
            List<CardObject> cards = new List<CardObject>();
            List<SimulantPlayer> players = new List<SimulantPlayer>(sim.opponents);
            players.Add(this);
            foreach (Player player in players)
                cards.AddRange(player.battlefield);

            return cards;
        }

        public override CardObject ChooseTargetOnBattlefield(Predicate<CardObject> targetPred, AssessmentModel model)
        {
            CardObject target = null;
            int bestScore = 0;
            List<SimulantPlayer> players = new List<SimulantPlayer>(sim.opponents);
            players.Add(this);
            foreach (Player p in players) 
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

        public override CardObject ChooseTargetCreatureOnBattlefield(bool opponent)
        {
            CardObject target = null;
            int bestScore = 0;
            if (opponent) //Targeting opponents
            {
                foreach (Player p in sim.opponents)
                {
                    foreach (CardObject c in p.battlefield)
                    {
                        if (c is CreatureBase)
                        {
                            int score = AssessCreatureThreat(c as CreatureBase);
                            if (score > bestScore)
                            {
                                target = c;
                                bestScore = score;
                            }
                        }
                    }
                }
            }
            return target;
        }

        public override CardObject ChooseTargetPermanentOnBattlefield(bool opponentOnly,List<CardObject> invalidTargets = null)
        {
            List<CardObject> possibleTargets = new List<CardObject>();
            List<SimulantPlayer> players = sim.opponents;
            players.Add(this);
            foreach (Player p in players)
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

        public override Player ChooseTargetPlayer(bool opponent)
        {
            List<Player> p = new List<Player>();
            List<SimulantPlayer> options = sim.opponents;
            options.Add(this);
            foreach (Player option in options)
            {
                if (opponent && option != this)
                    p.Add(option);
                else if (!opponent)
                    p.Add(option);
            }
            return p[Game.rand.Next(0, p.Count)];
        }
    }
}

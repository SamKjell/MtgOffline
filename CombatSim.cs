using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class CombatSim
    {
        List<AttackData> bestBlocks = new List<AttackData>();
        List<AttackData> bestAttacks = new List<AttackData>();
        int bestScore = -1000;

        Player simulator;

        public CombatSim(Player player)
        {
            simulator = player;
        }

        public List<AttackData> GetBestBlocks(List<AttackData> data, List<CreatureBase> blockers,int health)
        {
            //Score parameters: # of remaining blockers. # of destroyed attackers. Amount of health lost.
            DummyPlayer d = new DummyPlayer();
            d.health = health;
            List<AttackData> cloneData = Simulation.CloneList(data);
            foreach (AttackData cD in cloneData)
            {
                cD.target = d;
                if (cD.creature.controller is DummyPlayer)
                    d.actualPlayer = (cD.creature.controller as DummyPlayer).actualPlayer;
                else
                    d.actualPlayer = cD.creature.controller;
            }
            GenerateDefenseSimulationTree(cloneData, CloneCards(blockers), d);
            return bestBlocks;
        }

        void CalculateDefenseScore(List<AttackData> attackData,DummyPlayer d)
        {
            List<AttackData> data = Simulation.CloneList(attackData);
            if (data.Count == 0)
                return;
            int initialHP = d.health;
            int deadAttackers = 0;
            int deadBlockers = 0;
            DummyPlayer graveDummy = new DummyPlayer();
            foreach (AttackData attack in data)
            {
                attack.creature = (CreatureBase)attack.creature.SimulationCloneCard(d);
                foreach(AuraBase aura in attack.creature.auras)
                    aura.owner = graveDummy;
                attack.creature.owner = graveDummy;
                List<CreatureBase> cloneBlockers = new List<CreatureBase>();
                foreach (CreatureBase c in attack.blockers)
                {
                    CreatureBase creatureB = (CreatureBase)c.SimulationCloneCard(d);
                    creatureB.owner = graveDummy;
                    foreach (AuraBase aura in creatureB.auras)
                        aura.owner = attack.creature.owner;
                    cloneBlockers.Add(creatureB);
                }
                attack.blockers = cloneBlockers;
                
                if (attack.blockers.Count == 0)
                {
                    int cI = attack.creature.GetCombatInitiative();
                    int multi = (cI > 0) ? cI : 1;
                    int damage = attack.creature.GetPower() * multi;
                    d.TakeDamage(damage);
                    if (attack.creature.HasLifelink())
                        attack.creature.controller.Heal(damage);
                }
                else if (attack.blockers.Count == 1)
                {
                    int trampleDamage = attack.creature.Fight(attack.blockers[0],true,out bool attackerDied, out bool blockerDied);
                    if (attackerDied)
                        deadAttackers++;
                    if (blockerDied)
                        deadBlockers++;
                    if (attack.creature.HasTrample() && trampleDamage > 0)
                        d.TakeDamage(trampleDamage);
                }
                else if (attack.blockers.Count > 1)
                {
                    //Multi-blocking. Not yet supported.
                    throw new Exception("Multi-Blocking is not supposed to be occuring.");
                }
            }
            int score = d.health + deadAttackers - deadBlockers;
            if (score > bestScore)
            {
                bestScore = score;
                List<AttackData> newData = new List<AttackData>();
                foreach (AttackData aD in data)
                {
                    //The creatures on the battlefield for both blockers and attackers need to be synced.
                    //aD.creature = (CreatureBase)Player.GetCardByID(aD.creature.id, simulator.battlefield);
                    AttackData newD = new AttackData((CreatureBase)Player.GetCardByID(aD.creature.id, (aD.creature.controller as DummyPlayer).actualPlayer.battlefield), simulator);
                    foreach (CreatureBase blocker in aD.blockers)
                        newD.blockers.Add((CreatureBase)Player.GetCardByID(blocker.id,simulator.battlefield));

                    newData.Add(newD);
                }
                bestBlocks = newData;
            }
            d.health = initialHP;
        }

        void GenerateDefenseSimulationTree(List<AttackData> data, List<CreatureBase> blockers,DummyPlayer health)
        {
            foreach (CreatureBase blocker in blockers)
            {
                //Do nothing/Don't block
                List<CreatureBase> newBlockers = CloneCards(blockers);
                newBlockers.RemoveAt(blockers.IndexOf(blocker));
                GenerateDefenseSimulationTree(Simulation.CloneList(data), newBlockers, health);
                foreach (AttackData attack in data)
                {
                    bool canBlock = (attack.creature.HasFlying()) ? blocker.HasFlying() : true;
                    if (attack.blockers.Count == 0 && canBlock)
                    {
                        foreach (AttackData a in data)
                        {
                            //Extra code will go here to handle creatures that can block more than one creature.
                            a.blockers.RemoveAll(x => x.id == blocker.id);
                        }
                        attack.blockers.Add(blocker);
                        newBlockers = CloneCards(blockers);
                        newBlockers.RemoveAt(blockers.IndexOf(blocker));
                        GenerateDefenseSimulationTree(Simulation.CloneList(data), newBlockers, health);
                    }   
                }
            }
            CalculateDefenseScore(data, health);
        }

        public List<AttackData> GetBestAttacks(List<CreatureBase> possibleAttackers,List<Player>possibleTargets)
        {
            //Score parameters: # of destroyed blockers. # of remaining attackers. Total amount of health targets lost.
            List<DummyPlayer> targets = new List<DummyPlayer>();
            foreach (Player p in possibleTargets)
            {
                DummyPlayer d = new DummyPlayer();
                if (!(p is DummyPlayer))
                    d.actualPlayer = p;
                else
                    d.actualPlayer = (p as DummyPlayer).actualPlayer;
                d.health = p.health;
                List<CreatureBase> defenders= new List<CreatureBase>();
                foreach (CreatureBase c in p.GetCreaturesInLocation(p.battlefield))
                    defenders.Add(c);
                defenders = CloneCards(defenders);
                d.battlefield.AddRange(defenders);
                targets.Add(d);
            }
            GenerateAttackSimulationTree(CloneCards(possibleAttackers), targets);
            return bestAttacks;
        }

        void GenerateAttackSimulationTree(List<CreatureBase> attackers, List<DummyPlayer> targets)
        {
            List<AttackData> data = new List<AttackData>();
            List<CreatureBase> duplicateAttacks = new List<CreatureBase>();
            foreach (CreatureBase attacker in attackers)
            {
                //if (duplicateAttacks.Find(x => CompareCreatures(x, attacker)) != null)
                //    continue;
                //duplicateAttacks.Add(attacker);

                //Simulate not attacking:
                List<CreatureBase> newAttackers = CloneCards(attackers);
                newAttackers.RemoveAt(attackers.IndexOf(attacker));
                GenerateAttackSimulationTree(newAttackers, targets);


                foreach (DummyPlayer p in targets)
                {
                    data.Add(new AttackData(attacker, p));
                    newAttackers = CloneCards(attackers);
                    newAttackers.RemoveAt(attackers.IndexOf(attacker));
                    GenerateAttackSimulationTree(newAttackers, targets);
                }
            }
            CalculateAttackScore(data, targets);
            
        }

        bool CompareCreatures(CreatureBase a, CreatureBase b)
        {
            List<string> sA = b.GetStaticAbilities();
            foreach (string ability in a.GetStaticAbilities())
            {
                if (!sA.Contains(ability))
                    return false;
            }
            return a.GetPower() == b.GetPower() && a.GetToughness() == b.GetToughness() && a.name==b.name;
        }

        void CalculateAttackScore(List<AttackData> data,List<DummyPlayer>targets)
        {
            foreach (DummyPlayer target in targets)
            {
                List<AttackData> attacks = new List<AttackData>();
                foreach (AttackData aD in data)
                {
                    if (aD.target == target)
                        attacks.Add(aD);
                }
                //AI blocks
                List<CreatureBase> possibleBlockers = new List<CreatureBase>();
                foreach (CreatureBase c in target.GetCreaturesInLocation(target.battlefield))
                {
                    if (c.CanBlock())
                        possibleBlockers.Add(c);
                }
                //Initiate Simulation
                CombatSim sim = new CombatSim(target.actualPlayer);
                List<AttackData> blocks = sim.GetBestBlocks(attacks, possibleBlockers, target.health);
                CalculateSimulatedDefenseScore(blocks, target);
            }
        }

        void CalculateSimulatedDefenseScore(List<AttackData> attackData, DummyPlayer d)
        {
            List<AttackData> data = Simulation.CloneList(attackData);
            DummyPlayer graveDummy = new DummyPlayer();
            int initialHealth = d.health;
            int deadAttackers = 0;
            int deadBlockers = 0;
            foreach (AttackData attack in data)
            {
                attack.creature = (CreatureBase)attack.creature.SimulationCloneCard(attack.creature.controller);
                foreach (AuraBase aura in attack.creature.auras)
                    aura.owner = graveDummy;
                attack.creature.owner = graveDummy;
                List<CreatureBase> cloneBlockers = new List<CreatureBase>();
                foreach (CreatureBase c in attack.blockers)
                {
                    CreatureBase creatureB = (CreatureBase)c.SimulationCloneCard(d);
                    creatureB.owner = graveDummy;
                    foreach (AuraBase aura in creatureB.auras)
                        aura.owner = graveDummy;
                    cloneBlockers.Add(creatureB);

                }
                attack.blockers = cloneBlockers;

                if (attack.blockers.Count == 0)
                {
                    int cI = attack.creature.GetCombatInitiative();
                    int multi = (cI > 0) ? cI : 1;
                    int damage = attack.creature.GetPower() * multi;
                    d.TakeDamage(damage);
                    //if (attack.creature.HasLifelink())
                    //    attack.creature.controller.Heal(damage);
                }
                else if (attack.blockers.Count == 1)
                {
                    int trampleDamage = attack.creature.Fight(attack.blockers[0],true,out bool deadAttacker, out bool deadBlocker);
                    if (deadAttacker)
                        deadAttackers++;
                    if (deadBlocker)
                        deadBlockers++;
                    if (attack.creature.HasTrample() && trampleDamage > 0)
                        d.TakeDamage(trampleDamage);
                }
                else if (attack.blockers.Count > 1)
                {
                    //Multi-blocking. Not yet supported.
                    throw new Exception("Multi-Blocking is not supposed to be occuring.");
                }
            }
            int score = -d.health+deadBlockers-deadAttackers;
            d.health = initialHealth;
            if (score > bestScore)
            {
                bestScore = score;
                List<AttackData> newData = new List<AttackData>();
                foreach (AttackData attackD in data)
                {
                    AttackData newD = new AttackData((CreatureBase)Player.GetCardByID(attackD.creature.id, simulator.battlefield),d.actualPlayer);
                    newData.Add(newD);
                    //attackD.target = d.actualPlayer;
                    //attackD.creature = 
                    //for (int i = 0; i < attackD.blockers.Count; i++)
                    //    attackD.blockers[i] = (CreatureBase)Player.GetCardByID(attackD.blockers[i].id, (attackD.creature.controller as DummyPlayer).actualPlayer.battlefield);
                }
                bestAttacks = newData;
            }
        }


        private List<CreatureBase> CloneCards(List<CreatureBase> cards)
        {
            List<CreatureBase> l = new List<CreatureBase>();
            DummyPlayer dummy = new DummyPlayer();
            foreach (CreatureBase card in cards)
            {
                if (!(card.controller is DummyPlayer))
                    dummy.actualPlayer = card.controller;
                else
                    dummy.actualPlayer = (card.controller as DummyPlayer).actualPlayer;
                CreatureBase c = (CreatureBase)card.SimulationCloneCard(dummy);
                c.owner = dummy;
                foreach (AuraBase aura in c.auras)
                    aura.owner = dummy;
                l.Add(c);
            }
            return l;
        }
    }

    //Solely for the purpose of suppressing events
    public class DummyPlayer : Player
    {
        public Player actualPlayer;

        public override void Lose(string reason)
        {   
        }
        public override void ActivateAbility(Ability ability)
        {   
        }
        public override void TriggerAbility(StackObject ability)
        {
        }

        public override List<CardObject> GetBattlefield()
        {
            return actualPlayer.GetBattlefield();
        }
    }
}

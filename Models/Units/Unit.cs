using SmallWorld.Models.Interfaces;
using SmallWorld.Models.Tiles;
using System;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Units
{
    public enum FightIssue { Lose = -1, Draw = 0, Win = 1 };

    [DataContract()]
    public abstract class Unit : IUnit
    {
        protected static Random randomizer = new Random();

        [DataMember]
        public int MaxHealthPoints { get; protected set; }

        [DataMember]
        public int HealthPoints { get; protected set; }

        [DataMember]
        public int MaxMovementPoints { get; protected set; }

        [DataMember]
        public double MovementPoints { get; protected set; }

        [DataMember]
        public double DefaultMovementCost { get; protected set; }

        [DataMember]
        public int AttackPoints { get; protected set; }

        [DataMember]
        public int DefensePoints { get; protected set; }

        [DataMember]
        public int Score { get; protected set; }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext c)
        {
            randomizer = new Random();
        }

        public Unit()
        {
            MaxHealthPoints = HealthPoints = 5;
            MovementPoints = MaxMovementPoints = 1;
            DefaultMovementCost = 1;
            AttackPoints = 2;
            DefensePoints = 1;
            Score = 1;
        }

        public bool IsDead()
        {
            return HealthPoints == 0;
        }

        public bool IsAlive()
        {
            return HealthPoints > 0;
        }

        private double GetRealAttack()
        {
            return (double)AttackPoints * ((double)HealthPoints / (double)MaxHealthPoints);
        }

        private double GetRealDefense()
        {
            return (double)DefensePoints * ((double)HealthPoints / (double)MaxHealthPoints);
        }

        public FightIssue Fight(Unit defender)
        {
            Unit attacker = this;

            int currentRound = 1;
            int minRound = 3;
            int maxRound = minRound + Math.Max(attacker.HealthPoints, defender.HealthPoints);
            int totalRound = randomizer.Next(minRound, maxRound);

            double baseChanceToLose = 0.5;

            while (currentRound <= totalRound && attacker.IsAlive() && defender.IsAlive())
            {
                double realAttackAttacker = attacker.GetRealAttack();
                double realDefenseDefender = defender.GetRealDefense();
                double balanceOfPower;
                double chanceToLose;

                // Fighting random chance
                if (realAttackAttacker < realDefenseDefender)
                {
                    balanceOfPower = 1 - (realAttackAttacker / realDefenseDefender);
                    chanceToLose = baseChanceToLose * (1 + balanceOfPower);
                }
                else
                {
                    balanceOfPower = 1 - (realDefenseDefender / realAttackAttacker);
                    chanceToLose = baseChanceToLose * (1 - balanceOfPower);
                }

                double random = randomizer.NextDouble();

                // Attacker lose one health point
                if (random < chanceToLose)
                {
                    attacker.HealthPoints--;
                }
                // Defender lose one health point
                else
                {
                    defender.HealthPoints--;
                }

                // Next round
                currentRound++;
            }

            // Before attacker died
            if (attacker.IsDead())
            {
                attacker.BeforeDying();
            }
            // Before defender died
            else if (defender.IsDead())
            {
                defender.BeforeDying();
            }

            // Check again, because, before dying, some unit may resurrected (e.g: the elf unit)

            // Attacker died => Lose
            if (attacker.IsDead())
            {
                // After defender kills
                defender.AfterKill();
                return FightIssue.Lose;
            }
            // Defender died => Win
            else if (defender.IsDead())
            {
                // After attacker kills
                attacker.AfterKill();
                return FightIssue.Win;
            }
            // No unit died => Draw
            else
            {
                return FightIssue.Draw;
            }
        }

        public void Move(Tile origin, Tile destination, bool areAdjacent)
        {
            if (CanMove(origin, destination, areAdjacent))
            {
                MovementPoints -= GetMovementCost(origin, destination);
            }
        }

        public void Pass()
        {
            MovementPoints = 0;
        }

        public void ResetMovementPoints()
        {
            MovementPoints = MaxMovementPoints;
        }

        public virtual bool CanMove(Tile origin, Tile destination, bool areAdjacent)
        {
            // By default the movement is valide,
            // If the unit has anought movement points to move to the destination and if the tiles are adjacent
            // (but some units may redefine this, e.g: the orc unit)
            return MovementPoints >= GetMovementCost(origin, destination) && areAdjacent;
        }

        public virtual double GetMovementCost(Tile origin, Tile destination)
        {
            // By default the movement cost is one (but some units may redefine this, e.g: the orc unit)
            return DefaultMovementCost;
        }

        public virtual void BeforeDying()
        {
            // Nothing by default (but some units may redefine this, e.g: the elf unit)
        }

        public virtual void AfterKill()
        {
            // Nothing by default (but some units may redefine this, e.g: the orc unit)
        }
    }
}

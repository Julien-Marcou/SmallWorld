using SmallWorld.Models.Tiles;
using SmallWorld.Models.Units;

namespace SmallWorld.Models.Interfaces
{
    interface IUnit
    {
        int MaxHealthPoints { get; }

        int HealthPoints { get; }

        int MaxMovementPoints { get; }

        double MovementPoints { get; }

        double DefaultMovementCost { get; }

        int AttackPoints { get; }

        int DefensePoints { get; }

        int Score { get; }

        bool IsDead();

        bool IsAlive();

        FightIssue Fight(Unit defender);

        void Move(Tile origin, Tile destination, bool areAdjacent);

        void Pass();

        void ResetMovementPoints();

        bool CanMove(Tile origin, Tile destination, bool areAdjacent);

        double GetMovementCost(Tile origin, Tile destination);

        void BeforeDying();

        void AfterKill();
    }
}

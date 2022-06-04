using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UlearnGameTowerDefence.GameModel.Operations;

namespace UlearnGameTowerDefence.GameModel
{
    public class Projectile
    {
        public int Speed { get; }
        public int Damage { get; }
        public int TicksToHit { get; set; }
        public Point Position { get; set; }
        public double DirectionAngle { get; set; }

        public Point TargetPos { get; set; }
        public Enemy EnemyTarget { get; set; }

        public TowerType Type { get; }

        public Bitmap Texture { get; }

        public Projectile(TowerType type, Point pos, Enemy enemyTarget)
        {
            switch (type)
            {
                case TowerType.Cannon:
                    Damage = 10;
                    TicksToHit = 15;
                    Texture = new Bitmap(@"C:\Users\di-pl\Desktop\proga\game\UlearnGameTowerDefence\Resources\Towers\Cannon\Projectile.png");
                    break;

                case TowerType.BombTower:
                    Damage = 20;
                    TicksToHit = 20;
                    Texture = new Bitmap(@"C:\Users\di-pl\Desktop\proga\game\UlearnGameTowerDefence\Resources\Towers\Magic\Projectile.png");
                    break;

                case TowerType.AirStrike:
                    Damage = 5;
                    TicksToHit = 5;
                    break;
            }

            EnemyTarget = enemyTarget;
            TargetPos = enemyTarget.Position;
            Type = type;
            Position = pos;
            Speed = (int)DistanceBetween(Position, enemyTarget.LocationAfterTicks(TicksToHit)) / TicksToHit;
            DirectionAngle = AngleFromTo(Position, TargetPos);
        }

        public Projectile(TowerType type)
        {
            switch (type)
            {
                case TowerType.Cannon:
                    Damage = 10;
                    TicksToHit = 6;
                    Texture = new Bitmap(@"C:\Users\di-pl\Desktop\proga\game\UlearnGameTowerDefence\Resources\Towers\Cannon\Projectile.png");
                    break;

                case TowerType.BombTower:
                    Damage = 20;
                    TicksToHit = 12;
                    break;

                case TowerType.AirStrike:
                    Damage = 5;
                    TicksToHit = 3;
                    break;
            }
        }

        private Point LocationAfterTicks(int ticks)
        {
            var x = (int)Math.Round(Position.X + (Speed * ticks * Math.Cos(DirectionAngle)));
            var y = (int)Math.Round(Position.Y + (Speed * ticks * Math.Sin(DirectionAngle)));
            return new Point(x, y);
        }

        public void Move()
        {
            Position = LocationAfterTicks(1);
            TicksToHit--;
        }
    }
}
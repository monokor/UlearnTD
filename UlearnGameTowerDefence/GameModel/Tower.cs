using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UlearnGameTowerDefence.GameModel.Operations;

namespace UlearnGameTowerDefence.GameModel
{
    public class Tower
    {
        public double Radius { get; }
        private int ShootingCooldownTicks = 0;
        public int currentShootingCooldownTicks = 0;
        public Point Position { get; }
        public TowerType Type { get; }
        public Bitmap Texture { get; set; }
        public Projectile Projectile { get; }


        public Tower(TowerType type, Point point)
        {
            this.Type = type;
            this.Position = point;
            this.Radius = 500;
            this.Projectile = new Projectile(type);
            switch (type)
            {
                case TowerType.Cannon:
                    Texture = new Bitmap(@"C:\Users\di-pl\Desktop\proga\game\UlearnGameTowerDefence\Resources\Towers\Cannon\Building.png");
                    ShootingCooldownTicks = 10;
                    break;

                case TowerType.BombTower:
                    Texture = new Bitmap(@"C:\Users\di-pl\Desktop\proga\game\UlearnGameTowerDefence\Resources\Towers\Magic\Building.png");
                    ShootingCooldownTicks = 15;
                    break;
            }
        }

        public Enemy SearchForEnemy(List<Enemy> enemiesOnMap)
        {
            if (currentShootingCooldownTicks == 0)
            {
                foreach (var enemy in enemiesOnMap)
                {
                    if (enemy.WillGetDamage < enemy.Health && DistanceBetween(Position, enemy.Position) <= Radius)
                    {
                        //enemy.TimesToTakeDamages.Add(this.Projectile.TicksToHit, this.Projectile.Damage);
                        currentShootingCooldownTicks = ShootingCooldownTicks;
                        return enemy;
                    }
                }
            }
            else
                currentShootingCooldownTicks--;
            return null;
        }
    }
}

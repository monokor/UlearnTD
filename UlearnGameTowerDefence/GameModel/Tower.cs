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
                    Texture = new Bitmap(Application.StartupPath + @"..\..\..\Resources\Towers\Cannon\Building.png");
                    ShootingCooldownTicks = 10;
                    break;

                case TowerType.Crossbow:
                    Texture = new Bitmap(Application.StartupPath + @"..\..\..\Resources\Towers\Crossbow\Building.png");
                    ShootingCooldownTicks = 15;
                    break;
            }
        }

        public Enemy SearchForEnemy(List<Enemy> enemiesOnMap)
        {
            if (currentShootingCooldownTicks == 0) //если выстрел готов
            {
                foreach (var enemy in enemiesOnMap) //смотрим с близжайших к финишу
                {
                    if (enemy.WillGetDamage < enemy.Health && DistanceBetween(Position, enemy.Position) <= Radius) //если он не умрет от уже существующих пуль и попадает в радиус
                    {
                        //enemy.TimesToTakeDamages.Add(this.Projectile.TicksToHit, this.Projectile.Damage);
                        currentShootingCooldownTicks = ShootingCooldownTicks;
                        return enemy; //возвращаем левелу, он создаст снаряд
                    }
                }
            }
            else
                currentShootingCooldownTicks--;
            return null;
        }
    }
}

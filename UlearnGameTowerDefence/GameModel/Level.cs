using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UlearnGameTowerDefence.GameModel.Operations;

namespace UlearnGameTowerDefence.GameModel
{
    public class Level
    {
        public List<Enemy> Enemies { get; set; }
        private int EnemySpawnTicksCooldown;
        public List<Tower> Towers { get; set; }
        public List<Projectile> Projectiles { get; set; }
        public Map map;
        public int Money;
        private int MoneyTicksCooldown;

        public void Start(Map map)
        {
            this.map = map;
            Enemies = new List<Enemy>();
            
            //Enemies.Add(new Enemy(EnemyType.Simple, map));

            Towers = new List<Tower>();
            //Towers.Add(new Tower(TowerType.Cannon, map.TowerSlots[0]));
            //Towers.Add(new Tower(TowerType.Cannon, map.TowerSlots[1]));
            //Towers.Add(new Tower(TowerType.Cannon, map.TowerSlots[2]));

            Projectiles = new List<Projectile>();
            Money = 100;
            MoneyTicksCooldown = 0;

            EnemySpawnTicksCooldown = 20;
        }

        public void BeginAct()
        {
            if (EnemySpawnTicksCooldown <= 0)
            {
                Enemies.Add(new Enemy(EnemyType.Simple, map));
                Enemies.Add(new Enemy(EnemyType.Little, map));
                EnemySpawnTicksCooldown = 20;
            }
            EnemySpawnTicksCooldown--;

            if (MoneyTicksCooldown <= 0)
            {
                Money++;
                Money++;
                //MoneyTicksCooldown = 1;
            }
            MoneyTicksCooldown--;

            foreach (var enemy in Enemies)
            {
                enemy.Move();
            }


            var temp = Enemies
                .GroupBy(x => x.TargetNode)
                .Select(x => new
                {
                    TargetNode = x.Key,
                    Enemies = x.OrderBy(x => DistanceBetween(x.Position, x.TargetNode.Position)).ToList()
                })
                .OrderByDescending(x => x.TargetNode.Index)
                .SelectMany(x => x.Enemies)
                .ToList();
            Enemies = temp;


            foreach (var projectile in Projectiles.ToList())
            {
                projectile.Move();
                var thisEnemy = projectile.EnemyTarget;
                if (projectile.TicksToHit == 0)
                {
                    thisEnemy.TakeDamage(projectile.Damage);
                    Projectiles.Remove(projectile);
                }
                if (thisEnemy.Health <= 0)
                    Enemies.Remove(thisEnemy);
            }
            
            foreach (var tower in Towers)
            {
                var newTarget = tower.SearchForEnemy(Enemies);
                if (newTarget != null)
                {
                    Projectiles.Add(new Projectile(tower.Type, tower.Position, newTarget));
                    newTarget.WillGetDamage += tower.Projectile.Damage;
                }
            }
        }
    }
}

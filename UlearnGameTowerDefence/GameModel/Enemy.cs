using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UlearnGameTowerDefence.GameModel.Operations;

namespace UlearnGameTowerDefence.GameModel
{
    public class Enemy
    {
        public EnemyType Type { get; }
        public int Health { get; set; }
        public int Speed { get; }

        public Point Position { get; set; }
        public PathNode FromTargetNode { get; set; }
        public PathNode TargetNode { get; set; }
        private bool crossHisTarget = false;

        public Bitmap[] AnimationFrames { get; set; }
        public int AnimationFrame = 0;

        public int WillGetDamage = 0;

        public Map currentMap { get; set; }
        

        public Enemy(EnemyType type, Map map)
        {
            currentMap = map;
            Type = type;
            
            FromTargetNode = currentMap.PathNodes.First();
            TargetNode = FromTargetNode.NextNode;

            Position = FromTargetNode.Position;

            switch (type)
            {
                case EnemyType.Simple:
                    Health = 50;
                    Speed = 4;
                    AnimationFrames = new Bitmap[3]
                    {
                        new Bitmap(Application.StartupPath + @"..\..\..\Resources\Enemies\Simple\1.png"),
                        new Bitmap(Application.StartupPath + @"..\..\..\Resources\Enemies\Simple\2.png"),
                        new Bitmap(Application.StartupPath + @"..\..\..\Resources\Enemies\Simple\3.png"),
                    };
                    break;

                case EnemyType.Little:
                    Health = 20;
                    Speed = 8;
                    AnimationFrames = new Bitmap[1]
                    {
                        new Bitmap(Application.StartupPath + @"..\..\..\Resources\Enemies\Little\1.png"),
                    };
                    break;

                case EnemyType.Big:
                    Health = 75;
                    Speed = 7;
                    //texture
                    break;

                case EnemyType.Air:
                    Health = 20;
                    Speed = 20;
                    //texture
                    break;
            }

            var rnd = new Random();
            AnimationFrame = rnd.Next(AnimationFrames.Length);
        }
        public void NextAnimFrame()
        {
            AnimationFrame++;
            if (AnimationFrame >= AnimationFrames.Length)
                AnimationFrame = 0;
        }
        public Point LocationAfterTicks(int ticks)
        {
            var distanceToTargetNode = DistanceBetween(Position, TargetNode.Position);
            var ticksToTargetNode = distanceToTargetNode / Speed;
            var angle = AngleFromTo(Position, TargetNode.Position);
            if (ticksToTargetNode > ticks)
            {
                var x = (int)Math.Round(Position.X + (Speed * ticks * Math.Cos(angle)));
                var y = (int)Math.Round(Position.Y + (Speed * ticks * Math.Sin(angle)));
                return new Point(x, y);
            }
            else if (ticksToTargetNode == ticks)
            {
                crossHisTarget = true;
                return TargetNode.Position;
            }
            else
            {
                crossHisTarget = true;
                if (TargetNode == currentMap.PathNodes.Last.Value)
                    return TargetNode.Position;
                return LocationAfterTicks(ticks - ticksToTargetNode, TargetNode);
            }
        }

        private Point LocationAfterTicks(double ticksLeft, PathNode lastTarget)
        {
            var newTargetNode = currentMap.PathNodes.Find(lastTarget).Next.Value;
            var distanceToNextTargetNode = DistanceBetween(lastTarget.Position, newTargetNode.Position);
            var ticksToNextTargetNode = distanceToNextTargetNode / Speed;
            if (ticksToNextTargetNode > ticksLeft)
            {
                var x = (int)Math.Round(lastTarget.Position.X + (Speed * (int)ticksLeft * Math.Cos(lastTarget.DirectionAngleFromNode)));
                var y = (int)Math.Round(lastTarget.Position.Y + (Speed * (int)ticksLeft * Math.Sin(lastTarget.DirectionAngleFromNode)));
                return new Point(x, y);
            }
            else if (ticksToNextTargetNode == ticksLeft)
            {
                crossHisTarget = true;
                return newTargetNode.Position;
            }
            else
            {
                crossHisTarget = true;
                if (newTargetNode == currentMap.PathNodes.Last.Value)
                    return newTargetNode.Position;
                return LocationAfterTicks(ticksLeft - ticksToNextTargetNode, newTargetNode);
            }
        }

        public void Move()
        {
            crossHisTarget = false;

            Position = LocationAfterTicks(1);

            if (crossHisTarget)
            {
                TargetNode = TargetNode.NextNode;
            }
        }
        public void TakeDamage(int dmg)
        {
            WillGetDamage -= dmg; 
            Health -= dmg;
        }
    }
}
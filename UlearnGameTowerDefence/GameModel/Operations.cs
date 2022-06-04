using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlearnGameTowerDefence.GameModel
{
    public static class Operations
    {
        public static double DistanceBetween(Point first, Point second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2) + 
                             Math.Pow(first.Y - second.Y, 2));
        }

        public static double AngleFromTo(Point first, Point second)
        {
            var length = DistanceBetween(first, second);
            var angleR = Math.Acos((second.X - first.X) / length);

            //if (first.X < second.X)
            //    angleR += Math.PI;
            if (first.Y > second.Y)
                return -1 * angleR;

            return angleR;
        }

        //public static void Swap(Enemy enemy, Enemy nextEnemy, LinkedList<Enemy> list)
        //{
        //    LinkedListNode<Enemy> currentNode = list.Find(enemy);
        //    LinkedListNode<Enemy> nextNode = list.Find(nextEnemy);
        //    LinkedListNode<Enemy> nextnext = null;
        //    LinkedListNode<Enemy> prevprev = null;

        //    if (nextEnemy != list.Last.Value)
        //        nextnext = list.Find(nextEnemy).Next;
        //    if (enemy != list.First.Value)
        //        prevprev = list.Find(enemy).Previous;

        //    list.Remove(enemy);
        //    list.AddAfter
        //}
    }
}

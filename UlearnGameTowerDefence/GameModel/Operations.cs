using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlearnGameTowerDefence.GameModel
{
    public static class Operations //общие математические операции
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
    }
}

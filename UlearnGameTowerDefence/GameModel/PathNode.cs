using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UlearnGameTowerDefence.GameModel.Operations;

namespace UlearnGameTowerDefence.GameModel
{
    public class PathNode
    {
        public Point Position { get; }
        public PathNode NextNode { get; set; }
        public double DirectionAngleFromNode { get; set; }
        public int Index { get; set; }

        public PathNode(Point point)
        {
            Position = point;
        }

        public void CountAngle()
        {
            DirectionAngleFromNode = AngleFromTo(this.Position, NextNode.Position);
        }
    }
}

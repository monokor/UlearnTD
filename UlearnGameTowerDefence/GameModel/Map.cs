using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlearnGameTowerDefence.GameModel
{
    public class Map
    {
        public LinkedList<PathNode> PathNodes { get; }
        public Point[] TowerSlots { get; }

        public Bitmap Texture = new Bitmap(Application.StartupPath + @"..\..\..\Resources\Maps\testmapfull.png");

        public Map(LinkedList<PathNode> path, Point[] slots)
        {
            PathNodes = path;
            TowerSlots = slots;
            Initialize();
        }

        private void Initialize()
        {
            PathNode lastNode = null;
            var i = 0;
            foreach (var node in PathNodes)
            {
                node.Index = i;
                i++;

                if (node == PathNodes.First())
                {
                    lastNode = node;
                    continue;
                }
                
                lastNode.NextNode = node;
                lastNode.CountAngle();
                
                if (node == PathNodes.Last())
                {
                    node.NextNode = node;
                    node.DirectionAngleFromNode = 0;
                }

                lastNode = node;
            }
        }
    }
}

using UlearnGameTowerDefence.GameModel;
using System.Windows.Forms;

namespace UlearnGameTowerDefence
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.


            //создаем пути и слоты для башен, пока так
            var path = new LinkedList<PathNode>();
            //path.AddLast(new PathNode(new Point(0, 170)));
            //path.AddLast(new PathNode(new Point(120, 210)));
            //path.AddLast(new PathNode(new Point(210, 270)));
            //path.AddLast(new PathNode(new Point(470, 680)));
            //path.AddLast(new PathNode(new Point(570, 790)));
            //path.AddLast(new PathNode(new Point(670, 820)));
            //path.AddLast(new PathNode(new Point(760, 805)));
            //path.AddLast(new PathNode(new Point(810, 745)));
            //path.AddLast(new PathNode(new Point(740, 625)));
            //path.AddLast(new PathNode(new Point(785, 530)));
            //path.AddLast(new PathNode(new Point(930, 475)));
            //path.AddLast(new PathNode(new Point(1100, 480)));
            //path.AddLast(new PathNode(new Point(1325, 585)));
            //path.AddLast(new PathNode(new Point(1505, 655)));
            //path.AddLast(new PathNode(new Point(1650, 655)));
            //path.AddLast(new PathNode(new Point(1720, 600)));
            //path.AddLast(new PathNode(new Point(1715, 300)));
            //path.AddLast(new PathNode(new Point(1780, 170)));
            //path.AddLast(new PathNode(new Point(1900, 130)));

            path.AddLast(new PathNode(new Point(0, 530)));
            path.AddLast(new PathNode(new Point(255, 500)));
            path.AddLast(new PathNode(new Point(560, 450)));
            path.AddLast(new PathNode(new Point(630, 430)));
            path.AddLast(new PathNode(new Point(710, 360)));
            path.AddLast(new PathNode(new Point(760, 300)));
            path.AddLast(new PathNode(new Point(810, 262)));
            path.AddLast(new PathNode(new Point(880, 258)));
            path.AddLast(new PathNode(new Point(940, 290)));
            path.AddLast(new PathNode(new Point(988, 350)));
            path.AddLast(new PathNode(new Point(1080, 540)));
            path.AddLast(new PathNode(new Point(1170, 650)));
            path.AddLast(new PathNode(new Point(1260, 700)));
            path.AddLast(new PathNode(new Point(1360, 700)));
            path.AddLast(new PathNode(new Point(1530, 580)));
            path.AddLast(new PathNode(new Point(1580, 545)));
            path.AddLast(new PathNode(new Point(1630, 543)));
            path.AddLast(new PathNode(new Point(1704, 572)));
            path.AddLast(new PathNode(new Point(1790, 604)));
            path.AddLast(new PathNode(new Point(1920, 640)));

            var slots = new Point[6];
            //slots[0] = new Point(155, 375);
            //slots[1] = new Point(400, 395);
            //slots[2] = new Point(312, 612);
            //slots[3] = new Point(700, 740);
            //slots[4] = new Point(835, 605);
            //slots[5] = new Point(1212, 442);
            //slots[6] = new Point(1358, 707);
            //slots[7] = new Point(1632, 575);
            //slots[8] = new Point(1644, 352);
            //slots[9] = new Point(1808, 270);

            slots[0] = new Point(180, 616);
            slots[1] = new Point(515, 350);
            slots[2] = new Point(857, 394);
            slots[3] = new Point(1241, 846);
            slots[4] = new Point(1609, 662);
            slots[5] = new Point(1834, 509);


            //создаем карту и уровень
            var gamemap = new Map(path, slots);
            var level = new Level();
            level.Start(gamemap);

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(gamemap, level));
        }
    }
}
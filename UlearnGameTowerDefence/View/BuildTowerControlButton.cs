using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlearnGameTowerDefence.GameModel;

namespace UlearnGameTowerDefence.View
{
    public class BuildTowerControlButton : Button //кнопка, создающая башню конкретного типа
    {
        int SlotID;
        Level thisLevel;
        TowerType type;
        List<BuildTowerControlButton> currentBuildButtonsCollection; //список соседних таких кнопок, чтобы удалить все при нажатии

        public BuildTowerControlButton(int id, Level level, TowerType type, List<BuildTowerControlButton> list)
        {
            SlotID = id;
            thisLevel = level;
            Size = new Size(40, 40);
            this.type = type;
            this.currentBuildButtonsCollection = list;

            Bitmap texture = null;
            switch (type)
            {
                case TowerType.Cannon:
                    texture = new Bitmap(Application.StartupPath + @"..\..\..\Resources\Towers\Cannon\Building.png");
                    Image = new Bitmap(texture, new Size(40, 40));
                    break;

                case TowerType.BombTower:
                    texture = new Bitmap(Application.StartupPath + @"..\..\..\Resources\Towers\Magic\Building.png");
                    Image = new Bitmap(texture, new Size(40, 40));
                    break;
            }

            BackColor = Color.Transparent;
            FlatAppearance.BorderColor = Color.White;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            UseVisualStyleBackColor = false;
        }


        protected override void OnClick(EventArgs e)
        {
            if (thisLevel.Money >= 100)
            {
                thisLevel.Towers.Add(new Tower(type, thisLevel.map.TowerSlots[SlotID]));
                thisLevel.Money -= 100;
                foreach (var button in currentBuildButtonsCollection) //удаляем все соседние кнопки
                {
                    button.Visible = false;
                    button.Dispose();
                }
            }
        }
    }
}

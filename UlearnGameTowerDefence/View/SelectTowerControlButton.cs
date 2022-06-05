using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlearnGameTowerDefence.GameModel;

namespace UlearnGameTowerDefence.View
{
    public class SelectTowerControlButton : Button //кнопка при нажатии дает выбор типа башни
    {
        int SlotID;
        Level thisLevel;
        Form1 currentForm;

        public SelectTowerControlButton(int id, Level level, Form1 form)
        {
            SlotID = id;
            thisLevel = level;
            Size = new Size(60, 60);
            currentForm = form;

            Image = new Bitmap(Application.StartupPath + @"..\..\..\Resources\GUI\TowerSlotButton.png");

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
            var list = new List<BuildTowerControlButton>(); //список всех выпадающих кнопок

            //по кнопке на каждый тип башни
            var firstButton = new BuildTowerControlButton(SlotID, thisLevel, TowerType.Cannon, list);
            firstButton.Location = new Point(Location.X - Width * 3 / 4, this.Location.Y + Height / 10);
            var secondButton = new BuildTowerControlButton(SlotID, thisLevel, TowerType.Crossbow, list);
            secondButton.Location = new Point(Location.X + Width / 2, this.Location.Y + Height / 10);

            list.Add(firstButton);
            list.Add(secondButton);

            this.Visible = false;
            currentForm.Controls.Add(firstButton);
            currentForm.Controls.Add(secondButton);
        }
    }
}

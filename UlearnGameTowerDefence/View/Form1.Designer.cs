using UlearnGameTowerDefence.GameModel;
using System.Drawing;
using System.Windows.Forms;
using UlearnGameTowerDefence.View;

namespace UlearnGameTowerDefence
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        Map GameMap;
        Level thisLevel;
        PaintEventArgs paintEventArgs;
        List<SelectTowerControlButton> SelectButtons;
        double wCoef;
        double hCoef;
        int ticksCounter;
        bool isReadyTicks;

        private static int lastTick;
        private static int lastFrameRate;
        private static int frameRate;

        public static int CalculateFrameRate()
        {
            if (System.Environment.TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = System.Environment.TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }

        public Form1(Map map, Level level)
        {
            this.DoubleBuffered = true;
            GameMap = map;
            thisLevel = level;
            SelectButtons = new List<SelectTowerControlButton>();

            //таймер для тиков
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += TimerTick;
            timer.Start();

            ////для дебага, узнать сколько тиков в секунду
            //var fpsTimer = new System.Windows.Forms.Timer();
            //fpsTimer.Interval = 1000;
            //fpsTimer.Tick += ResetFramesCount;
            //fpsTimer.Start();


            ///тут я буду пытаться подогнать игру под все разрешения
            ///пока что желательно запускать все таки на 1920x1080

            //узнаем фактическое разрешение экрана
            var width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            wCoef = width / 1920;
            var height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            hCoef = height / 1080;

            //карта фоном с фактическим разрешением 
            this.BackgroundImage = new Bitmap(map.Texture, new Size((int)(map.Texture.Width * wCoef), (int)(map.Texture.Height * hCoef)));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //подстраиваем текстуры под фактическое разрешение
            foreach (var enemy in level.Enemies)
            {
                for (int i = 0; i < enemy.AnimationFrames.Count(); i++)
                {
                    enemy.AnimationFrames[i] = new Bitmap(enemy.AnimationFrames[i],
                                                          new Size((int)(enemy.AnimationFrames[i].Width * wCoef),
                                                                   (int)(enemy.AnimationFrames[i].Height * hCoef)));
                }
            }

            ////текстуры для башен, надо переписать
            //for (int i = 0; i < thisLevel.Towers.Count(); i++)
            //{
            //    thisLevel.Towers[i].Texture = new Bitmap(thisLevel.Towers[i].Texture,
            //                                          new Size((int)(thisLevel.Towers[i].Texture.Width * wCoef * 0.5),
            //                                                   (int)(thisLevel.Towers[i].Texture.Height * hCoef * 0.5)));
            //}

            ///
            ///


            //создаем кнопки для выбора типа башни на каждом слоте
            for (var i = 0; i < thisLevel.map.TowerSlots.Count(); i++)
            {
                var newButton = new SelectTowerControlButton(i, thisLevel, this);
                newButton.Location = new Point(thisLevel.map.TowerSlots[i].X - newButton.Width / 2, thisLevel.map.TowerSlots[i].Y - newButton.Height / 2);

                SelectButtons.Add(newButton);
                this.Controls.Add(newButton);
            }

            //полноэкранный режим
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            InitializeComponent();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            thisLevel.BeginAct();
            ticksCounter++;
            Invalidate();
        }

        private void ResetFramesCount(object sender, EventArgs e) //для подсчета тиков в секунду
        {
            isReadyTicks = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            paintEventArgs = e;
            //graphics.DrawImage(MapImg, new Point(0, 0));
            //graphics.FillRectangle(new TextureBrush(MapImg), new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));

            var pathPen = new Pen(Color.Red, 5);
            var slotPen = new Pen(Color.Green, 5);

            ////это можно раскомментить, если хочется посмотреть на слоты и пути
            //foreach (var node in GameMap.PathNodes)
            //{
            //    graphics.DrawEllipse(pathPen, node.Position.X - 5, node.Position.Y - 5, 10, 10);
            //    if (node == GameMap.PathNodes.Last())
            //        break;
            //    graphics.DrawLine(pathPen, node.Position, GameMap.PathNodes.Find(node).Next.Value.Position);
            //}
            //foreach (var slot in GameMap.TowerSlots)
            //{
            //    graphics.DrawEllipse(slotPen, slot.X - 5, slot.Y - 5, 10, 10);
            //}

            //рисуем врагов
            foreach (var enemy in thisLevel.Enemies)
            {
                if (enemy == null)
                    continue;
                
                graphics.DrawImage(
                    enemy.AnimationFrames[enemy.AnimationFrame], 
                    new Point(enemy.Position.X - enemy.AnimationFrames[enemy.AnimationFrame].Width / 2, 
                              enemy.Position.Y - enemy.AnimationFrames[enemy.AnimationFrame].Height / 2));

                //тут был просто метод переключающий кадры анимации
                //но таким способом тоже лагают
                test(enemy); 
            }
            //рисуем башни
            foreach (var tower in thisLevel.Towers)
            {
                if (tower == null)
                    continue;

                graphics.DrawImage(tower.Texture, 
                    new Point((tower.Position.X - (int)(tower.Texture.Width * wCoef * 0.5 / 4)), (tower.Position.Y - (int)(tower.Texture.Height * hCoef * 0.5 / 4))));
            }
            //рисуем снаряды
            foreach (var projectile in thisLevel.Projectiles)
            {
                if (projectile == null)
                    continue;

                graphics.DrawImage(projectile.Texture, projectile.Position);
            }

            //выводим деньги
            var brush = new SolidBrush(Color.Black);
            graphics.DrawString(thisLevel.Money.ToString(), DefaultFont, brush, 500, 500);

            if (isReadyTicks) //выводим тики в секунду, криво
            {
                graphics.DrawString(ticksCounter.ToString(), DefaultFont, brush, 700, 250);
                ticksCounter = 0;
                isReadyTicks = false;
            }
        }


        private void test(Enemy enemy) //костыль для проверки, не помог
        {
            enemy.NextAnimFrame();
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
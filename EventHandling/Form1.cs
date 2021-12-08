using EventHandling.Новая_папка;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventHandling
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        List<BaseObject> objects =new();
        Player player;
        Marker marker;
        Buff bust;
        Shadow sh;
        byte speed = 2;
        bool bustTrecker = false;
        int scoreCount=0;
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            bust = new Buff((rnd.Next() % pbMain.Width), (rnd.Next() % pbMain.Height), (rnd.Next() % 361));
            sh = new Shadow(0, 0, 0);
            player.onOverlap += (p, obj) =>
            {
                if (obj is Shadow);
                else
                {
                    Log.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {(obj is DrawingObjects ? "квадратом очков" : obj is Marker ? "маркером" : obj is Shadow ? "тенью" : "бустером")}\n" + Log.Text;
                }
            };
            player.onMarkerOvarlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            objects.Add(sh);
            for (int i = 0; i < 3; i++)
            objects.Add(new DrawingObjects(100, 100, 45));
            objects.Add(new DrawingObjects(200, 250, 0));
            objects.Add(bust);
            objects.Add(player);
            objects.Add(marker);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var graph = e.Graphics;
            graph.Clear(Color.White);
            ///
            foreach (var myObject in objects.ToList())
            {
                //проверка пересечения границ фигур 
                if (myObject != player && player.Overlaps(myObject, graph))
                {

                    player.overlap(myObject);
                    myObject.overlap(player);

                    if (myObject is DrawingObjects)
                    {
                        objects.Remove(myObject);
                        var temp = myObject;
                        temp = new DrawingObjects((rnd.Next() % pbMain.Width), (rnd.Next() % pbMain.Height), (rnd.Next() % 361));
                        scoreCount++;
                        score.Text = scoreCount.ToString();
                        objects.Add(temp);
                    }
                    else if (myObject == bust)
                    {
                        bustTrecker = true;
                        speed = 5;
                        objects.Remove(bust);
                        objects.Add(bust = new Buff((rnd.Next() % pbMain.Width), (rnd.Next() % pbMain.Height), (rnd.Next() % 361)));
                    }
                    //
                }
                //взаимодействие с тенью
                if (myObject != sh && sh.Overlaps(myObject, graph)) 
                {
                    foreach (var temp in objects.ToArray())
                    {
                        if (temp == sh)
                            continue;
                        temp.changeColor(temp, Color.White);
                    }
                }
                else
                {
                    myObject.returnColor();
                }
                graph.Transform = myObject.transformMatrix();
                myObject.render(graph);
            }
            //
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //высчитывание положения игрока
            if (marker != null)
            {
                float lengthXX = marker.x - player.x;
                float lengthYY = marker.y - player.y;

                float lengthPM = MathF.Sqrt(lengthYY * lengthYY + lengthXX * lengthXX);

                lengthXX /= lengthPM;
                lengthYY /= lengthPM;

                player.vX += lengthXX * 0.5f;
                player.vY += lengthYY * 0.5f;


                // расчитываем угол поворота игрока 
                player.angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
                player.vX += -player.vX * 0.1f;
                player.vY += -player.vY * 0.1f;
                player.x += player.vX*speed;
                player.y += player.vY*speed;
            }
            //
            //таймер для спавна бустера
            bust.timeOfLife -= timer.Interval;
            if (bust.timeOfLife <= 0)
            {
                objects.Remove(bust);
                bust = new Buff((rnd.Next() % pbMain.Width), (rnd.Next() % pbMain.Height), (rnd.Next() % 361));
                objects.Add(bust);
            }
            //
            //проверка на увеличение скорости
            if (bustTrecker == true)
            {
                bust.timeOfAction -= timer.Interval;
                if (bust.timeOfAction <= 0)
                {
                    speed = 3;
                    bustTrecker = false;
                }
            }
            //
            //движение темпой области
            if (sh.x <= pbMain.Width)
            {
                sh.x += timer.Interval - 20;
            }
            else{
                sh.x = -426;
            }
            //
            pbMain.Invalidate();
        }
        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }
            marker.x = e.X;
            marker.y = e.Y;
        }
    }
}

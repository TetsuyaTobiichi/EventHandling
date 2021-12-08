using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EventHandling.Новая_папка
{
    class Buff:BaseObject
    {
        public int timeOfLife = 3000;
        public int timeOfAction = 2000;
        public Buff(float x, float y, float angle) : base(x, y, angle){
        }
        public override void render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), -20, -20, 40, 40);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-20, -20, 40, 40));
            return path;
        }
        public override void returnColor()
        {
            color = Color.Green;
        }
    }
}

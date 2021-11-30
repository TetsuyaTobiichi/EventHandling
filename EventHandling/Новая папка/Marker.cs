using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandling.Новая_папка
{
    class Marker:BaseObject
    {
        public Marker(float x, float y, float angle) : base(x, y, angle) { }

        public override void render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Yellow), -4, -4, 8, 8);
            g.DrawEllipse(new Pen(Color.Red, 2), -4, -4, 8, 8);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-4, -4, 8, 8);
            return path;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandling.Новая_папка
{
    class Shadow:BaseObject
    {
        public Shadow(float x, float y, float angle) : base(x, y, angle) { }

        public override void render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, 200, 426);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, 200, 426));
            return path;
        }
    }
}

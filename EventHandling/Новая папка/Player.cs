using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandling.Новая_папка
{
      
        class Player : BaseObject
        {
        public Action<Marker> onMarkerOvarlap;
        public float vX, vY;
        public Player(float x, float y, float angle) : base(x, y, angle) {}

        public override void render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), -20, -20, 40, 40);
            g.DrawEllipse(new Pen(Color.Yellow,5), -20, -20, 40, 40);
            g.DrawEllipse(new Pen(Color.Yellow, 3), -10, -10, 20, 20);
            g.DrawLine(new Pen(Color.Green, 2), 0, 0, 40, 0);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-20, -20, 40, 40);
            return path;
        }
        public override void overlap(BaseObject obj)
        {
            base.overlap(obj);
            if(obj is Marker)
            {
                onMarkerOvarlap(obj as Marker);
            }
        }

    }
    }

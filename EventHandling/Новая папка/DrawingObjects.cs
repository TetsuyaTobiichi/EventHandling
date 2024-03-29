﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandling.Новая_папка
{
    class DrawingObjects:BaseObject
    {
        public DrawingObjects(float x, float y, float angle) : base(x, y, angle) {}
        

        public override void render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(color), -25, -15, 50, 30);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-25,-15,50,30));
            return path;
        }
        public override void returnColor()
        {
            this.color = Color.Yellow;
        }
    }
}

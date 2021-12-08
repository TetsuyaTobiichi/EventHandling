using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EventHandling.Новая_папка
{
    class BaseObject
    {
        public float x, y, angle;
        public Color color = Color.Red;
        public Action<BaseObject, BaseObject> onOverlap;
        public BaseObject(float x,float y,float angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
        }
        public virtual void render(Graphics g)
        {
        }
        public Matrix transformMatrix()
        {
            var matrix = new Matrix();
            matrix.Translate(x, y);
            matrix.Rotate(angle);
            return matrix;
        }
        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }
        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
   
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            path1.Transform(this.transformMatrix());
            path2.Transform(obj.transformMatrix());

           
            var region = new Region(path1);
            region.Intersect(path2); 
            return !region.IsEmpty(g); 
        }
        public virtual void overlap(BaseObject obj)
        {
            if (this.onOverlap != null)
            {
                this.onOverlap(this, obj);
            }
        }
        public virtual void changeColor(BaseObject obj,Color color)
        {
            obj.color = color;
        }
        public virtual void returnColor()
        {

        }
    }
}

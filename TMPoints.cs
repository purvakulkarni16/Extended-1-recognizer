using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class TMPoints
    {
        private float _x;
        private float _y;
        private long _time; 
        public float X { get { return _x; } set {_x = value; } }
        public float Y { get { return _y; } set { _y = value; } }
        public long Time { get { return _time; } set { _time = value; } }

        public static TMPoints Empty { get; internal set; }

        public TMPoints(TMPoints pt)
        {
            this.X = pt.X;
            this.Y = pt.Y;
            this.Time = pt.Time;
        }
        public TMPoints(PointF pt, double ms)
        {
            this.X = pt.X;
            this.Y = pt.Y;
            this.Time = (long)ms;
        }
        public TMPoints(PointF pt, long ms)
        {
            this.X = pt.X;
            this.Y = pt.Y;
            this.Time = ms;
        }
        public TMPoints(float x, float y, long ms)
        {
            this.X = x;
            this.Y =y;
            this.Time = ms;
        }
        public TMPoints(double x, double y, long ms)
        {
            this.X =(float) x;
            this.Y = (float)y;
            this.Time = ms;
        }
        public TMPoints(float x, float y, double ms)
        {
            this.X = x;
            this.Y = y;
            this.Time = (long)ms;
        }
        public TMPoints(double x, double y, double ms)
        {
            this.X = (float)x;
            this.Y = (float)y;
            this.Time = (long)ms;
        }
        public static explicit operator PointF(TMPoints pt)
        {
            return new PointF(pt.X, pt.Y);
        }

        public override string ToString()
        {
            return String.Format("{{X={0}, Y={1}, Time={2}}}", _x, _y, _time);
        }
        public static List<PointF> ConvertList(List<TMPoints> pts)
        {
            List<PointF> list = new List<PointF>(pts.Count);
            foreach (TMPoints pt in pts)
            {
                list.Add((PointF)pt); // explicit conversion
            }
            return list;
        }
    }
}

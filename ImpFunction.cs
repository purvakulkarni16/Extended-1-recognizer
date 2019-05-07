using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ImpFunction
    {
        static int size = 250;
        static int numofPoints = 64;// N=64
        
        public static List<TMPoints> Resample(List<TMPoints> points)
        {
            double I = ImpFunction.PathLength(points) / (numofPoints - 1); //Increment I between N new points
            double D = 0.0;
            List<TMPoints> NewPoints = new List<TMPoints>();
            NewPoints.Add(points[0]);
            for (int i = 1; i < points.Count; i++)
            {
                double d = ImpFunction.Distance((PointF)points[i - 1], (PointF)points[i]);
                if ((D + d) >= I)
                {
                    double qx = points[i - 1].X + ((I - D) / d) * (points[i].X - points[i - 1].X);
                    double qy = points[i - 1].Y + ((I - D) / d) * (points[i].Y - points[i - 1].Y);
                    TMPoints q = new TMPoints(qx, qy, 0);
                    NewPoints.Add(q);
                    points.Insert(i, q);
                    D = 0.0;
                }
                else
                    D = D + d;
            }
            if (NewPoints.Count == numofPoints - 1)
            {
                NewPoints.Add(new TMPoints(points[points.Count - 1].X, points[points.Count - 1].Y, 0));
            }
            return NewPoints;
        }

        public static double PathLength(List<TMPoints> points)//Calculate Path Length
        {
            double dist = 0.0;
            for (int i = 1; i < points.Count; i++)
            {
                dist += Distance((PointF)points[i - 1],(PointF) points[i]);
            }
            return dist;
        }
        public static double Distance(PointF p1,PointF p2) //Calculate distance between two points
        {
            double distx = p2.X - p1.X;
            double disty = p2.Y - p1.Y;
            return Math.Sqrt(distx * distx + disty * disty);
        }
        public static double[] DistanceAtBestAngle(List<PointF> points,Unistroke T,double a,double b, double threshold)
        {
            double Phi = 0.5 * (-1.0 + Math.Sqrt(5.0));
            double x1 = Phi * a + (1.0 - Phi) * b;
            double f1 = DistanceAtAngle(points, T, x1);
            double x2 = (1.0-Phi) * a + Phi*b;
            double f2 = DistanceAtAngle(points, T, x2);
            double i = 2.0;
            while (Math.Abs(b-a) > threshold)
            {
                if (f1 > f2)
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = (1.0 - Phi) * a + Phi * b;
                    f2 = DistanceAtAngle(points, T, x2);
                }
                else
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = Phi * a + (1.0 - Phi) * b;
                    f1 = DistanceAtAngle(points, T, x1);
                }
                i++;
            }
            return new double[3] { Math.Min(f1, f2), Rad2Deg((a + b) / 2.0), i };
        }
        public static List<PointF> RotateBy(List<PointF> points, double radians) //Rotate the gesture
        {
            PointF C = Centroid(points);
            double Cos = Math.Cos(radians);
            double Sin = Math.Sin(radians);
            List<PointF> newPoints = new List<PointF>();
            for (int i = 0; i < points.Count; i++)
            {
                newPoints.Add(new PointF((float)((points[i].X - C.X) * Cos - (points[i].Y - C.Y) * Sin + C.X), (float)((points[i].X - C.X) * Sin + (points[i].Y - C.Y) * Cos + C.Y)));
            }
            return newPoints;
        }

        public static PointF Centroid(List<PointF> points) //Function to find centroid
        {
            float x = 0, y = 0;
            for (int i = 0; i < points.Count; i++)
            {
                x += points[i].X;
                y += points[i].Y;
            }
            x = x / points.Count;
            y = y / points.Count;
            return new PointF(x, y);
        }

        public static double IndicativeAngle(List<PointF> points) //Function to find Indicative Angle
        {
            PointF NewPoints = Centroid(points);
            return Math.Atan2(points[0].Y-NewPoints.Y,points[0].X-NewPoints.X);
        }

       
        

        public static List<PointF> ScaleTo(List<PointF> points) //Function to scale the gesture. 1D gestures are scaled uniformly and 2D non uniformly
        {
            Rectangle b = BoundingBox(points);
            List<PointF> retpoints = new List<PointF>();
            float bwidth = b.Width;
            float bheight = b.Height;
            float ratio = Math.Min(bwidth / bheight, bheight / bwidth);
            bool uniform = ratio < 0.30 ? true : false;
            if (uniform)
            {
                for (var i = 0; i < points.Count; i++)
                {

                    PointF tmp = new PointF((points[i].X * ((float)size /(float) Math.Max(bwidth, bheight))), (points[i].Y * ((float)size /(float) Math.Max(bwidth, bheight))));
                    retpoints.Add(tmp);
                }
            }
            else
            {
                for (var i = 0; i < points.Count; i++)
                {
                    float px = (float)size / (float)b.Width;
                    px *= points[i].X;
                    float py =  (float)size /(float) b.Height;
                    py *= points[i].Y;
                    PointF tmp = new PointF(px,py);
                    retpoints.Add(tmp);
                }
            }
            return retpoints;
        }

        public static Rectangle BoundingBox(List<PointF> points) //Function to create a bounding box
        {
            float minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
            for (var i = 0; i < points.Count; i++)
            {
                minX = Math.Min(minX, points[i].X);
                maxX = Math.Max(maxX, points[i].X);
                maxY = Math.Max(maxY, points[i].Y);
                minY = Math.Min(minY, points[i].Y);
            }
            float bwidth = maxX - minX;
            float bheight = maxY - minY;
            return new Rectangle((int)minX, (int)minY, (int)(maxX - minX),(int)(maxY - minY));
        }

        public static double DistanceAtAngle(List<PointF> points,Unistroke T,double radians)
        {
            List<PointF> newPoints = RotateBy(points, radians);
            return PathDistance(newPoints, T.points);
        }

        public static List<PointF> TranslateTo(List<PointF> points,PointF pt) //Translating the gesture
        {
            PointF C = Centroid(points);
            List<PointF> newPoints = new List<PointF>();
            for(int i = 0; i < points.Count; i++)
            {
                newPoints.Add(new PointF(points[i].X + pt.X - C.X, points[i].Y + pt.Y - C.Y));
            }
            return newPoints;
        }
        public static double PathDistance(List<PointF> p1,List<PointF> p2) //Calculate Path Distance
        {
            double d = 0.0;
            for(int i = 0; i < p1.Count; i++)
            {
                d += Distance(p1[i], p2[i]);
            }
            return d / p1.Count;
        }
        public static double Deg2Rad(double d)
        {
            return (d * Math.PI / 180);
        }
        public static double Rad2Deg(double rad)
        {
            return (rad * 180.0 / Math.PI);
        }
    }
}

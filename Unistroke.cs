using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApp1
{
    internal class Unistroke
    {
        public string name;
        public List<TMPoints> coordinates;
        public List<PointF> points;
        public string orgname;
        public Unistroke(string name, List<TMPoints> coordinates,string orgname="")
        {
            this.name = name;
            this.coordinates = coordinates;        
            this.orgname = orgname;
            this.points = TMPoints.ConvertList(ImpFunction.Resample(coordinates));
            double radians = ImpFunction.IndicativeAngle(points);
            this.points = ImpFunction.RotateBy(points, -radians);
            this.points = ImpFunction.ScaleTo(points);
            points = ImpFunction.TranslateTo(points, new PointF(0, 0));
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsFormsApp1
{
    class LoadAndRecognize
    {
        private Dictionary<string, Unistroke> RecordedGestures;
        public static SizeF Square_Size = new SizeF(250, 250);
        public static double Diagonal = Math.Sqrt(250 * 250 + 250 * 250);
        public static double Half_Diagonal = 0.5 * Diagonal;
        public LoadAndRecognize()
        {
            RecordedGestures = new Dictionary<string, Unistroke>();
        }
      
        public int getNumGestures()
        {
            return RecordedGestures.Count;
        }

     
        //This function is to record the getsures with it's name and the points
        //the gesture drawn on canvas is converted into standard xml format
        public bool recordGesture(string filename, List<TMPoints> points)
        {
            int s = filename.LastIndexOf('\\');
            int e = filename.LastIndexOf('.');
            string name = filename.Substring(s + 1, e - s - 1);
            if (RecordedGestures.ContainsKey(name))
                RecordedGestures.Remove(name);
            Unistroke newPrototype = new Unistroke(name, points);
            RecordedGestures.Add(name, newPrototype);


            bool xml_create = true;
            XmlTextWriter interpreter = null;
            try
            {

                interpreter= new XmlTextWriter(filename, Encoding.UTF8);
                interpreter.Formatting = Formatting.Indented;
                interpreter.WriteStartDocument(true);
                interpreter.WriteStartElement("Gesture");
                interpreter.WriteAttributeString("AppName", Assembly.GetExecutingAssembly().GetName().Name);

                interpreter.WriteAttributeString("AppVer", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                interpreter.WriteAttributeString("Name", name);
                interpreter.WriteAttributeString("NumPts", XmlConvert.ToString(points.Count));
                interpreter.WriteAttributeString("Millseconds", XmlConvert.ToString(points[points.Count - 1].Time - points[0].Time));
                interpreter.WriteAttributeString("Date", DateTime.Now.ToLongDateString());
                interpreter.WriteAttributeString("TimeOfDay", DateTime.Now.ToLongTimeString());

                


                foreach (TMPoints p in points)
                {
                    interpreter.WriteStartElement("Point");
                    interpreter.WriteAttributeString("X", XmlConvert.ToString(p.X));
                    interpreter.WriteAttributeString("Y", XmlConvert.ToString(p.Y));
                    interpreter.WriteAttributeString("T", XmlConvert.ToString(p.Time));
                    interpreter.WriteEndElement();
                }

                interpreter.WriteEndDocument();
            }
            catch (XmlException )
            {
                
                xml_create = false;
            }
            catch (Exception )
            {
                
                xml_create = false;
            }
            finally
            {
                if (interpreter != null)
                    interpreter.Close();
            }
            return xml_create ;
        }
        //Fucntion to Recognize the gesture.
        public BestList Recog(List<TMPoints> timepoint)
        {
            DateTime t0 = DateTime.Now;
            timepoint= ImpFunction.Resample(timepoint);
            List<PointF> points = TMPoints.ConvertList(timepoint);
            double radians = ImpFunction.IndicativeAngle(points);
            points = ImpFunction.RotateBy(points, -radians);
            points = ImpFunction.ScaleTo(points);
            PointF Origin = new PointF(0, 0);
            points = ImpFunction.TranslateTo(points, Origin);
            BestList res = new BestList();
            foreach(Unistroke T in RecordedGestures.Values)
            {
                double[] d = ImpFunction.DistanceAtBestAngle(points, T, -1 * ImpFunction.Deg2Rad(45), ImpFunction.Deg2Rad(45), ImpFunction.Deg2Rad(2));
                double score = 1.0 - d[0] / Half_Diagonal;
                res.Add(T.name, score, d[0], d[1],T.orgname);
            } 
            res.SortDescending();
            return res;
        }
        //Function to Load all the gestures 
        public bool LoadGesture(string filename,string name="",string orgname="")
        {
            XmlTextReader read = null;
            bool flag = true;
            try
            {
                read = new XmlTextReader(filename);
                read.WhitespaceHandling = WhitespaceHandling.None;
                read.MoveToContent();
                Unistroke p = ReadGesture(read, name,orgname);
                
            if (RecordedGestures.ContainsKey(p.name))
                    RecordedGestures.Remove(p.name);
                RecordedGestures.Add(p.name, p);
            }
            catch (XmlException xmlex)
            {
                Console.Write(xmlex.Message);
                flag = false;
            }
            catch (Exception excp)
            {
                Console.Write(excp.Message);
                flag = false;
            }
            finally
            {
                if (read != null)
                    read.Close();
            }
            return flag;
        }

        public Unistroke ReadGesture(XmlTextReader reader,String name="",String orgname="")
        {
            if(name.Equals(""))
               name = reader.GetAttribute("Name");

            List<TMPoints> points = new List<TMPoints>(XmlConvert.ToInt32(reader.GetAttribute("NumPts")));

            reader.Read();
            

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                TMPoints point = new TMPoints(0, 0, 0);
                point.X = XmlConvert.ToSingle(reader.GetAttribute("X"));
                point.Y = XmlConvert.ToSingle(reader.GetAttribute("Y"));
                point.Time = XmlConvert.ToInt64(reader.GetAttribute("T"));
                points.Add(point);
                reader.ReadStartElement("Point");
            }

            return new Unistroke(name, points,orgname);
        }

    }
}

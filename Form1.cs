using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<TMPoints> totalpoints;
        private bool downClick;
        private LoadAndRecognize loadAndRec;
        private Random rand;
        private String csvpath = "details.csv";
        private int _type;
        private string gesname = "";
        private string DirPath = "";
        //Code to make the canvas
        public Form1()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            totalpoints = new List<TMPoints>(256);
            InitializeComponent();
           
            loadAndRec = new LoadAndRecognize();
            _type = 0;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (downClick)
            {
                downClick = false;
                if (loadAndRec.getNumGestures() > 0)
                {
                    BestList res = loadAndRec.Recog(totalpoints);
                    MessageBox.Show("Gesture matches " + res.m_strName);
                }
              
            }
        }
        //to store points when the mouse is moving
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (downClick)
            {
                totalpoints.Add(new TMPoints(e.X, e.Y, DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
                Invalidate(new Rectangle(e.X - 2, e.Y - 2, 4, 4));
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            downClick = true;
            totalpoints.Clear();
            totalpoints.Add(new TMPoints(e.X, e.Y, DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
            Invalidate();
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (totalpoints.Count > 0)
            {
                e.Graphics.FillEllipse( Brushes.White, totalpoints[0].X - 5f, totalpoints[0].Y - 5f, 10f, 10f);
            }
            foreach (TMPoints p in totalpoints)
            {
                e.Graphics.FillEllipse(Brushes.White, p.X - 2f, p.Y - 2f, 4f, 4f);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                DirPath = folderDlg.SelectedPath;

         
            }
            foreach (var file in GetAllFiles(DirPath))
            {
                loadAndRec.LoadGesture(file);
            }
        }
        private List<String> getTestSet(List<String> GenList, List<String> TrainSet)
        {
            List<String> TestSet = new List<String>();
            foreach (String s in GenList)
            {
                if (!TrainSet.Contains(s) && !TestSet.Contains(s))
                {
                    TestSet.Add(s);
                }
            }
            return TestSet;
        }
        private List<String> getRandom(List<String> list, int E, int range)
        {
            List<String> RandomList = new List<String>();

            int tmp = 0;
            while (RandomList.Count != E)
            {
                tmp = rand.Next(0, range);

                if (!RandomList.Contains(list[tmp]))
                    RandomList.Add(list[tmp]);
            }
            return RandomList;
        }

        private List<String> generateList(String gesture)
        {
            List<String> list = new List<String>();
            for (int i = 1; i < 11; i++)
            {
                if (i == 10)
                {
                    list.Add(gesture + i + ".xml");
                }
                else
                {
                    list.Add(gesture + "0" + i + ".xml");
                }
            }
            return list;
        }

        private void offlineTraining()
        {
            StringBuilder sb = new StringBuilder();
            rand = new Random();
            string DirPath = @"C:\Users\purva\Documents\Data_gesture\xml_logs\";
            File.WriteAllText(csvpath, "User[all-users],GestureType[all-gestures-types],RandomIteration[1to100],#ofTrainingExamples[E],TotalSizeOfTrainingSet[count],TrainingSetContents[specific-gesture-instances],Candidate[specific-instance],RecoResultGestureType[what-was-recognized],CorrectIncorrect[1or0],RecoResultScore,RecoResultBestMatch[specific-instance],RecoResultNBestSorted[instance-and-score]" + Environment.NewLine);
            List<String> list = GetUser(DirPath);
            List<String> Speed = new List<string>(new string[] { @"\slow", @"\medium", @"\fast" });
            List<String> Gestures = new List<string>(new string[] { @"\S", @"\C", @"\V", @"\circle", @"\triangle", @"\Line" });
            foreach (String user in list)
            { //Iterate through all users
                foreach (String Sp in Speed)
                { //iterate through all speeds
                    for (int i = 1; i < 10; i++)
                    { // E=1 - 9
                        for (int j = 1; j < 101; j++)
                        { // loop of 100 random 
                            LoadAndRecognize loadRec = new LoadAndRecognize();
                            List<String> TestSet = new List<String>(); //TestSet 
                            List<String> Genlist = new List<String>(); //Generate List of all the gestures
                            List<String> TrainSet = new List<String>(); //Training set which will increase as E increses
                            List<String> RandomSample = new List<String>(); //Random samples for testing from remaining (Training)
                            foreach (String gestures in Gestures) // iterate through all the gestures
                            {
                                Genlist.Clear(); //clear genlist at start of every gesture
                                TestSet.Clear(); //clear TestSet
                                List<String> TrainSettmp = new List<String>(); //temp trainset
                                Genlist.AddRange(generateList(gestures)); //generate genlist
                                TrainSettmp.AddRange(getRandom(Genlist, i, 10)); //generate trainset for particular gesture
                                foreach (var ts in TrainSettmp)
                                {
                                    int m = Sp.Length - 1;
                                    int n = ts.Length - 5;
                                    loadRec.LoadGesture(DirPath + user + Sp + ts, user + Sp.Substring(1, m) + ts.Substring(1, n), ts.Substring(1, n)); // train set
                                }
                                TestSet.AddRange(getRandom(getTestSet(Genlist, TrainSettmp), 1, 10 - i)); //get one random test pattern from testset

                                RandomSample.Add(TestSet[0]);
                                TrainSet.AddRange(TrainSettmp);
                            }
                            foreach (String smp in RandomSample)
                            {
                                XmlTextReader reader = new XmlTextReader(DirPath + user + @"\" + Sp + smp);
                                reader.WhitespaceHandling = WhitespaceHandling.None;
                                reader.MoveToContent();
                                Unistroke p = loadRec.ReadGesture(reader);
                                int m = Sp.Length - 1;
                                int n = smp.Length - 5;
                                p.orgname = p.name;
                                p.name = user + Sp.Substring(1, m) + smp.Substring(1, n);
                                BestList nbestList = loadRec.Recog(p.coordinates);
                                insertIntoFile(user, nbestList, j, i, TrainSet.Count, TrainSet, p);
                            }
                        }
                    }
                }
            }

        }
        //Creating a Log file
        private void insertIntoFile(string user, BestList nbestList, int j, int i, int count, List<String> trainSet, Unistroke p)
        {
            String record = user + "," + p.orgname.Substring(0, p.orgname.Length - 2) + "," + j + "," + i + "," + count + "," + getFormatedList(trainSet, user) + "," + getFormatedList(p.orgname, user, 0) + "," + nbestList.m_strorgName.Substring(0, nbestList.m_strorgName.Length - 2) + ",";
            record += nbestList.m_strorgName.Substring(0, nbestList.m_strorgName.Length - 2).Equals(p.orgname.Substring(0, p.orgname.Length - 2), StringComparison.InvariantCultureIgnoreCase) ? "1" : "0";
            record += "," + nbestList.m_strScore + "," + getFormatedList(nbestList.m_strorgName, user, 0) + "," + getRecoresultnBestSorted(nbestList, user) + Environment.NewLine;
            File.AppendAllText(csvpath, record);
        }

        private String getRecoresultnBestSorted(BestList nbestList, String user)
        {
            String res = "{";
            for (int i = 0; i < nbestList.m_listBestListArray.Count; i++)
            {
                BestListRes n = (BestListRes)nbestList.m_listBestListArray[i];
                res += getFormatedList(n.m_strOrgName, user, 0);
                res += "-" + n.m_dScore;
                if (i < nbestList.m_listBestListArray.Count - 1 && i < 20)
                {
                    res += "|";
                }
                if (i == 20)
                {
                    break;
                }
            }
            res += "}";
            return res;
        }
        //File formatting
        private string getFormatedList(string testStr, string user, int i)
        {
            return user + "-" + testStr[i] + "-" + testStr[testStr.Length - 2] + testStr[testStr.Length - 1];
        }

        private string getFormatedList(List<String> trainSet, String user)
        {
            String res = "{";
            for (int i = 0; i < trainSet.Count; i++)
            {
                res += user + "-" + trainSet[i][1] + "-" + trainSet[i][trainSet[i].Length - 6] + trainSet[i][trainSet[i].Length - 5];
                if (i < trainSet.Count - 1)
                {
                    res += "|";
                }
            }
            res += "}";
            return res;
        }

        public List<String> GetAllFiles(String sDir)
        {
            List<String> files = new List<String>(); try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(GetAllFiles(d));
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
            return files;
        }
        public List<String> GetUser(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    DirectoryInfo dir = new DirectoryInfo(d);
                    files.Add(dir.Name);
                }
            }
            catch (System.Exception except)
            {
                Console.WriteLine(except);
            }
            return files;
        }

        

        private void loadGestureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\purva\Documents\Data_gesture\xml_logs";
            foreach (string s in GetAllFiles(path))
            {
                loadAndRec.LoadGesture(s);
            }
        }

        private void readFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\purva\OneDrive\Desktop\Circle.xml";
            XmlTextReader read = new XmlTextReader(path);
            read.WhitespaceHandling = WhitespaceHandling.None;
            read.MoveToContent();
            Unistroke p = loadAndRec.ReadGesture(read);
            loadAndRec.Recog(p.coordinates);
        }

       
        private void offlineTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            offlineTraining();
        }

        private void generateSamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void slowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _type = 0;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _type = 1;
        }

        private void fastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _type = 2;
        }

        private void generateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String temp2 = @"C:\Users\purva\Documents\Data_gesture\xml_logs";
            String temp = "";
            switch (_type)
            {
                case 0:
                    temp2 += @"\Slow\";
                    break;
                case 1:
                    temp2 += @"\Medium\";
                    break;
                case 2:
                    temp2 += @"\Fast\";
                    break;


            }
            temp = temp2 + temp + gesname + ".xml";
            loadAndRec.recordGesture(temp, totalpoints);  

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            gesname = textBox1.Text;
        }
    }
}

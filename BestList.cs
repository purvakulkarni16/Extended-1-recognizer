using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class BestList //Calculating NBest List and sorting the list in the descending order to get top scores
    {
        public BestListRes BestListResult;
        public static BestList Empty = new BestList();
        private ArrayList BestListArray;
        public BestList()
        {
            BestListArray = new ArrayList();
        }
        public bool Emptycheck
        {
            get
            {
                return BestListArray.Count == 0;
            }
        }

        public void SortDescending()
        {
            BestListArray.Sort();
        }

        public void Add(string name, double score, double distance, double angle,string orgname="")
        {
            BestListRes r;
            r = new BestListRes(name, score, distance, angle,orgname);
            BestListArray.Add(r);
        }
        public string m_strName
        {
            get
            {
                if (BestListArray.Count > 0)
                {
                    BestListRes r = (BestListRes)BestListArray[0];
                    return r.m_strName;
                }
                return "";
            }
        }

        public string m_strorgName
        {
            get
            {
                if (BestListArray.Count > 0)
                {
                    BestListRes r = (BestListRes)BestListArray[0];
                    return r.m_strOrgName;
                }
                return "";
            }
        }

        public double m_strScore
        {
            get
            {
                if (BestListArray.Count > 0)
                {
                    BestListRes r = (BestListRes)BestListArray[0];
                    return r.m_dScore;
                }
                return -1;
            }
        }

        public double m_strAngle
        {
            get
            {
                if (BestListArray.Count > 0)
                {
                    BestListRes r = (BestListRes)BestListArray[0];
                    return r.m_dAngle;
                }
                return -1;
            }
        }

        public double m_strDistance
        {
            get
            {
                if (BestListArray.Count > 0)
                {
                    BestListRes r = (BestListRes)BestListArray[0];
                    return r.m_dDistance;
                }
                return -1;
            }
        }

        public BestListRes this[int i]
        {
            get
            {
                if (0 <= i && i < BestListArray.Count)
                {
                    return (BestListRes)BestListArray[i];
                }
                return null;
            }
        }

        public string[] m_strNames
        {
            get
            {
                string[] s = new string[BestListArray.Count];
                if (BestListArray.Count > 0)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        s[i] = ((BestListRes)BestListArray[i]).m_strName;
                    }
                }
                return s;
            }
        }
        public string[] m_strorgNames
        {
            get
            {
                string[] s = new string[BestListArray.Count];
                if (BestListArray.Count > 0)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        s[i] = ((BestListRes)BestListArray[i]).m_strOrgName;
                    }
                }
                return s;
            }
        }

        public string m_strNamesString
        {
            get
            {
                string s = String.Empty;
                if (BestListArray.Count > 0)
                {
                    foreach (BestListRes r in BestListArray)
                    {
                        s += String.Format("{0},", r.m_strName);
                    }
                }
                return s.TrimEnd(new char[1] { ',' });
            }
        }
        public string m_strorgNamesString
        {
            get
            {
                string s = String.Empty;
                if (BestListArray.Count > 0)
                {
                    foreach (BestListRes r in BestListArray)
                    {
                        s += String.Format("{0},", r.m_strOrgName);
                    }
                }
                return s.TrimEnd(new char[1] { ',' });
            }
        }

        public double[] m_dScores
        {
            get
            {
                double[] s = new double[BestListArray.Count];
                if (BestListArray.Count > 0)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        s[i] = ((BestListRes)BestListArray[i]).m_dScore;
                    }
                }
                return s;
            }
        }

        public string m_strScores
        {
            get
            {
                string s = String.Empty;
                if (BestListArray.Count > 0)
                {
                    foreach (BestListRes r in BestListArray)
                    {
                        s += String.Format("{0:F3},", Math.Round(r.m_dScore, 3));
                    }
                }
                return s.TrimEnd(new char[1] { ',' });
            }
        }
        public ArrayList m_listBestListArray {
            get
            {
                return BestListArray;
            } set
            {
                BestListArray = value;
            }
        }
    }
}

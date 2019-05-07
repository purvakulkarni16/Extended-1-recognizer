using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class BestListRes : IComparable
    {
        private string m_strgestureName;
        private double m_dgestureScore;
        private double m_dgestureDistance;
        private double m_dgestureAngle;
        private string m_strorgname;

        public int CompareTo(object obj)
        {
            if (m_dgestureScore < ((BestListRes)obj).m_dgestureScore)
            {
                return 1;
            }
            else if (m_dgestureScore > ((BestListRes)obj).m_dgestureScore)
            {
                return -1;
            }
            return 0;
        }
        public BestListRes(string a_strgestureName= "", double a_dgestureScore=-1d, double a_dgestureDistance=-1d, double a_dgestureAngle = 0d,string a_dorgstring="")
        {
            m_strgestureName = a_strgestureName;
            m_dgestureScore = a_dgestureScore;
            m_dgestureDistance = a_dgestureDistance;
            m_dgestureAngle = a_dgestureAngle;
            m_strorgname = a_dorgstring;
        }
        public string m_strName {
            get {
                return m_strgestureName;
            }
        }
        public string m_strOrgName
        {
            get
            {
                return m_strorgname;
            }
        }
        public double m_dScore{
            get {
                return m_dgestureScore;
            }
        }
        public double m_dDistance {
            get {
                return m_dgestureDistance;
            }
        }
        public double m_dAngle {
            get {
                return m_dgestureAngle;
            }
        }
        public bool Emptycheck {
            get {
                return m_dgestureScore == -1d;
            }
        }
    }
}

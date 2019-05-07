using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Size
    {
        private float width;
        private float height;
        public Size(float width, float height)
        {
            this.width = width;
            this.height = height;
        }
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
    }
}

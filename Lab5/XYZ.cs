using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{   //Класс координат фигуры
    public class XYZ
    {
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }
        public XYZ()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public XYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}

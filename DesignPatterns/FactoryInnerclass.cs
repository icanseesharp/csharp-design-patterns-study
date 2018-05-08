using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
   public class FactoryInnerclass
    {        
        public class Point
        {
            public double x { get; set; }
            public double y { get; set; }

            #region problem
            public Point(double p1, double p2)
            {
                x = p1;
                y = p2;
            }

            //public Point(double rho, double theta)
            //{
            //    x = rho * Math.Cos(theta);
            //    y = rho * Math.Sin(theta); 
            //}
            #endregion

            #region solution with PointFactory Inner class
            public static class Factory
            {
                //Factory Method
                public static Point NewCartesianPoint(double x, double y)
                {
                    return new Point(x, y);
                }
                //Factory method
                public static Point NewPolarPoint(double rho, double theta)
                {
                    return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
                }
            }
            #endregion
            public override string ToString()
            {
                return string.Format("x : {0} and y : {1}", x, y);
            }
        }

        public static void Main(string[] args)
        {
            var p1 =  Point.Factory.NewCartesianPoint(2.5, 6.5); //Point.NewCartesianPoint(2.5, 6.5);
            var p2 = Point.Factory.NewPolarPoint(2.5, Math.PI / 2); //Point.NewPolarPoint(2.5, Math.PI / 2);
            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.ReadKey();
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1
{
    class Polygon
    {
        static float extendX = 1000;
        public class Point
        {
            public float x, y;

            public Point(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
        };

        static bool IsPointOnLine(Point p, Point q, Point r)
        {
            if (q.x <= Math.Max(p.x, r.x) &&
                q.x >= Math.Min(p.x, r.x) &&
                q.y <= Math.Max(p.y, r.y) &&
                q.y >= Math.Min(p.y, r.y))
            {
                return true;
            }
            return false;
        }
        static float IsCollinear(Point p, Point q, Point r)
        {
            float valu = (q.y - p.y) * (r.x - q.x) -
                     (q.x - p.x) * (r.y - q.y);

            if (valu == 0)
            {
                return 0;
            }
            return (valu > 0) ? 1 : 2;
        }
        static bool doIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
            if (IsPointOnLine(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1 
            if (IsPointOnLine(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (IsPointOnLine(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (IsPointOnLine(p2, q1, q2))
            {
                return true;
            }

            // Doesn't collinear then
            return false;
        }
        static bool isInside(Point[] polygon, int n, Point p)
        {
            if (n < 3)
            {
                return false;
            }
            Point extend = new Point(extendX, p.y);
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;
                if (doIntersect(polygon[i],
                             polygon[next], p, extend))
                {
                    // If the point 'p' is colinear with line  
                    // segment 'i-next', If it lies, return true, otherwise false 
                    if (IsCollinear(polygon[i], p, polygon[next]) == 0)
                    {
                        return IsPointOnLine(polygon[i], p,
                                         polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            //if count is odd then Return true 
            return (count % 2 == 1);
        }
        public static void Main(string[] args)
        {
           
            Point[] polygon1 = { new Point(1, 0), new Point(8, 3), new Point(8, 8), new Point(1, 5) };
            Point p = new Point(3, 5);
            int n = polygon1.Length;
            if (isInside(polygon1, n, p))
            {

                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }
            Point[] polygon2 = { new Point(-3, 2), new Point(-2, (float)-0.8), new Point(0,(float)1.2), new Point((float)2.2, 0),new Point(2, (float)4.5) };
            Point p1 = new Point(0, 0);
            if (isInside(polygon2, n, p))
            {

                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }
        }
    }   
}

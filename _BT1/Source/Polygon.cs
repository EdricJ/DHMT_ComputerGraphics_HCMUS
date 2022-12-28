using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab1.Form1;

namespace lab1
{
    class Polygon
    {
        List<Point> listPoints = new List<Point>();
        public int type = 0;

        //constructor 
        public Polygon(List<Point> points, int nType)
        {
            listPoints = new List<Point>(points);
            type = nType;
        }
        
        public List<Point> getControlPoints()
        {
            return new List<Point>(listPoints);
        }
        
        public void setPoints(List<Point> points)
        {
            listPoints = new List<Point>(points);
        }

        public bool isPointInside(Point pQ) // !!!!cac diem trong da giac phai theo chieu kim dong ho //  su dung vector phap tuyen
        {
            Console.WriteLine("isPointInside");
            
            for (int i = 0; i < listPoints.Count; i++)
            {
                Console.WriteLine("Point: " + listPoints[i].ToString());
            }
            //vi ve da giac là vẽ nhiều cạnh nên nếu có nhiều điểm điều khiển phải xét trường hợp 
            if (listPoints.Count > 2)
            {
                Point preP = listPoints[listPoints.Count - 1];
                for (int i = 0; i < listPoints.Count; i++)
                {
                    // p(t) = (1-t) * p + t * q 
                    // tinh vector phap tuyen * Q so voi d
                    int x1 = preP.X, y1 = preP.Y, x2 = listPoints[i].X, y2 = listPoints[i].Y;
                    // listPoints.Count * Q > d => return 0
                    int d = (y1 - y2) * x1 + (x2 - x1) * y1;
                    if (((y1 - y2) * pQ.X + (x2 - x1) * pQ.Y) > d)
                    {
                        return false;
                    }
                    preP = listPoints[i];
                }
                return true;
            }
            else if (listPoints.Count == 2) // doan thang
            {
                int x1 = listPoints[0].X, y1 = listPoints[0].Y, x2 = listPoints[1].X, y2 = listPoints[1].Y, x3 = pQ.X, y3 = pQ.Y;
                
                if ((y1 - y2) * (x1 - x3) == (y1 - y3) * (x1 - x2))
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        //cũng xét vị trí điểm như bên ellipse, trả về đúng vị trí controlpoint điều khiển trên cạnh của đối tượng 
        public int selectedControlPointIndex(Point pQ)
        {
            int n = listPoints.Count;
            for (int i = 0; i < n; i++)
            {
                Point p = listPoints[i];
                if (pQ.X >= (p.X - Config.DISTANCE_POINT) && pQ.X <= (p.X + Config.DISTANCE_POINT) && pQ.Y >= (p.Y - Config.DISTANCE_POINT) && pQ.Y <= (p.Y + Config.DISTANCE_POINT))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

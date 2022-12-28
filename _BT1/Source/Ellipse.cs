using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab1.Form1;

namespace lab1
{
    class Ellipse
    {
        private Point origin = new Point(0, 0); //tạo các điểm ban đầu 
        private double rx = 0.0f, ry = 0.0f;
        private List<Point> controlPoints = new List<Point>();
        public int type = 0;

        public Ellipse(List<Point> points, int nType)
        {
            if (nType == ChosenOption.CIRCLE) // 1 diem tam, 1 diem nam tren duong tron
            {
                Point nOrigin = points[0], a = points[1];
                origin = nOrigin;
                double r = Math.Sqrt(Math.Pow(nOrigin.X - a.X, 2) + Math.Pow(nOrigin.Y - a.Y, 2));
                rx = r;
                ry = r;
            }
            else // 1 dien tam, 1 diem thuoc hcn bao xung quanh duong ellipse
            {
                Point nOrigin = points[0], h = points[1], k = points[2];    //thêm 1 điểm nữa vì ellipse có 2 bán kính 
                origin = nOrigin;
                rx = Math.Abs(nOrigin.X - h.X);
                ry = Math.Abs(nOrigin.Y - k.Y);
            }

            controlPoints = new List<Point>(points);    //lưu vết các điểm 
            type = nType;
        }

        //kiểm tra điểm có nằm trên đường ellipse, xét cả hoành và tung 
        public bool isPointInside(Point p)
        {
            double x = p.X, y = p.Y, h = origin.X, k = origin.Y;
            return (((Math.Pow(x - h, 2) / Math.Pow(rx, 2)) + (Math.Pow(y - k, 2) / Math.Pow(ry, 2))) < 1);
        }

        public List<Point> getControlPoints()
        {
            return controlPoints;
        }

        //đặt điểm với điểm gốc là điểm đầu tiên 
        public void setPoints(List<Point> points)
        {
            controlPoints = new List<Point>(points);
            origin = controlPoints[0];
        }

        //lựa chọn vị trí điểm khi so từ điểm đó đến khung bao cạnh của ellipse (trừu tượng một khung bao chữ nhật bao lấy ellipse), trả về vị trí các controlpoint trên cạnh khi click 
        public int selectedControlPointIndex(Point pQ)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                Point p = controlPoints[i];
                if (pQ.X >= (p.X - Config.DISTANCE_POINT) && pQ.X <= (p.X + Config.DISTANCE_POINT) && pQ.Y >= (p.Y - Config.DISTANCE_POINT) && pQ.Y <= (p.Y + Config.DISTANCE_POINT))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab1.Form1;

namespace lab1
{
    //lớp dùng để chọn và thao tác vào đối tượng (select)
    class AffineTransform
    {
        private List<Point> controlPoints = new List<Point>();
        int type = 0;

        //hàm constructor khởi lưu vào các tọa độ 
        public AffineTransform(List<Point> points, int nType)
        {
            controlPoints = new List<Point>(points);
            type = nType;
        }

        public List<Point> getControlPoints()
        {
            return new List<Point>(controlPoints);
        }

        private Point getOrigin()
        {
            //Tạo tâm 
            if (type == Config.LINE || type == Config.RECTANGLE || type == Config.TRIANGLE || type == Config.PENTAGON
                || type == Config.HEXAGON || type == Config.RANDOM)
            {
                Point re = new Point(0, 0);
                foreach (Point p in controlPoints)
                {
                    re.X += p.X;
                    re.Y += p.Y;
                }
                re.X /= controlPoints.Count;
                re.Y /= controlPoints.Count;
                return re;
            }

            // circle ellipse có tâm là điểm controlpoint đầu tiên
            return controlPoints[0];
        }

        private bool isSelectControlPoint(Point pQ) // dua vao diem select, xem co phai select control point hay k
        {
            foreach (Point p in controlPoints)
            {
                if (pQ.X >= (p.X - Config.DISTANCE_POINT) && pQ.X <= (p.X + Config.DISTANCE_POINT) && pQ.Y >= (p.Y - Config.DISTANCE_POINT) && pQ.Y <= (p.Y + Config.DISTANCE_POINT))
                {
                    return true;
                }
            }
            return false;
        }

        //Hàm tạo các điểm mới khi tịnh tiến
        public bool translate(Point nStart, Point nEnd)
        {
            //điểm đầu vẫn là điểm cuối 
            if (nStart == nEnd)
            {
                return false;   //tịnh tiến thất bại 
            }

            int dx = nEnd.X - nStart.X, dy = nEnd.Y - nStart.Y; //ước lượng khoảng cách đã được tịnh tiến 
            
            //vòng lặp đến hết các điểm tọa độ 
            for (int i = 0; i < controlPoints.Count; i++)
            {
                controlPoints[i] = new Point(controlPoints[i].X + dx, controlPoints[i].Y + dy); //cộng thêm khoảng ước lượng 
            }
            return true;
        }

        //Hàm tạo các điểm mới khi co giãn
        public bool scale(Point nStart, Point nEnd)
        {
            //kiểm tra điểm đầu điểm cuối 
            if (nStart == nEnd)
            {
                return false;
            }

            //calculate c vector
            Point origin = getOrigin();
            double rAdd = Math.Sqrt(Math.Pow(nStart.X - nEnd.X, 2) + Math.Pow(nStart.Y - nEnd.Y, 2)); // tu diem control point den mouse up
            double r = Math.Sqrt(Math.Pow(nStart.X - origin.X, 2) + Math.Pow(nStart.Y - origin.Y, 2)); // tu diem origin den control point
            double rCheck = Math.Sqrt(Math.Pow(nEnd.X - origin.X, 2) + Math.Pow(nEnd.Y - origin.Y, 2)); // tu diem origin den mouse up

            if(rCheck < r)
                rAdd = -rAdd;

            double c = (r + rAdd) / r;
            Console.WriteLine("Scale x" + c);

            // affine transform scale
            for (int i = 0; i < controlPoints.Count; i++)
            {
                int x = (int)(origin.X + (controlPoints[i].X - origin.X) * c);  //cộng thêm khoảng ước lượng từ vị trí scale với vị trí gốc ban đầu 
                int y = (int)(origin.Y + (controlPoints[i].Y - origin.Y) * c);
                controlPoints[i] = new Point(x, y);
            }
            return true;
        }

        //Hàm tạo các điểm mới khi xoay đối tượng 
        public bool rotate(Point nStart, Point nEnd)
        {
            //kiểm tra điểm đầu cuối 
            if (nStart == nEnd)
            {
                return false;
            }

            //calculate angle
            Point origin = getOrigin();
            double angle = Math.Atan2(nEnd.Y - origin.Y, nEnd.X - origin.X) - Math.Atan2(nStart.Y - origin.Y, nStart.X - origin.X);

            Console.WriteLine("Rotate " + angle);

            // affine transform scale
            for (int i = 0; i < controlPoints.Count; i++)
            {
                //nhân thêm với góc quay angle, trong quá trình quay có thể xảy ra thêm việc scale đối tượng 
                int x = (int)(origin.X + (controlPoints[i].X - origin.X) * Math.Cos(angle) - (controlPoints[i].Y - origin.Y) * Math.Sin(angle)); //hoành thì nhân thêm lượng cos tangle
                int y = (int)(origin.Y + (controlPoints[i].X - origin.X) * Math.Sin(angle) + (controlPoints[i].Y - origin.Y) * Math.Cos(angle));    //tung thì lượng sin 
                controlPoints[i] = new Point(x, y);
            }
            return true;
        }
    }
}

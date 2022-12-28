using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Random    //class dùng để vẽ tự do một đối tượng 
    {
        private OpenGL gl;
        private List<Point> listPoints = new List<Point>();

        //khởi tạo 
        public Random(OpenGL ngl)
        {
            gl = ngl;
        }

        //hàm dùng để sao chép lại class Random 
        public Random(Random newDrp)
        {
            gl = newDrp.gl;
            listPoints = new List<Point>(newDrp.listPoints);
        }

        //hàm copy 
        public Random(OpenGL ngl, List<Point> nPoints)
        {
            gl = ngl;
            listPoints = new List<Point>(nPoints);
        }

        //nhập các điểm tọa độ 
        public void add_Point(Point nP)
        {
            listPoints.Add(nP);
        }

        //xóa các điểm tọa độ 
        public void clear_Points()
        {
            listPoints.Clear();
        }

        //Nhập các điểm điều khiển 
        public List<Point> getControlPoints() // get theo chieu kim dong ho
        {
            List<Point> cp = new List<Point>(listPoints);
            // 1 theo chieu kim dong ho, 2 nguoc chieu
            //kiểm tra số phần tử trong listPoints
            if (listPoints.Count >= 2)
            {
                if (cp[1].X < cp[0].X)  //neu nguoc chieu thi doi nguoc lai
                {
                    cp.Reverse();   //đảo chiều 
                }
            }
            return cp;
        }

        //Trả về các điểm điều khiển để vẽ 
        public List<Point> getDrawingPoints()
        {
            return getControlPoints();
        }
    }
}

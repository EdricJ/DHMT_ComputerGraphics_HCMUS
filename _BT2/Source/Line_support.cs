using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Line_support
    {
        private Point pStart, pEnd; //điểm bắt đầu và điểm kết thúc 

        // addition data init
        public int xmin = 0, xmax = 0, ymin = 0, ymax = 0;  //các mốc tọa độ x, y 
        private double delta; // dy / dx

        // addition data update
        private int xLine = 0, yLine = 0;
        private int modeLine = 0; 

        //constant data mode 
        public const int THROUGH_NONE = 0, THROUGH_START = 1, THROUGH_MIDDLE = 2, THROUGH_END = 3;  // # 3th, 0 no, 1 start, 2 middle, 3 end


        public Line_support(Point start, Point end)
        {
            pStart = start;
            pEnd = end;
            //xác định điểm đầu tiên dựa theo trục hoành để dễ dàng tô (xác định như theo việc vẽ đường thẳng) 
            if (pStart.X > pEnd.X)
            {
                xmax = pStart.X;
                xmin = pEnd.X;
            }
            else
            {
                xmax = pEnd.X;
                xmin = pStart.X;
            }

            //xác định điểm đầu tiên dựa theo trục tung để dễ dàng tô 
            if (pStart.Y > pEnd.Y)
            {
                ymax = pStart.Y;
                ymin = pEnd.Y;
            }
            else
            {
                ymax = pEnd.Y;
                ymin = pStart.Y;
            }

            //tính delta theo như công thức trong lý thuyết 
            delta = (start.Y - end.Y) * 1.0 / (start.X - end.X);
        }

        //hàm xác định chế độ tô màu theo dòng dựa vào trạng thái các điểm 
        public void setModelineGoThrough(int y)
        {
            if (y == pStart.Y && y == pEnd.Y) // truong hop ca duong thang la scanline
            {
                modeLine = THROUGH_NONE;
            }
            else if (y == pStart.Y)     //điểm đầu tiên 
            {
                modeLine = THROUGH_START;
            }
            else if (y == pEnd.Y)   //điểm kết thúc 
            {
                modeLine = THROUGH_END;
            }
            else if (y > ymin && y < ymax)  //điểm vẫn nằm trong tọa độ shape 
            {
                modeLine = THROUGH_MIDDLE;
            }
        }

        //cập nhật lại điểm vẽ theo từng giai đoạn 
        public void updatePointGoThrough(int y)
        {
            yLine = y;
            setModelineGoThrough(y);    //gán mode 
            switch (modeLine)
            {
                case 1:         //nếu mode là điểm đang ở vị trí bắt đầu 
                    xLine = pStart.X;
                    break;
                case 2:         //nếu mode là diểm đang ở trong shape 
                    xLine = (int)((y - pStart.Y + delta * pStart.X) / delta);   //xác định điểm kế tiếp (tìm giao điểm)
                    break;
                case 3:         //nếu mode là điểm đang ở vị trí kết thúc 
                    xLine = pEnd.X;
                    break;
            }
        }

        //hàm trả về 1 điểm mới 
        public Point getPointGoThrough()
        {
            return new Point(xLine, yLine);
        }

        //gọi hàm trả về chế độ của điểm đang tô 
        public int getModeLineGoThrough()
        {
            return modeLine;
        }
    }
}

using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class FillColor_Scanline
    {
        private OpenGL gl;
        private Color fillColor = Color.Black;
        private List<Point> listPoints = new List<Point>(); // nhung diem cua da giac
        private List<Point> filledPoints = new List<Point>(); // nhung diem da to, luu lai de truyen vao ham draw de ve

        //constructor 
        public FillColor_Scanline(OpenGL ngl, List<Point> nPoints)
        {
            gl = ngl;
            listPoints = nPoints;
        }

        //hàm lấy những điểm cần tô 
        private List<Line_support> getLinesByPoints() // tu nhung diem 
        {
            List<Line_support> re = new List<Line_support>();
            if (listPoints.Count > 2)
            {
                Point preP = listPoints[listPoints.Count - 1];  //lấy ra điểm trước đó 
                for (int i = 0; i < listPoints.Count; i++)
                {
                    if (i == 0)
                    {
                        Line_support nLine = new Line_support(preP, listPoints[i]);
                        re.Add(nLine);
                    }
                    else
                    {
                        Line_support nLine = new Line_support(preP, listPoints[i]);
                        re.Add(nLine);
                    }
                    preP = listPoints[i];   //cập nhật lại điểm ở trước, và cứ thế tuần tự 
                }
            }
            return re;
        }

        public List<Point> getFilledPoints()
        {
            return filledPoints;
        }

        //tìm ở tọa độ đầu tiên theo trục tung những điểm lớn nhất, nhỏ nhất
        private List<int> getYMinMax() // [min, max]
        {
            List<int> re = new List<int>();
            re.Add(listPoints[0].Y);
            re.Add(listPoints[0].Y);
            foreach (Point p in listPoints) //gọi vòng lặp để kiểm tra 
            {
                if (p.Y < re[0])    //nếu điểm đầu tiên trong listPoints có tung độ y nhỏ hơn 
                {
                    re[0] = p.Y;    //cập nhật lại điểm y 
                }
                if (p.Y > re[1])    //ngược lại cũng cập nhật mới lại 
                {
                    re[1] = p.Y;
                }
            }
            return re;
        }

        //hàm thực hiện tô 
        public void fill()
        {
            if (listPoints.Count > 2)
            {
                List<Line_support> linesData = getLinesByPoints();  //lấy danh sách điểm 
                List<int> yMinMax = getYMinMax();   //xác định chính xác tọa độ 
                for (int yi = yMinMax[0]; yi <= yMinMax[1]; yi++)
                {
                    List<Line_support> listLines = new List<Line_support>();  //khơi tạo các danh sách mới để lưu 
                    List<Point> listPointsFill = new List<Point>();
                    foreach (Line_support line in linesData)    //gọi vòng chạy qua từng điểm 
                    {
                        line.setModelineGoThrough(yi);  //cài đặt chế độ 
                        if (line.getModeLineGoThrough() != Line_support.THROUGH_NONE)   
                        {
                            line.updatePointGoThrough(yi);      //cập nhật lại điểm cần tô theo dạng đường thẳng
                            listLines.Add(line);         //lưu vết các cạnh 
                        }
                    }

                    //sắp xếp cấu trúc AEL theo x_int 
                    listLines.Sort((a, b) =>
                    {
                        int x1 = a.getPointGoThrough().X, x2 = b.getPointGoThrough().X;
                        if (x1 > x2)    //xét điều kiện để sắp xếp lại các cạnh  
                        {
                            return 1;   //comparer = 1 
                        }
                        else if (x1 < x2)
                        {
                            return -1; //comparer = -1
                        }
                        return 0;   //comparer = 0 
                    });

                    for (int i = 0; i < listLines.Count; i++)
                    {
                        Point p = listLines[i].getPointGoThrough(); //xét điểm đang ở trạng thái 
                        
                        if (listLines[i].getModeLineGoThrough() == Line_support.THROUGH_MIDDLE)  //điểm đang xét đang trong đối tượng shape 
                        {
                            listPointsFill.Add(p);  //lưu điểm lại vào danh sách điểm cần tô 
                        }
                        if (listLines[i].getModeLineGoThrough() == Line_support.THROUGH_END || listLines[i].getModeLineGoThrough() == Line_support.THROUGH_START) //điểm đang xét ở một trong 2 trạng thái bắt đầu hoặc kết thúc 
                        {
                            if (i < listLines.Count - 1)     //nếu i ở gần vị trí cuối, hay điểm i vẫn thuộc trong đối tượng 
                            {
                                Point p2 = listLines[i + 1].getPointGoThrough();   //xét p2 là điểm sát biên cạnh 

                                if (p.X == p2.X && listLines[i].ymax >= p.Y && listLines[i + 1].ymax > p.Y)  //xét p vẫn chưa vượt ngưỡng khi gần kết thúc
                                {
                                    listPointsFill.Add(p);  //thêm vào danh sách điểm cần tô 
                                }

                                if (p.X == p2.X && listLines[i].ymin <= p.Y && listLines[i + 1].ymin < p.Y)  //xét p trên ngưỡng bắt đầu 
                                {
                                    listPointsFill.Add(p);
                                }
                            }
                        }
                    }

                    //lưu danh sách các điểm cần tô lại vào một danh sách tô public để truyền vào hàm draw 
                    if (listPointsFill.Count >= 2)  //có từ 2 điểm trở lên 
                    {
                        foreach (Point p in listPointsFill)     //gọi vòng lặp qua từng điểm để thêm các điểm đó vào danh sách tô public 
                        {
                            filledPoints.Add(new Point(p.X, p.Y));
                        }
                    }
                }
            }
        }
    }
}

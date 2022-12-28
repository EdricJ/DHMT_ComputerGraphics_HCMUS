using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab1.Form1;

namespace lab1
{
    class ChosenOption
    {
        private int mode = 0; // chon che độ để vẽ, ve hinh gi? vd: 0: duong thang, 1: duong tron,...
        private Point pStart, pEnd; // y = gl.Height - y
        private OpenGL gl;

        //constant data
        public const int RANDOM = -1, LINE = 0, CIRCLE = 1, RECTANGLE = 2, TRIANGLE = 3, ELLIPSE = 4, PENTAGON = 5, HEXAGON = 6;    //các chế độ 

        public ChosenOption(int newMode, Point nStart, Point nEnd, OpenGL ngl)
        {
            mode = newMode;
            pStart = nStart;
            pEnd = nEnd;
            gl = ngl;
        }

        //tính tổng giá trị ở các hoành độ 
        private int Total_X(List<Point> ctrlPoints)
        {
            int sum = 0;
            for(int i = 0; i < ctrlPoints.Count; i++)
            {
                sum += ctrlPoints[i].X;
            }

            return sum;
        }

        //tính tổng giá trị ở các tung độ 
        private int Total_Y(List<Point> ctrlPoints)
        {
            int sum = 0;
            for (int i = 0; i < ctrlPoints.Count; i++)
            {
                sum += ctrlPoints[i].Y;
            }

            return sum;
        }

        //tìm tọa độ từ các điểm điều khiển 
        public ChosenOption(int newMode, List<Point> controlPoints, OpenGL ngl)
        {
            Console.WriteLine("ChosenOption");
            foreach (Point p in controlPoints)
            {
                Console.WriteLine(p.ToString());
            }
            mode = newMode;
            gl = ngl;
            
            //dựa theo quy luật nột tiếp một hình tròn vì các hình này đều có các cạnh bằng nhau 
            if (controlPoints.Count >= 2)
            {
                if (newMode == LINE)
                {
                    pStart = controlPoints[0];
                    pEnd = controlPoints[1];
                }
                else if (newMode == TRIANGLE)
                {
                    pStart.X = Total_X(controlPoints) / 3;   //chọn tọa độ điểm bắt đầu từ tâm 
                    pStart.Y = Total_Y(controlPoints) / 3;
                    pEnd = controlPoints[2];
                }
                else if (newMode == RECTANGLE)
                {
                    pStart.X = Total_X(controlPoints) / 4;  //có 4 đỉnh 
                    pStart.Y = Total_Y(controlPoints) / 4;
                    pEnd = controlPoints[3];
                }
                else if (newMode == PENTAGON)
                {
                    pStart.X = Total_X(controlPoints) / 5;  //có 5 đỉnh 
                    pStart.Y = Total_Y(controlPoints) / 5;
                    pEnd = controlPoints[4];
                }
                else if (newMode == HEXAGON)
                {
                    pStart.X = Total_X(controlPoints) / 6;  //có 6 đỉnh 
                    pStart.Y = Total_Y(controlPoints) / 6;
                    pEnd = controlPoints[5];
                }
                //hình tròn thì lấy điểm điều kiển ở tâm và trên đường tròn 
                else if (newMode == CIRCLE)
                {
                    pStart = controlPoints[0];
                    pEnd = controlPoints[1];
                }
                //ellipse lấy thêm tọa độ nằm ở đỉnh của 2 trục cung voi tam
                else if (newMode == ELLIPSE)
                {
                    pStart = controlPoints[0];
                    pEnd = new Point(controlPoints[1].X, controlPoints[2].Y);
                }
            }
        }

        //hàm dùng để xét các điểm điều khiển trên khung tròn bao (trừu tượng các hình đa giác đều nội tiếp đường tròn)
        private List<Point> getControlPointsFromEqualPolygon(int pNum)
        {
            List<Point> listP = new List<Point>();
            double x1 = pStart.X, x2 = pEnd.X, y1 = pStart.Y, y2 = pEnd.Y,
              AB = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)); //tính khoảng cách giữa 2 tọa độ 

            double r = Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2)), //tính khoảng cách bán kính 
                alpha = Math.Atan2((x2 - x1), (y2 - y1)); 

            for (double i = 0; i < pNum; i++)
            {
                double theta = (2.0f * Config.pi * i / pNum + Config.pi / 2 - alpha);//get the current angle 
                double x = r * Math.Cos(theta); //calculate the x component 
                double y = r * Math.Sin(theta); //calculate the y component 
                listP.Add(new Point((int)(x1 + x), (int)(y1 + y)));
            }
            return listP;
        }

        //kiểm tra xem có vẽ ngược chiều kim đồng hồ 
        private bool isListPointsClockWise(List<Point> points)
        {
            int smallestYIndex = 0;
            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].Y < points[smallestYIndex].Y)
                {
                    smallestYIndex = i;
                }
            }

            return points[(smallestYIndex + 1) % points.Count].X < points[(smallestYIndex - 1) < 0 ? (points.Count - 1) : (smallestYIndex - 1)].X; //trả về các điểm tọa độ 
        }

        public List<Point> getControlPoints()
        {
            List<Point> listP = new List<Point>();
            if (mode == LINE)
            {
                listP.Add(pStart);
                listP.Add(pEnd);
            }
            else if (mode == CIRCLE) // hinh tron thi tam va 4 diem tren duong tron
            {
                listP.Add(pStart);
                listP.AddRange(getControlPointsFromEqualPolygon(4));
            }
            else if (mode == RECTANGLE) // hinh chu nhat  thi 4 diem goc
            {
                listP.AddRange(getControlPointsFromEqualPolygon(4));
            }
            else if (mode == TRIANGLE) // hinh tam giac thi 3 diem
            {
                listP.AddRange(getControlPointsFromEqualPolygon(3));
            }
            else if (mode == PENTAGON)
            {
                listP.AddRange(getControlPointsFromEqualPolygon(5)); // hinh ngu giac thi 5 diem
            }
            else if (mode == HEXAGON)
            {
                listP.AddRange(getControlPointsFromEqualPolygon(6));    // hinh luc giac thi 6 diem
            }
            else if (mode == ELLIPSE)
            {
                listP.Add(pStart);
                double x1 = pStart.X, x2 = pEnd.X, y1 = pStart.Y, y2 = pEnd.Y;
                // tinh r1, r2 (bán kính theo tung và hoành)
                double rx = Math.Abs(x1 - x2), ry = Math.Abs(y1 - y2);

                double theta = 2 * Config.pi / 4;   //chia theo 4 phần của ellipse 
                double c = Math.Cos(theta);//precalculate the sine and cosine
                double s = Math.Sin(theta);
                double t; //temp 

                double x = 1;//we start at angle = 0 
                double y = 0;

                //chạy theo 4 đỉnh 
                for (double i = 0; i < 4; i++)
                {
                    //apply radius and offset
                    listP.Add(new Point((int)(x * rx + x1), (int)(y * ry + y1)));//output vertex 

                    //apply the rotation matrix
                    t = x;
                    x = c * x - s * y;
                    y = s * t + c * y;
                }
            }

            // vi 1 la theo chieu kim dong ho, 2 la nguoc chieu => neu nguoc chieu thi doi nguoc lai
            if (!isListPointsClockWise(listP) && mode != CIRCLE && mode != ELLIPSE)
            {
                Console.WriteLine("Reversed");
                listP.Reverse();
            }
            return listP;
        }

        public List<Point> getDrawingPoints()
        {
            List<Point> re = new List<Point>();
            if (mode == LINE)
            {
                /*
                List<Point> temp = new List<Point>(getControlPoints());
                pStart = temp[0];
                pEnd = temp.Last();

                //Vẽ từ điểm có hoành độ nhỏ hơn
                if (pStart.X > pEnd.X)
                    (pStart, pEnd) = (pEnd, pStart);

                //Tịnh tiến sao cho pStart trùng với (0, 0), vector tịnh tiến là move
                Point move = new Point(pStart.X, pStart.Y);
                (pStart.X, pStart.Y) = (0, 0);
                (pEnd.X, pEnd.Y) = (pEnd.X - move.X, pEnd.Y - move.Y);

                //Nếu dx bằng 0, đường thẳng đứng
                if (pEnd.X == 0)
                {
                    for (int i = Math.Min(0, pEnd.Y); i <= Math.Max(0, pEnd.Y); i++)
                        re.Add(new Point(move.X, i + move.Y));
                    //return;
                }
                else
                {
                    //Thuật toán Bresenham
                    int dy2 = 2 * pEnd.Y, dx2 = 2 * pEnd.X;
                    float m = (float)dy2 / dx2;
                    bool negativeM = false, largeM = false;

                    //Nếu m < 0, đối xứng qua trục x = 0
                    if (m < 0)
                    {
                        pEnd.Y = -pEnd.Y;
                        dy2 = -dy2;
                        m = -m;
                        negativeM = true;
                    }

                    //Nếu m > 1, đối xứng qua trục y = x
                    if (m > 1)
                    {
                        (pEnd.X, pEnd.Y) = (pEnd.Y, pEnd.X);
                        (dy2, dx2) = (dx2, dy2);
                        largeM = true;
                    }

                    //Tính p0
                    int p = dy2 - pEnd.X;

                    //List chứa các điểm vẽ
                    List<Point> points = new List<Point>();

                    int x = 0, y = 0;
                    points.Add(new Point(x, y));
                    while (x < pEnd.X)
                    {
                        if (p > 0)
                        {
                            x++;
                            y++;
                            p += dy2 - dx2;
                        }
                        else
                        {
                            x++;
                            p += dy2;
                        }
                        points.Add(new Point(x, y));
                    }

                    //Đối xứng lại qua trục y = x (nếu cần)
                    if (largeM == true)
                        for (int i = 0; i < points.Count; i++)
                            points[i] = new Point(points[i].Y, points[i].X);

                    //Đối xứng lại qua trục y = 0 (nếu cần)
                    if (negativeM == true)
                        for (int i = 0; i < points.Count; i++)
                            points[i] = new Point(points[i].X, -points[i].Y);


                    temp.Clear();
                    re.AddRange(points);
                }
                */
                re.Add(pStart);
                re.Add(pEnd);
            }
            else if (mode == CIRCLE)
            {
                
                //Vẽ đường tròn bằng 2 điểm chuột pStart và pEnd

                //Tính tập điểm theo chiều kim đồng hồ
                //Nhưng winform tính điểm (0, 0) từ góc trên xuống => ngược chiều kim đồng hồ

                List<Point> temp = new List<Point>(getControlPoints());
                pStart= temp[0];
                pEnd= temp[1];

                //Tính bán kính r
                double r = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));

                //Tính p0
                double decision = 5 / 4 - r;

                //Điểm đầu (0, r)
                int x = 0;
                int y = (int)r;

                //Tâm đường tròn, cũng chính là vector tịnh tiến
                Point pCenter = new Point(pStart.X, pStart.Y);

                //Tập điểm vẽ ở 1/8 và đối xứng của 1/8 qua trục y = x
                List<Point> partsOfCircle = new List<Point>();

                //Thêm điểm đầu vào tập điểm vẽ
                partsOfCircle.Add(new Point(x, y));
                re.Add(new Point(x + pCenter.X, y + pCenter.Y));

                int x2 = x * 2, y2 = y * 2;

                //Thuật toán Midpoint
                while (y > x)
                {
                    if (decision < 0)
                    {
                        decision += x2 + 3;
                        x++;
                        x2 += 2;
                    }
                    else
                    {
                        decision += x2 - y2 + 5;
                        x++;
                        y--;
                        x2 += 2;
                        y2 -= 2;
                    }

                    //Tịnh tiến mỗi điểm theo tâm, rồi thêm vào tập điểm vẽ
                    partsOfCircle.Add(new Point(x, y));
                    re.Add(new Point(x + pCenter.X, y + pCenter.Y));
                }

                //Đối xứng qua trục y = x, rồi tịnh tiến theo tâm
                Point p;
                int i, size = partsOfCircle.Count();
                for (i = size - 1; i >= 0; i--)
                {
                    p = partsOfCircle[i];
                    partsOfCircle.Add(new Point(p.Y, p.X));
                    re.Add(new Point(p.Y + pCenter.X, p.X + pCenter.Y));
                }

                //Đối xứng 1/4 qua trục y = 0, rồi tịnh tiến theo tâm
                size = partsOfCircle.Count();
                for (i = size - 1; i >= 0; i--)
                {
                    p = partsOfCircle[i];
                    partsOfCircle.Add(new Point(p.X, -p.Y));
                    re.Add(new Point(p.X + pCenter.X, -p.Y + pCenter.Y));
                }

                //Đối xứng 1/2 qua trục x = 0, rồi tịnh tiến theo tâm
                size = partsOfCircle.Count();
                for (i = size - 1; i >= 0; i--)
                {
                    p = partsOfCircle[i];
                    re.Add(new Point(-p.X + pCenter.X, p.Y + pCenter.Y));
                }

                temp.Clear();
                
                //or
                //re = getControlPointsFromEqualPolygon(25); //5 points_pow(r,2)
            }
            else if (mode == RECTANGLE)
            {
                re = getControlPointsFromEqualPolygon(4);
            }
            else if (mode == TRIANGLE)
            {
                re = getControlPointsFromEqualPolygon(3);
            }
            else if (mode == PENTAGON)
            {
                re = getControlPointsFromEqualPolygon(5);
            }
            else if (mode == HEXAGON)
            {
                re = getControlPointsFromEqualPolygon(6);
            }
            else if (mode == ELLIPSE)
            {
                /*
                 If the ellipse is ((x-cx)/a)^2 + ((y-cy)/b)^2 = 1 then change the glVertex2f call to glVertext2d(a*x + cx, b*y + cy);
                 
                 */
                double x1 = pStart.X, x2 = pEnd.X, y1 = pStart.Y, y2 = pEnd.Y;
                
                // tinh r1, r2
                double rx = Math.Abs(x1 - x2), ry = Math.Abs(y1 - y2);  //tính các khoảng ước lượng 

                double theta = 2 * Config.pi / 360.0;   //tính góc 
                double c = Math.Cos(theta);//precalculate the sine and cosine
                double s = Math.Sin(theta);
                double t; //temp 

                double x = 1;//we start at angle = 0 
                double y = 0;

                for (double i = 0; i < 360.0; i++)
                {
                    //apply radius and offset
                    //gl.Vertex(x * rx + x1, y * ry + y1);//output vertex 
                    re.Add(new Point((int)(x * rx + x1), (int)(y * ry + y1)));

                    //apply the rotation matrix
                    t = x;
                    x = c * x - s * y;
                    y = s * t + c * y;
                }
            }
            return re;
        }
    }
}
using Org.BouncyCastle.Asn1;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    internal class FillColor
    {
        private OpenGL gl;
        private Point pStart;
        private Color fillColor = Color.White; //
        private int BOUNDARY = 0, FLOOD = 1;
        private List<Point> filledPoints = new List<Point>();

        //Is_sameColor
        private bool sameRGB(Color c1, Color c2)
        {
            return (c1.R == c2.R && c1.G == c2.G && c1.B == c2.B);
        }

        //constructor
        public FillColor(OpenGL ngl, Point nStart, Color nFillColor)
        {
            gl = ngl;
            pStart = nStart;
            fillColor = nFillColor;
        }

        //lấy giá trị màu của điểm ảnh 
        private Color getPixel_Color(int x, int y)
        {
            byte[] color = new byte [3];  //unsigned char ~~ byte, if new byte [4] items, the fourth is color.A
            gl.ReadPixels(x, gl.RenderContextProvider.Height - y, 1, 1, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, color);   //if 4 items, mode OpenGL.GL_RGBA
            /* ~~
            Color result = Color.Black;
            result.R = ptr[0];
            result.G = ptr[1];
            result.B = ptr[2];
            if 4 items result.A = ptr[3];
            */

            return Color.FromArgb(color[0], color[1], color[2]);    //gọi hàm để tổng hợp 3 điểm màu trong pixel lại và trả về lại giá trị màu pixel đó 
        }

        //đặt, gán màu cho điểm ảnh 
        private void putPixel_Color(int x, int y)
        {
            byte[] colorByte = new byte[3]; //unsigned char ~~ byte
            colorByte[0] = fillColor.R;         //lưu giá trị màu theo từng kênh 
            colorByte[1] = fillColor.G;
            colorByte[2] = fillColor.B;


            gl.RasterPos(x, gl.RenderContextProvider.Height - y);   //xét về vị trí thuộc đối tượng hình 
            gl.DrawPixels(1, 1, OpenGL.GL_RGB, colorByte);      //if 4 items, mode OpenGL.GL_RGBA
            gl.Flush();

            filledPoints.Add(new Point(x, gl.RenderContextProvider.Height - y));    //lưu lại các điểm được tô, được gán màu 
        }

        public List<Point> getFilledPoints()
        {
            return filledPoints;    //trả về danh sách điểm đã tô 
        }


        private void BoundaryFillHelper(int x, int y, Color edgeColor)
        {
            if (x < gl.RenderContextProvider.Width && y < gl.RenderContextProvider.Height && x >= 0 && y >= 0) // xet điểm ở trong opengl provider
            {
                if (!sameRGB(getPixel_Color(x, y), fillColor) && !sameRGB(getPixel_Color(x, y), edgeColor)) //xét điểm màu hiện tại có là điểm màu đã tô hoặc là điểm màu biên cạnh hay không 
                {
                    putPixel_Color(x, y);
                            //gọi đệ quy để gán lần lượt các điểm lân cận 4 đến khi kín toàn bộ shape 
                    BoundaryFillHelper(x - 1, y, edgeColor);
                    BoundaryFillHelper(x, y + 1, edgeColor);
                    BoundaryFillHelper(x + 1, y, edgeColor);
                    BoundaryFillHelper(x, y - 1, edgeColor);
                }
            }
        }

        private void FloodFillHelper(int x, int y, Color oldColor)
        {
            if (x < gl.RenderContextProvider.Width && y < gl.RenderContextProvider.Height && x >= 0 && y >= 0) // xet điểm ở trong opengl provider
            {
                if (sameRGB(getPixel_Color(x, y), oldColor))    //điểm màu đang xét có là điểm màu cũ
                {
                    putPixel_Color(x, y);         //gán điểm đầu tiên 
                    
                    FloodFillHelper(x - 1, y, oldColor);
                    FloodFillHelper(x, y + 1, oldColor);
                    FloodFillHelper(x + 1, y, oldColor);
                    FloodFillHelper(x, y - 1, oldColor);
                }
            }
        }

        //cải tiến phương pháp tô loang không theo đệ quy (khử đệ quy)
        void Re_FillHelper(Color use, int mode)
        {
            Stack<Point> A = new Stack<Point>();   //khởi tạo một stack để chứa các điểm cần xét theo lân cận 4 
            A.Push(pStart); //đẩy vào điểm đầu tiên là điểm bắt đầu tô 

            while (A.Count > 0) //vòng lặp để thực hiện tô 
            {
                Point np = A.Pop();     //lấy vào điểm đầu tiên ra, hoặc lấy từng điểm ra khi đã push ở phía dưới 
                if (np.X < gl.RenderContextProvider.Width && np.Y < gl.RenderContextProvider.Height && np.X >= 0 && np.Y >= 0) // xet điểm ở trong opengl provider
                {
                    if (mode == BOUNDARY)   // kiểm tra chế độ tô 
                    {

                        if (!sameRGB(getPixel_Color(np.X, np.Y), fillColor) && !sameRGB(getPixel_Color(np.X, np.Y), use))   //kiểm tra màu đang xét với màu trước đó như Boundary fill  
                        {
                            putPixel_Color(np.X, np.Y); //gán giá trị pixel

                            A.Push(new Point(np.X - 1, np.Y));  //đẩy vào stack lần lượt các điểm theo lận cận 4 
                            A.Push(new Point(np.X, np.Y + 1));
                            A.Push(new Point(np.X + 1, np.Y));
                            A.Push(new Point(np.X, np.Y - 1));

                            //chưa thể thực hiện việc tô 8 điểm lân cận vì các đường biên của đối tượng chưa được khép kín hoàn toàn, khi tô 8 điểm lân cận sẽ bị tràn màu  
                            /*A.Push(new Point(np.X - 1, np.Y));
                            A.Push(new Point(np.X + 1, np.Y));
                            A.Push(new Point(np.X, np.Y - 1));
                            A.Push(new Point(np.X, np.Y + 1));
                            A.Push(new Point(np.X - 1, np.Y + 1));
                            A.Push(new Point(np.X + 1, np.Y - 1));
                            A.Push(new Point(np.X + 1, np.Y + 1));
                            A.Push(new Point(np.X - 1, np.Y - 1));*/
                        }
                    }
                    else if(mode == FLOOD)
                    {
                        if (sameRGB(getPixel_Color(np.X, np.Y), use))   //kiểm tra giá trị màu tương tự như Flood fill 
                        {
                            putPixel_Color(np.X, np.Y);

                            A.Push(new Point(np.X - 1, np.Y));
                            A.Push(new Point(np.X, np.Y + 1));
                            A.Push(new Point(np.X + 1, np.Y));
                            A.Push(new Point(np.X, np.Y - 1));

                            /*A.Push(new Point(np.X - 1, np.Y));
                            A.Push(new Point(np.X + 1, np.Y));
                            A.Push(new Point(np.X, np.Y - 1));
                            A.Push(new Point(np.X, np.Y + 1));
                            A.Push(new Point(np.X - 1, np.Y + 1));
                            A.Push(new Point(np.X + 1, np.Y - 1));
                            A.Push(new Point(np.X + 1, np.Y + 1));
                            A.Push(new Point(np.X - 1, np.Y - 1));*/
                        }
                    }
                }
            }
        }

        public void BoundaryFill(Color edgeColor)
        {
            BoundaryFillHelper(pStart.X, pStart.Y, edgeColor);
        }

        public void FloodFill(Color oldColor)
        {
            FloodFillHelper(pStart.X, pStart.Y, oldColor);
        }

        //cải tiến phương pháp tô loang không theo đệ quy (khử đệ quy)
        public void RE_BoundaryFill(Color edgeColor)
        {
            Re_FillHelper(edgeColor, BOUNDARY);     //gọi hàm thực hiện việc gán điểm 
        }

        public void RE_FloodFill(Color oldColor)
        {
            Re_FillHelper(oldColor, FLOOD);
        }
    }
}


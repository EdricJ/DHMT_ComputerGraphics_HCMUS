using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Draw // for just drawing, tick chọn chức năng Draw và lựa chọn các đối tượng để vẽ 
    {
        OpenGL gl;
        uint openglMode;
        List<Point> listPoints = new List<Point>();
        Color user_color = Color.Black;
        float lineWidth = 1.0f; //kích thước nét vẽ 
        float pointSize = 1.5f; //Kích thước các vị trí tọa độ 

        //hàm constructor phương thức copy 
        public Draw(OpenGL ngl, uint nOpenglMode, List<Point> nListPoints, Color nColor, float nLineWidth)
        {
            gl = ngl;
            openglMode = nOpenglMode;
            listPoints = nListPoints;
            user_color = nColor;
            lineWidth = nLineWidth;
        }

        public Draw(OpenGL ngl, uint nOpenglMode, List<Point> nListPoints, Color nColor, float nLineWidth, float nPointSize)
        {
            gl = ngl;
            openglMode = nOpenglMode;
            listPoints = nListPoints;
            user_color = nColor;
            lineWidth = nLineWidth;
            pointSize = nPointSize;
        }

        //lưu màu của người dùng 
        public void setColor(Color nColor)
        {
            user_color = nColor;
        }

        //lưu nét vẽ của người dùng 
        public void setLineWidth(float nLineWidth)
        {
            lineWidth = nLineWidth;
        }

        //lưu các vị trí tọa độ 
        public void setPoints(List<Point> nPoints)
        {
            listPoints = new List<Point>(nPoints);
        }

        public void draw()
        {
            //người dùng chọn màu 
            gl.Color(user_color.R / 255.0, user_color.G / 255.0, user_color.B / 255.0, 0);
            
            //lưu lại nếu người dùng chọn nét vẽ và kích thước điểm 
            gl.LineWidth(lineWidth);
            gl.PointSize(pointSize);

            //gọi chế độ vẽ 
            gl.Begin(openglMode);

            foreach (Point p in listPoints)
            {
                gl.Vertex(p.X, p.Y);
            }
            gl.End();
            gl.Flush();
        }
    }
}
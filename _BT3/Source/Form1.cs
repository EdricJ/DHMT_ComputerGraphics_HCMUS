using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab3
{
    public partial class Form1 : Form
    {
        private Point pStart, pEnd; //tọa độ các điểm 
        private bool started = false, ended = false;
        // started: luc bat dau mouse down va dang di chuyen tren man hinh, luc nay ended = false
        // ended: luc ket thuc mouse up, luc nay started = false
        private Stopwatch time_case;    //để tính thời gian vẽ 1 đối tượng 

        Random rand; // de cho ve da giac random
        
        //draw polygon
        List<Draw> Polygon_helper = new List<Draw>();    //hỗ trợ các đường thẳng để vẽ đa giác đều 
        List<Polygon> listPolygons = new List<Polygon>();
        
        //draw ellipse
        List<Draw> Ellipse_helper = new List<Draw>();
        List<Ellipse> listEllipses = new List<Ellipse>();        

        //selected
        List<Point> selectedPoints = new List<Point>();
        int selectedIndex = -1, selectedType = -1, selectedControlPointIndex = -1;  //kiểm tra vào các vị trí click vào đối tượng khi đang select 
        const int SELECTED_NONE = 0, SELECTED_POLYGON = 1, SELECTED_ELLIPSE = 2;

        //lớp lưu lại các chức năng cần gọi 
        public class Config
        {
            public const double pi = 3.1415926f;
            public const int RANDOM = -1, LINE = 0, CIRCLE = 1, RECTANGLE = 2, TRIANGLE = 3, ELLIPSE = 4, PENTAGON = 5, HEXAGON = 6;    //các mode vẽ polygon 
            public const int DISTANCE_POINT = 10; // khoang cach giua cac diem controlpoints năm trên dường biên  
        }

        //kiểm tra chức năng để trả về đúng đối tượng cần vẽ 
        private int getOption()
        {
            if (rd_bt_Line.Checked) //nếu radiobutton được chọn 
            {
                return ChosenOption.LINE;
            }
            else if (rd_bt_Circle.Checked)
            {
                return ChosenOption.CIRCLE;
            }
            else if (rd_bt_Rectangle.Checked)
            {
                return ChosenOption.RECTANGLE;
            }
            else if (rd_bt_Triangle.Checked)
            {
                return ChosenOption.TRIANGLE;
            }
            else if (rd_bt_Ellipse.Checked)
            {
                return ChosenOption.ELLIPSE;
            }
            else if (rd_bt_Pentagon.Checked)
            {
                return ChosenOption.PENTAGON;
            }
            else if (rd_bt_Hexagon.Checked)
            {
                return ChosenOption.HEXAGON;
            }
            return ChosenOption.LINE;
        }

        //khởi tạo lại điểm mới 
        void resetPoints()
        {
            pStart.X = pStart.Y = 0;
            pEnd.X = pEnd.Y = 0;
        }

        //Khởi tạo
        public Form1()
        {
            InitializeComponent();
            OpenGL gl = openGLControl.OpenGL;

            rand = new Random(gl);  //khởi tạo việc vẽ đối tượng 
            resetPoints();      //khởi tạo các điểm ban đầu 
            colorDialog1.Color = Color.Black;
        }

        //một số hàm cần thiết cho openGLControl
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(255.0f, 255.0f, 255.0f, 255.0f); //màu cho openGLControl  
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
        }

        private void form1_openGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            //click chuột trái để chọn điểm đầu tiên và vẽ 
            if (e.Button == MouseButtons.Left)
            {
                if (rd_bt_Random.Checked)
                {
                    started = true;     //tính thời gian vẽ 
                    ended = false;
                    Point nextP = new Point(e.Location.X, gl.RenderContextProvider.Height - e.Location.Y);  //tính tọa độ tiếp theo từ tọa độ ban đầu, với tung độ là khoảng cách so với tung độ của điểm đầu 
                    rand.add_Point(nextP);
                }
                
            }

            //click chuột phải để dừng lại việc vẽ random một hình tự do 
            if (e.Button == MouseButtons.Right)
            {
                if (rd_bt_Random.Checked)
                {
                    time_case = System.Diagnostics.Stopwatch.StartNew();
                    List<Point> points = rand.getControlPoints();
                    Polygon_helper.Add(new Draw(gl, OpenGL.GL_LINE_LOOP, points, colorDialog1.Color, (float)numericUpDown1.Value));
                    listPolygons.Add(new Polygon(points, ChosenOption.RANDOM));
                    //reset
                    started = false;
                    ended = true;
                    
                    rand.clear_Points();    //xoa tập điểm 
                }
            }
        }

        private void form1_openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            if (started)
            {
                pEnd = new Point(e.Location.X, gl.RenderContextProvider.Height - e.Location.Y); // tọa độ khi rê chuột đến vị trí cuối cùng 
            }
        }

        private void openGLControl_DragLeave(object sender, EventArgs e)
        {
            Console.WriteLine("Drag_Leave");
        }

        //click_button để xóa các đối tượng đã được vẽ trên openGLControl
        private void bt_Clear_Click(object sender, EventArgs e)
        {
            Polygon_helper.Clear();
            listPolygons.Clear();
            Ellipse_helper.Clear();
            listEllipses.Clear();

            //selected - xóa các điểm điều khiển của đối tượng được chọn 
            selectedPoints.Clear();
            selectedIndex = -1; selectedType = -1; selectedControlPointIndex = -1; //khởi tạo về mặc định 
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //gọi vòng lặp để vẽ lần lượt 
            foreach (Draw Draw in Polygon_helper)
            {
                Draw.draw();
            }
            foreach (Draw Draw in Ellipse_helper)
            {
                Draw.draw();
            }

            //lưu các điểm đang được select 
            List<Point> controlPoints = selectedPoints;
            if (controlPoints.Count > 0)
            {
                //Liệt kê tập điểm vẽ cho OpenGL 
                Draw controlPointsDraw = new Draw(gl, OpenGL.GL_POINTS, controlPoints, colorDialog1.Color, 1.0f, 5.0f);
                controlPointsDraw.draw();
            }

            if (started) // neu mouse dang down va di chuyen, ve duong tuy vao muốn ve cai gi
            {
                if (rd_bt_Draw.Checked)
                {
                    int drawingMode = getOption();
                    ChosenOption choose = new ChosenOption(drawingMode, pStart, pEnd, gl);
                    
                    uint openglMode = (drawingMode == ChosenOption.LINE) ? OpenGL.GL_LINES : OpenGL.GL_LINE_LOOP;
                    Draw live = new Draw(gl, openglMode, choose.getDrawingPoints(), colorDialog1.Color, (float)numericUpDown1.Value);
                    live.draw();    //khởi tạo biến và vẽ 
                }
                else if (rd_bt_Random.Checked)
                {
                    List<Point> points = rand.getControlPoints();
                    points.Add(new Point(pEnd.X, pEnd.Y));  //lưu điểm cuối nếu vẽ random vì không biết vẽ đến khi nào nên phải lưu điểm cuối 
                    //khi vẽ Random thì dùng GL_LINE_LOOP để các đường thẳng nối nhau 
                    Draw live = new Draw(gl, OpenGL.GL_LINE_LOOP, points, colorDialog1.Color, (float)numericUpDown1.Value);
                    live.draw();
                }
            }

            if (ended) // da mouse up, tinh thoi gian ve hinh
            {
                time_case.Stop();
                var elapsedMs = time_case.ElapsedMilliseconds;
                textBox1.Text = elapsedMs.ToString() + " ms";   //ghi vào hộp text box
                ended = false;
            }
        }

        //button Color được chọn 
        private void bt_Bangmau_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) // chi la de font mau sang hon :>
            {
                //gán màu được chọn hiển thị lên button 
                bt_Bangmau.BackColor = colorDialog1.Color;
            }
        }

        private void form1_openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            // update toa do end
            pEnd = new Point(e.Location.X, gl.RenderContextProvider.Height - e.Location.Y); //lưu tọa độ điểm cuối 
            if (rd_bt_Draw.Checked)
            {
                // chọn chế độ vẽ 
                int drawingMode = getOption();
                // calculate draw / control points
                ChosenOption newDrawChosen = new ChosenOption(drawingMode, pStart, pEnd, gl);
                uint openglMode = (drawingMode == ChosenOption.LINE) ? OpenGL.GL_LINES : OpenGL.GL_LINE_LOOP;   //vẽ đoạn thẳng 
                List<Point> points = newDrawChosen.getDrawingPoints();

                if ((drawingMode == ChosenOption.CIRCLE || drawingMode == ChosenOption.ELLIPSE) && points.Count > 2) //xét điểm điều khiển phải nhiều hơn 2 
                {
                    Ellipse_helper.Add(new Draw(gl, openglMode, points, colorDialog1.Color, (float)numericUpDown1.Value));
                    listEllipses.Add(new Ellipse(newDrawChosen.getControlPoints(), drawingMode));
                }
                else
                {
                    Polygon_helper.Add(new Draw(gl, openglMode, points, colorDialog1.Color, (float)numericUpDown1.Value));
                    listPolygons.Add(new Polygon(newDrawChosen.getControlPoints(), drawingMode));
                }

                // update status (da xong ve~ di chuyen tren man hinh)
                started = false;
                ended = true;
                // Bat dau stop watch dem thoi gian thuc thi ve hinh
                time_case = System.Diagnostics.Stopwatch.StartNew();
                // Reset 2 diem khi thực hiện xong 
                resetPoints();
            }
            //kiểm tra đến thao tác chọn đối tượng 
            else if (rd_bt_Select.Checked) // select and (dragging / scale / rotate / nothing)
            {
                //thao tác đầu là click chọn vào đối tượng 
                if (pStart == pEnd) //only select
                {
                    if (selectedType == SELECTED_POLYGON)
                    {
                        selectedPoints = new List<Point>(listPolygons[selectedIndex].getControlPoints());   //lưu vết các điểm điều khiển chọn 
                    }
                    else if (selectedType == SELECTED_ELLIPSE)
                    {
                        selectedPoints = new List<Point>(listEllipses[selectedIndex].getControlPoints());
                    }
                }
                //khi điểm đầu khác điểm cuối 
                else // select and scale
                {
                    List<Point> controlPoints = new List<Point>(), drawingPoints = new List<Point>();
                    // select and drag
                    if (selectedType == SELECTED_POLYGON)  //select a polygon
                    {
                        //khởi tạo một biến transform mới 
                        AffineTransform af = new AffineTransform(listPolygons[selectedIndex].getControlPoints(), listPolygons[selectedIndex].type); //gọi thao tác điều chỉnh với đối tượng 
                        bool didTransform = false;

                        //begin transform
                        if (selectedControlPointIndex == -1) // neu select polygon nhung khong select control point => translate (tịnh tiến)
                        {
                            if (af.translate(pStart, pEnd))
                            {
                                controlPoints = af.getControlPoints();
                                didTransform = true;    //đã thực hiện 
                            }
                        }
                        else // else rotate va scale => pStart la 1 diem control point, chọn vào các điểm control trên đường vẽ 
                        {
                            //do both rotate and scale (get updated control points after rotate)
                            // if one of them failed => do nothing =)))
                            if (af.rotate(pStart, pEnd) && af.scale(af.getControlPoints()[selectedControlPointIndex], pEnd))
                            {
                                controlPoints = af.getControlPoints();
                                didTransform = true;
                            }
                        }
                        // end transform

                        if (didTransform)   //nếu thao tác là một hình random 
                        {
                            //geting drawing points
                            if (listPolygons[selectedIndex].type == ChosenOption.RANDOM)
                            {
                                Random newDRP = new Random(gl, controlPoints);
                                drawingPoints = new List<Point>(newDRP.getDrawingPoints());
                            }
                            else // trường hợp khác là đoạn thẳng 
                            {
                                ChosenOption newDCO = new ChosenOption(listPolygons[selectedIndex].type, controlPoints, gl);
                                drawingPoints = new List<Point>(newDCO.getDrawingPoints());
                            }

                            // update lists
                            listPolygons[selectedIndex].setPoints(controlPoints);
                            selectedPoints = controlPoints;
                            Polygon_helper[selectedIndex].setPoints(drawingPoints);
                        }
                        else
                        {
                            selectedPoints = listPolygons[selectedIndex].getControlPoints();
                        }
                    }

                    //tương tự như chọn vào các hình trên 
                    if (selectedType == SELECTED_ELLIPSE) //select a ellipse
                    {
                        AffineTransform af = new AffineTransform(listEllipses[selectedIndex].getControlPoints(), listEllipses[selectedIndex].type);

                        bool didTransform = false;
                        //begin transform
                        if (selectedControlPointIndex == -1) // neu select ellipse nhung khong select control point => translate
                        {
                            if (af.translate(pStart, pEnd))
                            {
                                controlPoints = af.getControlPoints();
                                didTransform = true;
                            }
                        }
                        else //else rotate and scale, pStart la 1 diem control point
                        {
                            if (af.rotate(pStart, pEnd) && af.scale(af.getControlPoints()[selectedControlPointIndex], pEnd))
                            {
                                controlPoints = af.getControlPoints();
                                didTransform = true;
                            }
                        }
                        //end transform

                        if (didTransform) //trường hợp khác của ellipse là circle 
                        {
                            ChosenOption newDCO = new ChosenOption(listEllipses[selectedIndex].type, controlPoints, gl);
                            drawingPoints = new List<Point>(newDCO.getDrawingPoints());

                            listEllipses[selectedIndex].setPoints(controlPoints);
                            Ellipse_helper[selectedIndex].setPoints(drawingPoints);
                            selectedPoints = controlPoints;
                        }
                        else
                        {
                            selectedPoints = listEllipses[selectedIndex].getControlPoints();
                        }
                    }

                    //SELECTED NONE khi không click chuột vào đối tượng 
                }
            }
        }

        //bắt đầu click chuột để vẽ 
        private void form1_openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            pStart = new Point(e.Location.X, gl.RenderContextProvider.Height - e.Location.Y);   //tạo điểm đầu tiên 
            pEnd = pStart;

            if (rd_bt_Draw.Checked)
                started = true;     //bắt đầu tính thời gian vẽ 

            //xét điểm click chuột khi thao tác với đối tượng 
            else if (rd_bt_Select.Checked)
            {
                Point p = e.Location;
                p.Y = gl.RenderContextProvider.Height - p.Y;    //tọa độ Y cuối 
                bool selected = false;

                for (int i = 0; i < listPolygons.Count; i++)
                {
                    selectedControlPointIndex = listPolygons[i].selectedControlPointIndex(p);   //tìm xem vị trí control point 
                    if (selectedControlPointIndex != -1)
                    {
                        Console.WriteLine("selected a control point");
                        selectedIndex = i;      //lưu vị trí cho từng điểm control point  
                        selectedType = SELECTED_POLYGON;    //gán chế độ đang chọn shape nào 
                        selected = true;
                        break;
                    }

                    //kiểm tra điểm click là controlpoint trong đối tượng 
                    if (listPolygons[i].isPointInside(p))
                    {
                        Console.WriteLine("selected");
                        selectedIndex = i;
                        selectedType = SELECTED_POLYGON;

                        selected = true;
                        break;
                    }
                }

                if (!selected)
                {
                    //tương tự như với các hình khác 
                    for (int i = 0; i < listEllipses.Count; i++)
                    {
                        selectedControlPointIndex = listEllipses[i].selectedControlPointIndex(p);   //tương tự như trên polygon 
                        if (selectedControlPointIndex != -1)
                        {
                            Console.WriteLine("selected a control point");
                            selectedIndex = i;
                            selectedType = SELECTED_ELLIPSE;    //gán mode 
                            selected = true;
                            break;
                        }

                        //kiểm tra điểm click là controlpoint trong đối tượng
                        if (listEllipses[i].isPointInside(p))
                        {
                            Console.WriteLine("selected");
                            selectedIndex = i;
                            selectedType = SELECTED_ELLIPSE;
                            selected = true;
                            break;
                        }
                    }
                }

                if (!selected)
                {
                    //neu diem khong thuoc da giac nao, clear
                    Console.WriteLine("nothing selected");
                    selectedPoints.Clear();
                    selectedIndex = -1;
                    selectedControlPointIndex = -1;
                    selectedType = SELECTED_NONE;
                }
            }
        }
    }
}

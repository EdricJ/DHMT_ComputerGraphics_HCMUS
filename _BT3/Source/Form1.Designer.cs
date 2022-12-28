namespace lab3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openGLControl = new SharpGL.OpenGLControl();
            this.bt_Clear = new System.Windows.Forms.Button();
            this.bt_Bangmau = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.rd_bt_Line = new System.Windows.Forms.RadioButton();
            this.rd_bt_Circle = new System.Windows.Forms.RadioButton();
            this.rd_bt_Rectangle = new System.Windows.Forms.RadioButton();
            this.rd_bt_Triangle = new System.Windows.Forms.RadioButton();
            this.rd_bt_Ellipse = new System.Windows.Forms.RadioButton();
            this.rd_bt_Pentagon = new System.Windows.Forms.RadioButton();
            this.rd_bt_Hexagon = new System.Windows.Forms.RadioButton();
            this.rd_bt_Random = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.rd_bt_Draw = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rd_bt_Select = new System.Windows.Forms.RadioButton();
            this.gB_Mode = new System.Windows.Forms.GroupBox();
            this.gB_Draw = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.gB_Mode.SuspendLayout();
            this.gB_Draw.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(13, 13);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(809, 416);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.DragLeave += new System.EventHandler(this.openGLControl_DragLeave);
            this.openGLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.form1_openGLControl_MouseClick);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form1_openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.form1_openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.form1_openGLControl_MouseUp);
            // 
            // bt_Clear
            // 
            this.bt_Clear.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.bt_Clear.Location = new System.Drawing.Point(455, 504);
            this.bt_Clear.Name = "bt_Clear";
            this.bt_Clear.Size = new System.Drawing.Size(89, 47);
            this.bt_Clear.TabIndex = 1;
            this.bt_Clear.Text = "Clear";
            this.bt_Clear.UseVisualStyleBackColor = false;
            this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
            // 
            // bt_Bangmau
            // 
            this.bt_Bangmau.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bt_Bangmau.Location = new System.Drawing.Point(353, 505);
            this.bt_Bangmau.Name = "bt_Bangmau";
            this.bt_Bangmau.Size = new System.Drawing.Size(90, 46);
            this.bt_Bangmau.TabIndex = 2;
            this.bt_Bangmau.Text = "Bang mau";
            this.bt_Bangmau.UseVisualStyleBackColor = false;
            this.bt_Bangmau.Click += new System.EventHandler(this.bt_Bangmau_Click);
            this.bt_Bangmau.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form1_openGLControl_MouseDown);
            // 
            // rd_bt_Line
            // 
            this.rd_bt_Line.AutoSize = true;
            this.rd_bt_Line.Location = new System.Drawing.Point(19, 21);
            this.rd_bt_Line.Name = "rd_bt_Line";
            this.rd_bt_Line.Size = new System.Drawing.Size(57, 20);
            this.rd_bt_Line.TabIndex = 3;
            this.rd_bt_Line.TabStop = true;
            this.rd_bt_Line.Text = "LINE";
            this.rd_bt_Line.UseVisualStyleBackColor = true;
            // 
            // rd_bt_Circle
            // 
            this.rd_bt_Circle.AutoSize = true;
            this.rd_bt_Circle.Location = new System.Drawing.Point(717, 19);
            this.rd_bt_Circle.Name = "rd_bt_Circle";
            this.rd_bt_Circle.Size = new System.Drawing.Size(75, 20);
            this.rd_bt_Circle.TabIndex = 4;
            this.rd_bt_Circle.TabStop = true;
            this.rd_bt_Circle.Text = "CIRCLE";
            this.rd_bt_Circle.UseVisualStyleBackColor = true;
            // 
            // rd_bt_Rectangle
            // 
            this.rd_bt_Rectangle.AutoSize = true;
            this.rd_bt_Rectangle.Location = new System.Drawing.Point(474, 21);
            this.rd_bt_Rectangle.Name = "rd_bt_Rectangle";
            this.rd_bt_Rectangle.Size = new System.Drawing.Size(110, 20);
            this.rd_bt_Rectangle.TabIndex = 5;
            this.rd_bt_Rectangle.TabStop = true;
            this.rd_bt_Rectangle.Text = "RECTANGLE";
            this.rd_bt_Rectangle.UseVisualStyleBackColor = true;
            // 
            // rd_bt_Triangle
            // 
            this.rd_bt_Triangle.AutoSize = true;
            this.rd_bt_Triangle.Location = new System.Drawing.Point(104, 21);
            this.rd_bt_Triangle.Name = "rd_bt_Triangle";
            this.rd_bt_Triangle.Size = new System.Drawing.Size(95, 20);
            this.rd_bt_Triangle.TabIndex = 6;
            this.rd_bt_Triangle.TabStop = true;
            this.rd_bt_Triangle.Text = "TRIANGLE";
            this.rd_bt_Triangle.UseVisualStyleBackColor = true;
            // 
            // rd_bt_Ellipse
            // 
            this.rd_bt_Ellipse.AutoSize = true;
            this.rd_bt_Ellipse.Location = new System.Drawing.Point(608, 20);
            this.rd_bt_Ellipse.Name = "rd_bt_Ellipse";
            this.rd_bt_Ellipse.Size = new System.Drawing.Size(81, 20);
            this.rd_bt_Ellipse.TabIndex = 7;
            this.rd_bt_Ellipse.TabStop = true;
            this.rd_bt_Ellipse.Text = "ELLIPSE";
            this.rd_bt_Ellipse.UseVisualStyleBackColor = true;
            // 
            // rd_bt_Pentagon
            // 
            this.rd_bt_Pentagon.AutoSize = true;
            this.rd_bt_Pentagon.Location = new System.Drawing.Point(340, 21);
            this.rd_bt_Pentagon.Name = "rd_bt_Pentagon";
            this.rd_bt_Pentagon.Size = new System.Drawing.Size(104, 20);
            this.rd_bt_Pentagon.TabIndex = 8;
            this.rd_bt_Pentagon.TabStop = true;
            this.rd_bt_Pentagon.Text = "PENTAGON";
            this.rd_bt_Pentagon.UseVisualStyleBackColor = true;
            // 
            // rd_bt_Hexagon
            // 
            this.rd_bt_Hexagon.AutoSize = true;
            this.rd_bt_Hexagon.Location = new System.Drawing.Point(221, 21);
            this.rd_bt_Hexagon.Name = "rd_bt_Hexagon";
            this.rd_bt_Hexagon.Size = new System.Drawing.Size(94, 20);
            this.rd_bt_Hexagon.TabIndex = 9;
            this.rd_bt_Hexagon.TabStop = true;
            this.rd_bt_Hexagon.Text = "HEXAGON";
            this.rd_bt_Hexagon.UseVisualStyleBackColor = true;
            // 
            // rd_bt_Random
            // 
            this.rd_bt_Random.AutoSize = true;
            this.rd_bt_Random.Location = new System.Drawing.Point(221, 22);
            this.rd_bt_Random.Name = "rd_bt_Random";
            this.rd_bt_Random.Size = new System.Drawing.Size(88, 20);
            this.rd_bt_Random.TabIndex = 10;
            this.rd_bt_Random.TabStop = true;
            this.rd_bt_Random.Text = "RANDOM";
            this.rd_bt_Random.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(566, 524);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 11;
            // 
            // rd_bt_Draw
            // 
            this.rd_bt_Draw.AutoSize = true;
            this.rd_bt_Draw.Location = new System.Drawing.Point(18, 21);
            this.rd_bt_Draw.Name = "rd_bt_Draw";
            this.rd_bt_Draw.Size = new System.Drawing.Size(70, 20);
            this.rd_bt_Draw.TabIndex = 12;
            this.rd_bt_Draw.TabStop = true;
            this.rd_bt_Draw.Text = "DRAW";
            this.rd_bt_Draw.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(713, 523);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 13;
            // 
            // rd_bt_Select
            // 
            this.rd_bt_Select.AutoSize = true;
            this.rd_bt_Select.BackColor = System.Drawing.SystemColors.Info;
            this.rd_bt_Select.Location = new System.Drawing.Point(119, 21);
            this.rd_bt_Select.Name = "rd_bt_Select";
            this.rd_bt_Select.Size = new System.Drawing.Size(80, 20);
            this.rd_bt_Select.TabIndex = 14;
            this.rd_bt_Select.TabStop = true;
            this.rd_bt_Select.Text = "SELECT";
            this.rd_bt_Select.UseVisualStyleBackColor = false;
            // 
            // gB_Mode
            // 
            this.gB_Mode.BackColor = System.Drawing.SystemColors.Info;
            this.gB_Mode.Controls.Add(this.rd_bt_Draw);
            this.gB_Mode.Controls.Add(this.rd_bt_Random);
            this.gB_Mode.Controls.Add(this.rd_bt_Select);
            this.gB_Mode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gB_Mode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gB_Mode.Location = new System.Drawing.Point(13, 498);
            this.gB_Mode.Name = "gB_Mode";
            this.gB_Mode.Size = new System.Drawing.Size(324, 53);
            this.gB_Mode.TabIndex = 16;
            this.gB_Mode.TabStop = false;
            this.gB_Mode.Text = "Mode";
            // 
            // gB_Draw
            // 
            this.gB_Draw.BackColor = System.Drawing.SystemColors.Info;
            this.gB_Draw.Controls.Add(this.rd_bt_Line);
            this.gB_Draw.Controls.Add(this.rd_bt_Circle);
            this.gB_Draw.Controls.Add(this.rd_bt_Rectangle);
            this.gB_Draw.Controls.Add(this.rd_bt_Triangle);
            this.gB_Draw.Controls.Add(this.rd_bt_Ellipse);
            this.gB_Draw.Controls.Add(this.rd_bt_Pentagon);
            this.gB_Draw.Controls.Add(this.rd_bt_Hexagon);
            this.gB_Draw.Location = new System.Drawing.Point(13, 444);
            this.gB_Draw.Name = "gB_Draw";
            this.gB_Draw.Size = new System.Drawing.Size(809, 48);
            this.gB_Draw.TabIndex = 17;
            this.gB_Draw.TabStop = false;
            this.gB_Draw.Text = "Drawing options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(710, 504);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Time execute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(563, 505);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Line Width";
            // 
            // Form1
            // 
            this.AcceptButton = this.bt_Clear;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(833, 560);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gB_Draw);
            this.Controls.Add(this.gB_Mode);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.bt_Bangmau);
            this.Controls.Add(this.bt_Clear);
            this.Controls.Add(this.openGLControl);
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Name = "Form1";
            this.Text = "20120201_BT3";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.gB_Mode.ResumeLayout(false);
            this.gB_Mode.PerformLayout();
            this.gB_Draw.ResumeLayout(false);
            this.gB_Draw.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button bt_Clear;
        private System.Windows.Forms.Button bt_Bangmau;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.RadioButton rd_bt_Line;
        private System.Windows.Forms.RadioButton rd_bt_Circle;
        private System.Windows.Forms.RadioButton rd_bt_Rectangle;
        private System.Windows.Forms.RadioButton rd_bt_Triangle;
        private System.Windows.Forms.RadioButton rd_bt_Ellipse;
        private System.Windows.Forms.RadioButton rd_bt_Pentagon;
        private System.Windows.Forms.RadioButton rd_bt_Hexagon;
        private System.Windows.Forms.RadioButton rd_bt_Random;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RadioButton rd_bt_Draw;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rd_bt_Select;
        private System.Windows.Forms.GroupBox gB_Mode;
        private System.Windows.Forms.GroupBox gB_Draw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}


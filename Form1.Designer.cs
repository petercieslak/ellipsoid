namespace mgmk_ellipse {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            canvasGroupbox = new GroupBox();
            canvas = new PictureBox();
            menu = new GroupBox();
            scale = new NumericUpDown();
            accuracy = new NumericUpDown();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            illuminanceParam = new TextBox();
            ellipsoidParamSubmit = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            cEllipsoidParam = new TextBox();
            bEllipsoidParam = new TextBox();
            aEllipsoidParam = new TextBox();
            label1 = new Label();
            canvasGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
            menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)accuracy).BeginInit();
            SuspendLayout();
            // 
            // canvasGroupbox
            // 
            canvasGroupbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            canvasGroupbox.Controls.Add(canvas);
            canvasGroupbox.Location = new Point(12, 12);
            canvasGroupbox.Name = "canvasGroupbox";
            canvasGroupbox.Size = new Size(1145, 564);
            canvasGroupbox.TabIndex = 0;
            canvasGroupbox.TabStop = false;
            // 
            // canvas
            // 
            canvas.Dock = DockStyle.Fill;
            canvas.Location = new Point(3, 19);
            canvas.Name = "canvas";
            canvas.Size = new Size(1139, 542);
            canvas.TabIndex = 2;
            canvas.TabStop = false;
            canvas.Paint += canvas_Paint;
            canvas.MouseClick += canvas_MouseClick;
            canvas.MouseDown += canvas_MouseDown;
            canvas.MouseMove += canvas_MouseMove;
            canvas.MouseUp += canvas_MouseUp;
            canvas.Resize += canvas_Resize;
            // 
            // menu
            // 
            menu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            menu.Controls.Add(scale);
            menu.Controls.Add(accuracy);
            menu.Controls.Add(label8);
            menu.Controls.Add(label7);
            menu.Controls.Add(label6);
            menu.Controls.Add(label5);
            menu.Controls.Add(illuminanceParam);
            menu.Controls.Add(ellipsoidParamSubmit);
            menu.Controls.Add(label4);
            menu.Controls.Add(label3);
            menu.Controls.Add(label2);
            menu.Controls.Add(cEllipsoidParam);
            menu.Controls.Add(bEllipsoidParam);
            menu.Controls.Add(aEllipsoidParam);
            menu.Controls.Add(label1);
            menu.Location = new Point(1163, 12);
            menu.Name = "menu";
            menu.Size = new Size(135, 564);
            menu.TabIndex = 1;
            menu.TabStop = false;
            menu.Enter += groupBox3_Enter;
            // 
            // scale
            // 
            scale.DecimalPlaces = 1;
            scale.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            scale.Location = new Point(86, 260);
            scale.Name = "scale";
            scale.Size = new Size(43, 23);
            scale.TabIndex = 16;
            scale.Value = new decimal(new int[] { 1, 0, 0, 0 });
            scale.ValueChanged += scale_ValueChanged;
            // 
            // accuracy
            // 
            accuracy.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            accuracy.Location = new Point(86, 292);
            accuracy.Name = "accuracy";
            accuracy.Size = new Size(43, 23);
            accuracy.TabIndex = 15;
            accuracy.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 292);
            label8.Name = "label8";
            label8.Size = new Size(57, 15);
            label8.TabIndex = 12;
            label8.Text = "accuracy:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 262);
            label7.Name = "label7";
            label7.Size = new Size(36, 15);
            label7.TabIndex = 11;
            label7.Text = "scale:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(7, 228);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 10;
            label6.Text = "Rendering";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 139);
            label5.Name = "label5";
            label5.Size = new Size(72, 15);
            label5.TabIndex = 9;
            label5.Text = "illuminance:";
            // 
            // illuminanceParam
            // 
            illuminanceParam.Location = new Point(86, 136);
            illuminanceParam.Name = "illuminanceParam";
            illuminanceParam.Size = new Size(43, 23);
            illuminanceParam.TabIndex = 8;
            // 
            // ellipsoidParamSubmit
            // 
            ellipsoidParamSubmit.Location = new Point(54, 165);
            ellipsoidParamSubmit.Name = "ellipsoidParamSubmit";
            ellipsoidParamSubmit.Size = new Size(75, 23);
            ellipsoidParamSubmit.TabIndex = 7;
            ellipsoidParamSubmit.Text = "submit";
            ellipsoidParamSubmit.UseVisualStyleBackColor = true;
            ellipsoidParamSubmit.Click += ellipsoidParamSubmit_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 110);
            label4.Name = "label4";
            label4.Size = new Size(16, 15);
            label4.TabIndex = 6;
            label4.Text = "c:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 81);
            label3.Name = "label3";
            label3.Size = new Size(17, 15);
            label3.TabIndex = 5;
            label3.Text = "b:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 52);
            label2.Name = "label2";
            label2.Size = new Size(16, 15);
            label2.TabIndex = 4;
            label2.Text = "a:";
            // 
            // cEllipsoidParam
            // 
            cEllipsoidParam.Location = new Point(29, 107);
            cEllipsoidParam.Name = "cEllipsoidParam";
            cEllipsoidParam.Size = new Size(100, 23);
            cEllipsoidParam.TabIndex = 3;
            // 
            // bEllipsoidParam
            // 
            bEllipsoidParam.Location = new Point(29, 78);
            bEllipsoidParam.Name = "bEllipsoidParam";
            bEllipsoidParam.Size = new Size(100, 23);
            bEllipsoidParam.TabIndex = 2;
            // 
            // aEllipsoidParam
            // 
            aEllipsoidParam.Location = new Point(29, 49);
            aEllipsoidParam.Name = "aEllipsoidParam";
            aEllipsoidParam.Size = new Size(100, 23);
            aEllipsoidParam.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 0;
            label1.Text = "Ellipsoid parameters";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1310, 588);
            Controls.Add(menu);
            Controls.Add(canvasGroupbox);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            canvasGroupbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
            menu.ResumeLayout(false);
            menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)scale).EndInit();
            ((System.ComponentModel.ISupportInitialize)accuracy).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox canvasGroupbox;
        private GroupBox menu;
        private PictureBox canvas;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox cEllipsoidParam;
        private TextBox bEllipsoidParam;
        private TextBox aEllipsoidParam;
        private Label label1;
        private Button ellipsoidParamSubmit;
        private Label label5;
        private TextBox illuminanceParam;
        private Label label6;
        private Label label8;
        private Label label7;
        private NumericUpDown accuracy;
        private NumericUpDown scale;
    }
}
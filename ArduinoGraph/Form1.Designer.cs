namespace ArduinoGraph
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
            this.components = new System.ComponentModel.Container();
            this.serialHandler = new System.ComponentModel.BackgroundWorker();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.GL_Control = new OpenGL.GlControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.acuisitionTimer = new System.Windows.Forms.Timer(this.components);
            this.portEnumTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // serialHandler
            // 
            this.serialHandler.DoWork += new System.ComponentModel.DoWorkEventHandler(this.serialConnection_DoWork);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.PortName = "COM3";
            this.serialPort1.ReadTimeout = 1;
            this.serialPort1.WriteTimeout = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(420, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(547, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open Port";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GL_Control
            // 
            this.GL_Control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GL_Control.AutoSize = true;
            this.GL_Control.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GL_Control.ColorBits = ((uint)(24u));
            this.GL_Control.Cursor = System.Windows.Forms.Cursors.Default;
            this.GL_Control.DepthBits = ((uint)(0u));
            this.GL_Control.Location = new System.Drawing.Point(4, 31);
            this.GL_Control.MinimumSize = new System.Drawing.Size(400, 200);
            this.GL_Control.MultisampleBits = ((uint)(0u));
            this.GL_Control.Name = "GL_Control";
            this.GL_Control.Size = new System.Drawing.Size(626, 355);
            this.GL_Control.StencilBits = ((uint)(0u));
            this.GL_Control.TabIndex = 3;
            this.GL_Control.TabStop = false;
            this.GL_Control.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_ContextCreated);
            this.GL_Control.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.Gl_Render);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(634, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // acuisitionTimer
            // 
            this.acuisitionTimer.Interval = 1;
            this.acuisitionTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // portEnumTimer
            // 
            this.portEnumTimer.Enabled = true;
            this.portEnumTimer.Interval = 1000;
            this.portEnumTimer.Tick += new System.EventHandler(this.portEnumTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.GL_Control);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[initializing...]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker serialHandler;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private OpenGL.GlControl GL_Control;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer acuisitionTimer;
        private System.Windows.Forms.Timer portEnumTimer;
    }
}


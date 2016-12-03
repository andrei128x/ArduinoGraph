using OpenGL;
using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace ArduinoGraph
{
    public partial class Form1 : Form
    {
        enum PortStates
        {
            PORT_OPENING,
            PORT_RUNNING,
            PORT_CLOSING,
            PORT_CLOSED
        }

        PortStates currentPortState = PortStates.PORT_CLOSED;

        int count = 0;

        // COM ports list, initially empty, but not null
        string[] portNames = { };

        int recvSize;


        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataAcquisition.Clear();
        }

        private void serialConnection_DoWork(object sender, DoWorkEventArgs e)
        {
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentPortState == PortStates.PORT_CLOSED)
            {

                currentPortState = PortStates.PORT_OPENING;
                acuisitionTimer.Start();

            }
            else
            {
                currentPortState = PortStates.PORT_CLOSING;
            }
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            GL_Control.Invalidate();
        }

        void glControl1_ContextCreated(object sender, OpenGL.GlControlEventArgs e)
        {
            // Here you can allocate resources or initialize state
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();
            Gl.Ortho(0.0, 1.0f, 0.0, 1.0, 0.0, 1.0);

            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();

            Gl.Enable(EnableCap.Blend);
            Gl.Enable(EnableCap.LineSmooth);
            Gl.Enable(EnableCap.PolygonSmooth);
            Gl.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            Gl.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            Gl.LineWidth(.8f);
            Gl.PointSize(3f);

            Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Gl.ClearColor(.17f, .13f, .25f, .5f);
        }

        private void Gl_Render(object sender, OpenGL.GlControlEventArgs e)
        {
            
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DataAcquisition.Draw();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int result = 0;
            if (currentPortState == PortStates.PORT_OPENING)
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.Open();
                serialPort1.DiscardInBuffer();
                currentPortState = PortStates.PORT_RUNNING;
                DataAcquisition.Clear();
            }

            if (currentPortState == PortStates.PORT_RUNNING)
            {
                byte[] localData = new byte[1024];
                int totalBytes = serialPort1.BytesToRead;
                int readBytes = totalBytes;

                try
                {
                    if (readBytes > 256)
                    {
                        readBytes = 256;
                    }

                    recvSize = serialPort1.Read(localData, 0, readBytes); // <<bug , limit and monitor max totalbytes

                    DataAcquisition.Shift(recvSize);
                    DataAcquisition.AddData(localData, readBytes);

                    //count += recvSize;
                    result = totalBytes - readBytes;
                }
                catch (TimeoutException ex)
                {
                    recvSize = 0;
                }

                count++;
                // instrumentation update
                if (count % 10 == 0)
                {
                    Text = comboBox1.Text + " | " + count + " |" + recvSize + " | " + totalBytes + " | " + result.ToString();
                    
                }
                GL_Control.Invalidate();
                // Redraw the whole array

            }

            if(currentPortState == PortStates.PORT_CLOSING)
            {
                serialPort1.DiscardInBuffer();
                serialPort1.Close();
                currentPortState = PortStates.PORT_CLOSED;
                acuisitionTimer.Stop();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentPortState == PortStates.PORT_RUNNING)
            {
                currentPortState = PortStates.PORT_CLOSING;
            }
        }


        // poll every 1second for COM port changes; if change detected, update ports list here
        // TODO: update in place - when last selected port exists already, preserve selection after update
        private void portEnumTimer_Tick(object sender, EventArgs e)
        {
            if (!SerialPort.GetPortNames().SequenceEqual(portNames))
            {
                portNames = SerialPort.GetPortNames();
                comboBox1.Items.AddRange(portNames);

                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
            }
        }
    }
}

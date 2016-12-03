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
        /* CONSTANTS */

        // define a list of possible application states for the COM port connection
        enum PortStates
        {
            PORT_OPENING,
            PORT_RUNNING,
            PORT_CLOSING,
            PORT_CLOSED
        }


        /* relevant VARIABLES */

        // default COM port connection state
        PortStates currentPortState = PortStates.PORT_CLOSED;

        // COM ports list, initially empty, but not null
        string[] portNames = { };



        /* other INTERNAL variables */
        int count = 0;
        int recvSize;


        /* METHODS */
        public Form1()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ModuleDataBuffer.Clear();
        }
        private void serialConnection_DoWork(object sender, DoWorkEventArgs e)
        {
            //
        }

        // BUTTON CLICK -> start measuring
        private void button1_Click(object sender, EventArgs e)
        {
            if (currentPortState == PortStates.PORT_CLOSED)
            {

                currentPortState = PortStates.PORT_OPENING;
                acquisitionTimer.Start();
            }
            else if(currentPortState==PortStates.PORT_RUNNING)
            {
                currentPortState = PortStates.PORT_CLOSING;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            GL_Control.Invalidate();
        }

        // Gl_Control Context created
        void glControl1_ContextCreated(object sender, OpenGL.GlControlEventArgs e)
        {
            ModuleGraphics.GraphicContextCreated();
        }

        // method called when Render is requested via Invalidate
        private void Gl_Render(object sender, OpenGL.GlControlEventArgs e)
        {
            
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            ModuleDataBuffer.Draw();
        }

        // the MAIN DATA SAMPLING timer, reading data from the COM - acquisition timer
        private void acquisitionTimer_Tick(object sender, EventArgs e)
        {
            int result = 0;
            int totalBytes = 0;

            // state    -   PORT_OPENING
            // type     -   transitory to PORT_RUNNING
            // performs -   opening COM connection and start of acquisition timer
            // error    -   return to PORT_CLOSED state
            if (currentPortState == PortStates.PORT_OPENING)
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    serialPort1.DiscardInBuffer();
                    currentPortState = PortStates.PORT_RUNNING;
                    ModuleDataBuffer.Clear();
                }
                catch
                {
                    currentPortState = PortStates.PORT_CLOSED;
                }
            }

            // state    -   PORT_RUNNING
            // type     -   permanent state
            // performs -   continuosly read COM port data and request redrawing of newly incoming data
            if (currentPortState == PortStates.PORT_RUNNING)
            {
                byte[] localData = new byte[1024];
                totalBytes = serialPort1.BytesToRead;
                int readBytes = totalBytes;

                try
                {
                    if (readBytes > 256)
                    {
                        readBytes = 256;
                    }

                    recvSize = serialPort1.Read(localData, 0, readBytes); // <<bug , limit and monitor max totalbytes

                    ModuleDataBuffer.Shift(recvSize);
                    ModuleDataBuffer.AddData(localData, readBytes);

                    result = totalBytes - readBytes;
                }
                catch (TimeoutException ex)
                {
                    recvSize = 0;
                }

                
                

            }

            count++;
            // instrumentation update
            if (count % 10 == 0)
            {
                Text = comboBox1.Text + " | " + count + " |" + recvSize + " | " + totalBytes + " | " + result.ToString();

            }
            GL_Control.Invalidate();
            // Redraw the whole array

            // state    -   PORT_CLOSING
            // type     -   transitory state to PORT_CLOSED
            // performs -   closes the port and stops the acquisition timer
            if (currentPortState == PortStates.PORT_CLOSING)
            {
                serialPort1.DiscardInBuffer();
                serialPort1.Close();
                currentPortState = PortStates.PORT_CLOSED;
                //acquisitionTimer.Stop();
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
        // TODO update in place - when last selected port exists already, preserve selection after update
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

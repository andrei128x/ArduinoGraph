using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serialGraph
{
    class DataAcquisition
    {
        const int SIZE = 1280;
        const int INVALID_DATA = -10000;

        public static float[] buff = new float[SIZE];


        /* clear buffer upon start of new acquisition */
        public static void Clear()
        {
            for (int idx = 0; idx < SIZE; idx++)
            {
                buff[idx] = INVALID_DATA;
            }
        }


        /* re-draw the buffer on the screen */
        public static void Draw()
        {
            for (int idx = 0; idx < SIZE-1; idx += 1)
            {
                if (buff[idx] > INVALID_DATA/2)
                {
                    Gl.Color3(.79f, .59f, .39f);
                    Gl.Begin(PrimitiveType.LineStrip);
                    Gl.Vertex2((float)(idx) / SIZE, 0.1f + buff[idx]);
                    Gl.Vertex2(((float)(idx) + 1) / SIZE, 0.1f + buff[idx + 1]);
                    Gl.End();

                    Gl.Color3(.3f, .7f, .7f);
                    Gl.Begin(PrimitiveType.Points);
                    Gl.Vertex2((float)(idx) / SIZE, 0.1f + buff[idx]);
                    Gl.End();
                }
            }
        }


        /* shift buffer to make space for the new incoming data */
        public static void Shift(int value)
        {
            for (int idx = 0; idx < SIZE-value; idx++)
            {
                buff[idx] = buff[idx + value];
            }
        }


        /* add the incoming data to the end (right) of the buffer */
        public static void AddData(byte[] data, int count)
        {
            //buff[SIZE - 1] = (float)s / 1000;
            for (int idx = 0; idx < count; idx++)
            {
                buff[SIZE - count + idx] = (float)data[idx]/1000;
            }
        }
    }
}

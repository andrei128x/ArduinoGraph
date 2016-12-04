using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoGraph
{
    class ClassDataOperations
    {
        const int SIZE = 4096;
        const int INVALID_DATA = -10000;

        public static float[] buff = new float[SIZE];


        /* clear buffer upon start of new acquisition */
        public static void Clear()
        {
            //for (int idx = 0; idx < SIZE; idx++)
            //{
            //    buff[idx] = INVALID_DATA;
            //}

            Array.Clear(buff, 0, buff.Length);
        }


        /* re-draw the buffer on the screen */
        public static void Draw()
        {
            for (int idx = 0; idx < SIZE-1; idx += 1)
            {
                if (buff[idx] > INVALID_DATA/2)
                {
                    ClassGraphicsOperations.LineSegment((float)(idx) / SIZE, 0.1f + buff[idx], ((float)(idx) + 1) / SIZE, 0.1f + buff[idx + 1]);
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

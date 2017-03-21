using System;

namespace ArduinoGraph
{
    class ModuleDataOperations
    {
        const int SIZE = 200;
        const int INVALID_DATA = -10000;

        public static float[] buff = new float[SIZE];

        private static int bufferRead = 0;
        private static int bufferWrite = 0;
        private static int bufferReadLen = 0;

        /* clear buffer upon start of new acquisition */
        public static void Clear()
        {
            Array.Clear(buff, 0, buff.Length);
        }


        /* re-draw the buffer on the screen */
        public static void Draw()
        {
            for (int idx = 0; idx < SIZE-1; idx += 1)
            {

                int x1, x2;
                x1 = getRingIdx(bufferRead + idx);
                x2 = getRingIdx(bufferRead + idx + 1);

                if (buff[x1] > INVALID_DATA / 2)
                {
                    float valueY1 = buff[getRingIdx(bufferRead + idx)];
                    float valueY2 = buff[getRingIdx(bufferRead + idx + 1)];
                    ModuleGraphicsOperations.LineSegment((float)(x1) / SIZE, 0.1f + valueY1, ((float)(x2)) / SIZE, 0.1f + valueY2);

                }
            }
        }


        /* shift buffer to make space for the new incoming data */
        public static void Shift(int value)
        {
            //for (int idx = 0; idx < SIZE - value; idx++)
            //{
            //    buff[idx] = buff[idx + value];
            //}
        }


        /* add the incoming data to the end (right) of the buffer */
        public static void AddData(byte[] data, int count)
        {
            //for (int idx = 0; idx < count; idx++)
            //{
            //    buff[SIZE - count + idx] = (float)data[idx] / 1000;
            //}
            bufferRead = bufferWrite;
            int writePos = bufferWrite;
            for (int idx = 0; idx < count; idx++)
            {
                writePos = getRingIdx(bufferWrite + idx);
                buff[writePos] = (float)data[idx] / 1000;
            }

            bufferReadLen = count;
            bufferWrite = getRingIdx(bufferWrite + count);

        }

        private static int getRingIdx(int idx)
        {
            if (idx < 0)
            {
                throw ( new ArgumentException("wrong index requested"));
            }

            return (idx) % SIZE;
        
        }
    }
}

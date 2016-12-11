using System;

namespace ArduinoGraph
{
    class ClassDataOperations
    {
        const int SIZE = 50;
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
            int readPos1, readPos2;
            for (int idx = 0; idx < bufferReadLen; idx += 1)
            {
                readPos1 = getNextIdx(bufferRead + idx);
                readPos2 = getNextIdx(bufferRead + idx + 1);

                if (buff[readPos1] > INVALID_DATA/2)
                {
                    ClassGraphicsOperations.LineSegment((float)(readPos1) / SIZE, 0.1f + buff[readPos1], ((float)(readPos2) + 1) / SIZE, 0.1f + buff[readPos2 + 1]);
                }
            }
        }


        /* shift buffer to make space for the new incoming data */
        public static void Shift(int value)
        {
            //for (int idx = 0; idx < SIZE-value; idx++)
            //{
            //    buff[idx] = buff[idx + value];
            //}
        }


        /* add the incoming data to the end (right) of the buffer */
        public static void AddData(byte[] data, int count)
        {
            //for (int idx = 0; idx < count; idx++)
            //{
            //    buff[SIZE - count + idx] = (float)data[idx]/1000;
            //}
            int writePos = bufferWrite;
            for (int idx = 0; idx < count; idx++)
            {
                writePos = getNextIdx(bufferWrite + idx);
                buff[writePos] = (float)data[idx] / 1000;
            }

            bufferReadLen = count;
            bufferWrite = getNextIdx(bufferWrite + count);


        }

        private static int getNextIdx(int idx)
        {
            if (idx < 0)
            {
                throw ( new ArgumentException("wrong index requested"));
            }

            return (idx + 1) % SIZE;
        
        }
    }
}

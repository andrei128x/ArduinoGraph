using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoGraph
{
    class ModuleGraphics
    {
        // method called when Gl_Control's context is created
        public static void GraphicContextCreated()
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

        // method for drawing a line segment from the DataBuffer
        public static void LineSegment(float x1, float y1, float x2, float y2)
        {
            Gl.Color3(.79f, .59f, .39f);
            Gl.Begin(PrimitiveType.LineStrip);
            Gl.Vertex2(x1,y1);
            Gl.Vertex2(x2,y2);
            Gl.End();

            Gl.Color3(.3f, .7f, .7f);
            Gl.Begin(PrimitiveType.Points);
            Gl.Vertex2(x1, y1);
            Gl.End();
        }
    }
}

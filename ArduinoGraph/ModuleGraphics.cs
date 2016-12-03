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
        //
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
    }
}

using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.TubeMode;

namespace WSX.Draw3D.DrawTools
{
    public class Draw3DTube
    {
        public void DrawTube(StandardTubeMode standardTubeMode, OpenGL openGL)
        {
            switch (standardTubeMode.TubeTypes)
            {
                case StandardTubeMode.TubeType.Circle:
                    this.DrawCircleTube(standardTubeMode, openGL);
                    break;
                case StandardTubeMode.TubeType.Square:
                    this.DrawSquareTube(standardTubeMode, openGL);
                    break;
                case StandardTubeMode.TubeType.Rectangle:
                    this.DrawRectangleTube(standardTubeMode, openGL);
                    break;
                case StandardTubeMode.TubeType.Sport:
                    this.DrawSportTube(standardTubeMode, openGL);
                    break;
            }
        }

        private void DrawCircleTube(StandardTubeMode standardTubeMode, OpenGL openGL)
        {
            new DrawCircleUtils().DrawCircle(openGL, standardTubeMode);
        }

        private void DrawSquareTube(StandardTubeMode standardTubeMode, OpenGL openGL)
        {
            new DrawSquareUtils().DrawSquareTube(openGL, standardTubeMode);
        }
        private void DrawRectangleTube(StandardTubeMode standardTubeMode, OpenGL openGL)
        {
            new DrawRectangleUtils().DrawRectangleTube(openGL, standardTubeMode);
        }
        private void DrawSportTube(StandardTubeMode standardTubeMode, OpenGL openGL)
        {
            new DrawSportUtils().DrawSportTube(openGL, standardTubeMode);
        }
    }
}

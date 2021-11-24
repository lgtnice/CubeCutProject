using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Common
{
    class LayerColors
    {
        public readonly static Dictionary<int, float[]> Colors = new Dictionary<int, float[]>();
        static LayerColors()
        {
            Colors.Add((int)LayerId.Background, new float[] { 1f, 1f, 1f });
            Colors.Add((int)LayerId.One, new float[] { 0f, 1f, 0f });
            Colors.Add((int)LayerId.Two, new float[] { 0f, 1f, 1f });
            Colors.Add((int)LayerId.Three, new float[] { 0f, 0f, 1f });
            Colors.Add((int)LayerId.Four, new float[] { 1f, 0f, 1f });
            Colors.Add((int)LayerId.Five, new float[] { 1f, 1f, 0.5f });
            Colors.Add((int)LayerId.Six, new float[] { 0f, 1f, 0.5f });
            Colors.Add((int)LayerId.Seven, new float[] { 0.5f, 1f, 1f });
            Colors.Add((int)LayerId.Eight, new float[] { 0.5f, 0.5f, 1f });
            Colors.Add((int)LayerId.Nine, new float[] { 1f, 0f, 0.5f });
            Colors.Add((int)LayerId.Ten, new float[] { 1f, 0.5f, 0.25f });
            Colors.Add((int)LayerId.Eleven, new float[] { 0.5f, 0f, 1f });
            Colors.Add((int)LayerId.Twelve, new float[] { 0f, 0.5f, 1f });
            Colors.Add((int)LayerId.Thirteen, new float[] { 0.5f, 0f, 0.5f });
            Colors.Add((int)LayerId.Fourteen, new float[] { 0f, 0.5f, 0.5f });
            Colors.Add((int)LayerId.Fifteen, new float[] { 0f, 0.5f, 0f });
            //Colors.Add((int)LayerId.Sixteen, new float[] { 1f, 1f, 1f });
        }
    }
}

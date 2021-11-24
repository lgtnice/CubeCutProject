using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using WSX.Draw3D.Common;
using WSX.Draw3D.Utils;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Layers
{
    /// <summary>
    /// 标记辅助层
    /// </summary>
    public class MarkLayer : LayerBase
    {
        private readonly float[] sectionColor = new float[] { 0f, 0.5f, 1f };
        /// <summary>
        /// 截面数据
        /// </summary>
        public IDrawObject SectionObject { get; set; }
        public override string LayerName { get { return "MarkLayer"; } }

        public String TubeMessage { set; get; }
        public override void Draw(OpenGL gl)
        {
            base.Draw(gl);
            if (GlobalModel.Params.AdditionalInfo.IsShowSection)
            {
                this.SectionObject?.Draw(gl, sectionColor);
            }
        }
    }
}

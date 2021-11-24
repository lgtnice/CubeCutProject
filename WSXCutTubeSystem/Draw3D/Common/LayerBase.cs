using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.Draw3D.DrawTools;
using WSX.Draw3D.Utils;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Common
{
    public abstract class LayerBase
    {
        public virtual string LayerName { get; }//图层名称
        public List<IDrawObject> Objects { private set; get; } = new List<IDrawObject>();
        /// <summary>
        /// 当前选中的对象
        /// </summary>
        public List<IDrawObject> SelectObjects { get => this.Objects.Where(e => e.IsSelected).ToList(); }
        public bool Locked { get; set; }//锁定
        public bool Visible { get; set; } = true;//隐藏或者显示
        public virtual void Draw(OpenGL gl)
        {
            if (!this.Visible) return;
            this.Objects.ForEach(e => { e.Draw(gl, this.GetColor(e.LayerId)); });
        }

        public void DrawFlags(OpenGL gl)
        {
            if (!this.Visible || this.Objects.Count == 0)
                return;
            this.Objects.ForEach(e =>
            {
                if (GlobalModel.Params.AdditionalInfo.IsShowMachinePath)
                { e.ShowMachinePath(GlobalModel.ModelMatrix2, gl, this.GetColor(e.LayerId)); }
                if (GlobalModel.Params.AdditionalInfo.IsShowStartMovePoint) e.ShowStartMovePoint(gl);
                if (GlobalModel.Params.AdditionalInfo.IsShowBoundRect) e.ShowBoundRect(gl);
                if (GlobalModel.Params.AdditionalInfo.IsShowFigureSN) e.ShowFigureSN(gl);
            });
        }
        public void DrawVacantPath(OpenGL gl)
        {
            if (GlobalModel.Params.AdditionalInfo.IsShowVacantPath)
            {
                List<VacantPath> paths = new List<VacantPath>();
                //按照图层Id分组
                var groups = this.Objects.GroupBy(g => g.LayerId).ToList();
                foreach (var g in groups)
                {
                    List<IDrawObject> values = g.ToList().OrderBy(o => o.SN).ToList();
                    VacantPath path = new VacantPath()
                    {
                        LayerId = values.Count > 0 ? values[0].LayerId : (int)LayerId.One
                    };
                    for (int i = 0; i < values.Count - 1; i += 2)
                    {
                        Point3D start = values[i].EndMovePoint;
                        Point3D end = values[i + 1].StartMovePoint;
                        path.Points.Add(new Point3D(start));
                        path.Points.Add(new Point3D(end));
                    }
                    paths.Add(path);
                }
                
                paths.ForEach(p =>
                {
                    p.Draw(gl, this.GetColor(p.LayerId));
                    p.ShowMachinePath(GlobalModel.ModelMatrix2, gl, this.GetColor(p.LayerId));
                });
            }
        }



        public virtual float[] GetColor(int layerId)
        {
            if (LayerColors.Colors.ContainsKey(layerId))
            {
                return LayerColors.Colors[layerId];
            }
            else
            {
                return new float[] { 0f, 1f, 0f };
            }
        }
        public virtual void Add(IDrawObject objects)
        {
            Objects.Add(objects);
        }
        public virtual void AddRange(IList<IDrawObject> objects)
        {
            Objects.AddRange(objects);
        }

        private void UpdateSN()
        {
            Objects = Objects.OrderBy(e => e.MinZ).ToList();
        }

        public virtual void Clear()
        {
            Objects.Clear();
        }
        public virtual void Remove(IDrawObject drawObject)
        {
            Objects.Remove(drawObject);
        }
        public List<IDrawObject> GetHitObjects(float[] matrix, RectangleF rect, bool anyPoint)
        {
            var hits = new List<IDrawObject>();
            Objects.ForEach(obj =>
            {
                if (obj.ObjectInRectangle(matrix, rect, anyPoint))
                {
                    hits.Add(obj);
                }
            });
            return hits;
        }

        public List<IDrawObject> GetHitObjects(float[] matrix, PointF point)
        {
            var hits = new List<IDrawObject>();
            Objects.ForEach(obj =>
            {
                if (obj.PointInObject(matrix, point))
                {
                    hits.Add(obj);
                }
            });
            return hits;
        }

        public void ClearSelectedStatus()
        {
            Objects.ForEach(obj => obj.IsSelected = false);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Figure3DModel;
using WSX.Draw3D.Common;
using WSX.Draw3D.DrawTools;

namespace WSXCutTubeSystem.Manager
{
    public static class FigureManager
    {
        /// <summary>
        /// 转换为可以保存的类型
        /// </summary>
        /// <param name="drawObjects"></param>
        /// <returns></returns>
        public static List<FigureBase3DModel> ToFigureBaseModel(List<IDrawObject> drawObjects, bool isMark = false)
        {
            List<FigureBase3DModel> rets = new List<FigureBase3DModel>();
            foreach (IDrawObject drawObj in drawObjects)
            {
                var obj = ToFigureBase3D(drawObj);
                if (obj != null)
                {
                    obj.IsMark = isMark;
                    rets.Add(obj);
                }
            }
            return rets;
        }

        /// <summary>
        /// 转换IDrawObject
        /// </summary>
        public static List<IDrawObject> ToIDrawObjects(List<FigureBase3DModel> figures)
        {
            if (figures == null)
                return null;

            List<IDrawObject> objects = new List<IDrawObject>();
            foreach (FigureBase3DModel fig in figures)
            {
                var obj = ToIDrawObject(fig);
                if (obj != null)
                {
                    objects.Add(obj);
                }
            }
            return objects;
        }
        private static FigureBase3DModel ToFigureBase3D(IDrawObject fig)
        {
            switch (fig.Type)
            {
                case FigureType.Line:
                    {
                        var figure = fig as Line3D;
                        Line3DModel line = new Line3DModel()
                        {
                            P1 = figure.P1,
                            P2 = figure.P2
                        };
                        line.CopyBase(fig);
                        return line;
                    }
                    break;
                case FigureType.Polyline:
                    {
                        var figure = fig as Polyline3D;
                        Polyline3DModel lines = new Polyline3DModel()
                        {
                            Points = figure.Points
                        };
                        lines.CopyBase(fig);
                        return lines;
                    }
                    break;
                case FigureType.Spline:
                    {
                        var figure = fig as Spline3D;
                        Spline3DModel spline = new Spline3DModel()
                        {
                            Knots = figure.Knots,
                            ControlPoints = figure.ControlPoints
                        };
                        spline.CopyBase(fig);
                        return spline;
                    }
                    break;
                case FigureType.GeoCurve:
                    {
                        var figure = fig as GeoCurve3D;
                        GeoCurve3DModel geo = new GeoCurve3DModel();
                        figure.Geometry.ForEach(f =>
                        {
                            var g = ToFigureBase3D(f);
                            geo.Geometry.Add(g);
                        });
                        geo.CopyBase(fig);
                        return geo;
                    }
                    break;

                default:
                    break;
            }
            return null;
        }
        private static IDrawObject ToIDrawObject(FigureBase3DModel fig)
        {
            switch (fig.Type)
            {
                case FigureType.Line:
                    {
                        var figure = fig as Line3DModel;
                        Line3D line = new Line3D()
                        {
                            P1 = figure.P1,
                            P2 = figure.P2
                        };
                        line.CopyBase(fig);
                        line.Update();
                        return line;
                    }
                    break;
                case FigureType.Polyline:
                    {
                        var figure = fig as Polyline3DModel;
                        Polyline3D lines = new Polyline3D()
                        {
                            Points = figure.Points
                        };
                        lines.CopyBase(fig);
                        lines.Update();
                        return lines;
                    }
                    break;
                case FigureType.Spline:
                    {
                        var figure = fig as Spline3DModel;
                        Spline3D spline = new Spline3D()
                        {
                            Knots = figure.Knots,
                            ControlPoints = figure.ControlPoints
                        };
                        spline.CopyBase(fig);
                        spline.UpdatePolyline();
                        spline.Update();
                        return spline;
                    }
                    break;
                case FigureType.GeoCurve:
                    {
                        var figure = fig as GeoCurve3DModel;
                        GeoCurve3D geo = new GeoCurve3D();
                        figure.Geometry.ForEach(f =>
                        {
                            var g = ToIDrawObject(f);
                            geo.Geometry.Add(g);
                        });
                        geo.CopyBase(fig);
                        geo.UpdatePolyline();
                        geo.Update();
                        return geo;
                    }
                    break;

                default:
                    break;
            }
            return null;
        }
        #region 扩展方法
        public static void CopyBase(this IDrawObject draw, FigureBase3DModel figure)
        {
            draw.LayerId = figure.LayerId;
            draw.IsClosed = figure.IsClosed;
            draw.IsLineBold = figure.IsLineBold;
            draw.IsSelected = figure.IsSelected;
            draw.IsVisible = figure.IsVisible;
            draw.IsLocked = figure.IsLocked;
            draw.IsInnerCut = figure.IsInnerCut;
            draw.SN = figure.SN;
            draw.MicroConnParams = figure.MicroConnParams;
            draw.CompensationParam = figure.CompensationParam;
        }
        public static void CopyBase(this FigureBase3DModel figure, IDrawObject draw)
        {
            figure.LayerId = draw.LayerId;
            figure.IsClosed = draw.IsClosed;
            figure.IsLineBold = draw.IsLineBold;
            figure.IsSelected = draw.IsSelected;
            figure.IsVisible = draw.IsVisible;
            figure.IsLocked = draw.IsLocked;
            figure.IsInnerCut = draw.IsInnerCut;
            figure.SN = draw.SN;
            figure.MicroConnParams = draw.MicroConnParams;
            figure.CompensationParam = draw.CompensationParam;
        }
        #endregion
        /// <summary>
        /// 添加到显示画布
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="figures"></param>
        public static void AddToDrawObject(IModel model, List<FigureBase3DModel> figures, bool isClear = false)
        {
            if (model == null) return;
            if (figures != null)
            {
                if (isClear)
                {
                    model.DrawLayer.Clear();
                    model.MarkLayer.Clear();
                }
                var draws = figures.Where(e => !e.IsMark).ToList();
                var marks = figures.Where(e => e.IsMark).ToList();
                model.DrawLayer.AddRange(FigureManager.ToIDrawObjects(draws));
                model.MarkLayer.AddRange(FigureManager.ToIDrawObjects(marks));

                //canvas.Model.DrawingLayer.UpdateSN();
                //GlobalData.Model.GlobalModel.TotalDrawObjectCount = canvas.Model.DrawingLayer.Objects.Count;

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.MicroConn;

namespace WSX.Draw3D.Utils
{
    public class MicroConnectUtil
    {
        public static List<Point3D> GetMicroConnectPoints(List<Point3D> points, List<MicroConnectModel> connectModels, float pathStartParam, bool isClose,
            out List<float[]> microSectionsOut)
        {
            microSectionsOut = null;
            List<float[]> microSections = new List<float[]>();
            connectModels.ForEach(e => {
                if(e.Flags == MicroConnectFlags.GapPoint)
                {
                    var section = MicroConnectUtil.GetPositionByDistanceReverse(pathStartParam, e.Size, points, isClose);
                    microSections.Add(section);
                }
                else
                {
                    var section = MicroConnectUtil.GetPositionByDistance(e.Position, e.Size, points, isClose);
                    microSections.Add(section);
                }
                
            });

            microSections = microSections.OrderBy(e => e[0]).ToList();
            microSections = MicroConnectUtil.MergeSection(microSections);

            var x = microSections.Where(e => e[0] > e[1]).ToList();
            if (x.Count > 0)
            {
                microSections.Remove(x[0]);
                microSections.Insert(0, x[0]); 
                microSections = MicroConnectUtil.MergeSection(microSections);
            }
            microSectionsOut = microSections;

            List<float> startPos = new List<float>();
            List<float> removePos = new List<float>(); 
            List<float> pos = new List<float>();
            microSections.ForEach(e => {
                pos.Add(e[0]); 
                pos.Add(e[1]);
                startPos.Add(e[0]);

                if(e[0] > e[1])
                {
                    for (int i = (int)Math.Ceiling(e[0]); i < points.Count; i++)
                    {
                        removePos.Add(i);
                    }
                    for (int i = 0; i < e[1]; i++)
                    {
                        removePos.Add(i);
                    }
                }else
                {
                    for (int i = (int)Math.Ceiling(e[0]); i <= (int)Math.Floor(e[1]); i++)
                    {
                        removePos.Add(i);
                    }
                }
            });

            for (int i = 0; i < points.Count; i++)
            {
                if (!pos.Contains(i) && !removePos.Contains(i))
                {
                    pos.Add(i);
                }
            }

            pos = pos.OrderBy(e => e).ToList();

            Point3D point = null;
            List<Point3D> microPoints = new List<Point3D>();
            pos.ForEach(p => {
                if (p == Math.Floor(p))
                {
                    point = points[(int)p];
                } else
                {
                    point = MicroConnectUtil.GetPointByPosition(points, p, isClose);
                }

                if (startPos.Contains(p))
                {
                    point.HasMicroConn = true;
                }

                microPoints.Add(point);
            });

            return microPoints;
        }

        private static List<float[]> MergeSection(List<float[]> microSections)
        {
            //不要在排序
            float[] newSection = null;
            List<float[]> newSections = new List<float[]>();
            microSections.ForEach(e => {
                if (newSection == null)
                {
                    newSection = new float[2];
                    newSection[0] = e[0];
                    newSection[1] = e[1];
                    newSections.Add(newSection);
                }
                else if ((e[0] >= newSection[0] && e[0] <= newSection[1]) || //a b 93介于91和95之间
                         (e[0] >= newSection[0] && newSection[0] > newSection[1]) || // c 98介于91和05之间
                         (newSection[0] > newSection[1] && e[0] <= newSection[1])    // d 03介于91和05之间
                         )
                {
                    /**
                     *  注意微连区间横跨起点的情况:
                     * a 91-95 93-98 => 91-98
                     * b 91-95 93-08 => 91-08
                     * c 91-05 98-08 => 91-08
                     * d 91-05 03-08 => 91-08
                     */
                    if (e[1] >= newSection[1] || (newSection[0] < newSection[1] && e[0] > e[1])) // || b
                    {
                        //e区间扩展了newSection区间
                        newSection[1] = e[1];
                    }
                    else
                    {
                        //e区间b被newSection的区间包含 不处理
                    }
                }
                else if(e[0] > newSection[1])
                {
                    //有间隔，另起新区间
                    newSection = new float[2];
                    newSection[0] = e[0];
                    newSection[1] = e[1];
                    newSections.Add(newSection);
                }
            });
            return newSections;
        }

        public static bool IsPosInMicroConnect(float postition, MicroConnectModel connectParam, List<Point3D> points, bool isClosed)
        {
            if(connectParam.Flags == MicroConnectFlags.GapPoint)
            {
                float[] microSection = MicroConnectUtil.GetPositionByDistanceReverse(connectParam.Position, connectParam.Size, points, isClosed);
                return MicroConnectUtil.IsPosInMicroConnect(postition, microSection);
            }else
            {
                float[] microSection = MicroConnectUtil.GetPositionByDistance(connectParam.Position, connectParam.Size, points, isClosed);
                return MicroConnectUtil.IsPosInMicroConnect(postition, microSection);
            }
        }


        /// <summary>
        /// 位置是否在微连区间(冷却点在微连缺口区间内则删除冷却点)
        /// </summary>
        /// <param name="position"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        private static bool IsPosInMicroConnect(float position, float[] section)
        {
            if (section[0] < section[1] && position > section[0] && position < section[1])
                return true;
            if (section[0] > section[1] && position > section[0])
                return true;
            if (section[0] > section[1] && position < section[1])
                return true;
            return false;
        }


        private static Point3D GetPointByPosition(List<Point3D> points,float position, bool isClosed)
        {
            if (position < 0 || (isClosed && position >= points.Count) || (!isClosed && position >= points.Count - 1))
                return null;
            int startIdx = (int)Math.Floor(position);
            float ratio = position - (float)startIdx;
            int endIdx = (startIdx == points.Count - 1) ? 0 : startIdx + 1;

            if (ratio <= 0.00001)
            {
                return points[startIdx];
            }
            else
            {
                double distance = HitUtil.Distance(points[startIdx], points[endIdx]) * ratio;
                return HitUtil.GetLinePointByDistance(points[startIdx], points[endIdx], (float)distance);
            }
        }

        private static float[] GetPositionByDistance(float startPos, double size,List<Point3D> points, bool isClose)
        {
            int idx = (int)Math.Floor(startPos);
            int idx2 = idx + 1;
            if (idx == points.Count - 1)
                idx2 = 0;

            float ratio = startPos - (float)idx;
            double length = HitUtil.Distance(points[idx], points[idx2]);

            double sum = 0;
            size += ratio * (float)length;//为了后面的循环统一逻辑
            for (int i=idx; i<points.Count-1; i++)
            {
                idx = i;
                idx2 = i + 1;
                length = HitUtil.Distance(points[idx], points[idx2]);

                if(sum + length >= size)
                {
                    double endPos = (size-sum) / length;
                    if(endPos >= (1 - 0.0001))
                    {
                        endPos = (float)(idx + 1) + 0.0f;
                    }else
                    {
                        endPos = idx + endPos;
                    }

                    return new float[] {startPos, (float)endPos };
                }
                else
                {
                    sum += length;
                }
            }

            if (isClose)
            {
                for (int i = 0; i <= (int)Math.Floor(startPos); i++)
                {
                    idx = i - 1;
                    idx2 = i;
                    if (idx < 0)
                    {
                        idx = points.Count - 1;
                    }
                    length = HitUtil.Distance(points[idx], points[idx2]);

                    if (sum + length > size)
                    {
                        double endPos = (size - sum) / length;
                        if (endPos >= (1 - 0.0001))
                        {
                            endPos = (float)(idx + 1) + 0.0f;
                        }else
                        {
                            endPos = idx + endPos;
                        }
                        return new float[] { startPos, (float)endPos };
                    }
                    else
                    {
                        sum += length;
                    }
                }
            }
            return null;
        }

        private static float[] GetPositionByDistanceReverse(float endPos, double size, List<Point3D> points, bool isClose)
        {
            if (float.IsNaN(endPos))
            {
                endPos = 0.0f;
            }
            float posRatio = endPos - (float)Math.Floor(endPos);

            int idx1 ,idx2;
            float ratio = posRatio;
            double sum = 0;
            double length = double.NaN;
            double lineLength = double.NaN;
            for (int i = (int)Math.Floor(endPos); i >=0 ; i--)
            {
                idx1 = i;
                idx2 = i + 1;
                if (idx2 == points.Count)
                {
                    idx2 = 0;
                }

                lineLength = HitUtil.Distance(points[idx1], points[idx2]) * ratio;
                length = lineLength * ratio;

                if (sum + length >= size)
                {
                    double startPos = (sum + length - size) / lineLength;
                    if (startPos >= (1 - 0.0001))
                    {
                        startPos = (float)(idx1 + 1) + 0.0f;
                    }
                    else
                    {
                        startPos = idx1 + startPos;
                    }

                    return new float[] { (float)startPos, (float)endPos };
                }
                else
                {
                    sum += length;
                }
                ratio = 1;
            }

            if (isClose)
            {
                for (int i = points.Count-1; i >= (int)Math.Floor(endPos); i--)
                {
                    idx1 = i;
                    idx2 = i + 1;
                    if (idx2 == points.Count)
                    {
                        idx2 = 0;
                    }

                    if (i == (int)Math.Floor(endPos))
                    {
                        ratio = 1 - posRatio;
                    }else
                    {
                        ratio = 1;
                    }

                    lineLength = HitUtil.Distance(points[idx1], points[idx2]) * ratio;
                    length = lineLength * ratio;

                    if (sum + length > size)
                    {
                        double startPos = (sum + length - size) / lineLength;

                        if (i == (int)Math.Floor(endPos))
                        {
                            startPos = posRatio + startPos;
                        }

                        if (startPos >= (1 - 0.0001))
                        {
                            startPos = (float)(idx1 + 1) + 0.0f;
                        }
                        else
                        {
                            startPos = idx1 + startPos;
                        }
                        return new float[] { (float)startPos, (float)endPos };
                    }
                    else
                    {
                        sum += length;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取微连后的起点位置
        /// </summary>
        /// <param name="pathStartParam">默认或设置后的起点位置</param>
        /// <param name="microSections">微连区间</param>
        /// <returns></returns>
        public static float GetMicroConnectPathStartParam(float pathStartParam, List<float[]> microSections)
        {
            pathStartParam = float.IsNaN(pathStartParam) ? 0.0f : pathStartParam;
            float pathStartParamMicroConn = pathStartParam;
            microSections.ForEach(e => {
                if(pathStartParam > e[0] && pathStartParam < e[1])
                {
                    pathStartParamMicroConn = e[1];
                    return;
                }
                else if (e[0] > e[1] && (pathStartParam > e[0] || pathStartParam < e[1]))
                {
                    pathStartParamMicroConn = e[1];
                    return;
                }
            });
            return pathStartParamMicroConn;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.ParaModel;
using WSX.Draw3D.Common;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Utils
{
    /// <summary>
    /// 阵列
    /// </summary>
    public class ArrayHelper
    {
        public static List<IDrawObject> CalArray(List<IDrawObject> drawObjects, List<IDrawObject> markObjects)
        {
            List<IDrawObject> addObjects = new List<IDrawObject>();

            var arrayModel = GlobalModel.Params.ArrayParam;

            float offset = arrayModel.Distance;
            List<float> maxZs = new List<float>();
            if (arrayModel.ArrayMode == ArrayMode.Gap)
            {
                foreach (var obj in drawObjects)
                {
                    if (arrayModel.IsOnlyCopySelected && !obj.IsSelected)
                        continue;
                    maxZs.Add(obj.MaxZ);
                }
                float maxZ = maxZs.Max();
                offset = maxZ + arrayModel.Distance;
            }

            IDrawObject newObj = null;
            List<IDrawObject> copyObjects = new List<IDrawObject>();
            List<IDrawObject> latestObject = new List<IDrawObject>();
            foreach (var obj in drawObjects)
            {
                if (arrayModel.IsOnlyCopySelected && !obj.IsSelected)
                    continue;
                latestObject.Add(obj);
            }

            int addCount = 0;
            for (int i = 0; i < arrayModel.Count; i++)
            {
                copyObjects.Clear();
                copyObjects.AddRange(latestObject);
                latestObject.Clear();
                foreach (var obj in copyObjects)
                {
                    addCount++;
                    newObj = obj.Clone();
                    newObj.SN = drawObjects.Count + addCount;
                    newObj.MoveAxisZ(offset);
                    addObjects.Add(newObj);
                    latestObject.Add(newObj);
                }
            }

            if (!arrayModel.IsOnlyCopySelected)
            {
                // 辅助线
                copyObjects.Clear();
                latestObject.Clear();
                foreach (var obj in markObjects)
                {
                    if (arrayModel.IsOnlyCopySelected && !obj.IsSelected)
                        continue;
                    latestObject.Add(obj);
                }

                for (int i = 0; i < arrayModel.Count; i++)
                {
                    copyObjects.Clear();
                    copyObjects.AddRange(latestObject);
                    latestObject.Clear();
                    foreach (var obj in copyObjects)
                    {
                        newObj = obj.Clone();
                        newObj.MoveAxisZ(offset);
                        newObj.LayerId = 0;//redo undo 识别 辅助线
                        addObjects.Add(newObj);
                        latestObject.Add(newObj);
                    }
                }
            }

            return addObjects;
        }
    }
}

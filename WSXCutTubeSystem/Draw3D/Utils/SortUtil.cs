using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.Enums;
using WSX.Draw3D.Common;

namespace WSX.Draw3D.Utils
{
    /// <summary>
    /// 排序
    /// </summary>
    public class SortUtil
    {
        public static List<IDrawObject> Sort(List<IDrawObject> objects, SortMode sortMode)
        {
            List<IDrawObject> newObjcts = new List<IDrawObject>();
            objects.ForEach(e => newObjcts.Add(e.Clone()));

            int sn = 0;
            switch (sortMode)
            {
                case SortMode.YSmallToLarge:
                    newObjcts = newObjcts.OrderBy(e => e.MinZ).ToList();
                    break;
                case SortMode.YLargeToSmall:
                    newObjcts = newObjcts.OrderByDescending(e => e.MaxZ).ToList();
                    break;
                case SortMode.Reverse:
                    newObjcts = newObjcts.OrderByDescending(e => e.SN).ToList();
                    break;
                case SortMode.BySide:
                    newObjcts = SortUtil.SortBySide(newObjcts);
                    break;
                default:
                    break;
            }
            newObjcts.ForEach(e => e.SN = ++sn);
            return newObjcts;
        }

        public static List<IDrawObject> SortBySide(List<IDrawObject> objects)
        {
            //按面排序？？
            System.Windows.Forms.MessageBox.Show("该功能未实现");

            return objects;
        }
    }
}

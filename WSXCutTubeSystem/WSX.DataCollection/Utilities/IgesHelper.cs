using System.Collections.Generic;
using System.IO;
using System.Linq;
using WSX.CommomModel.ParaModel;
using WSX.Iges;
using WSX.Iges.Entities;

namespace WSX.DataCollection.Utilities
{
    public class IgesHelper
    {

        public static void ReadFile(string path)
        {
            IgesFile igesFile;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                igesFile = IgesFile.Load(fs);
            }
            foreach (IgesEntity entity in igesFile.Entities)
            {
                switch (entity.EntityType)
                {
                    case IgesEntityType.Line:
                        {
                            IgesLine line = (IgesLine)entity;
                        }
                        break;
                        // ...
                }
            }
        }

        public static bool TryGetStandardTube(string fileName, out StandardTubeMode standard)
        {
            standard = null;
            IgesFile igesFile;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                igesFile = IgesFile.Load(fs);
            }
            //DrawIgesUtils.Entities = igesFile.Entities;
            if (TryGetCircleTube(igesFile.Entities, out standard) ||
                TryGetSquareTube(igesFile.Entities, out standard) ||
                TryGetRectangleTube(igesFile.Entities, out standard) ||
                TryGetSportTube(igesFile.Entities, out standard))
            {
                //标准管
                return true;
            }
            else
            {
                //异型管
            }
            return false;
        }
        private static bool TryGetCircleTube(List<IgesEntity> entities, out StandardTubeMode standard)
        {
            standard = null;
            var curves = entities.Where(e => e.EntityType == IgesEntityType.TrimmedParametricSurface).ToList();
            if (curves.Count == 6)
            {
                //空心管
                for (int i = 0; i < curves.Count; i++)
                {
                    var surface = curves[i] as IgesTrimmedParametricSurface;
                    if (surface != null && surface.OuterBoundary is IgesCurveOnAParametricSurface)
                    {

                    }
                }
            }
            else if (curves.Count == 4)
            {
                //实心管
                for (int i = 0; i < curves.Count; i++)
                {
                    var surface = curves[i] as IgesTrimmedParametricSurface;
                    if (surface.OuterBoundary is IgesRationalBSplineCurve)
                    {

                    }
                }
            }

            return false;
        }
        private static bool TryGetSquareTube(List<IgesEntity> entities, out StandardTubeMode standard)
        {
            standard = null;
            return false;
        }
        private static bool TryGetRectangleTube(List<IgesEntity> entities, out StandardTubeMode standard)
        {
            standard = null;
            return false;
        }
        private static bool TryGetSportTube(List<IgesEntity> entities, out StandardTubeMode standard)
        {
            standard = null;
            return false;
        }

    } 
}

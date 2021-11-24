using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Figure3DModel;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.Logger;

namespace WSX.DataCollection.Document
{
    /// <summary>
    /// 切管文档
    /// </summary>
    /// <typeparam name="T">Entities的数据类型</typeparam>
    [XmlRoot("WTFDocument")]
    public class WTFDocument: IDocument<FigureBase3DModel>
    {
        [XmlElement("Entities")]
        public EntityCollection<FigureBase3DModel> Entity { get; set; } = new EntityCollection<FigureBase3DModel>();
        public StandardTubeMode TubeMode { get; set; }
        public bool ReadFromFile(string fileName)
        {
            try
            {
                string xmlString = File.ReadAllText(fileName);
                if (string.IsNullOrEmpty(xmlString))
                {
                    return false;
                }
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    XmlAttributeOverrides attrOverrides = this.GetAttributeOverrides();
                    XmlSerializer serializer = new XmlSerializer(typeof(WTFDocument), attrOverrides);
                    var source = (serializer.Deserialize(stream) as WTFDocument);
                    this.Entity = source.Entity;
                    this.TubeMode = source.TubeMode;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Instance.Error(ex.Message);
                return false;
            }
        }
        public bool WriteToFile(string fileName)
        {
            try
            {
                XmlWriter writer = null;    //声明一个xml编写器
                XmlWriterSettings writerSetting = new XmlWriterSettings
                {
                    Indent = true,//定义xml格式，自动创建新的行
                    Encoding = UTF8Encoding.UTF8,//编码格式
                };
                writer = XmlWriter.Create(fileName, writerSetting);
                XmlAttributeOverrides attrOverrides = this.GetAttributeOverrides();
                XmlSerializer xser = new XmlSerializer(typeof(WTFDocument), attrOverrides);  //实例化序列化对象
                xser.Serialize(writer, this);  //序列化对象到xml文档
                writer.Close();
            }
            catch (Exception ex)
            {
                LogUtil.Instance.Error(ex.Message);
                return false;
            }
            return true;
        }

        private XmlAttributeOverrides GetAttributeOverrides()
        {
            XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();
            XmlAttributes attrs = new XmlAttributes();
            XmlElementAttribute attrLine3D = new XmlElementAttribute();
            attrLine3D.ElementName = "Line3D";
            attrLine3D.Type = typeof(Line3DModel);
            XmlElementAttribute attrPolyline3D = new XmlElementAttribute();
            attrPolyline3D.ElementName = "Polyline3D";
            attrPolyline3D.Type = typeof(Polyline3DModel);
            XmlElementAttribute attrSpline3D = new XmlElementAttribute();
            attrSpline3D.ElementName = "Spline3D";
            attrSpline3D.Type = typeof(Spline3DModel);
            XmlElementAttribute attrGeoCurve3D = new XmlElementAttribute();
            attrGeoCurve3D.ElementName = "GeoCurve3D";
            attrGeoCurve3D.Type = typeof(GeoCurve3DModel);

            // Add the XmlElementAttribute to the collection of objects.
            attrs.XmlElements.Add(attrLine3D);
            attrs.XmlElements.Add(attrPolyline3D);
            attrs.XmlElements.Add(attrSpline3D);
            attrs.XmlElements.Add(attrGeoCurve3D);

            attrOverrides.Add(typeof(EntityCollection<FigureBase3DModel>), "Figures", attrs);
            attrOverrides.Add(typeof(GeoCurve3DModel), "Geometry", attrs);
            return attrOverrides;
        }
    }
}

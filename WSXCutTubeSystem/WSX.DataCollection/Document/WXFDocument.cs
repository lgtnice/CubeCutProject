using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Figure2DModel;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.Logger;

namespace WSX.DataCollection.Document
{
    /// <summary>
    /// 平板切割文档
    /// </summary>
    /// <typeparam name="T">Entities的数据类型</typeparam>
    [XmlRoot("WXFDocument")]
    public class WXFDocument : IDocument<FigureBase2DModel>
    {
        [XmlElement("Entities")]
        public EntityCollection<FigureBase2DModel> Entity { get; set; } = new EntityCollection<FigureBase2DModel>();
        [XmlIgnore]
        public StandardTubeMode TubeMode { get; set; }
        public WXFDocument()
        {

        }

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
                    XmlAttributeOverrides attrOverrides  = this.GetAttributeOverrides();
                    XmlSerializer serializer = new XmlSerializer(typeof(WXFDocument), attrOverrides);
                    var source = (serializer.Deserialize(stream) as WXFDocument);
                    this.Entity = source.Entity;
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
                XmlSerializer xser = new XmlSerializer(typeof(WXFDocument), attrOverrides);  //实例化序列化对象
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
            XmlElementAttribute attrArc = new XmlElementAttribute();
            attrArc.ElementName = "Arc";
            attrArc.Type = typeof(ArcModel);
            XmlElementAttribute attrCircle = new XmlElementAttribute();
            attrCircle.ElementName = "Circle";
            attrCircle.Type = typeof(CircleModel);
            XmlElementAttribute attrEllipse = new XmlElementAttribute();
            attrEllipse.ElementName = "Ellipse";
            attrEllipse.Type = typeof(EllipseModel);
            XmlElementAttribute attrLwPolyline = new XmlElementAttribute();
            attrLwPolyline.ElementName = "LwPolyline";
            attrLwPolyline.Type = typeof(LwPolylineModel);
            XmlElementAttribute attrPoint = new XmlElementAttribute();
            attrPoint.ElementName = "Point";
            attrPoint.Type = typeof(PointModel);
            XmlElementAttribute attrPolyBezier = new XmlElementAttribute();
            attrPolyBezier.ElementName = "PolyBezier";
            attrPolyBezier.Type = typeof(PolyBezierModel);
            XmlElementAttribute attrText = new XmlElementAttribute();
            attrText.ElementName = "Text";
            attrText.Type = typeof(TextModel);
            // Add the XmlElementAttribute to the collection of objects.
            attrs.XmlElements.Add(attrArc);
            attrs.XmlElements.Add(attrCircle);
            attrs.XmlElements.Add(attrEllipse);
            attrs.XmlElements.Add(attrLwPolyline);
            attrs.XmlElements.Add(attrPoint);
            attrs.XmlElements.Add(attrPolyBezier);
            attrs.XmlElements.Add(attrText);

            attrOverrides.Add(typeof(EntityCollection<FigureBase2DModel>), "Figures", attrs);
            return attrOverrides;
        }
    }
}

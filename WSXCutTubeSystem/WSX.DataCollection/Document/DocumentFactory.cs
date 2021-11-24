using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.DataCollection.Document
{
    public class DocumentFactory
    {
        /// <summary>
        /// 创建文档操作实例
        /// </summary>
        /// <typeparam name="T">Entity的数据类型</typeparam>
        /// <param name="exName"></param>
        /// <returns></returns>
        public static IDocument<T> CreateInstance<T>(string exName)
        {
            switch (exName)
            {
                case ".IGS": return new IGESDocument() as IDocument<T>;
                case ".DXF": return new DXFDocument() as IDocument<T>;
                case ".WTF": return new WTFDocument() as IDocument<T>;
                case ".WXF": return new WXFDocument() as IDocument<T>;
            }
            return null;
        }
    }
}

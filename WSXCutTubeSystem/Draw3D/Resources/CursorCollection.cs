using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.Enums;
using WSX.Draw3D.Canvas;

namespace WSX.Draw3D.Resources
{
    public class CursorCollection
    {
        public void InitialCursors()
        {
            //各个OperationMode、CommandType 如果有指定的光标则 AddCursor，
            //否则GetCursor返回默认的

            this.AddCursor(OperationMode.Move, Cursors.Hand);
            this.AddCursor(OperationMode.View, "rotate.cur");
            this.AddCursor(CommandType.SetCoolPoint, "coolpoint.cur");
            this.AddCursor(CommandType.SetMicroConnect, "micorconn.cur");
            this.AddCursor(CommandType.SetStartPoint, this.GetCursor(CommandType.SetMicroConnect));
        }

        private Dictionary<object, Cursor> dicCursor = new Dictionary<object, Cursor>();

        private void AddCursor(object key, Cursor cursor)
        {
            this.dicCursor.Add(key, cursor);
        }

        private void AddCursor(object key, string resourceName)
        {
            Cursor cursor = new Cursor(GetType(), resourceName);
            this.dicCursor[key] = cursor;
        }

        public Cursor GetCursor(object key)
        {
            if (this.dicCursor.ContainsKey(key))
            {
                return this.dicCursor[key];
            }
            return Cursors.Arrow;
        }
    }
}
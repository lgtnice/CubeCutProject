using DevExpress.Mvvm;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Common;
using WSX.DataCollection.Document;
using WSX.Draw3D.Common;
using WSX.Draw3D.Utils;
using WSXCutTubeSystem.Views.UCControl;

namespace WSXCutTubeSystem.Manager
{
    /// <summary>
    /// 图形文件处理
    /// </summary>
    public class FileManager
    {
        public const string DEFAULT_FILE_NAME = "New";
        public event Action<string> OnFileNameChanged;
        public string FilePath { get; private set; }
        public string FileName { get; private set; }
        private List<FigureBase3DModel> oldFigures { get; set; }
        private static FileManager instance;
        public static FileManager Instance { get => instance ?? (instance = new FileManager()); }
        public bool New(IModel model)
        {
            if (!IsNeedToSave(model)) return false;//取消创建
            //canvas.Model.DrawingLayer.Objects.Clear();
            //GlobalModel.TotalDrawObjectCount = 0;//计数归零
            //canvas.DoInvalidate(true);
            this.oldFigures = null;
            this.UpdateFilePath(null);
            return true;
        }
        private bool IsNeedToSave(IModel model)
        {
            var curfigures = new List<FigureBase3DModel>();
            curfigures.AddRange(FigureManager.ToFigureBaseModel(model.DrawLayer.Objects));
            curfigures.AddRange(FigureManager.ToFigureBaseModel(model.MarkLayer.Objects, true));
            if (oldFigures != null && oldFigures.Count == 0) oldFigures = null;
            if (curfigures != null && curfigures.Count == 0) curfigures = null;
            string oldHashcode = JsonConvert.SerializeObject(oldFigures);
            string curHashcode = JsonConvert.SerializeObject(curfigures);
            if (oldHashcode.GetHashCode() != curHashcode.GetHashCode())
            {
                var result = XtraMessageBox.Show(string.Format("是否保存对\"{0}\"的修改？", this.FileName), "消息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.SaveFile(model);
                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public void OpenFileDialog(IModel model)
        {
            try
            {
                if (!IsNeedToSave(model)) return;
                IDocument<FigureBase3DModel> doc = null;
                UCFilePreview figurePreview = new UCFilePreview();
                OpenFileDialogEx open = new OpenFileDialogEx();
                open.Filter = "所有支持文件(*.WTF;*.IGS)|*.WTF;*.IGS;|WSX默认切割文件(*.WTF)|*.WTF|IGES Files(*.IGS)|*.IGS";
                open.PreviewControl = figurePreview;
                open.OnFileSelectChanged += (s, e) =>
                {
                    if (figurePreview.IsPreView)
                    {
                        doc = ReadFromFile(e.Path);
                        if (doc != null)
                        {
                            figurePreview.FigurePreview(doc);
                        }
                    }
                    else
                    {
                        figurePreview.FigurePreview(null);
                    }
                };
                if (open.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(open.FileName, model, false, doc);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void OpenFile(string fileName, IModel model, bool isCheck, IDocument<FigureBase3DModel> doc = null)
        {
            try
            {
                if (isCheck && !IsNeedToSave(model)) return;
                if (doc == null)
                {
                    doc = ReadFromFile(fileName);
                }
                model.TubeMode = doc.TubeMode;
                model.MarkLayer.SectionObject = new StandardTubeConverter(model.TubeMode).GetSectionObject();
                FigureManager.AddToDrawObject(model, doc.Entity.Figures, true);
                Messenger.Default.Send<object>(null, "OnPreview");
                this.oldFigures = new List<FigureBase3DModel>();
                this.oldFigures.AddRange(FigureManager.ToFigureBaseModel(model.DrawLayer.Objects));
                this.oldFigures.AddRange(FigureManager.ToFigureBaseModel(model.MarkLayer.Objects, true));
                this.UpdateFilePath(fileName);
                LoggerManager.AddSystemInfos(string.Format("打开文件：{0}", fileName), WSX.Logger.LogLevel.Info);
            }
            catch (Exception ex)
            {
                LoggerManager.AddSystemInfos(string.Format("打开文件异常：{0}，原因：{1}", fileName, ex.Message), WSX.Logger.LogLevel.Error);
                XtraMessageBox.Show(ex.Message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateFilePath(string path)
        {
            this.FilePath = path;
            this.FileName = string.IsNullOrEmpty(path) ? DEFAULT_FILE_NAME : Path.GetFileName(path);//Path.GetFileNameWithoutExtension(path);
            this.OnFileNameChanged?.Invoke(this.FileName);
        }
        /// <summary>
        /// 解析图形文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private IDocument<FigureBase3DModel> ReadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string exName = Path.GetExtension(fileName);
                var doc = DocumentFactory.CreateInstance<FigureBase3DModel>(exName);
                doc.ReadFromFile(fileName);
                return doc;
            }
            return null;
        }
        public void SaveFileDialog(IModel model, string exName = ".WTF")
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "WSX默认切割文件|*.WTF";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    WriteToFile(model, save.FileName);
                    this.UpdateFilePath(save.FileName);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.AddSystemInfos("保存文件异常:" + ex, WSX.Logger.LogLevel.Error);
                XtraMessageBox.Show(ex.Message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SaveFile(IModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(this.FilePath) || Path.GetExtension(this.FilePath).ToUpper() != ".WXF")
                {
                    this.SaveFileDialog(model);
                }
                else
                {
                    this.WriteToFile(model, this.FilePath);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.AddSystemInfos("保存文件异常" + ex, WSX.Logger.LogLevel.Error);
                XtraMessageBox.Show(ex.Message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void WriteToFile(IModel model, string fileName)
        {
            var figures = new List<FigureBase3DModel>();
            figures.AddRange(FigureManager.ToFigureBaseModel(model.DrawLayer.Objects));
            figures.AddRange(FigureManager.ToFigureBaseModel(model.MarkLayer.Objects, true));
            string exName = Path.GetExtension(fileName);
            var doc = DocumentFactory.CreateInstance<FigureBase3DModel>(exName);
            doc.Entity.Figures = figures;
            doc.TubeMode = model.TubeMode;
            doc.WriteToFile(fileName);
            this.oldFigures = doc.Entity.Figures;
        }
        public void RegisterFileType()
        {
            var info = new FileTypeRegInfo()
            {
                ExtendName = ".WTF",
                Description = "WSX Cut Document(.WTF)",
                IcoPath = Application.StartupPath + @"\Assets\file2.ico",
                ExePath = Application.StartupPath + @"\WSXCutTubeSystem.exe"
            };
            FileAssociator.RegisterFileType(info);
        }

    }
}

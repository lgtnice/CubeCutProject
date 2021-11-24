using DevExpress.Mvvm.DataAnnotations;
using System;
using WSX.GlobalData.Model;

namespace WSX.ViewModels
{
    [POCOViewModel()]
    public class MainViewModel
    {
        //public OperationStatus OperStatus { get; private set; }

        public MainViewModel()
        {
            //Messenger.Default.Register<object>(this, "OperStatusChanged", status => this.OperStatus = (OperationStatus)status);
        }

        public AdditionalInfo AdditionalInfo { get => GlobalModel.Params.AdditionalInfo; }


        //public void AddSysLog(string msg, Color color)
        //{
        //    Messenger.Default.Send<object>(Tuple.Create(msg, color), "AddSysLog");
        //}
    }
}


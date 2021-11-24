using System;
using WSX.CommomModel.Enums;

namespace WSX.CommomModel.DrawModel
{
    public class CommandEntity
    {
        public string CommandName { set; get; }

        public string ParameterName { set; get; }

        public string ParameterValue { get; set; }

        public InputDataFormat ParaDataFormat { set; get; }

        public Action<object> CallbackAction { set; get; }

        public bool ShowCommandName { set; get; }
    }
}

using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.ParaModel;

namespace WSX.GlobalData.Model
{
    /// <summary>
    /// 系统所有配置参数
    /// </summary>
    public class GlobalParameters
    {
        public AdditionalInfo AdditionalInfo { get; set; } = new AdditionalInfo();
        public LayerConfigModel LayerConfig { get; set; } = new LayerConfigModel();

        public ArrayModel ArrayParam { set; get; } = new ArrayModel();

        public CompensationModel CompensationParam { set; get; } = new CompensationModel();

        public MicroConnectParam MicroConnectParam { set; get; } = new MicroConnectParam();

        public GapModel GapParam { set; get; } = new GapModel();
    }
}

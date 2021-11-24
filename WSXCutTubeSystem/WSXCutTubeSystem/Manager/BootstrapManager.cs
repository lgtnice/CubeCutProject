using System;
using System.Collections.Generic;
using System.Threading;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics;
using WSX.CommomModel.Utilities;
using WSX.GlobalData.Model;
using WSXCutTubeSystem.Models;
using WSXCutTubeSystem.Views.Forms;

namespace WSXCutTubeSystem.Manager
{
    public class BootstrapManager
    {
        public static string ActiveWindowCmd = "Active";
        private static Mutex SysMutex;

        private static List<InitStep> InitSteps;

        static BootstrapManager()
        {
            //Fixed Steps
            InitSteps = new List<InitStep>
            {
                new InitStep(LoadConfiguration, "加载配置文件…"),
                new InitStep(CheckLicense, "检查权限模块…"),
                new InitStep(InitHardware, "初始化硬件模块…"),
                new InitStep(ClearCache, "清除系统缓存…")
            };
        }

        public static void Attatch(string descripion, Action action)
        {
            InitSteps.Add(new InitStep(action, descripion));
        }

        public static bool IsLaunched
        {
            get
            {
                bool flag = false;
                SysMutex = new Mutex(true, "WSXCUTTUBE_Mutex", out flag);
                return !flag;
            }
        }

        public static bool IsHardwareExist
        {
            get
            {
                //var para = SystemConfig.Load(@".\Configs\SysConfig.json");
                //return TcpClientStream.IsExist(para.IpAddress, para.Port);

                return false;
            }
        }

        public static bool Intialize()
        {
            FrmStartup frm = new FrmStartup(InitSteps);
            frm.ShowDialog();
            return frm.IsInitDone;
        }

        #region Intializement Step
        private static void LoadConfiguration()
        {
            FileManager.Instance.RegisterFileType();
            var jsonValue = SerializeUtil.JsonReadByFile<GlobalParameters>(GlobalModel.ConfigFileName);
            if (jsonValue != null)
            {
                GlobalModel.Params = jsonValue;

                #region Check if valid
                var layers = GlobalModel.Params.LayerConfig.LayerCrafts;
                if (layers.Count != 15)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        layers[i + 1] = DefaultParaHelper.GetDefaultLayerCraftModel();
                    }
                }
                #endregion
            }
            else
            {
                GlobalModel.Params.LayerConfig = DefaultParaHelper.GetDefaultLayerConfigModel();
            }
            //SystemContext.SystemPara = SystemConfig.Load(@".\Configs\SysConfig.json");
            LoadUnitInfo();
        }

        private static void CheckLicense()
        {

        }

        private static void InitHardware()
        {
            //TODO: Ethernet not found, set dummy mode     
            //string ip = SystemContext.SystemPara.IpAddress;
            //int port = SystemContext.SystemPara.Port;
            //var device = TcpClientStream.SearchDevice(ip, port);
            //if (device == null)
            //{
            //    SystemContext.IsDummyMode = true;
            //    var motor = new DummyMotorController();
            //    var laser = new DummyLaser();
            //    var blowing = new DummyIO();
            //    var height = new DummyHeightController();
            //    SystemContext.Hardware = new HardwareProxy(motor, laser, blowing, height);
            //}
            //else
            //{
            //    SystemContext.IsDummyMode = false;
            //    var para = SystemContext.SystemPara;                                    
            //    var motor = new MotorController(device, para.MotorConfig);
            //    var laser = new LaserController(device, 10);
            //    var blowing = new IOController(device);
            //    var height = new HeightController(motor, HeightConfig.Default);
            //    SystemContext.Hardware = new HardwareProxy(motor, laser, blowing, height);
            //}    
        }

        private static void ClearCache()
        {

        }
        #endregion

        private static void LoadUnitInfo()
        {
            var tmp1 = UnitTimeTypes.Millisecond;
            var tmp2 = UnitSpeedTypes.Millimeter_Second;
            var tmp3 = UnitAcceleratedTypes.MillimeterPerSecondSquared;
            var tmp4 = UnitPressureTypes.BAR;

            if (GlobalModel.Params != null)
            {
                tmp1 = GlobalModel.Params.LayerConfig.UnitTimeType;
                tmp2 = GlobalModel.Params.LayerConfig.UnitSpeedType;
                tmp3 = GlobalModel.Params.LayerConfig.UnitAcceleratedType;
                tmp4 = GlobalModel.Params.LayerConfig.UnitPressureType;
            }

            UnitObserverFacade.Instance.TimeUnitObserver.UnitType = tmp1;
            UnitObserverFacade.Instance.SpeedUnitObserver.UnitType = tmp2;
            UnitObserverFacade.Instance.AccelerationUnitObserver.UnitType = tmp3;
            UnitObserverFacade.Instance.PressureUnitObserver.UnitType = tmp4;
        }
    }


  
}

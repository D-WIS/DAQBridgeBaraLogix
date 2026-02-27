using DWIS.RigOS.Common.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWIS.DAQBridge.BaraLogix.Server
{
    public class ConfigurationForBaraLogix : ConfigurationForOPCUA
    {
        public bool InitializeInputOPCUAVariables { get; set; } = true;
    }
}

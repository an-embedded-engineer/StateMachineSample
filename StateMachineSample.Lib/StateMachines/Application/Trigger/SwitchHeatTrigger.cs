using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class SwitchHeatTrigger : Trigger
    {
        public static SwitchHeatTrigger Instance { get; private set; } = new SwitchHeatTrigger();

        public SwitchHeatTrigger() : base("Switch Heat Trigger")
        {
        }
    }
}

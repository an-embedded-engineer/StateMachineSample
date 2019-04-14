using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class SwitchStopTrigger : Trigger
    {
        public static SwitchStopTrigger Instance { get; private set; } = new SwitchStopTrigger();

        public SwitchStopTrigger() : base("Switch Stop Trigger", SwitchStopEffect.Instance)
        {
        }
    }
}

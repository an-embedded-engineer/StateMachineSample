using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class SwitchDryTrigger : Trigger
    {
        public static SwitchDryTrigger Instance { get; private set; } = new SwitchDryTrigger();

        public SwitchDryTrigger() : base("Switch Dry Trigger")
        {
        }
    }
}

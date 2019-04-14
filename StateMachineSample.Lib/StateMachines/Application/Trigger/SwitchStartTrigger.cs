using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class SwitchStartTrigger : Trigger
    {
        public static SwitchStartTrigger Instance { get; private set; } = new SwitchStartTrigger();

        public SwitchStartTrigger() : base("Switch Start Trigger", SwitchStartEffect.Instance)
        {
        }
    }
}

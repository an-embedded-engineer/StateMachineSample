using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class SwitchCoolTrigger : Trigger
    {
        public static SwitchCoolTrigger Instance { get; private set; } = new SwitchCoolTrigger();

        public SwitchCoolTrigger() : base("Switch Cool Trigger")
        {
        }
    }
}

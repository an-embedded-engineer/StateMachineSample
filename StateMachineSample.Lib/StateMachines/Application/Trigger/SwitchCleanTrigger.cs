using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class SwitchCleanTrigger : Trigger
    {
        public static SwitchCleanTrigger Instance { get; private set; } = new SwitchCleanTrigger();

        public SwitchCleanTrigger() : base("Switch Clean Trigger")
        {
        }
    }
}

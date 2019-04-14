using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class CleanEndTrigger : Trigger
    {
        public static CleanEndTrigger Instance { get; private set; } = new CleanEndTrigger();

        public CleanEndTrigger() : base("Clean End Trigger", CleanEndEffect.Instance)
        {
        }
    }
}

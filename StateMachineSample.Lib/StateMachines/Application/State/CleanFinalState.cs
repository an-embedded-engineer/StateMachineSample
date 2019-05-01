using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class CleanFinalState : State
    {
        public static CleanFinalState Instance { get; private set; } = new CleanFinalState();

        private CleanFinalState() : base("Clean Final")
        {
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
            };
        }
    }
}

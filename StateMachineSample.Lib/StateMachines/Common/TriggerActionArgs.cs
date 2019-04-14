using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public class TriggerActionArgs
    {
        public StateMachine Context { get; }

        public Trigger Trigger { get; }

        public TriggerActionArgs(StateMachine context, Trigger trigger)
        {
            this.Context = context;

            this.Trigger = trigger;
        }
    }
}

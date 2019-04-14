using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class SwitchStopEffect : Effect
    {
        public static SwitchStopEffect Instance { get; private set; } = new SwitchStopEffect();

        public SwitchStopEffect() : base("Switch Stop Effect")
        {
        }

        protected override void ExecuteAction(StateMachine context)
        {
            if (context is ModelStateMachine stm)
            {
                var model = stm.Model;

                model.Stop();
            }
        }
    }
}

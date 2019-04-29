using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class SwitchStartEffect : Effect
    {
        public static SwitchStartEffect Instance { get; private set; } = new SwitchStartEffect();

        public SwitchStartEffect() : base("Switch Start Effect")
        {
        }

        protected override void ExecuteAction(StateMachine context)
        {
            if (context is ModelStateMachine stm)
            {
                var model = stm.Model;

                model.Start();
            }
        }
    }
}

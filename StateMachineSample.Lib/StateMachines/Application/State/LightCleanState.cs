using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class LightCleanState : State
    {
        public static LightCleanState Instance { get; private set; } = new LightCleanState();

        private LightCleanState() : base("Light Clean")
        {
            this.OnDo += this.DoEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
            };
        }

        private void DoEventHandler(StateMachine context)
        {
            var stm = context.GetAs<CleanStateMachine>();

            var model = stm.Model;

            var result = model.LightCleanControl();

            if (result == true)
            {
                stm.ChangeState(CleanFinalState.Instance);
            }
        }
    }
}

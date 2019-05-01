using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class DeepCleanState : State
    {
        public static DeepCleanState Instance { get; private set; } = new DeepCleanState();

        private DeepCleanState() : base("Deep Clean")
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

            var result = model.DeepCleanControl();

            if (result == true)
            {
                stm.ChangeState(CleanFinalState.Instance);
            }
        }
    }
}

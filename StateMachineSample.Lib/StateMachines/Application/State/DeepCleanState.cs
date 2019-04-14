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
            this.OnEnter += this.EnterEventHandler;
            this.OnDo += this.DoEventHandler;
            this.OnExit += this.ExitEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {

            };
        }

        private void EnterEventHandler(StateMachine context)
        {

        }

        private void DoEventHandler(StateMachine context)
        {
            var stm = context.GetAs<CleanStateMachine>();

            var model = stm.Model;

            var result = model.DeepCleanControl();

            if (result == true)
            {
                var parent = stm.Parent;

                parent.SendTrigger(CleanEndTrigger.Instance);
            }
        }

        private void ExitEventHandler(StateMachine context)
        {

        }
    }
}

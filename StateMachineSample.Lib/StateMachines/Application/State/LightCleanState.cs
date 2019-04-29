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
            this.OnEntry += this.EntryEventHandler;
            this.OnDo += this.DoEventHandler;
            this.OnExit += this.ExitEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {

            };
        }

        private void EntryEventHandler(StateMachine context)
        {

        }

        private void DoEventHandler(StateMachine context)
        {
            var stm = context.GetAs<CleanStateMachine>();

            var model = stm.Model;

            var result = model.LightCleanControl();

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class CleanState : State
    {
        public static CleanState Instance { get; private set; } = new CleanState();

        public CleanStateMachine SubContext { get; private set; }

        private CleanState() : base("Clean")
        {
            this.OnEntry += this.EntryEventHandler;
            this.OnDo += this.DoEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
                { SwitchStopTrigger.Instance.Name, this.SwitchStopTriggerHandler },
            };
        }

        private void EntryEventHandler(StateMachine context)
        {
            var parent = context.GetAs<ModelStateMachine>();

            this.SubContext = new CleanStateMachine(parent);
        }

        private void DoEventHandler(StateMachine context)
        {
            this.SubContext.Update();

            if (this.SubContext.CurrentState is CleanFinalState)
            {
                var effect = CleanEndEffect.Instance;

                context.ChangeState(RunningState.Instance, effect);
            }
        }

        private void SwitchStopTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            var effect = args.Trigger.Effect;

            context.ChangeState(StopState.Instance, effect);
        }
    }
}

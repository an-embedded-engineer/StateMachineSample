using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class RunningState : State
    {
        public static RunningState Instance { get; private set; } = new RunningState();

        public RunningStateMachine SubContext { get; private set; }

        private RunningState() : base("Running")
        {
            this.OnEnter += this.EnterEventHandler;
            this.OnDo += this.DoEventHandler;
            this.OnExit += this.ExitEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
                { SwitchStopTrigger.Instance.Name, this.SwitchStopTriggerHandler },
                { SwitchCleanTrigger.Instance.Name, this.SwitchCleanTriggerHandler },
                { SwitchCoolTrigger.Instance.Name, this.SubContextTriggerHandler },
                { SwitchHeatTrigger.Instance.Name, this.SubContextTriggerHandler },
                { SwitchDryTrigger.Instance.Name, this.SubContextTriggerHandler },
            };
        }

        private void EnterEventHandler(StateMachine context)
        {
            if (this.SubContext == null)
            {
                var parent = context.GetAs<ModelStateMachine>();

                this.SubContext = new RunningStateMachine(parent);
            }
        }

        private void DoEventHandler(StateMachine context)
        {
            this.SubContext.Update();
        }

        private void ExitEventHandler(StateMachine context)
        {

        }

        private void SwitchStopTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            var effect = args.Trigger.Effect;

            context.ChangeState(StopState.Instance, effect);
        }

        private void SwitchCleanTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(CleanState.Instance);
        }

        private void SubContextTriggerHandler(TriggerActionArgs args)
        {
            var trigger = args.Trigger;

            var context = this.SubContext;

            context.SendTrigger(trigger);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class StopState : State
    {
        public static StopState Instance { get; private set; } = new StopState();

        private StopState() : base("Stop")
        {
            this.OnEnter += this.EnterEventHandler;
            this.OnDo += this.DoEventHandler;
            this.OnExit += this.ExitEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
                { SwitchStartTrigger.Instance.Name, this.SwitchStartTriggerHandler },
            };
        }

        private void EnterEventHandler(StateMachine context)
        {

        }

        private void DoEventHandler(StateMachine context)
        {

        }

        private void ExitEventHandler(StateMachine context)
        {

        }

        private void SwitchStartTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(RunningState.Instance);
        }
    }
}

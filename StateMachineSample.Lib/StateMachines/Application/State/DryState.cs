using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class DryState : State
    {
        public static DryState Instance { get; private set; } = new DryState();

        private DryState() : base("Dry")
        {
            this.OnEntry += this.EntryEventHandler;
            this.OnDo += this.DoEventHandler;
            this.OnExit += this.ExitEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
                { SwitchCoolTrigger.Instance.Name, this.SwitchCoolTriggerHandler },
                { SwitchHeatTrigger.Instance.Name, this.SwitchHeatTriggerHandler },
            };
        }

        private void EntryEventHandler(StateMachine context)
        {

        }

        private void DoEventHandler(StateMachine context)
        {
            var stm = context.GetAs<RunningStateMachine>();

            var model = stm.Model;

            model.DryControl();
        }

        private void ExitEventHandler(StateMachine context)
        {

        }

        private void SwitchCoolTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(CoolState.Instance);
        }

        private void SwitchHeatTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(HeatState.Instance);
        }
    }
}

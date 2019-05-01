using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class HeatState : State
    {
        public static HeatState Instance { get; private set; } = new HeatState();

        private HeatState() : base("Heat")
        {
            this.OnDo += this.DoEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
                { SwitchCoolTrigger.Instance.Name, this.SwitchCoolTriggerHandler },
                { SwitchDryTrigger.Instance.Name, this.SwitchDryTriggerHandler },
            };
        }

        private void DoEventHandler(StateMachine context)
        {
            var stm = context.GetAs<RunningStateMachine>();

            var model = stm.Model;

            model.HeatControl();
        }

        private void SwitchCoolTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(CoolState.Instance);
        }

        private void SwitchDryTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(DryState.Instance);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class CoolState : State
    {
        public static CoolState Instance { get; private set; } = new CoolState();

        private CoolState() : base("Cool")
        {
            this.OnEnter += this.EnterEventHandler;
            this.OnDo += this.DoEventHandler;
            this.OnExit += this.ExitEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
                { SwitchHeatTrigger.Instance.Name, this.SwitchHeatTriggerHandler },
                { SwitchDryTrigger.Instance.Name, this.SwitchDryTriggerHandler },
            };
        }

        private void EnterEventHandler(StateMachine context)
        {
            
        }

        private void DoEventHandler(StateMachine context)
        {
            var stm = context.GetAs<RunningStateMachine>();

            var model = stm.Model;

            model.CoolControl();
        }

        private void ExitEventHandler(StateMachine context)
        {

        }

        private void SwitchHeatTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(HeatState.Instance);
        }

        private void SwitchDryTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(DryState.Instance);
        }
    }
}

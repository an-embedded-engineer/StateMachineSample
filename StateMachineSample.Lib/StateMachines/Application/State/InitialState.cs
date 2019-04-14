using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class InitialState : State
    {
        public static InitialState Instance { get; private set; } = new InitialState();

        private InitialState() : base("Initial")
        {
            this.OnEnter += this.EnterEventHandler;
            this.OnDo += this.DoEventHandler;
            this.OnExit += this.ExitEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
                { InitializedTrigger.Instance.Name, this.InitializedTriggerHandler },
            };
        }

        private void EnterEventHandler(StateMachine context)
        {
            var stm = context.GetAs<ModelStateMachine>();

            var model = stm.Model;

            model.Initialize();

            stm.SendTrigger(InitializedTrigger.Instance);
        }

        private void DoEventHandler(StateMachine context)
        {

        }

        private void ExitEventHandler(StateMachine context)
        {

        }

        private void InitializedTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(StopState.Instance);
        }

    }
}

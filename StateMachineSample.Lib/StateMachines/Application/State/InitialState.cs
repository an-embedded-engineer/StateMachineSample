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
            this.OnEntry += this.EntryEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {
                { InitializedTrigger.Instance.Name, this.InitializedTriggerHandler },
            };
        }

        private void EntryEventHandler(StateMachine context)
        {
            var stm = context.GetAs<ModelStateMachine>();

            var model = stm.Model;

            model.Initialize();

            stm.SendTrigger(InitializedTrigger.Instance);
        }

        private void InitializedTriggerHandler(TriggerActionArgs args)
        {
            var context = args.Context;

            context.ChangeState(StopState.Instance);
        }

    }
}

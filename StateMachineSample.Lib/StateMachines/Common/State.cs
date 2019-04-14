using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public delegate void StateEventHandler(StateMachine conetxt);

    public abstract class State
    {
        public string Name { get; }

        protected StateEventHandler OnEnter;
        protected StateEventHandler OnDo;
        protected StateEventHandler OnExit;

        private TriggerActionMap TriggerActionMap { get; set; }

        protected abstract TriggerActionMap GenerateTriggerActionMap();

        public State(string name)
        {
            this.Name = name;

            this.TriggerActionMap = this.GenerateTriggerActionMap();
        }

        public void ExecuteEnterAction(StateMachine context)
        {
            Messenger.Send($"Enter : {this.Name}");

            this.OnEnter?.Invoke(context);
        }

        public void ExecuteDoAction(StateMachine context)
        {
            Messenger.Send($"Do : {this.Name}");

            this.OnDo?.Invoke(context);
        }

        public void ExecuteExitAction(StateMachine context)
        {
            Messenger.Send($"Exit : {this.Name}");

            this.OnExit?.Invoke(context);
        }

        public void SendTrigger(StateMachine context, Trigger trigger)
        {
            if (this.TriggerActionMap.ContainsKey(trigger.Name) == true)
            {
                Messenger.Send($"Trigger : {trigger.Name}");

                var action = this.TriggerActionMap[trigger.Name];

                var args = new TriggerActionArgs(context, trigger);

                action(args);
            }
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}

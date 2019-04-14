using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public abstract class StateMachine : NotificationObject
    {
        private State _CurrentState { get; set; }

        public State CurrentState
        {
            get { return this._CurrentState; }
            set
            {
                if (this._CurrentState != value)
                {
                    this._CurrentState = value;
                    this.RaisePropertyChanged(nameof(this.CurrentState));
                }
            }
        }

        public State PreviousState { get; private set; }

        protected abstract State GetInitialState();

        public StateMachine()
        {
        }

        public void SendTrigger(Trigger trigger)
        {
            Messenger.Send($"Send Trigger : {trigger.Name}");

            this.CurrentState?.SendTrigger(this, trigger);
        }

        public void ChangeState(State new_state, Effect effect = null)
        {
            if (this.CurrentState != new_state)
            {
                var old_state = this.CurrentState;

                this.CurrentState?.ExecuteExitAction(this);

                this.CurrentState = new_state;
                this.PreviousState = old_state;

                if (old_state != null)
                {
                    Messenger.Send($"State Changed : {old_state} => {new_state}");
                }

                effect?.Execute(this);

                this.CurrentState?.ExecuteEnterAction(this);
            }
        }

        public void Update()
        {
            this.CurrentState?.ExecuteDoAction(this);
        }

        public T GetAs<T>() where T : StateMachine
        {
            if (this is T stm)
            {
                return stm;
            }
            else
            {
                throw new InvalidOperationException($"State Machine is not {nameof(T)}");
            }
        }

        protected void ChangeToInitialState()
        {
            var initial_state = this.GetInitialState();

            this.ChangeState(initial_state);
        }
    }
}

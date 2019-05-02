using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using StateMachineSample.WPF.Models;

using StateMachineSample.Lib;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;

using StmMessenger = StateMachineSample.Lib.Messenger;

namespace StateMachineSample.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public AirConditioner Model { get; }

        public ModelStateMachine StateMachine { get; }

        public ReactiveProperty<string> Message { get; }

        public ReadOnlyReactiveCollection<string> MessageLog { get; }

        public ReactiveProperty<State> CurrentState { get; }

        public int MaxTargetTemperature { get; }

        public int MinTargetTemperature { get; }

        public ReactiveProperty<int> TargetTemperature { get; }

        public ReactiveProperty<string> Status { get; }

        public ReactiveProperty<string> TargetTemp { get; }

        public ReactiveProperty<string> Temperature { get; }

        public ReactiveProperty<string> Humidity { get; }

        public ReactiveCommand StopCommand { get; }

        public ReactiveCommand StartCommand { get; }

        public ReactiveCommand CoolCommand { get; }

        public ReactiveCommand HeatCommand { get; }

        public ReactiveCommand DryCommand { get; }

        public ReactiveCommand CleanCommand { get; }

        public ReactiveCommand UpCommand { get; }

        public ReactiveCommand DownCommand { get; }

        public MainWindowViewModel()
        {
            this.MaxTargetTemperature = AirConditioner.MaxTargetTemperature;

            this.MinTargetTemperature = AirConditioner.MinTargetTemperature;


            this.Status = new ReactiveProperty<string>("");

            this.TargetTemp = new ReactiveProperty<string>("");

            this.Temperature = new ReactiveProperty<string>("");

            this.Humidity = new ReactiveProperty<string>("");


            this.Message = new ReactiveProperty<string>();

            this.MessageLog = this.Message.ToReadOnlyReactiveCollection();

            StmMessenger.OnMessageReceived += (message) =>
            {
                this.Message.Value = message;
            };

            this.Model = new AirConditioner();

            this.StateMachine = new ModelStateMachine(this.Model);


            this.CurrentState = this.StateMachine.ObserveProperty(stm => stm.CurrentState).ToReactiveProperty();

            this.TargetTemperature = this.Model.ToReactivePropertyAsSynchronized(model => model.TargetTemperature);


            this.StopCommand = this.CurrentState.Select(s => s is RunningState || s is CleanState).ToReactiveCommand();

            this.StartCommand = this.CurrentState.Select(s => s is StopState).ToReactiveCommand();

            this.CoolCommand = this.CurrentState.Select(s => s is RunningState).ToReactiveCommand();

            this.HeatCommand = this.CurrentState.Select(s => s is RunningState).ToReactiveCommand();

            this.DryCommand = this.CurrentState.Select(s => s is RunningState).ToReactiveCommand();

            this.CleanCommand = this.CurrentState.Select(s => s is RunningState).ToReactiveCommand();

            this.UpCommand = this.TargetTemperature.Select(v => v < this.MaxTargetTemperature).ToReactiveCommand();

            this.DownCommand = this.TargetTemperature.Select(v => v > this.MinTargetTemperature).ToReactiveCommand();


            this.StopCommand.Subscribe(_ => this.StateMachine.SendTrigger(SwitchStopTrigger.Instance));

            this.StartCommand.Subscribe(_ => this.StateMachine.SendTrigger(SwitchStartTrigger.Instance));

            this.CoolCommand.Subscribe(_ => this.StateMachine.SendTrigger(SwitchCoolTrigger.Instance));

            this.HeatCommand.Subscribe(_ => this.StateMachine.SendTrigger(SwitchHeatTrigger.Instance));

            this.DryCommand.Subscribe(_ => this.StateMachine.SendTrigger(SwitchDryTrigger.Instance));

            this.CleanCommand.Subscribe(_ => this.StateMachine.SendTrigger(SwitchCleanTrigger.Instance));

            this.UpCommand.Subscribe(_ => this.Model.Up());

            this.DownCommand.Subscribe(_ => this.Model.Down());


            var interval = Observable.Interval(TimeSpan.FromMilliseconds(100));

            var timer_sub = interval.Subscribe(
                i =>
                {
                    if (this.StateMachine.CurrentState is RunningState running_state)
                    {
                        var sub = running_state.SubContext;

                        this.Status.Value = $"{this.StateMachine.CurrentState} - {sub.CurrentState}";
                    }
                    else if (this.StateMachine.CurrentState is CleanState clean_state)
                    {
                        var sub = clean_state.SubContext;

                        this.Status.Value = $"{this.StateMachine.CurrentState} - {sub.CurrentState}";
                    }
                    else
                    {
                        this.Status.Value = $"{this.StateMachine.CurrentState}";
                    }
                    
                    this.TargetTemp.Value = $"{this.Model.TargetTemperature}[℃]";
                    this.Temperature.Value = $"{this.Model.Temperature}[℃]";
                    this.Humidity.Value = $"{this.Model.Humidity}[％]";

                    this.StateMachine.Update();
                });



        }
    }
}

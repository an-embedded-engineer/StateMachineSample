using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class StainLevelAnalysisState : State
    {
        public static StainLevelAnalysisState Instance { get; private set; } = new StainLevelAnalysisState();

        private StainLevelAnalysisState() : base("Stain Level AnalysisState")
        {
            this.OnEnter += this.EnterEventHandler;
            this.OnDo += this.DoEventHandler;
            this.OnExit += this.ExitEventHandler;
        }

        protected override TriggerActionMap GenerateTriggerActionMap()
        {
            return new TriggerActionMap()
            {

            };
        }

        private void EnterEventHandler(StateMachine context)
        {

        }

        private void DoEventHandler(StateMachine context)
        {
            var stm = context.GetAs<CleanStateMachine>();

            var model = stm.Model;

            var level = model.StainLevelAnalys();

            switch (level)
            {
                case StainLevel.Unknown:
                    /* Nothing to do */
                    break;
                case StainLevel.Low:
                    stm.ChangeState(LightCleanState.Instance);
                    break;
                case StainLevel.High:
                    stm.ChangeState(DeepCleanState.Instance);
                    break;
                default:
                    /* Nothing to do */
                    break;
            }
        }

        private void ExitEventHandler(StateMachine context)
        {

        }
    }
}

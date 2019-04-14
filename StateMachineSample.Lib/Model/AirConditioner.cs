using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public class AirConditioner : NotificationObject
    {
        public const int MaxTargetTemperature = 40;

        public const int MinTargetTemperature = 15;

        public const int MaxHumidity = 90;

        public const int MinHumidity = 20;

        private int _Temperature { get; set; }

        public int Temperature
        {
            get { return this._Temperature; }
            set
            { 
                if (this._Temperature != value)
                {
                    this._Temperature = value;
                    this.RaisePropertyChanged(nameof(this.Temperature));
                }
            }
        }

        private int _TargetTemperature { get; set; }

        public int TargetTemperature
        {
            get { return this._TargetTemperature; }
            set
            {
                if (this._TargetTemperature != value)
                {
                    this._TargetTemperature = value;
                    this.RaisePropertyChanged(nameof(this.TargetTemperature));
                }
            }
        }

        private int _Humidity { get; set; }

        public int Humidity
        {
            get { return this._Humidity; }
            set
            {
                if (this._Humidity != value)
                {
                    this._Humidity = value;
                    this.RaisePropertyChanged(nameof(this.Humidity));
                }
            }
        }

        public StainLevel StainLevel { get; private set; }

        private int AnalyseCount { get; set; }

        private int CleanCount { get; set; }

        private StainLevel PrevStainLevel { get; set; }

        private bool AnalyseFinished => (this.StainLevel != StainLevel.Unknown);

        private bool CleanFinished => ((this.StainLevel == StainLevel.High && this.CleanCount >= 20) 
                                    || (this.StainLevel == StainLevel.Low && this.CleanCount >= 10));

        public void Initialize()
        {
            this.Temperature = 30;

            this.TargetTemperature = AirConditioner.MinTargetTemperature;

            this.Humidity = 50;

            this.StainLevel = StainLevel.Unknown;

            this.PrevStainLevel = StainLevel.Unknown;

            this.AnalyseCount = 0;

            this.CleanCount = 0;
        }

        public void Stop()
        {
            this.Humidity = 50;

            this.StainLevel = StainLevel.Unknown;

            this.PrevStainLevel = StainLevel.Unknown;

            this.AnalyseCount = 0;

            this.CleanCount = 0;
        }

        public void Up()
        {
            if (this.TargetTemperature < AirConditioner.MaxTargetTemperature)
            {
                this.TargetTemperature++;
            }
        }

        public void Down()
        {
            if (this.TargetTemperature > AirConditioner.MinTargetTemperature)
            {
                this.TargetTemperature--;
            }
        }

        public void CoolControl()
        {
            if (this.TargetTemperature < this.Temperature)
            {
                this.Temperature--;
            }
        }

        public void HeatControl()
        {
            if (this.TargetTemperature > this.Temperature)
            {
                this.Temperature++;
            }
        }

        public void DryControl()
        {
            if (this.Humidity > AirConditioner.MinHumidity)
            {
                this.Humidity--;
            }
        }

        public StainLevel StainLevelAnalys()
        {
            this.AnalyseCount++;

            if (this.AnalyseCount >= 5)
            {
                switch (this.PrevStainLevel)
                {
                    case StainLevel.Unknown:
                        this.StainLevel = StainLevel.Low;
                        break;
                    case StainLevel.Low:
                        this.StainLevel = StainLevel.High;
                        break;
                    case StainLevel.High:
                        this.StainLevel = StainLevel.Low;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.StainLevel = StainLevel.Unknown;
            }

            return this.StainLevel;
        }

        public bool DeepCleanControl()
        {
            this.CleanCount++;

            return this.CleanFinished;
        }

        public bool LightCleanControl()
        {
            this.CleanCount++;

            return this.CleanFinished;
        }

        public void CleanEnd()
        {
            this.PrevStainLevel = this.StainLevel;

            this.StainLevel = StainLevel.Unknown;

            this.AnalyseCount = 0;

            this.CleanCount = 0;
        }

    }
}

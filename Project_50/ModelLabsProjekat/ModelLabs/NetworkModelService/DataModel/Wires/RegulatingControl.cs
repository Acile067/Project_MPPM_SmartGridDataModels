using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingControl : PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModeKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
        private long terminal = 0;

        public RegulatingControl(long globalId)
            : base(globalId)
        {
        }
        public bool Discrete
        {
            get { return discrete; }
            set { discrete = value; }
        }
        public RegulatingControlModeKind Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        public PhaseCode MonitoredPhase
        {
            get { return monitoredPhase; }
            set { monitoredPhase = value; }
        }
        public float TargetRange
        {
            get { return targetRange; }
            set { targetRange = value; }
        }
        public float TargetValue
        {
            get { return targetValue; }
            set { targetValue = value; }
        }
        public long Terminal
        {
            get { return terminal; }
            set { terminal = value; }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingControl x = (RegulatingControl)obj;
                return (x.discrete == this.discrete &&
                        x.mode == this.mode &&
                        x.monitoredPhase == this.monitoredPhase &&
                        x.targetRange == this.targetRange &&
                        x.targetValue == this.targetValue &&
                        x.terminal == this.terminal);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.REGCONTROL_DIS:
                case ModelCode.REGCONTROL_MODE:
                case ModelCode.REGCONTROL_MONIPHASE:
                case ModelCode.REGCONTROL_TARGRANGE:
                case ModelCode.REGCONTROL_TARGVALUE:
                case ModelCode.REGCONTROL_TERMINAL:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGCONTROL_DIS:
                    property.SetValue(discrete);
                    break;
                case ModelCode.REGCONTROL_MODE:
                    property.SetValue((short)mode);
                    break;
                case ModelCode.REGCONTROL_MONIPHASE:
                    property.SetValue((short)monitoredPhase);
                    break;
                case ModelCode.REGCONTROL_TARGRANGE:
                    property.SetValue(targetRange);
                    break;
                case ModelCode.REGCONTROL_TARGVALUE:
                    property.SetValue(targetValue);
                    break;
                case ModelCode.REGCONTROL_TERMINAL:
                    property.SetValue(terminal);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGCONTROL_DIS:
                    discrete = property.AsBool();
                    break;
                case ModelCode.REGCONTROL_MODE:
                    mode = (RegulatingControlModeKind)property.AsEnum();
                    break;
                case ModelCode.REGCONTROL_MONIPHASE:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.REGCONTROL_TARGRANGE:
                    targetRange = property.AsFloat();
                    break;
                case ModelCode.REGCONTROL_TARGVALUE:
                    targetValue = property.AsFloat();
                    break;
                case ModelCode.REGCONTROL_TERMINAL:
                    terminal = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (terminal != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGCONTROL_TERMINAL] = new List<long>();
                references[ModelCode.REGCONTROL_TERMINAL].Add(terminal);
            }

            base.GetReferences(references, refType);
        }
    }
}

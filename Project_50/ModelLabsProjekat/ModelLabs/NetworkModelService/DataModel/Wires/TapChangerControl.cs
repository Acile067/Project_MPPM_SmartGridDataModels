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
    public class TapChangerControl : RegulatingControl
    {
        private float limVoltage;
        private bool lineDropCompensation;
        private float lineDropR;
        private float lineDropX;
        private float reverseLineDropR;
        private float reverseLineDropX;
        private List<long> tapChangers = new List<long>();

        public TapChangerControl(long globalId) : base(globalId)
        {
        }

        public float LimVoltage
        {
            get { return limVoltage; }
            set { limVoltage = value; }
        }
        public bool LineDropCompensation
        {
            get { return lineDropCompensation; }
            set { lineDropCompensation = value; }
        }
        public float LineDropR
        {
            get { return lineDropR; }
            set { lineDropR = value; }
        }
        public float LineDropX
        {
            get { return lineDropX; }
            set { lineDropX = value; }
        }
        public float ReverseLineDropR
        {
            get { return reverseLineDropR; }
            set { reverseLineDropR = value; }
        }
        public float ReverseLineDropX
        {
            get { return reverseLineDropX; }
            set { reverseLineDropX = value; }
        }
        public List<long> TapChangers
        {
            get { return tapChangers; }
            set { tapChangers = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TapChangerControl x = (TapChangerControl)obj;
                return ((x.LimVoltage == this.LimVoltage) &&
                        (x.LineDropCompensation == this.LineDropCompensation) &&
                        (x.LineDropR == this.LineDropR) &&
                        (x.LineDropX == this.LineDropX) &&
                        (x.ReverseLineDropR == this.ReverseLineDropR) &&
                        (x.ReverseLineDropX == this.ReverseLineDropX) &&
                        CompareHelper.CompareLists(x.TapChangers, this.TapChangers)
                        );
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
                case ModelCode.TAPCHANGCONTROL_LIMVOLT:
                case ModelCode.TAPCHANGCONTROL_LDC:
                case ModelCode.TAPCHANGCONTROL_LDR:
                case ModelCode.TAPCHANGCONTROL_LDX:
                case ModelCode.TAPCHANGCONTROL_RLDR:
                case ModelCode.TAPCHANGCONTROL_RLDX:
                case ModelCode.TAPCHANGCONTROL_TAPCHANGS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TAPCHANGCONTROL_LIMVOLT:
                    property.SetValue(limVoltage);
                    break;
                case ModelCode.TAPCHANGCONTROL_LDC:
                    property.SetValue(lineDropCompensation);
                    break;
                case ModelCode.TAPCHANGCONTROL_LDR:
                    property.SetValue(lineDropR);
                    break;
                case ModelCode.TAPCHANGCONTROL_LDX:
                    property.SetValue(lineDropX);
                    break;
                case ModelCode.TAPCHANGCONTROL_RLDR:
                    property.SetValue(reverseLineDropR);
                    break;
                case ModelCode.TAPCHANGCONTROL_RLDX:
                    property.SetValue(reverseLineDropX);
                    break;
                case ModelCode.TAPCHANGCONTROL_TAPCHANGS:
                    property.SetValue(tapChangers);
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
                case ModelCode.TAPCHANGCONTROL_LIMVOLT:
                    limVoltage = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGCONTROL_LDC:
                    lineDropCompensation = property.AsBool();
                    break;
                case ModelCode.TAPCHANGCONTROL_LDR:
                    lineDropR = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGCONTROL_LDX:
                    lineDropX = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGCONTROL_RLDX:
                    reverseLineDropX = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGCONTROL_RLDR:
                    reverseLineDropR = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
        public override bool IsReferenced
        {
            get
            {
                return (tapChangers.Count > 0) || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (tapChangers != null && tapChangers.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TAPCHANGCONTROL_TAPCHANGS] = tapChangers.GetRange(0, tapChangers.Count);
            }

            base.GetReferences(references, refType);
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TAPCHANGER_TCC:
                    tapChangers.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }
    }
}

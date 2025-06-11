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
    public class TapChanger : PowerSystemResource
    {
        private int highStep;
        private float initialDelay;
        private int lowStep;
        private bool ltcFlag;
        private int neutralStep;
        private float neutralU;
        private int normalStep;
        private bool regulationStatus;
        private float subsequentDelay;
        private long tapChangerControl = 0;

        public TapChanger(long globalId) : base(globalId)
        {
        }
        public int HighStep
        {
            get { return highStep; }
            set { highStep = value; }
        }
        public float InitialDelay
        {
            get { return initialDelay; }
            set { initialDelay = value; }
        }
        public int LowStep
        {
            get { return lowStep; }
            set { lowStep = value; }
        }
        public bool LtcFlag
        {
            get { return ltcFlag; }
            set { ltcFlag = value; }
        }
        public int NeutralStep
        {
            get { return neutralStep; }
            set { neutralStep = value; }
        }
        public float NeutralU
        {
            get { return neutralU; }
            set { neutralU = value; }
        }
        public int NormalStep
        {
            get { return normalStep; }
            set { normalStep = value; }
        }
        public bool RegulationStatus
        {
            get { return regulationStatus; }
            set { regulationStatus = value; }
        }
        public float SubsequentDelay
        {
            get { return subsequentDelay; }
            set { subsequentDelay = value; }
        }
        public long TapChangerControl
        {
            get { return tapChangerControl; }
            set { tapChangerControl = value; }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TapChanger x = (TapChanger)obj;
                return (x.highStep == this.highStep &&
                        x.initialDelay == this.initialDelay &&
                        x.lowStep == this.lowStep &&
                        x.ltcFlag == this.ltcFlag &&
                        x.neutralStep == this.neutralStep &&
                        x.neutralU == this.neutralU &&
                        x.normalStep == this.normalStep &&
                        x.regulationStatus == this.regulationStatus &&
                        x.subsequentDelay == this.subsequentDelay &&
                        x.tapChangerControl == this.tapChangerControl);
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
                case ModelCode.TAPCHANGER_HIGHSTEP:
                case ModelCode.TAPCHANGER_INITDELAY:
                case ModelCode.TAPCHANGER_LOWSTEP:
                case ModelCode.TAPCHANGER_LTCFLAG:
                case ModelCode.TAPCHANGER_NTRSTEP:
                case ModelCode.TAPCHANGER_NTRU:
                case ModelCode.TAPCHANGER_NORSTEP:
                case ModelCode.TAPCHANGER_REGSTATUS:
                case ModelCode.TAPCHANGER_SUBSDELAY:
                case ModelCode.TAPCHANGER_TCC:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TAPCHANGER_HIGHSTEP:
                    prop.SetValue(highStep);
                    break;
                case ModelCode.TAPCHANGER_INITDELAY:
                    prop.SetValue(initialDelay);
                    break;
                case ModelCode.TAPCHANGER_LOWSTEP:
                    prop.SetValue(lowStep);
                    break;
                case ModelCode.TAPCHANGER_LTCFLAG:
                    prop.SetValue(ltcFlag);
                    break;
                case ModelCode.TAPCHANGER_NTRSTEP:
                    prop.SetValue(neutralStep);
                    break;
                case ModelCode.TAPCHANGER_NTRU:
                    prop.SetValue(neutralU);
                    break;
                case ModelCode.TAPCHANGER_NORSTEP:
                    prop.SetValue(normalStep);
                    break;
                case ModelCode.TAPCHANGER_REGSTATUS:
                    prop.SetValue(regulationStatus);
                    break;
                case ModelCode.TAPCHANGER_SUBSDELAY:
                    prop.SetValue(subsequentDelay);
                    break;
                case ModelCode.TAPCHANGER_TCC:
                    prop.SetValue(tapChangerControl);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TAPCHANGER_HIGHSTEP:
                    highStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_INITDELAY:
                    initialDelay = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_LOWSTEP:
                    lowStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_LTCFLAG:
                    ltcFlag = property.AsBool();
                    break;
                case ModelCode.TAPCHANGER_NTRSTEP:
                    neutralStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_NTRU:
                    neutralU = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_NORSTEP:
                    normalStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_REGSTATUS:
                    regulationStatus = property.AsBool();
                    break;
                case ModelCode.TAPCHANGER_SUBSDELAY:
                    subsequentDelay = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_TCC:
                    tapChangerControl = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;

            }
        }
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (tapChangerControl != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TAPCHANGER_TCC] = new List<long>();
                references[ModelCode.TAPCHANGER_TCC].Add(tapChangerControl);
            }

            base.GetReferences(references, refType);
        }
    }
}

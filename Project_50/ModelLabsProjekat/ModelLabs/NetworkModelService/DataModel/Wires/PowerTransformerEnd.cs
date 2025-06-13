using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.Wires;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PowerTransformerEnd : TransformerEnd
    {
        private float b;
        private float b0;
        private WindingConnection connectionKind;
        private float g;
        private float g0;
        private long phaseAngleClock;
        private float r;
        private float r0;
        private float ratedS;
        private float ratedU;
        private float x;
        private float x0;
        private long powerTransformer = 0;
        public PowerTransformerEnd(long globalId)
            : base(globalId)
        {
        }

        public float B
        {
            get { return b; }
            set { b = value; }
        }
        public float B0
        {
            get { return b0; }
            set { b0 = value; }
        }
        public WindingConnection ConnectionKind
        {
            get { return connectionKind; }
            set { connectionKind = value; }
        }
        public float G
        {
            get { return g; }
            set { g = value; }
        }
        public float G0
        {
            get { return g0; }
            set { g0 = value; }
        }
        public long PhaseAngleClock
        {
            get { return phaseAngleClock; }
            set { phaseAngleClock = value; }
        }
        public float R
        {
            get { return r; }
            set { r = value; }
        }
        public float R0
        {
            get { return r0; }
            set { r0 = value; }
        }
        public float RatedS
        {
            get { return ratedS; }
            set { ratedS = value; }
        }
        public float RatedU
        {
            get { return ratedU; }
            set { ratedU = value; }
        }
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float X0
        {
            get { return x0; }
            set { x0 = value; }
        }
        public long PowerTransformer
        {
            get { return powerTransformer; }
            set { powerTransformer = value; }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PowerTransformerEnd x = (PowerTransformerEnd)obj;
                return (x.b == this.b && x.b0 == this.b0 &&
                        x.connectionKind == this.connectionKind &&
                        x.g == this.g && x.g0 == this.g0 &&
                        x.phaseAngleClock == this.phaseAngleClock &&
                        x.r == this.r && x.r0 == this.r0 &&
                        x.ratedS == this.ratedS &&
                        x.ratedU == this.ratedU &&
                        x.x == this.x && x.x0 == this.x0 &&
                        x.powerTransformer == this.powerTransformer
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
        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.POWERTREND_B:
                case ModelCode.POWERTREND_B0:
                case ModelCode.POWERTREND_CONNKIND:
                case ModelCode.POWERTREND_G:
                case ModelCode.POWERTREND_G0:
                case ModelCode.POWERTREND_PHANCLOCK:
                case ModelCode.POWERTREND_R:
                case ModelCode.POWERTREND_R0:
                case ModelCode.POWERTREND_RATEDS:
                case ModelCode.POWERTREND_RATEDU:
                case ModelCode.POWERTREND_X:
                case ModelCode.POWERTREND_X0:
                case ModelCode.POWERTREND_POWTRANSFORMER:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }
        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.POWERTREND_B:
                    prop.SetValue(b);
                    break;

                case ModelCode.POWERTREND_B0:
                    prop.SetValue(b0);
                    break;
                case ModelCode.POWERTREND_CONNKIND:
                    prop.SetValue((short)connectionKind);
                    break;
                case ModelCode.POWERTREND_G:
                    prop.SetValue(g);
                    break;
                case ModelCode.POWERTREND_G0:
                    prop.SetValue(g0);
                    break;
                case ModelCode.POWERTREND_PHANCLOCK:
                    prop.SetValue(phaseAngleClock);
                    break;
                case ModelCode.POWERTREND_R:
                    prop.SetValue(r);
                    break;
                case ModelCode.POWERTREND_R0:
                    prop.SetValue(r0);
                    break;
                case ModelCode.POWERTREND_RATEDS:
                    prop.SetValue(ratedS);
                    break;
                case ModelCode.POWERTREND_RATEDU:
                    prop.SetValue(ratedU);
                    break;
                case ModelCode.POWERTREND_X:
                    prop.SetValue(x);
                    break;
                case ModelCode.POWERTREND_X0:
                    prop.SetValue(x0);
                    break;
                case ModelCode.POWERTREND_POWTRANSFORMER:
                    prop.SetValue(powerTransformer);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.POWERTREND_B:
                    b = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_B0:
                    b0 = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_CONNKIND:
                    connectionKind = (WindingConnection)prop.AsEnum();
                    break;
                case ModelCode.POWERTREND_G:
                    g = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_G0:
                    g0 = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_PHANCLOCK:
                    phaseAngleClock = prop.AsLong();
                    break;
                case ModelCode.POWERTREND_R:
                    r = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_R0:
                    r0 = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_RATEDS:
                    ratedS = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_RATEDU:
                    ratedU = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_X:
                    x = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_X0:
                    x0 = prop.AsFloat();
                    break;
                case ModelCode.POWERTREND_POWTRANSFORMER:
                    powerTransformer = prop.AsReference();
                    break;
                default:
                    base.SetProperty(prop);
                    break;
            }
        }
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (powerTransformer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.POWERTREND_POWTRANSFORMER] = new List<long>();
                references[ModelCode.POWERTREND_POWTRANSFORMER].Add(powerTransformer);
            }

            base.GetReferences(references, refType);
        }
    }
}

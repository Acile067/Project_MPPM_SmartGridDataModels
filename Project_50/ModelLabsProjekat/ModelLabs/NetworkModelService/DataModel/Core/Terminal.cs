using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Wires;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private bool connected;
        private PhaseCode phases;
        private int sequenceNumber;
        private long conductingEquipment = 0;
        private List<long> regulatingControls = new List<long>();
        private List<long> transformerEnds = new List<long>();
        public Terminal(long globalId) : base(globalId)
        {
        }
        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
        public PhaseCode Phases
        {
            get { return phases; }
            set { phases = value; }
        }
        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }
        public long ConductingEquipment
        {
            get { return conductingEquipment; }
            set { conductingEquipment = value; }
        }
        public List<long> RegulatingControls
        {
            get { return regulatingControls; }
            set { regulatingControls = value; }
        }
        public List<long> TransformerEnds
        {
            get { return transformerEnds; }
            set { transformerEnds = value; }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return ((x.Connected == this.Connected) &&
                        (x.Phases == this.Phases) &&
                        (x.SequenceNumber == this.SequenceNumber) &&
                        (x.ConductingEquipment == this.ConductingEquipment) &&
                        CompareHelper.CompareLists(x.RegulatingControls, this.RegulatingControls) &&
                        CompareHelper.CompareLists(x.TransformerEnds, this.TransformerEnds));
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
                case ModelCode.TERMINAL_CONNECTED:
                case ModelCode.TERMINAL_PHASES:
                case ModelCode.TERMINAL_SEQNUM:
                case ModelCode.TERMINAL_CONDEQUIPMENT:
                case ModelCode.TERMINAL_REGCONTROLS:
                case ModelCode.TERMINAL_TRANSENDS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CONNECTED:
                    property.SetValue(connected);
                    break;
                case ModelCode.TERMINAL_PHASES:
                    property.SetValue((short)phases);
                    break;
                case ModelCode.TERMINAL_SEQNUM:
                    property.SetValue(sequenceNumber);
                    break;
                case ModelCode.TERMINAL_CONDEQUIPMENT:
                    property.SetValue(conductingEquipment);
                    break;
                case ModelCode.TERMINAL_REGCONTROLS:
                    property.SetValue(regulatingControls);
                    break;
                case ModelCode.TERMINAL_TRANSENDS:
                    property.SetValue(transformerEnds);
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
                case ModelCode.TERMINAL_CONNECTED:
                    connected = property.AsBool();
                    break;
                case ModelCode.TERMINAL_PHASES:
                    phases = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.TERMINAL_SEQNUM:
                    sequenceNumber = property.AsInt();
                    break;
                case ModelCode.TERMINAL_CONDEQUIPMENT:
                    conductingEquipment = property.AsReference();
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
                return (transformerEnds.Count > 0) || (regulatingControls.Count > 0) || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (conductingEquipment != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONDEQUIPMENT] = new List<long>();
                references[ModelCode.TERMINAL_CONDEQUIPMENT].Add(conductingEquipment);
            }

            if (regulatingControls != null && regulatingControls.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_REGCONTROLS] = regulatingControls.GetRange(0, regulatingControls.Count);
            }

            if (transformerEnds != null && transformerEnds.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_TRANSENDS] = transformerEnds.GetRange(0, transformerEnds.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGCONTROL_TERMINAL:
                    regulatingControls.Add(globalId);
                    break;
                case ModelCode.TRANSEND_TERMINAL:
                    transformerEnds.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TRANSEND_TERMINAL:

                    if (transformerEnds.Contains(globalId))
                    {
                        transformerEnds.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                case ModelCode.REGCONTROL_TERMINAL:

                    if (regulatingControls.Contains(globalId))
                    {
                        regulatingControls.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }
    }
}

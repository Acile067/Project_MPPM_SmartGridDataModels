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
    public class TransformerEnd : IdentifiedObject
    {
        private long terminal = 0;
        public TransformerEnd(long globalId)
            : base(globalId)
        {
        }
        public long Terminal
        {
            get
            {
                return terminal;
            }
            set
            {
                terminal = value;
            }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TransformerEnd x = (TransformerEnd)obj;
                return (x.Terminal == this.Terminal);
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
                case ModelCode.TRANSEND_TERMINAL:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TRANSEND_TERMINAL:
                    prop.SetValue(terminal);
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
                case ModelCode.TRANSEND_TERMINAL:
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
                references[ModelCode.TRANSEND_TERMINAL] = new List<long>();
                references[ModelCode.TRANSEND_TERMINAL].Add(terminal);
            }

            base.GetReferences(references, refType);
        }
    }
}

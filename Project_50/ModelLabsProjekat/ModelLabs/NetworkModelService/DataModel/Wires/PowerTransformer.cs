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
	public class PowerTransformer : ConductingEquipment
	{
		private string vectorGroup = string.Empty;

		private List<long> powerTransformerEnds = new List<long>();		

		public PowerTransformer(long globalId)
			: base(globalId)
		{
		}

		public string VectorGroup
        {
			get { return vectorGroup; }
			set { vectorGroup = value;}
		}

		public List<long> PowerTransformerEnds
        {
			get { return powerTransformerEnds; }
			set { powerTransformerEnds = value;}
		}
			

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				PowerTransformer x = (PowerTransformer)obj;
				return (x.vectorGroup == this.vectorGroup &&
						CompareHelper.CompareLists(x.powerTransformerEnds, this.powerTransformerEnds, true));
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

		#region IAccess implementation

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{				
				case ModelCode.POWERTR_VECGROUP:
				case ModelCode.POWERTR_POWERTRENDS:				
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{			
				case ModelCode.POWERTR_VECGROUP:
					prop.SetValue(vectorGroup);
					break;

                case ModelCode.POWERTR_POWERTRENDS:
                    prop.SetValue(powerTransformerEnds);
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
				case ModelCode.POWERTR_VECGROUP:
                    vectorGroup = property.AsString();
					break;
			
				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation



		#region IReference implementation
		
		public override bool IsReferenced
		{
			get
			{
				return (powerTransformerEnds.Count > 0) || base.IsReferenced;
			}
		}
	
		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (powerTransformerEnds != null && powerTransformerEnds.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.POWERTR_POWERTRENDS] = powerTransformerEnds.GetRange(0, powerTransformerEnds.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.POWERTREND_POWTRANSFORMER:
                    powerTransformerEnds.Add(globalId);
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
				case ModelCode.POWERTREND_POWTRANSFORMER:

					if (powerTransformerEnds.Contains(globalId))
					{
                        powerTransformerEnds.Remove(globalId);
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
	
		#endregion IReference implementation
	}
}

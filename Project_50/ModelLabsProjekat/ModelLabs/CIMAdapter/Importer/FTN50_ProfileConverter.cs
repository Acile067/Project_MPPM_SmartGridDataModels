using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    public static class FTN50_ProfileConverter
    {
        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
        {
            if ((cimIdentifiedObject != null) && (rd != null))
            {
                if (cimIdentifiedObject.MRIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
                }
                if (cimIdentifiedObject.NameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
                }
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
                }
            }
        }
        //Terminal
        public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTerminal != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

                if (cimTerminal.ConnectedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTED, cimTerminal.Connected));
                }
                if (cimTerminal.PhasesHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_PHASES, (short)GetDMSPhaseCode(cimTerminal.Phases)));
                }
                if (cimTerminal.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_SEQNUM, cimTerminal.SequenceNumber));
                }
                if (cimTerminal.ConductingEquipmentHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to ConductingEquipment: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONDEQUIPMENT, gid));
                }
            }
        }
        //
        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
            }
        }

        //TapChanger
        public static void PopulateTapChangerProperties(FTN.TapChanger cimTapChanger, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTapChanger != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulatePowerSystemResourceProperties(cimTapChanger, rd);

                if (cimTapChanger.HighStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_HIGHSTEP, cimTapChanger.HighStep));
                }
                if (cimTapChanger.InitialDelayHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_INITDELAY, cimTapChanger.InitialDelay));
                }
                if (cimTapChanger.LowStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_LOWSTEP, cimTapChanger.LowStep));
                }
                if (cimTapChanger.LtcFlagHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_LTCFLAG, cimTapChanger.LtcFlag));
                }
                if (cimTapChanger.NeutralStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_NTRSTEP, cimTapChanger.NeutralStep));
                }
                if (cimTapChanger.NeutralUHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_NTRU, cimTapChanger.NeutralU));
                }
                if (cimTapChanger.NormalStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_NORSTEP, cimTapChanger.NormalStep));
                }
                if (cimTapChanger.RegulationStatusHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_REGSTATUS, cimTapChanger.RegulationStatus));
                }
                if (cimTapChanger.SubsequentDelayHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_SUBSDELAY, cimTapChanger.SubsequentDelay));
                }
                if (cimTapChanger.TapChangerControlHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTapChanger.TapChangerControl.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTapChanger.GetType().ToString()).Append(" rdfID = \"").Append(cimTapChanger.ID);
                        report.Report.Append("\" - Failed to set reference to TapChangerControl: rdfID \"").Append(cimTapChanger.TapChangerControl.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_TCC, gid));
                }
            }
        }
        //
        public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegulatingControl != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulatePowerSystemResourceProperties(cimRegulatingControl, rd);

                if (cimRegulatingControl.DiscreteHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_DIS, cimRegulatingControl.Discrete));
                }
                if (cimRegulatingControl.ModeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_MODE, (short)GetDMSRegulatingControlModeKind(cimRegulatingControl.Mode)));
                }
                if (cimRegulatingControl.MonitoredPhaseHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_MONIPHASE, (short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase)));
                }
                if (cimRegulatingControl.TargetRangeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_TARGRANGE, cimRegulatingControl.TargetRange));
                }
                if (cimRegulatingControl.TargetValueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_TARGVALUE, cimRegulatingControl.TargetValue));
                }
                if (cimRegulatingControl.TerminalHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegulatingControl.Terminal.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegulatingControl.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulatingControl.ID);
                        report.Report.Append("\" - Failed to set reference to Terminal: rdfID \"").Append(cimRegulatingControl.Terminal.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_TERMINAL, gid));
                }
            }
        }
        //TapChangerControl
        public static void PopulateTapChangerControlProperties(FTN.TapChangerControl cimTapChangerControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTapChangerControl != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulateRegulatingControlProperties(cimTapChangerControl, rd, importHelper, report);

                if (cimTapChangerControl.LimitVoltageHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGCONTROL_LIMVOLT, cimTapChangerControl.LimitVoltage));
                }
                if (cimTapChangerControl.LineDropCompensationHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGCONTROL_LDC, cimTapChangerControl.LineDropCompensation));
                }
                if (cimTapChangerControl.LineDropRHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGCONTROL_LDR, cimTapChangerControl.LineDropR));
                }
                if (cimTapChangerControl.LineDropXHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGCONTROL_LDX, cimTapChangerControl.LineDropX));
                }
                if (cimTapChangerControl.ReverseLineDropRHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGCONTROL_RLDR, cimTapChangerControl.ReverseLineDropR));
                }
                if (cimTapChangerControl.ReverseLineDropXHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGCONTROL_RLDX, cimTapChangerControl.ReverseLineDropX));
                }
            }
        }
        //

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

                if (cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALSERVICE, cimEquipment.NormallyInService));
                }
            }
        }

        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
        {
            if ((cimConductingEquipment != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
            }
        }
        //PowerTransformer
        public static void PopulatePowerTransformerProperties(FTN.PowerTransformer cimPowerTransformer, ResourceDescription rd)
        {
            if ((cimPowerTransformer != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulateConductingEquipmentProperties(cimPowerTransformer, rd);

                if (cimPowerTransformer.VectorGroupHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTR_VECGROUP, cimPowerTransformer.VectorGroup));
                }
            }
        }
        //
        public static void PopulateTransformerEndProperties(FTN.TransformerEnd cimTransformerEnd, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTransformerEnd != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulateIdentifiedObjectProperties(cimTransformerEnd, rd);

                if (cimTransformerEnd.TerminalHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTransformerEnd.Terminal.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTransformerEnd.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerEnd.ID);
                        report.Report.Append("\" - Failed to set reference to Terminal: rdfID \"").Append(cimTransformerEnd.Terminal.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TRANSEND_TERMINAL, gid));
                }
            }
        }
        //PowerTransformerEnd
        public static void PopulatePowerTransformerEndProperties(FTN.PowerTransformerEnd cimPowerTransformerEnd, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPowerTransformerEnd != null) && (rd != null))
            {
                FTN50_ProfileConverter.PopulateTransformerEndProperties(cimPowerTransformerEnd, rd, importHelper, report);

                if (cimPowerTransformerEnd.BHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_B, cimPowerTransformerEnd.B));
                }
                if (cimPowerTransformerEnd.B0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_B0, cimPowerTransformerEnd.B0));
                }
                if (cimPowerTransformerEnd.ConnectionKindHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_CONNKIND, (short)GetDMSWindingConnection(cimPowerTransformerEnd.ConnectionKind)));
                }
                if (cimPowerTransformerEnd.GHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_G, cimPowerTransformerEnd.G));
                }
                if (cimPowerTransformerEnd.G0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_G0, cimPowerTransformerEnd.G0));
                }
                if (cimPowerTransformerEnd.PhaseAngleClockHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_PHANCLOCK, cimPowerTransformerEnd.PhaseAngleClock));
                }
                if (cimPowerTransformerEnd.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_R, cimPowerTransformerEnd.R));
                }
                if (cimPowerTransformerEnd.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_R0, cimPowerTransformerEnd.R0));
                }
                if (cimPowerTransformerEnd.RatedSHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_RATEDS, cimPowerTransformerEnd.RatedS));
                }
                if (cimPowerTransformerEnd.RatedUHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_RATEDU, cimPowerTransformerEnd.RatedU));
                }
                if (cimPowerTransformerEnd.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_X, cimPowerTransformerEnd.X));
                }
                if (cimPowerTransformerEnd.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTREND_X0, cimPowerTransformerEnd.X0));
                }
                if (cimPowerTransformerEnd.PowerTransformerHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimPowerTransformerEnd.PowerTransformer.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimPowerTransformerEnd.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerTransformerEnd.ID);
                        report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimPowerTransformerEnd.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.POWERTREND_POWTRANSFORMER, gid));
                }
            }
        }

        public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
        {
            switch (phases)
            {
                case FTN.PhaseCode.A:
                    return PhaseCode.A;
                case FTN.PhaseCode.AB:
                    return PhaseCode.AB;
                case FTN.PhaseCode.ABC:
                    return PhaseCode.ABC;
                case FTN.PhaseCode.ABCN:
                    return PhaseCode.ABCN;
                case FTN.PhaseCode.ABN:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.AC:
                    return PhaseCode.AC;
                case FTN.PhaseCode.ACN:
                    return PhaseCode.ACN;
                case FTN.PhaseCode.AN:
                    return PhaseCode.AN;
                case FTN.PhaseCode.B:
                    return PhaseCode.B;
                case FTN.PhaseCode.BC:
                    return PhaseCode.BC;
                case FTN.PhaseCode.BCN:
                    return PhaseCode.BCN;
                case FTN.PhaseCode.BN:
                    return PhaseCode.BN;
                case FTN.PhaseCode.C:
                    return PhaseCode.C;
                case FTN.PhaseCode.CN:
                    return PhaseCode.CN;
                case FTN.PhaseCode.N:
                    return PhaseCode.N;
                case FTN.PhaseCode.s12N:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.s1N:
                    return PhaseCode.AN;
                case FTN.PhaseCode.s2N:
                    return PhaseCode.BN;
                default: return PhaseCode.A;//Default should never happen, but just in case. I dont know why Unknown is not comming 
            }
        }
        public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
        {
            switch (windingConnection)
            {
                case FTN.WindingConnection.D:
                    return WindingConnection.D;
                case FTN.WindingConnection.I:
                    return WindingConnection.I;
                case FTN.WindingConnection.Z:
                    return WindingConnection.Z;
                case FTN.WindingConnection.Y:
                    return WindingConnection.Y;
                default:
                    return WindingConnection.Y;
            }
        }
        public static RegulatingControlModeKind GetDMSRegulatingControlModeKind(FTN.RegulatingControlModeKind RegulatingControlModeKind)
        {
            switch (RegulatingControlModeKind)
            {
                case FTN.RegulatingControlModeKind.activePower:
                    return RegulatingControlModeKind.activePower;
                case FTN.RegulatingControlModeKind.admittance:
                    return RegulatingControlModeKind.admittance;
                case FTN.RegulatingControlModeKind.currentFlow:
                    return RegulatingControlModeKind.currentFlow;
                case FTN.RegulatingControlModeKind.@fixed:
                    return RegulatingControlModeKind.@fixed;
                case FTN.RegulatingControlModeKind.powerFactor:
                    return RegulatingControlModeKind.powerFactor;
                case FTN.RegulatingControlModeKind.reactivePower:
                    return RegulatingControlModeKind.reactivePower;
                case FTN.RegulatingControlModeKind.temperature:
                    return RegulatingControlModeKind.temperature;
                case FTN.RegulatingControlModeKind.timeScheduled:
                    return RegulatingControlModeKind.timeScheduled;
                case FTN.RegulatingControlModeKind.voltage:
                    return RegulatingControlModeKind.voltage;
                default:
                    return RegulatingControlModeKind.activePower;
            }
        }
    }
}

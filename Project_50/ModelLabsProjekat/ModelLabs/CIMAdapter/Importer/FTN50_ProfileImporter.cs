using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    public class FTN50_ProfileImporter
    {
        private static FTN50_ProfileImporter ptImporter = null;
        private static object singletoneLock = new object();

        private ConcreteModel concreteModel;
        private Delta delta;
        private ImportHelper importHelper;
        private TransformAndLoadReport report;

        public static FTN50_ProfileImporter Instance
        {
            get
            {
                if (ptImporter == null)
                {
                    lock (singletoneLock)
                    {
                        if (ptImporter == null)
                        {
                            ptImporter = new FTN50_ProfileImporter();
                            ptImporter.Reset();
                        }
                    }
                }
                return ptImporter;
            }
        }
        public Delta NMSDelta
        {
            get
            {
                return delta;
            }
        }

        public void Reset()
        {
            concreteModel = null;
            delta = new Delta();
            importHelper = new ImportHelper();
            report = null;
        }

        public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
        {
            LogManager.Log("Importing FTN50_Profile Elements...", LogLevel.Info);
            report = new TransformAndLoadReport();
            concreteModel = cimConcreteModel;
            delta.ClearDeltaOperations();

            if ((concreteModel != null) && (concreteModel.ModelMap != null))
            {
                try
                {
                    // convert into DMS elements
                    ConvertModelAndPopulateDelta();
                }
                catch (Exception ex)
                {
                    string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
                    LogManager.Log(message);
                    report.Report.AppendLine(ex.Message);
                    report.Success = false;
                }
            }
            LogManager.Log("Importing FTN50_Profile Elements - END.", LogLevel.Info);
            return report;
        }

        private void ConvertModelAndPopulateDelta()
        {
            LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)
            ImportPowerTransformers();
            ImportTerminals();
            ImportTapChangerControls();
            ImportTapChangers();
            ImportPowerTransformerEnds();


            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
        }
        private void ImportPowerTransformers()
        {
            SortedDictionary<string, object> cimPowerTransformers = concreteModel.GetAllObjectsOfType("FTN.PowerTransformer");
            if (cimPowerTransformers != null)
            {
                foreach (KeyValuePair<string, object> cimPowerTransformerPair in cimPowerTransformers)
                {

                    var obj = cimPowerTransformerPair.Value;
                    Console.WriteLine("Runtime type: " + obj.GetType().FullName);
                    Console.WriteLine("Runtime assembly: " + obj.GetType().Assembly.FullName);

                    Console.WriteLine("Expected type: " + typeof(FTN.PowerTransformer).FullName);
                    Console.WriteLine("Expected assembly: " + typeof(FTN.PowerTransformer).Assembly.FullName);

                    FTN.PowerTransformer cimPowerTransformer = cimPowerTransformerPair.Value as FTN.PowerTransformer;

                    ResourceDescription rd = CreatePowerTransformerDescription(cimPowerTransformer);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("PowerTransformer ID = ").Append(cimPowerTransformer.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("PowerTransformer ID = ").Append(cimPowerTransformer.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreatePowerTransformerDescription(FTN.PowerTransformer cimPowerTransformer)
        {
            ResourceDescription rd = null;
            if (cimPowerTransformer != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTR, importHelper.CheckOutIndexForDMSType(DMSType.POWERTR));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimPowerTransformer.ID, gid);

                ////populate ResourceDescription
                FTN50_ProfileConverter.PopulatePowerTransformerProperties(cimPowerTransformer, rd);
            }
            return rd;
        }

        private void ImportTerminals()
        {
            SortedDictionary<string, object> cimTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");
            if (cimTerminals != null)
            {
                foreach (KeyValuePair<string, object> cimTerminalsPair in cimTerminals)
                {
                    FTN.Terminal cimTerminal = cimTerminalsPair.Value as FTN.Terminal;

                    ResourceDescription rd = CreateTerminalResourceDescription(cimTerminal);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateTerminalResourceDescription(FTN.Terminal cimTerminal)
        {
            ResourceDescription rd = null;
            if (cimTerminal != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTerminal.ID, gid);

                ////populate ResourceDescription
                FTN50_ProfileConverter.PopulateTerminalProperties(cimTerminal, rd, importHelper, report);
            }
            return rd;
        }
        private void ImportTapChangerControls()
        {
            SortedDictionary<string, object> cimTapChangerControls = concreteModel.GetAllObjectsOfType("FTN.TapChangerControl");
            if (cimTapChangerControls != null)
            {
                foreach (KeyValuePair<string, object> cimTapChangerControlPair in cimTapChangerControls)
                {
                    FTN.TapChangerControl cimTapChangerControl = cimTapChangerControlPair.Value as FTN.TapChangerControl;

                    ResourceDescription rd = CreateTapChangerControlResourceDescription(cimTapChangerControl);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("TapChangerControl ID = ").Append(cimTapChangerControl.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("TapChangerControl ID = ").Append(cimTapChangerControl.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        private ResourceDescription CreateTapChangerControlResourceDescription(FTN.TapChangerControl cimTapChangerControl)
        {
            ResourceDescription rd = null;
            if (cimTapChangerControl != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TAPCHANGCONTROL, importHelper.CheckOutIndexForDMSType(DMSType.TAPCHANGCONTROL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTapChangerControl.ID, gid);

                ////populate ResourceDescription
                FTN50_ProfileConverter.PopulateTapChangerControlProperties(cimTapChangerControl, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportTapChangers()
        {
            SortedDictionary<string, object> cimTapChangers = concreteModel.GetAllObjectsOfType("FTN.TapChanger");
            if (cimTapChangers != null)
            {
                foreach (KeyValuePair<string, object> cimTapChangerPair in cimTapChangers)
                {
                    FTN.TapChanger cimTapChanger = cimTapChangerPair.Value as FTN.TapChanger;

                    ResourceDescription rd = CreateTapChangerResourceDescription(cimTapChanger);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("TapChanger ID = ").Append(cimTapChanger.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("TapChanger ID = ").Append(cimTapChanger.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateTapChangerResourceDescription(FTN.TapChanger cimTapChanger)
        {
            ResourceDescription rd = null;
            if (cimTapChanger != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TAPCHANGER, importHelper.CheckOutIndexForDMSType(DMSType.TAPCHANGER));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTapChanger.ID, gid);

                ////populate ResourceDescription
                FTN50_ProfileConverter.PopulateTapChangerProperties(cimTapChanger, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportPowerTransformerEnds()
        {
            SortedDictionary<string, object> cimPowerTransformerEnds = concreteModel.GetAllObjectsOfType("FTN.PowerTransformerEnd");
            if (cimPowerTransformerEnds != null)
            {
                foreach (KeyValuePair<string, object> cimPowerTransformerEndPair in cimPowerTransformerEnds)
                {
                    FTN.PowerTransformerEnd cimPowerTransformerEnd = cimPowerTransformerEndPair.Value as FTN.PowerTransformerEnd;

                    ResourceDescription rd = CreateTapPowerTransformerEndDescription(cimPowerTransformerEnd);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("PowerTransformerEnd ID = ").Append(cimPowerTransformerEnd.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("PowerTransformerEnd ID = ").Append(cimPowerTransformerEnd.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        private ResourceDescription CreateTapPowerTransformerEndDescription(FTN.PowerTransformerEnd cimPowerTransformerEnd)
        {
            ResourceDescription rd = null;
            if (cimPowerTransformerEnd != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTREND, importHelper.CheckOutIndexForDMSType(DMSType.POWERTREND));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimPowerTransformerEnd.ID, gid);

                ////populate ResourceDescription
                FTN50_ProfileConverter.PopulatePowerTransformerEndProperties(cimPowerTransformerEnd, rd, importHelper, report);
            }
            return rd;
        }
    }
}

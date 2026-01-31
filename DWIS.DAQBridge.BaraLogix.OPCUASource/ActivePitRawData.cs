using DWIS.API.DTO;
using DWIS.RigOS.Common.Worker;
using DWIS.Vocabulary.Schemas;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using OSDC.UnitConversion.Conversion;
using OSDC.UnitConversion.Conversion.DrillingEngineering;
using System.Reflection;

namespace DWIS.DAQBridge.BaraLogix.OPCUASource
{
    public class ActivePitRawData : DWISDataWithOPCUA
    {
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, OPCUANode>> LocalOPCUANodes = new(BuildOPCUANodes(typeof(ActivePitRawData)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> LocalSparQLQueries = new(BuildSparQLQueries(typeof(ActivePitRawData)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> LocalManifests = new(BuildManifests(typeof(ActivePitRawData), "BaraLogixDataManifest", "Halliburton", "DWISBridge"));
        public override Lazy<IReadOnlyDictionary<PropertyInfo, OPCUANode>> OPCUANodes { get => LocalOPCUANodes; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> SparQLQueries { get => LocalSparQLQueries; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> Manifests { get => LocalManifests; }

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "MeasuredTemperature")]
        public ScalarProperty? MeasuredTemperature { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "MeasuredPressure")]
        public ScalarProperty? MeasuredPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "MassDensityFromCoriolisAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? MassDensityFromCoriolisAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "ShearStressAt3RPMAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? ShearStressAt3RPMAtMeasuredTemperatureAndPressure { get; set; } = null;
      
        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "ShearStressAt6RPMAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? ShearStressAt6RPMAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "ShearStressAt100RPMAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? ShearStressAt100RPMAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "ShearStressAt200RPMAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? ShearStressAt200RPMAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "ShearStressAt300RPMAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? ShearStressAt300RPMAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "ShearStressAt600RPMAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? ShearStressAt600RPMAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "PlasticViscosityAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? PlasticViscosityAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "YieldPointAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? YieldPointAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "YPLYieldStressAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? YPLYieldStressAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "YPLConsistencyIndexAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? YPLConsistencyIndexAtMeasuredTemperatureAndPressure { get; set; } = null;

        [OPCUANode("http://ddhub.no/BaraLogixDataManifest/Variables/", "BaraLogixDataManifest.", "YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure")]
        public ScalarProperty? YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure { get; set; } = null;

    }
}

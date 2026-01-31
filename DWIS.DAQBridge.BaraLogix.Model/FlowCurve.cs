using DWIS.RigOS.Common.Model;
using DWIS.RigOS.Common.Worker;
using DWIS.Vocabulary.Schemas;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using OSDC.UnitConversion.Conversion;
using OSDC.UnitConversion.Conversion.DrillingEngineering;

namespace DWIS.DAQBridge.BaraLogix.Model
{
    public class FlowCurveMeasurement
    {
        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("ConcentricCylindersRotationalSpeed")]
        [SemanticFact("ConcentricCylindersRotationalSpeed", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.SetPoint)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Verbs.Enum.HasDynamicValue, "ConcentricCylindersRotationalSpeed")]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Verbs.Enum.IsOfMeasurableQuantity, DrillingPhysicalQuantity.QuantityEnum.AngularVelocityDrilling)]
        [SemanticFact("ConcentricCylindersMeasurementPrinciple#01", Nouns.Enum.ConcentricCylindersMeasurementPrinciple)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Verbs.Enum.ControlsMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        public ScalarProperty? ConcentricCylindersRotationalSpeed { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("ConcentricCylindersShearRateNewtonianHypothesis")]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis#01", Nouns.Enum.ComputedData)]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis#01", Verbs.Enum.HasDynamicValue, "ConcentricCylindersShearRateNewtonianHypothesis")]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.FluidShearRate)]
        [SemanticFact("GaussianUncertainty#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis#01", Verbs.Enum.HasUncertainty, "GaussianUncertainty#01")]
        [SemanticFact("GaussianUncertainty#01", Verbs.Enum.HasUncertaintyMean, "ConcentricCylindersShearRateNewtonianHypothesis#01")]
        [SemanticFact("ConcentricCylindersMeasurementPrinciple#01", Nouns.Enum.ConcentricCylindersMeasurementPrinciple)]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis#01", Verbs.Enum.UsesMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.SetPoint)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Verbs.Enum.ControlsMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis#01", Verbs.Enum.IsDependentOn, "ConcentricCylindersRotationalSpeed#01")]
        [SemanticFact("NewtonianFluidModel#01", Nouns.Enum.NewtonianFluid)]
        [SemanticFact("ConcentricCylindersShearRateNewtonianHypothesis#01", Verbs.Enum.IsCalculatedUsingRheologicalHypothesis, "NewtonianFluidModel#01")]
        public ScalarProperty? ConcentricCylindersShearRateNewtonianHypothesis { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("ConcentricCylindersShearRateNonNewtonianHypothesis")]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis#01", Nouns.Enum.ComputedData)]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis#01", Verbs.Enum.HasDynamicValue, "ConcentricCylindersShearRateNonNewtonianHypothesis")]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.FluidShearRate)]
        [SemanticFact("GaussianUncertainty#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis#01", Verbs.Enum.HasUncertainty, "GaussianUncertainty#01")]
        [SemanticFact("GaussianUncertainty#01", Verbs.Enum.HasUncertaintyMean, "ConcentricCylindersShearRateNonNewtonianHypothesis#01")]
        [SemanticFact("ConcentricCylindersMeasurementPrinciple#01", Nouns.Enum.ConcentricCylindersMeasurementPrinciple)]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis#01", Verbs.Enum.UsesMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.SetPoint)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Verbs.Enum.ControlsMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis#01", Verbs.Enum.IsDependentOn, "ConcentricCylindersRotationalSpeed#01")]
        [SemanticFact("NonNewtonianFluidModel#01", Nouns.Enum.NonNewtonianFluid)]
        [SemanticFact("ConcentricCylindersShearRateNonNewtonianHypothesis#01", Verbs.Enum.IsCalculatedUsingRheologicalHypothesis, "NonNewtonianFluidModel#01")]
        public ScalarProperty? ConcentricCylindersShearRateNonNewtonianHypothesis { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("ConcentricCylindersTorque")]
        [SemanticFact("ConcentricCylindersTorque", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("ConcentricCylindersTorque#01", Nouns.Enum.Measurement)]
        [SemanticFact("ConcentricCylindersTorque#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersTorque#01", Verbs.Enum.HasDynamicValue, "ConcentricCylindersTorque")]
        [SemanticFact("ConcentricCylindersTorque#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.TorqueSmall)]
        [SemanticFact("GaussianUncertainty#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("ConcentricCylindersTorque#01", Verbs.Enum.HasUncertainty, "GaussianUncertainty#01")]
        [SemanticFact("GaussianUncertainty#01", Verbs.Enum.HasUncertaintyMean, "ConcentricCylindersTorque#01")]
        [SemanticFact("ConcentricCylindersMeasurementPrinciple#01", Nouns.Enum.ConcentricCylindersMeasurementPrinciple)]
        [SemanticFact("ConcentricCylindersTorque#01", Verbs.Enum.UsesMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.SetPoint)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Verbs.Enum.ControlsMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersTorque#01", Verbs.Enum.IsDependentOn, "ConcentricCylindersRotationalSpeed#01")]
        public ScalarProperty? ConcentricCylindersTorque { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("ConcentricCylindersShearStressNewtonianHypothesis")]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis#01", Nouns.Enum.ComputedData)]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis#01", Verbs.Enum.HasDynamicValue, "ConcentricCylindersShearStressNewtonianHypothesis")]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.FluidShearStress)]
        [SemanticFact("GaussianUncertainty#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis#01", Verbs.Enum.HasUncertainty, "GaussianUncertainty#01")]
        [SemanticFact("GaussianUncertainty#01", Verbs.Enum.HasUncertaintyMean, "ConcentricCylindersShearStressNewtonianHypothesis#01")]
        [SemanticFact("ConcentricCylindersMeasurementPrinciple#01", Nouns.Enum.ConcentricCylindersMeasurementPrinciple)]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis#01", Verbs.Enum.UsesMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.SetPoint)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Verbs.Enum.ControlsMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis#01", Verbs.Enum.IsDependentOn, "ConcentricCylindersRotationalSpeed#01")]
        [SemanticFact("NewtonianFluidModel#01", Nouns.Enum.NewtonianFluid)]
        [SemanticFact("ConcentricCylindersShearStressNewtonianHypothesis#01", Verbs.Enum.IsCalculatedUsingRheologicalHypothesis, "NewtonianFluidModel#01")]
        public ScalarProperty? ConcentricCylindersShearStressNewtonianHypothesis { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("ConcentricCylindersShearStressNonNewtonianHypothesis")]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis#01", Nouns.Enum.ComputedData)]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis#01", Verbs.Enum.HasDynamicValue, "ConcentricCylindersShearStressNonNewtonianHypothesis")]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.FluidShearStress)]
        [SemanticFact("GaussianUncertainty#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis#01", Verbs.Enum.HasUncertainty, "GaussianUncertainty#01")]
        [SemanticFact("GaussianUncertainty#01", Verbs.Enum.HasUncertaintyMean, "ConcentricCylindersShearStressNonNewtonianHypothesis#01")]
        [SemanticFact("ConcentricCylindersMeasurementPrinciple#01", Nouns.Enum.ConcentricCylindersMeasurementPrinciple)]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis#01", Verbs.Enum.UsesMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.SetPoint)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("ConcentricCylindersRotationalSpeed#01", Verbs.Enum.ControlsMeasurementPrinciple, "ConcentricCylindersMeasurementPrinciple#01")]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis#01", Verbs.Enum.IsDependentOn, "ConcentricCylindersRotationalSpeed#01")]
        [SemanticFact("NonNewtonianFluidModel#01", Nouns.Enum.NonNewtonianFluid)]
        [SemanticFact("ConcentricCylindersShearStressNonNewtonianHypothesis#01", Verbs.Enum.IsCalculatedUsingRheologicalHypothesis, "NonNewtonianFluidModel#01")]
        public ScalarProperty? ConcentricCylindersShearStressNonNewtonianHypothesis { get; set; } = null;

        public FlowCurveMeasurement() : base()
        {
            ConcentricCylindersRotationalSpeed = new ScalarProperty();
            ConcentricCylindersShearRateNewtonianHypothesis = new ScalarProperty();
            ConcentricCylindersShearRateNonNewtonianHypothesis = new ScalarProperty();
            ConcentricCylindersTorque = new ScalarProperty();
            ConcentricCylindersShearStressNewtonianHypothesis = new ScalarProperty();
            ConcentricCylindersShearStressNonNewtonianHypothesis = new ScalarProperty();
        }
    }

    public class FlowCurve : SemanticInfo
    {
        public List<FlowCurveMeasurement>? Measurements { get; set; } = null;
    }
}

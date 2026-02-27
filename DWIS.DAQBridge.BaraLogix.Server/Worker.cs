using DWIS.Client.ReferenceImplementation.OPCFoundation;
using DWIS.RigOS.Common.Worker;
using OSDC.DotnetLibraries.General.Common;
using System.Reflection;
using YPLCalibrationFromRheometer.Model;
using DWIS.DAQBridge.BaraLogix.Model;

namespace DWIS.DAQBridge.BaraLogix.Server
{
    public class Worker : DWISWorkerWithOPCUA<ConfigurationForBaraLogics>
    {
        private ActivePitRawData ActivePitRawData { get; set; } = new ActivePitRawData();
        private ActivePitOutputData ActivePitOutputData { get; set; } = new ActivePitOutputData();

        private TimeSpan OPCUALoopSpan { get; set; } = TimeSpan.FromSeconds(1);

        public Worker(ILogger<IDWISWorker<ConfigurationForBaraLogics>> logger, ILogger<DWISClientOPCF>? loggerDWISClient) : base(logger, loggerDWISClient)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectToOPCUA();
            ConnectToBlackboard();
            if (Configuration is not null && _DWISClient != null && _DWISClient.Connected)
            {
                OPCUALoopSpan = Configuration.OPCUALoopDuration;
                if (Configuration.InitializeInputOPCUAVariables)
                {
                    await RegisterToOPCUA(ActivePitRawData, "BaraLogixDataManifest", "Halliburton");
                }
                await RegisterToBlackboard(ActivePitOutputData);
                await Loop(stoppingToken);
            }
        }

        protected override async Task Loop(CancellationToken stoppingToken)
        {
            PeriodicTimer timer = new PeriodicTimer(OPCUALoopSpan);
            double opcDuration = OPCUALoopSpan.TotalSeconds;
            double dwisDuration = LoopSpan.TotalSeconds;
            int count = 1;
            if (!Numeric.EQ(opcDuration, 0))
            {
                count = (int)(dwisDuration / opcDuration);
            }
            if (count <= 0)
            {
                count = 1;
            }
            int k = 0;
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    // process series
                    await ReadOPCUA(ActivePitRawData, Configuration?.NameSpace, Configuration?.NodeIDPrefix, Configuration?.OPCUAIDs, Configuration?.UnitConversions);
                    k++;
                    if (k == count)
                    {
                        DateTime d1 = DateTime.UtcNow;
                        if (ActivePitOutputData.MeasuredPressure is not null && ActivePitRawData.MeasuredPressure != null)
                        {
                            ActivePitOutputData.MeasuredPressure.Value = ActivePitRawData.MeasuredPressure.Value;
                        }
                        if (ActivePitOutputData.MeasuredTemperature is not null && ActivePitRawData.MeasuredTemperature != null)
                        {
                            ActivePitOutputData.MeasuredTemperature.Value = ActivePitRawData.MeasuredTemperature.Value;
                        }
                        if (ActivePitOutputData.MassDensityFromCoriolisAtMeasuredTemperatureAndPressure is not null && ActivePitRawData.MassDensityFromCoriolisAtMeasuredTemperatureAndPressure != null)
                        {
                            ActivePitOutputData.MassDensityFromCoriolisAtMeasuredTemperatureAndPressure.Value = ActivePitRawData.MassDensityFromCoriolisAtMeasuredTemperatureAndPressure.Value;
                        }
                        if (ActivePitOutputData.PlasticViscosityAtMeasuredTemperatureAndPressure is not null && ActivePitRawData.PlasticViscosityAtMeasuredTemperatureAndPressure != null)
                        {
                            ActivePitOutputData.PlasticViscosityAtMeasuredTemperatureAndPressure.Value = ActivePitRawData.PlasticViscosityAtMeasuredTemperatureAndPressure.Value;
                        }
                        if (ActivePitOutputData.YieldPointAtMeasuredTemperatureAndPressure is not null && ActivePitRawData.YieldPointAtMeasuredTemperatureAndPressure != null)
                        {
                            ActivePitOutputData.YieldPointAtMeasuredTemperatureAndPressure.Value = ActivePitRawData.YieldPointAtMeasuredTemperatureAndPressure.Value;
                        }
                        double? yieldStress = null;
                        double? consistencyIndex = null;
                        double? flowBehaviorIndex = null;
                        if (ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure is not null &&
                            ActivePitRawData.ShearStressAt3RPMAtMeasuredTemperatureAndPressure != null &&
                            ActivePitRawData.ShearStressAt6RPMAtMeasuredTemperatureAndPressure != null &&
                            ActivePitRawData.ShearStressAt100RPMAtMeasuredTemperatureAndPressure != null &&
                            ActivePitRawData.ShearStressAt200RPMAtMeasuredTemperatureAndPressure != null &&
                            ActivePitRawData.ShearStressAt300RPMAtMeasuredTemperatureAndPressure != null &&
                            ActivePitRawData.ShearStressAt600RPMAtMeasuredTemperatureAndPressure != null)
                        {
                            ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements ??= new List<FlowCurveMeasurement>();
                            ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements.Clear();
                            FlowCurveMeasurement measurement = new FlowCurveMeasurement();
                            if (measurement.ConcentricCylindersRotationalSpeed is not null)
                            {
                                measurement.ConcentricCylindersRotationalSpeed.Value = 3.0 / 60.0;
                            }
                            if (measurement.ConcentricCylindersShearStressNewtonianHypothesis is not null)
                            {
                                measurement.ConcentricCylindersShearStressNewtonianHypothesis.Value = ActivePitRawData.ShearStressAt3RPMAtMeasuredTemperatureAndPressure.Value;
                            }
                            ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements.Add(measurement);
                            measurement = new FlowCurveMeasurement();
                            if (measurement.ConcentricCylindersRotationalSpeed is not null)
                            {
                                measurement.ConcentricCylindersRotationalSpeed.Value = 6.0 / 60.0;
                            }
                            if (measurement.ConcentricCylindersShearStressNewtonianHypothesis is not null)
                            {
                                measurement.ConcentricCylindersShearStressNewtonianHypothesis.Value = ActivePitRawData.ShearStressAt6RPMAtMeasuredTemperatureAndPressure.Value;
                            }
                            ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements.Add(measurement);
                            measurement = new FlowCurveMeasurement();
                            if (measurement.ConcentricCylindersRotationalSpeed is not null)
                            {
                                measurement.ConcentricCylindersRotationalSpeed.Value = 100.0 / 60.0;
                            }
                            if (measurement.ConcentricCylindersShearStressNewtonianHypothesis is not null)
                            {
                                measurement.ConcentricCylindersShearStressNewtonianHypothesis.Value = ActivePitRawData.ShearStressAt100RPMAtMeasuredTemperatureAndPressure.Value;
                            }
                            ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements.Add(measurement);
                            measurement = new FlowCurveMeasurement();
                            if (measurement.ConcentricCylindersRotationalSpeed is not null)
                            {
                                measurement.ConcentricCylindersRotationalSpeed.Value = 200.0 / 60.0;
                            }
                            if (measurement.ConcentricCylindersShearStressNewtonianHypothesis is not null)
                            {
                                measurement.ConcentricCylindersShearStressNewtonianHypothesis.Value = ActivePitRawData.ShearStressAt200RPMAtMeasuredTemperatureAndPressure.Value;
                            }
                            ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements.Add(measurement);
                            measurement = new FlowCurveMeasurement();
                            if (measurement.ConcentricCylindersRotationalSpeed is not null)
                            {
                                measurement.ConcentricCylindersRotationalSpeed.Value = 300.0 / 60.0;
                            }
                            if (measurement.ConcentricCylindersShearStressNewtonianHypothesis is not null)
                            {
                                measurement.ConcentricCylindersShearStressNewtonianHypothesis.Value = ActivePitRawData.ShearStressAt300RPMAtMeasuredTemperatureAndPressure.Value;
                            }
                            ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements.Add(measurement);
                            measurement = new FlowCurveMeasurement();
                            if (measurement.ConcentricCylindersRotationalSpeed is not null)
                            {
                                measurement.ConcentricCylindersRotationalSpeed.Value = 600.0 / 60.0;
                            }
                            if (measurement.ConcentricCylindersShearStressNewtonianHypothesis is not null)
                            {
                                measurement.ConcentricCylindersShearStressNewtonianHypothesis.Value = ActivePitRawData.ShearStressAt600RPMAtMeasuredTemperatureAndPressure.Value;
                            }
                            ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements.Add(measurement);
                            Calibrate(ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements, ref yieldStress, ref consistencyIndex, ref flowBehaviorIndex);
                            if ((yieldStress is not null && (double.IsInfinity(yieldStress.Value) || double.IsNaN(yieldStress.Value))) ||
                                (consistencyIndex is not null && (double.IsInfinity(consistencyIndex.Value) || double.IsNaN(consistencyIndex.Value))) ||
                                (flowBehaviorIndex is not null && (double.IsInfinity(flowBehaviorIndex.Value) || double.IsNaN(flowBehaviorIndex.Value))))
                            {
                                yieldStress = null;
                                consistencyIndex = null;
                                flowBehaviorIndex = null;
                            }
                            foreach (var m in ActivePitOutputData.FlowCurveAtMeasuredTemperatureAndPressure.Measurements)
                            {
                                if (m is not null)
                                {
                                    if (m.ConcentricCylindersTorque is not null &&
                                        m.ConcentricCylindersTorque.Value is not null &&
                                        (double.IsNaN(m.ConcentricCylindersTorque.Value.Value) ||
                                         double.IsInfinity(m.ConcentricCylindersTorque.Value.Value)))
                                    {
                                        m.ConcentricCylindersTorque.Value = null;
                                    }
                                    if (m.ConcentricCylindersRotationalSpeed is not null &&
                                        m.ConcentricCylindersRotationalSpeed.Value is not null &&
                                        (double.IsNaN(m.ConcentricCylindersRotationalSpeed.Value.Value) ||
                                         double.IsInfinity(m.ConcentricCylindersRotationalSpeed.Value.Value)))
                                    {
                                        m.ConcentricCylindersRotationalSpeed.Value = null;
                                    }
                                    if (m.ConcentricCylindersShearRateNewtonianHypothesis is not null &&
                                        m.ConcentricCylindersShearRateNewtonianHypothesis.Value is not null &&
                                        (double.IsNaN(m.ConcentricCylindersShearRateNewtonianHypothesis.Value.Value) ||
                                         double.IsInfinity(m.ConcentricCylindersShearRateNewtonianHypothesis.Value.Value)))
                                    {
                                        m.ConcentricCylindersShearRateNewtonianHypothesis.Value = null;
                                    }
                                    if (m.ConcentricCylindersShearRateNonNewtonianHypothesis is not null &&
                                        m.ConcentricCylindersShearRateNonNewtonianHypothesis.Value is not null &&
                                        (double.IsNaN(m.ConcentricCylindersShearRateNonNewtonianHypothesis.Value.Value) ||
                                         double.IsInfinity(m.ConcentricCylindersShearRateNonNewtonianHypothesis.Value.Value)))
                                    {
                                        m.ConcentricCylindersShearRateNonNewtonianHypothesis.Value = null;
                                    }
                                    if (m.ConcentricCylindersShearStressNewtonianHypothesis is not null &&
                                        m.ConcentricCylindersShearStressNewtonianHypothesis.Value is not null &&
                                        (double.IsNaN(m.ConcentricCylindersShearStressNewtonianHypothesis.Value.Value) ||
                                         double.IsInfinity(m.ConcentricCylindersShearStressNewtonianHypothesis.Value.Value)))
                                    {
                                        m.ConcentricCylindersShearStressNewtonianHypothesis.Value = null;
                                    }
                                    if (m.ConcentricCylindersShearStressNonNewtonianHypothesis is not null &&
                                        m.ConcentricCylindersShearStressNonNewtonianHypothesis.Value is not null &&
                                        (double.IsNaN(m.ConcentricCylindersShearStressNonNewtonianHypothesis.Value.Value) ||
                                         double.IsInfinity(m.ConcentricCylindersShearStressNonNewtonianHypothesis.Value.Value)))
                                    {
                                        m.ConcentricCylindersShearStressNonNewtonianHypothesis.Value = null;
                                    }
                                }
                            }
                        }
                        if (ActivePitOutputData.YPLConsistencyIndexAtMeasuredTemperatureAndPressure is not null &&
                            (consistencyIndex is not null ||
                             ActivePitRawData.YPLConsistencyIndexAtMeasuredTemperatureAndPressure != null))
                        {
                            if (consistencyIndex is not null)
                            {
                                ActivePitOutputData.YPLConsistencyIndexAtMeasuredTemperatureAndPressure.Value = consistencyIndex;
                            }
                            else
                            {
                                ActivePitOutputData.YPLConsistencyIndexAtMeasuredTemperatureAndPressure.Value = ActivePitRawData.YPLConsistencyIndexAtMeasuredTemperatureAndPressure!.Value;
                            }
                        }
                        if (ActivePitOutputData.YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure is not null &&
                            (flowBehaviorIndex is not null ||
                             ActivePitRawData.YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure != null))
                        {
                            if (flowBehaviorIndex is not null)
                            {
                                ActivePitOutputData.YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure.Value = flowBehaviorIndex;
                            }
                            else
                            {
                                ActivePitOutputData.YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure.Value = ActivePitRawData.YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure!.Value;
                            }
                        }
                        if (ActivePitOutputData.YPLYieldStressAtMeasuredTemperatureAndPressure is not null &&
                            (yieldStress is not null ||
                             ActivePitRawData.YPLYieldStressAtMeasuredTemperatureAndPressure != null))
                        {
                            if (yieldStress is not null)
                            {
                                ActivePitOutputData.YPLYieldStressAtMeasuredTemperatureAndPressure.Value = yieldStress;
                            }
                            else
                            {
                                ActivePitOutputData.YPLYieldStressAtMeasuredTemperatureAndPressure.Value = ActivePitRawData.YPLYieldStressAtMeasuredTemperatureAndPressure!.Value;
                            }
                        }
                        if (ActivePitOutputData.MeasuredPressure is not null && ActivePitRawData.MeasuredPressure != null)
                        {
                            ActivePitOutputData.MeasuredPressure.Value = ActivePitRawData.MeasuredPressure.Value;
                        }
                        if (ActivePitOutputData.MeasuredPressure is not null && ActivePitRawData.MeasuredPressure != null)
                        {
                            ActivePitOutputData.MeasuredPressure.Value = ActivePitRawData.MeasuredPressure.Value;
                        }

                        await PublishBlackboardAsync(ActivePitOutputData, stoppingToken);
                        DateTime d2 = DateTime.UtcNow;
                        double elapsed = (d2 - d1).TotalSeconds;
                        lock (_lock)
                        {
                            if (Logger is not null && Logger.IsEnabled(LogLevel.Information) &&
                                ActivePitOutputData.MeasuredPressure is not null &&
                                ActivePitOutputData.MeasuredPressure.Value is not null)
                            {
                                Logger.LogInformation("Measured Pressure: " + ActivePitOutputData.MeasuredPressure.Value.Value.ToString("F3"));
                            }
                        }
                        k = 0;
                    }
                }
                catch (Exception e)
                {
                    Logger?.LogError(e.ToString());
                }
                ConfigurationUpdater<ConfigurationForBaraLogics>.Instance.UpdateConfiguration(this);
            }
        }

        private void Calibrate(List<FlowCurveMeasurement> measurements, ref double? yieldStress, ref double? consistencyIndex, ref double? flowBehaviorIndex)
        {
            if (measurements is not null)
            {
                CouetteRheometer r1b1 = new CouetteRheometer();
                r1b1.BobLength = 0.03810;
                r1b1.UseISOConvention = false;
                r1b1.BobRadius = 0.01725;
                r1b1.RheometerType = CouetteRheometer.RheometerTypeEnum.RotorBob;
                r1b1.ConicalAngle = 0.5236;
                r1b1.Gap = 0.00117;
                r1b1.MeasurementPrecision = 0.25;
                r1b1.NewtonianEndEffectCorrection = 1.064;
                Rheogram input = new Rheogram();
                input.ID = Guid.NewGuid();
                input.Name = "raw rheogram";
                input.Description = "raw rheogram";
                input.SetRheometer(r1b1);
                input.RateSource = Rheogram.RateSourceEnum.RotationalSpeed;
                input.StressSource = Rheogram.StressSourceEnum.BobNewtonianShearStress;
                input.Measurements ??= new List<RheometerMeasurement>();
                foreach (var measurement in measurements)
                {
                    if (measurement.ConcentricCylindersRotationalSpeed is not null &&
                        measurement.ConcentricCylindersRotationalSpeed.Value is not null &&
                        measurement.ConcentricCylindersShearStressNewtonianHypothesis is not null &&
                        measurement.ConcentricCylindersShearStressNewtonianHypothesis.Value is not null)
                    {
                        RheometerMeasurement m = new RheometerMeasurement();
                        m.RotationalSpeed = measurement.ConcentricCylindersRotationalSpeed.Value.Value;
                        m.BobNewtonianShearStress = measurement.ConcentricCylindersShearStressNewtonianHypothesis.Value.Value;
                        input.Measurements.Add(m);
                    }
                }
                input.CalibrationMethod = Rheogram.CalibrationMethodEnum.Mullineux;
                input.Calculate();
                YPLCorrection YPLCorrector = new YPLCorrection();
                YPLCorrector.ID = Guid.NewGuid();
                YPLCorrector.Name = "YPL Corrector for R1B1";
                YPLCorrector.Description = "YPL Corrector for R1B1";
                YPLCorrector.RheogramInput = input;
                if (YPLCorrector.CalculateFullyCorrected(Rheogram.CalibrationMethodEnum.Mullineux) &&
                    YPLCorrector.RheogramFullyCorrected is not null &&
                    YPLCorrector.RheogramFullyCorrected.Count == measurements.Count &&
                    YPLCorrector.YPLModelFullyCorrected is not null)
                {
                    for (int i = 0; i < measurements.Count; i++)
                    {
                        measurements[i].ConcentricCylindersShearRateNonNewtonianHypothesis ??= new ScalarProperty();
                        measurements[i].ConcentricCylindersShearRateNonNewtonianHypothesis!.Value = YPLCorrector.RheogramFullyCorrected[i].ShearRate;
                        measurements[i].ConcentricCylindersShearStressNonNewtonianHypothesis ??= new ScalarProperty();
                        measurements[i].ConcentricCylindersShearStressNonNewtonianHypothesis!.Value = YPLCorrector.RheogramFullyCorrected[i].ShearStress;
                        measurements[i].ConcentricCylindersShearRateNewtonianHypothesis ??= new ScalarProperty();
                        measurements[i].ConcentricCylindersShearRateNewtonianHypothesis!.Value = input.Measurements[i].BobNewtonianShearRate;
                        measurements[i].ConcentricCylindersShearStressNewtonianHypothesis ??= new ScalarProperty();
                        measurements[i].ConcentricCylindersShearStressNewtonianHypothesis!.Value = input.Measurements[i].BobNewtonianShearStress;
                    }
                    yieldStress = YPLCorrector.YPLModelFullyCorrected.Tau0;
                    consistencyIndex = YPLCorrector.YPLModelFullyCorrected.K;
                    flowBehaviorIndex = YPLCorrector.YPLModelFullyCorrected.N;
                }
            }
        }

        protected override void AssignValue(DWISData data, object propValue, object? value)
        {
            if (propValue is FlowCurve flowCurveProp)
            {
                if (value is string sval)
                {
                    IList<FlowCurveMeasurement>? lmeasurements = System.Text.Json.JsonSerializer.Deserialize<IList<FlowCurveMeasurement>>(sval);
                    if (lmeasurements != null)
                    {
                        flowCurveProp.Measurements = lmeasurements.ToList();
                    }
                }
            }
            else
            {
                base.AssignValue(data, propValue, value);
            }
        }

        protected override object? ProcessValue(object? propVal)
        {
            object? val = null;
            if (propVal is FlowCurve flowCurveProp)
            {
                if (flowCurveProp.Measurements is not null)
                {
                    try
                    {
                        val = System.Text.Json.JsonSerializer.Serialize(flowCurveProp.Measurements);
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError(e.ToString());
                    }
                }
            }
            else
            {
                val = base.ProcessValue(propVal);
            }
            return val;
        }

        protected override void SetDefaultProperty(PropertyInfo property, DWISData data)
        {
            if (property.PropertyType == typeof(FlowCurve))
            {
                FlowCurve flowCurveProp = new FlowCurve();
                property.SetValue(data, flowCurveProp);
            }
            else
            {
                base.SetDefaultProperty(property, data);
            }
        }
    }
}

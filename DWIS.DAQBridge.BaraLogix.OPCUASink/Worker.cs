using DWIS.Client.ReferenceImplementation.OPCFoundation;
using DWIS.RigOS.Common.Worker;
using OSDC.DotnetLibraries.General.Common;
using System.Reflection;

namespace DWIS.DAQBridge.BaraLogix.OPCUASink
{
    public class Worker : DWISWorker<Configuration>
    {
        private ActivePitOutputData ActivePitOutputData { get; set; } = new ActivePitOutputData();

        public Worker(ILogger<IDWISWorker<Configuration>> logger, ILogger<DWISClientOPCF>? loggerDWISClient) : base(logger, loggerDWISClient)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectToBlackboard();
            if (_DWISClient != null && _DWISClient.Connected)
            {
                await RegisterQueries(ActivePitOutputData);
                await Loop(stoppingToken);
            }
        }

        protected override async Task Loop(CancellationToken cancellationToken)
        {
            PeriodicTimer timer = new PeriodicTimer(LoopSpan);
            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                await ReadBlackboardAsync(ActivePitOutputData, cancellationToken);
                if (Logger is not null && Logger.IsEnabled(LogLevel.Information) &&
                            ActivePitOutputData.MeasuredPressure is not null &&
                            ActivePitOutputData.MeasuredPressure.Value is not null)
                {
                    Logger.LogInformation("Measured Pressure: " + ActivePitOutputData.MeasuredPressure.Value.Value.ToString("F3"));
                }
                ConfigurationUpdater<Configuration>.Instance.UpdateConfiguration(this);
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
                    val = System.Text.Json.JsonSerializer.Serialize(flowCurveProp.Measurements);
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

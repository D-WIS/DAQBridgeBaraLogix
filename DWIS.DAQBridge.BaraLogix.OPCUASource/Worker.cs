using DWIS.Client.ReferenceImplementation.OPCFoundation;
using DWIS.RigOS.Common.Worker;
using Microsoft.Extensions.Logging;
using OSDC.DotnetLibraries.General.Common;

namespace DWIS.DAQBridge.BaraLogix.OPCUASource
{
    public class Worker : DWISWorkerWithOPCUA<ConfigurationForOPCUA>
    {
        private ActivePitRawData ActivePitRawData { get; set; } = new ActivePitRawData();
        private TimeSpan OPCUALoopSpan { get; set; } = TimeSpan.FromSeconds(1);

        public Worker(ILogger<IDWISWorker<ConfigurationForOPCUA>> logger, ILogger<DWISClientOPCF>? loggerDWISClient) : base(logger, loggerDWISClient)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectToOPCUA();
            if (Configuration is not null && _OPCUAClient != null && _OPCUAClient.Connected)
            {
                OPCUALoopSpan = Configuration.OPCUALoopDuration;
                await RegisterToOPCUA(ActivePitRawData, "BaraLogixDataManifest", "Halliburton");
                await Loop(stoppingToken);
            }
        }

        protected override async Task Loop(CancellationToken stoppingToken)
        {
            PeriodicTimer timer = new PeriodicTimer(OPCUALoopSpan);
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                if (_OPCUAClient != null && _OPCUAClient.Connected)
                {
                    FillRandomData(ActivePitRawData);
                    double tau0 = _random.NextDouble();
                    double K = _random.NextDouble();
                    double n = _random.NextDouble();
                    if (ActivePitRawData.ShearStressAt3RPMAtMeasuredTemperatureAndPressure is not null)
                    {
                        ActivePitRawData.ShearStressAt3RPMAtMeasuredTemperatureAndPressure.Value = tau0 + K * Math.Pow(3.0 * 1.703, n);
                    }
                    if (ActivePitRawData.ShearStressAt6RPMAtMeasuredTemperatureAndPressure is not null)
                    {
                        ActivePitRawData.ShearStressAt6RPMAtMeasuredTemperatureAndPressure.Value = tau0 + K * Math.Pow(6.0 * 1.703, n);
                    }
                    if (ActivePitRawData.ShearStressAt100RPMAtMeasuredTemperatureAndPressure is not null)
                    {
                        ActivePitRawData.ShearStressAt100RPMAtMeasuredTemperatureAndPressure.Value = tau0 + K * Math.Pow(100.0 * 1.703, n);
                    }
                    if (ActivePitRawData.ShearStressAt200RPMAtMeasuredTemperatureAndPressure is not null)
                    {
                        ActivePitRawData.ShearStressAt200RPMAtMeasuredTemperatureAndPressure.Value = tau0 + K * Math.Pow(200.0 * 1.703, n);
                    }
                    if (ActivePitRawData.ShearStressAt300RPMAtMeasuredTemperatureAndPressure is not null)
                    {
                        ActivePitRawData.ShearStressAt300RPMAtMeasuredTemperatureAndPressure.Value = tau0 + K * Math.Pow(300.0 * 1.703, n);
                    }
                    if (ActivePitRawData.ShearStressAt600RPMAtMeasuredTemperatureAndPressure is not null)
                    {
                        ActivePitRawData.ShearStressAt600RPMAtMeasuredTemperatureAndPressure.Value = tau0 + K * Math.Pow(600.0 * 1.703, n);
                    }
                    await PublishOPCUAAsync(ActivePitRawData, Configuration?.NameSpace, Configuration?.NodeIDPrefix, Configuration?.OPCUAIDs, stoppingToken);
                    lock (_lock)
                    {
                        if (Logger is not null && Logger.IsEnabled(LogLevel.Information) && ActivePitRawData.MeasuredPressure is not null && ActivePitRawData.MeasuredPressure.Value is not null)
                        {
                            Logger.LogInformation("Active pit measured pressure: " + ActivePitRawData.MeasuredPressure.Value);
                        }
                    }
                }
                ConfigurationUpdater<ConfigurationForOPCUA>.Instance.UpdateConfiguration(this);
            }
        }
    }
}

using DWIS.DAQBridge.BaraLogix.OPCUASink;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

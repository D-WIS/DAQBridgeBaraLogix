# Programs Related to the D-WIS Bridge for the BaraLogix Inline Rheometer
This solution provides the services, helpers, and shared models needed to bridge
the BaraLogix Inline Rheometer into the D-WIS Blackboard. Each project has a specific
role in the pipeline, from emitting raw signals to publishing corrected rheology
measurements and validating the outputs.

## Projects and how to use them
### DWIS.DAQBridge.BaraLogix.Server
The main bridge service. It reads raw signals from the BaraLogix OPC-UA server,
applies non-Newtonian corrections, fits yield power law parameters, and publishes
both raw and corrected `FlowCurve` values (with semantic descriptions) to the D-WIS
Blackboard. Run this in production alongside the OPC-UA server.

### DWIS.DAQBridge.BaraLogix.OPCUASource
A helper program that emulates a live OPC-UA data source. Use it for local testing,
integration validation, and demos when the physical BaraLogix instrument is not
available.

### DWIS.DAQBridge.BaraLogix.OPCUASink
A helper program that acts as a consumer. It queries the D-WIS Blackboard using
semantic descriptions and verifies that published signals can be discovered and
read. Use it to validate end-to-end publishing and troubleshoot mapping issues.

### DWIS.DAQBridge.BaraLogix.Model
A shared class library containing the domain data structures used across the bridge
stack. Reference this library from services and tooling to ensure consistent
serialization, validation, and versioned contracts.

### DWIS.DAQBridge.BaraLogix.ExportDataStructures
A command-line tool that generates example JSON payloads and manifest files for
BaraLogix signals. Use it as a reference for the on-wire data shapes and for
bootstrapping downstream consumers or documentation.

### DWIS.DAQBridge.BaraLogix.Schemas
Schema assets and definitions that support validation and interoperability across
the bridge and D-WIS tooling. Use these when integrating with systems that require
explicit schema artifacts.

## Typical usage flows
- Production: run `DWIS.DAQBridge.BaraLogix.Server` alongside the OPC-UA server and
  point it at a D-WIS Blackboard.
- Local testing: run `DWIS.DAQBridge.BaraLogix.OPCUASource`, then start
  `DWIS.DAQBridge.BaraLogix.Server`, and verify outputs with
  `DWIS.DAQBridge.BaraLogix.OPCUASink`.

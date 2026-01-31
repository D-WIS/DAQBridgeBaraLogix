# Bridge for the BaraLogix Inline Rheometer to DWIS
This service is the main DWIS bridge between the OPC-UA server that provides access to
raw measurements from the BaraLogix Inline Rheometer and the D-WIS Blackboard. It
collects, transforms, and publishes measurements alongside their semantic descriptions
so downstream consumers can interpret and validate the data consistently.

## Responsibilities
- Connect to the BaraLogix OPC-UA server and acquire raw instrument signals.
- Apply non-Newtonian corrections to shear-rate and shear-stress measurements.
- Fit yield power law (Herschel–Bulkley) parameters from corrected data.
- Publish both raw and corrected `FlowCurve` values and signal semantics to D-WIS.

## Rheology corrections and calibration
The shear-rate correction follows Skadsem and Saasen (2019) for concentric cylinder
viscometer flows of Herschel-Bulkley fluids. The shear-stress correction follows the
method described by Étienne Lac and Andrew Parry (2017) for non-Newtonian end effects
in standard oilfield rheometers. After correction, the yield power law parameters are
calibrated using the optimal fitting method described by Glen Mullineux (2008).

Both the original Fann 35 R1B1 measurements and the corrected values are exposed in a
`FlowCurve` data structure, enabling consumers to compare raw and corrected curves and
to use the corrected parameters for modeling and interpretation.

## Deployment
To install the bridge, you need a local D-WIS Blackboard and the bridge installed on
the same machine as the OPC-UA server that exposes the BaraLogix signals.

To install and run a replicated DWIS Blackboard, use the following command:
```sh
docker run  -dit --name blackboard -P -p 48030:48030/tcp --hostname localhost  digiwells/ddhubserver:latest --useHub --hubURL https://dwis.digiwells.no/blackboard/applications
```

To install and run the BaraLogix bridge, use the following command:
```sh
docker run -dit --name BaraLogix -v c:\Volumes\DWISDAQBridgeBaraLogixServer:/home digiwells/dwisdaqbridgebaralogixserver:stable
```

In the folder `c:\Volumes\DWISDAQBridgeBaraLogixServer`, you can update the configuration
file to match your environment.

Here is an example `config.json` file for a Docker-based configuration. The OPC-UA server
runs in the same Docker environment on port 48031. The D-WIS Blackboard runs in the
same Docker environment on port 48030. The loop duration for OPC-UA polling is 1 s,
and the loop duration for D-WIS publishing is also 1 s. The OPC-UA namespace and node
IDs match the internal ones, and all unit conversions are identity transforms.

```json
{
  "LoopDuration": "00:00:01",
  "GeneralBlackboard": "opc.tcp://host.docker.internal:48030",
  "OPCUAURL": "opc.tcp://host.docker.internal:48031",
  "OPCUALoopDuration": "00:00:01",
  "NameSpace": "http://ddhub.no/BaraLogixDataManifest/Variables/",
  "NodeIDPrefix": "BaraLogixDataManifest.",
  "OPCUAIDs": {
    "BaraLogixDataManifest.MeasuredTemperature": "BaraLogixDataManifest.MeasuredTemperature",
    "BaraLogixDataManifest.MeasuredPressure": "BaraLogixDataManifest.MeasuredPressure",
    "BaraLogixDataManifest.MassDensityFromCoriolisAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.MassDensityFromCoriolisAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.ShearStressAt3RPMAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.ShearStressAt3RPMAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.ShearStressAt6RPMAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.ShearStressAt6RPMAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.ShearStressAt100RPMAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.ShearStressAt100RPMAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.ShearStressAt200RPMAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.ShearStressAt200RPMAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.ShearStressAt300RPMAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.ShearStressAt300RPMAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.ShearStressAt600RPMAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.ShearStressAt600RPMAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.PlasticViscosityAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.PlasticViscosityAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.YieldPointAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.YieldPointAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.YPLYieldStressAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.YPLYieldStressAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.YPLConsistencyIndexAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.YPLConsistencyIndexAtMeasuredTemperatureAndPressure",
    "BaraLogixDataManifest.YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure": "BaraLogixDataManifest.YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure"
  },
  "UnitConversions": {
    "BaraLogixDataManifest.MeasuredTemperature": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.MeasuredPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.MassDensityFromCoriolisAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.ShearStressAt3RPMAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.ShearStressAt6RPMAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.ShearStressAt100RPMAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.ShearStressAt200RPMAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.ShearStressAt300RPMAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.ShearStressAt600RPMAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.PlasticViscosityAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.YieldPointAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.YPLYieldStressAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.YPLConsistencyIndexAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    },
    "BaraLogixDataManifest.YPLFlowBehaviorIndexAtMeasuredTemperatureAndPressure": {
      "ConversionFactor": 1.0,
      "ConversionOffset": 0.0
    }
  }
}

```

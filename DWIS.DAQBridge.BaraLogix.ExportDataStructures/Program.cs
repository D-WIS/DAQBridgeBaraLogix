using DWIS.DAQBridge.BaraLogix.Model;
using NJsonSchema;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DWIS.DAQBridge.BaraLogix.ExportDataStructures
{
    class Program
    {
        static void Main()
        {
            GenerateJsonSchemas();
            GenerateManifests();
        }

        static void GenerateJsonSchemas()
        {
            string rootDir = ".\\";
            bool found = false;
            do
            {
                DirectoryInfo? info = Directory.GetParent(rootDir);
                if (info != null && "DWIS.DAQBridge.BaraLogix.ExportDataStructures".Equals(info.Name))
                {
                    found = true;
                }
                else
                {
                    rootDir += "..\\";
                }
            } while (!found);
            rootDir += "..\\DWIS.DAQBridge.BaraLogix.Schemas\\";
            List<FlowCurveMeasurement> measurements = new List<FlowCurveMeasurement>();
            FlowCurveMeasurement measurement = new FlowCurveMeasurement();
            measurement.ConcentricCylindersShearStressNewtonianHypothesis = new RigOS.Common.Worker.ScalarProperty() { Value = 0 };
            measurement.ConcentricCylindersRotationalSpeed = new RigOS.Common.Worker.ScalarProperty() { Value = 0 };
            measurement.ConcentricCylindersShearRateNewtonianHypothesis = new RigOS.Common.Worker.ScalarProperty(){ Value = 0 };
            measurement.ConcentricCylindersShearRateNonNewtonianHypothesis = new RigOS.Common.Worker.ScalarProperty() { Value= 0 };
            measurement.ConcentricCylindersShearStressNewtonianHypothesis = new RigOS.Common.Worker.ScalarProperty() { Value = 0 };
            measurement.ConcentricCylindersShearStressNonNewtonianHypothesis = new RigOS.Common.Worker.ScalarProperty() { Value = 0 };
            measurement.ConcentricCylindersTorque = new RigOS.Common.Worker.ScalarProperty() { Value = 0 };
            measurements.Add(measurement);
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            string json = JsonSerializer.Serialize(measurements, options);
            using (StreamWriter writer = new StreamWriter(rootDir + "FlowCurveMeasurementsExample.json"))
            {
                writer.WriteLine(json);
            }
        }   
        
        static void GenerateManifests()
        {
            string rootDir = ".\\";
            bool found = false;
            do
            {
                DirectoryInfo? info = Directory.GetParent(rootDir);
                if (info != null && "DWIS.DAQBridge.BaraLogix.ExportDataStructures".Equals(info.Name))
                {
                    found = true;
                }
                else
                {
                    rootDir += "..\\";
                }
            } while (!found);
            rootDir += "..\\DWIS.DAQBridge.BaraLogix.Schemas\\";
            ActivePitOutputData data = new ActivePitOutputData();
            if (data.Manifests is not null)
            {
                foreach (var kpv in data.Manifests.Value)
                {
                    string name = kpv.Key.Name;
                    using (StreamWriter writer = new StreamWriter(rootDir + name + "Manifest" + ".json"))
                    {
                        string json = System.Text.Json.JsonSerializer.Serialize(kpv.Value);
                        writer.WriteLine(json);
                    }
                }
            }
        }
    }
}
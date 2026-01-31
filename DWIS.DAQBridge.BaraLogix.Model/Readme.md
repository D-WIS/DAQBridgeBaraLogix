# Input and Output Data Structures used by the BaraLogix Inline Rheometer Bridge to D-WIS
This class library defines the core domain model for the BaraLogix Inline Rheometer
bridge to D-WIS. It provides strongly typed input, output, and exchange data
structures that capture measured signals, derived values, identifiers, and metadata
needed to move data reliably across the bridge boundary.

The model is intended to be consumed by the bridge components (source, sink, server,
and schema tooling) so they can share a consistent contract for serialization,
validation, and interoperability with D-WIS. Use these types whenever you need a
stable, versioned representation of BaraLogix measurements and related context.


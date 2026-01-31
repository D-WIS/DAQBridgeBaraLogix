# Generator of json Data Structures and Manifests
This command line program exports sample JSON payloads and signal manifests for the
BaraLogix Inline Rheometer bridge. It produces an example file that documents a list
of `FlowCurveMeasurement` entries, plus a set of Manifest files that describe each
signal published to the D-WIS Blackboard.

Use this tool when you need a concrete reference for the on‑wire data shapes or when
bootstrapping downstream consumers. The generated Manifests include the semantic
metadata for each signal, enabling consistent interpretation, validation, and
integration with D‑WIS tooling.



# Emitter (Source) for Data Provided by the BaraLogix Inline Rheometer
This helper program simulates a live OPC-UA data source for the BaraLogix Inline
Rheometer. It publishes sample measurements to an OPC-UA server so the BaraLogix
bridge can read, transform, and publish the processed results to the D-WIS Blackboard
along with their semantic descriptions.

Use this program for local testing, integration validation, and demos when a physical
instrument is not available. It provides a stable stream of signals that mirrors the
expected node IDs and namespaces so the bridge pipeline can be exercised end-to-end.

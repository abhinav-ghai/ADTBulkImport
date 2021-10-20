# ADTBulkImport

ADT Bulk Import Converter tool is a .NET Core tool that takes DTDL models in a directory (1 JSON model per file) and recursively iterates through the directory structure to generate 1 output file in NDJSON format that can be used for providing input bulk import of models to Azure Digital Twin service.

Usage: ADTBulkImportConverter.exe "Input Folder Path" "OutputFileName.ndjson"

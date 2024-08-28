# Exapt

A CLI wrapper around [Exapunks](https://store.steampowered.com/app/716490/EXAPUNKS/) for validating solutions.

## Usage

Run Exapt from the root directory with the following command:

```text
dotnet run --project Exapt -- <game directory> <solution file>
```

where `<game directory>` is the path to the root directory of the 2022-10-13 Windows version of Exapunks (regardless of the OS in which Exapt is being run). This version of the game can be obtained by running the following command in the Steam console:

```text
download_depot 716490 716491 2461759427682173730
```

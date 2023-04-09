using System.Collections.Generic;

namespace MigrationTool;

public struct Mapping
{
    List<TablePair> TablePairs;
}

public struct TablePair
{
    public Table PervasiveTable;
    public Table SqlTable;
}

public struct Table
{
    public string Name;
    public List<string> Fields;
}
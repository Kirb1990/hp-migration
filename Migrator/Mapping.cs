using System.Collections.Generic;

namespace MigrationTool;

public struct Mapping
{
    public List<TablePair> TablePairs;
}

public struct TablePair
{
    public Table PervasiveTable;
    public Table SqlTable;
}

public struct Table
{
    public string Name;
    public List<Field> Fields;
}

public struct Field
{
    public int Index;
    public string Name;
}
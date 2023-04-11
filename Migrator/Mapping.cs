using System.Collections.Generic;
using System.Linq;

namespace MigrationTool;

public struct Mapping
{
    public List<TablePair> TablePairs;

    public bool TryGet(string tableName, out TablePair tablePair)
    {
        tablePair = new TablePair();
        
        foreach (TablePair pair in TablePairs.Where(pair => pair.PervasiveTable.Name.Equals(tableName) || pair.SqlTable.Name.Equals(tableName)))
        {
            tablePair = pair;
            return true;
        }

        return false;
    }
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
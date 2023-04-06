using CommandLine;

namespace MigrationTool;

internal class Options
{
    [Option("migrate", HelpText = "Migrate the database")]
    public bool Migrate { get; set; }
            
    [Option("migrate:refresh", HelpText = "First refresh the database and migrate afterwards")]
    public bool MigrateWithRefresh { get; set; }

    [Option("refresh", HelpText = "Refresh the database")]
    public bool Refresh { get; set; }

    [Option("--seed", HelpText = "Seed the database")]
    public bool Seed { get; set; }
}

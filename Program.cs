using ETLPipeline;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string appSettingpath = @"F:\C# Programming\ETLPipeline Project\ETLPipeline\appsetting.json";
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(appSettingpath).Build();

        string? baseConnectionString = configuration.GetConnectionString(nameof(baseConnectionString));
        string? csvPath = configuration.GetSection(nameof(csvPath))["BasePath"];
        string? ApiUrl = configuration.GetSection(nameof(ApiUrl))["URL"];
        string? loadConnectionString = configuration.GetConnectionString(nameof(loadConnectionString));

        var reviews = await Extract.ExtractingFromCSV(csvPath);
        var users = await Extract.HittingApi(ApiUrl);
        var cars = await Extract.ReadFromDatabase(baseConnectionString);

        Transform transform = new Transform()
        {
            Cars= cars,
            Reviews= reviews,
            Users= users
        };

        var result = transform.MergingData();

        Load loadData = new Load()
        {
            ConnectionString = loadConnectionString,
            TransformedData = result
        };

        loadData.InsertData();

    }
}
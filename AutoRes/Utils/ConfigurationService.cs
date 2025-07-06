using System;
using System.Text.Json;

public class ConfigurationService()
{
    private static readonly string configPath = @"C:\ProgramData\AutoRes\config.json";

    public static List<Configuration> Load()
    {
        if (!File.Exists(configPath)) return new List<Configuration>();
        var json = File.ReadAllText(configPath);
        return JsonSerializer.Deserialize<List<Configuration>>(json);
    }

    public static void Save(List<Configuration> configs)
    {
        var json = JsonSerializer.Serialize(configs, new JsonSerializerOptions { WriteIndented = true });
        Directory.CreateDirectory(Path.GetDirectoryName(configPath));
        File.WriteAllText(configPath, json);
    }

    public static void Update(Guid id, Configuration updatedConfig)
    {
        var configs = Load();

        var index = configs.FindIndex(c => c.Id == id);
        if (index == -1)
            throw new Exception($"No se encontró ninguna configuración con el ID: {id}");

        configs[index] = updatedConfig;

        Save(configs);
    }

    public static void Delete(Guid id)
    {
        var configs = Load();

        var configToRemove = configs.FirstOrDefault(c => c.Id == id);
        if (configToRemove != null)
        {
            configs.Remove(configToRemove);
            Save(configs);
        }
    }

}

namespace AutoResService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private List<Configuration> _configs;
    private ProcessWatcher _watcher;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("AutoResService iniciado correctamente.");

        _configs = ConfigurationService.Load();
        _logger.LogInformation($"Cantidad de configuraciones cargadas: {_configs.Count}");
        if (_configs.Any())
        {
            _watcher = new ProcessWatcher(_configs);
            _watcher.Start();
        }
        else
        {
            _logger.LogInformation("No configurations found. Service will idle.");
        }

        return Task.CompletedTask;
    }
}


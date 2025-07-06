using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static DisplayManager;

public class ProcessWatcher
{
    private List<Configuration> _configs;
    private HashSet<string> _runningIntercepted = new();
    private CancellationTokenSource _cts;
    private Task _backgroundTask;

    int currentProcessId;
    private string currentProcessName;
    string[] originalResolution;

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);

    public ProcessWatcher(List<Configuration> configs)
    {
        _configs = configs;
    }
    public void Start()
    {
        _cts = new CancellationTokenSource();
        originalResolution = DisplayManager.GetCurrentResolution().Split('x');

        _backgroundTask = Task.Run(() => WatchProcesses(_cts.Token), _cts.Token);
    }

    public void Stop()
    {
        if (_cts != null)
        {
            ApplyOriginalResolution();
            _cts?.Cancel();
            _backgroundTask?.Wait();
            _cts.Dispose();
            _cts = null;
        }
    }

    private void WatchProcesses(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            foreach (var proc in Process.GetProcesses())
            {
                try
                {
                    string procFullPath = proc.MainModule.FileName;
                    string procDirectory = Path.GetDirectoryName(procFullPath);

                    var match = _configs.FirstOrDefault(c =>
                        string.Equals(procFullPath, c.Path, StringComparison.OrdinalIgnoreCase) ||
                        procDirectory.StartsWith(Path.GetDirectoryName(c.Path), StringComparison.OrdinalIgnoreCase));

                    if (match != null && !_runningIntercepted.Contains(proc.Id.ToString()) && match.Enabled)
                    {
                        string targetDirectory = Path.GetDirectoryName(match.Path);

                        // Obtener lista de procesos cuyo ejecutable esté en targetDirectory o en cualquier subdirectorio
                        var relatedProcesses = Process.GetProcesses()
                            .Where(p =>
                            {
                                try
                                {
                                    string pPath = p.MainModule.FileName;
                                    string pDir = Path.GetDirectoryName(pPath);
                                    return pDir.StartsWith(targetDirectory, StringComparison.OrdinalIgnoreCase);
                                }
                                catch
                                {
                                    return false;
                                }
                            })
                            .ToList();

                        var relatedProcessIds = relatedProcesses.Select(p => (uint)p.Id).ToList();

                        IntPtr foregroundWindow = GetForegroundWindow();
                        GetWindowThreadProcessId(foregroundWindow, out uint foregroundProcId);

                        if (relatedProcessIds.Contains(foregroundProcId))
                        {
                            currentProcessName = proc.ProcessName;
                            currentProcessId = proc.Id;

                            _runningIntercepted.Add(proc.Id.ToString());
                            HandleMatchedProcess(proc, match);
                        }
                    }
                }
                catch
                {
                    // Ignorar procesos sin acceso
                }
            }

            Thread.Sleep(1500);
        }
    }

    private void HandleMatchedProcess(Process proc, Configuration config)
    {
        Task.Run(() =>
        {
            try
            {
                string[] resolution = config.Resolution.Split('x');
                int width = int.Parse(resolution[0]);
                int height = int.Parse(resolution[1]);

                DisplayManager.ApplyResolution(width, height, currentProcessName);

                IntPtr mainWindowHandle = IntPtr.Zero;

                while (!proc.HasExited)
                {
                    proc.Refresh(); 
                    if (proc.MainWindowHandle != IntPtr.Zero)
                    {
                        mainWindowHandle = proc.MainWindowHandle;
                        break;
                    }
                    Thread.Sleep(500);
                }

                if (mainWindowHandle == IntPtr.Zero)
                {
                    ApplyOriginalResolution();
                    return;
                }

                while (!proc.HasExited && IsWindow(mainWindowHandle))
                {
                    Thread.Sleep(500);
                }

                ApplyOriginalResolution();
            }
            catch
            {
                ApplyOriginalResolution();  
            }
        });
    }



    public void ApplyOriginalResolution() 
    {
        if (currentProcessName != null) 
        {
            int originalWidth = int.Parse(originalResolution[0]);
            int originalHeight = int.Parse(originalResolution[1]);

            DisplayManager.ApplyResolution(originalWidth, originalHeight, currentProcessName);
            _runningIntercepted.Remove(currentProcessId.ToString());

            // Limpiar la lista de procesos con error
            DisplayManager._errorProcesses.Clear();
        }
    }
}

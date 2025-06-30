using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static AutoResService.DisplayManager;

namespace AutoResService
{
    public class ProcessWatcher
    {
        private List<Configuration> _configs;
        private HashSet<string> _runningIntercepted = new();

        public ProcessWatcher(List<Configuration> configs)
        {
            _configs = configs;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var proc in Process.GetProcesses())
                    {
                        try
                        {
                            string fullPath = proc.MainModule.FileName;

                            var match = _configs.FirstOrDefault(c =>
                                string.Equals(fullPath, c.Path, StringComparison.OrdinalIgnoreCase));

                            if (match != null && !_runningIntercepted.Contains(proc.Id.ToString()))
                            {
                                _runningIntercepted.Add(proc.Id.ToString());
                                HandleMatchedProcess(proc, match);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Algunos procesos lanzan excepción por acceso denegado
                        }
                    }

                    Thread.Sleep(3000);
                }
            });
        }

        private void HandleMatchedProcess(Process proc, Configuration config)
        {
            Task.Run(() =>
            {
                string[] originalResolution = GetCurrentResolution().Split('x');
                string[] resolution = config.Resolution.Split('x');
                int width = int.Parse(resolution[0]);
                int height = int.Parse(resolution[1]);
                int originalWidth = int.Parse(originalResolution[0]);
                int originalHeight = int.Parse(originalResolution[1]);


                DisplayManager.ApplyResolution(width,height);

                proc.WaitForExit();

                DisplayManager.ApplyResolution(originalWidth,originalHeight);
                _runningIntercepted.Remove(proc.Id.ToString());
            });
        }

        public static string GetCurrentResolution()
        {
            DEVMODE devMode = new DEVMODE();
            devMode.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));

            if (EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode))
            {
                return $"{devMode.dmPelsWidth}x{devMode.dmPelsHeight}";
            }

            throw new InvalidOperationException("No se pudo obtener la resolución actual.");
        }

    }

}

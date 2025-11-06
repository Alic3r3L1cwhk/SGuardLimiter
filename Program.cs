using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading;

[SupportedOSPlatform("windows")]
internal static class Program
{
    private const string AppName = "Anti-ACE-SGuard";
    private static readonly string[] TargetProcesses = { "SGuard64", "SGuardSvc64" };
    private const int MaxWaitSeconds = 30;
    private static int CpuIndex;

    static void Main()
    {
        try
        {
            // 获取CPU核心数量，设置为最后一个CPU
            int cpuCount = Environment.ProcessorCount;
            CpuIndex = cpuCount - 1;
            
            Console.Title = AppName;
            Console.WriteLine("[" + AppName + "] 服务已启动");
            Console.WriteLine("检测到 CPU 核心数: " + cpuCount);
            Console.WriteLine("将绑定到: CPU" + CpuIndex + " (最后一个核心)\n");
            Console.WriteLine("正在查找目标进程: " + string.Join(", ", TargetProcesses));
            Console.WriteLine("最多等待 " + MaxWaitSeconds + " 秒...\n");

            bool foundProcesses = WaitForProcesses();

            if (!foundProcesses)
            {
                Console.WriteLine("\n[错误] 在 " + MaxWaitSeconds + " 秒内未找到目标进程");
                Console.WriteLine("服务即将关闭...");
                Console.WriteLine("\n按任意键退出...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n[成功] 找到目标进程，开始应用设置...\n");
            ApplySettings();

            Console.WriteLine("\n任务完成，服务即将关闭...");
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n[严重错误] 程序异常: " + ex.Message);
            Console.WriteLine("详细信息: " + ex.ToString());
            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }
    }

    private static bool WaitForProcesses()
    {
        var stopwatch = Stopwatch.StartNew();
        int dotCount = 0;

        while (stopwatch.Elapsed.TotalSeconds < MaxWaitSeconds)
        {
            bool allFound = true;
            foreach (var processName in TargetProcesses)
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    allFound = false;
                }
                else
                {
                    foreach (var p in processes) p.Dispose();
                }
            }

            if (allFound)
            {
                return true;
            }

            string dots = new string('.', dotCount % 4);
            Console.Write("\r等待中" + dots + "   ");
            dotCount++;
            Thread.Sleep(500);
        }

        return false;
    }

    private static void ApplySettings()
    {
        bool foundAny = false;
        int successCount = 0;
        int failCount = 0;

        foreach (var processName in TargetProcesses)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                foundAny = true;
                Console.WriteLine("找到进程: " + processName);
            }

            foreach (var proc in processes)
            {
                try
                {
                    proc.PriorityClass = ProcessPriorityClass.Idle;
                    proc.ProcessorAffinity = unchecked((IntPtr)(1L << CpuIndex));
                    
                    successCount++;
                    Console.WriteLine("  √ 成功设置 " + proc.ProcessName + " (PID: " + proc.Id + ")");
                    Console.WriteLine("    - 优先级: 低");
                    Console.WriteLine("    - CPU亲和性: CPU" + CpuIndex);
                }
                catch (Exception ex)
                {
                    failCount++;
                    Console.WriteLine("  × 设置 " + proc.ProcessName + " (PID: " + proc.Id + ") 失败");
                    Console.WriteLine("    错误: " + ex.Message);
                }
                finally
                {
                    proc.Dispose();
                }
            }
        }

        if (!foundAny)
        {
            Console.WriteLine("[警告] 未找到任何目标进程");
        }
        else
        {
            Console.WriteLine("\n统计信息:");
            Console.WriteLine("  成功: " + successCount);
            Console.WriteLine("  失败: " + failCount);
        }
    }
}
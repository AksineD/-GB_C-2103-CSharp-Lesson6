#region

using System.Diagnostics;

#endregion

void PrintHeader()
{
    Console.WriteLine($"{"PID",-6} {"Process Name",-26} {"Physical memory usage",-21}");
    Console.WriteLine("=".PadLeft(6, '=') + " " + "=".PadLeft(26, '=') + " " + "=".PadLeft(21, '='));
}

void ListAllProcesses()
{
    var processes = Process.GetProcesses();

    for (var i = 0; i < processes.Length; i++)
    {
        var p = processes[i];
        var pName = p.ProcessName;
        if (pName.Length > 26) pName = p.ProcessName.Substring(0, 23) + "...";

        Console.WriteLine($"{p.Id,-6} {pName,-26} {p.WorkingSet64,-21}");
    }
}

void KillProcessById(int pid)
{
    try
    {
        var p = Process.GetProcessById(pid);

        if (!p.HasExited)
        {
            p.Kill(true);
            Console.WriteLine("The process closed successfully");
        }
        else
        {
            Console.WriteLine("The process was closed before or not exist.");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

void KillProcessByName(string procName)
{
    try
    {
        var parr = Process.GetProcessesByName(procName);
        if (parr.Length > 0)
            foreach (var p in parr)
                if (!p.HasExited)
                {
                    p.Kill(true);
                    Console.WriteLine("The process closed successfully");
                }
                else
                {
                    Console.WriteLine("The process was closed before or not exist.");
                }
        else Console.WriteLine("No process found with that name");
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

void GetUserInput()
{
    Console.WriteLine("Enter a process name or process id to close it. Enter \"exit\" to exit the application");
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) return;
    if (input.ToLower() == "exit") Environment.Exit(0);

    var flag = int.TryParse(input, out var prsPid);
    if (flag) KillProcessById(prsPid);
    else KillProcessByName(input);
}

PrintHeader();
ListAllProcesses();
while (true) GetUserInput();
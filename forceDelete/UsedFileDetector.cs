// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.UsedFileDetector
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  internal static class UsedFileDetector
  {
    private const int NoError = 0;
    private const int ErrorMoreData = 234;

    public static List<int> GetProcesses(string absoluteFileName)
    {
      uint pSessionHandle;
      if (NativeMethods.RmStartSession(out pSessionHandle, 0, Guid.NewGuid().ToString("N")) != 0)
        throw new Win32Exception();
      List<int> processes = new List<int>();
      try
      {
        string[] rgsFilenames = new string[1]
        {
          absoluteFileName
        };
        if (NativeMethods.RmRegisterResources(pSessionHandle, (uint) rgsFilenames.Length, rgsFilenames, 0U, (UniqueProcess[]) null, 0U, (string[]) null) != 0)
          throw new Win32Exception();
        uint pnProcInfoNeeded = 0;
        uint pnProcInfo = 0;
        uint lpdwRebootReasons = 0;
        int list = NativeMethods.RmGetList(pSessionHandle, out pnProcInfoNeeded, ref pnProcInfo, (ProcessInfo[]) null, ref lpdwRebootReasons);
        while (list == 234)
        {
          ProcessInfo[] rgAffectedApps = new ProcessInfo[(int) pnProcInfoNeeded];
          pnProcInfo = (uint) rgAffectedApps.Length;
          list = NativeMethods.RmGetList(pSessionHandle, out pnProcInfoNeeded, ref pnProcInfo, rgAffectedApps, ref lpdwRebootReasons);
          if (list == 0)
          {
            for (int index = 0; (long) index < (long) pnProcInfo; ++index)
              processes.Add(rgAffectedApps[index].Process.ProcessId);
          }
        }
        if (list != 0)
          throw new Win32Exception();
      }
      finally
      {
        NativeMethods.RmEndSession(pSessionHandle);
      }
      return processes;
    }
  }
}

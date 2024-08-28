// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.SystemHelper
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  public static class SystemHelper
  {
    public static bool IsWindowsVistaOrNewer()
    {
      OperatingSystem osVersion = Environment.OSVersion;
      Version version = osVersion.Version;
      return osVersion.Platform == PlatformID.Win32NT && version.Major >= 6;
    }

    public static List<int> GetProcesses()
    {
      List<int> processes = new List<int>();
      foreach (Process process in Process.GetProcesses())
        processes.Add(process.Id);
      return processes;
    }
  }
}

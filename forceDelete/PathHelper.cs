// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.PathHelper
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  internal static class PathHelper
  {
    private const int MaxPathLength = 260;
    private const string NetworkDevicePrefix = "\\Device\\LanmanRedirector\\";
    private static object syncObject = new object();
    private static Dictionary<string, string> deviceMap;

    public static string ConvertDevicePathToDosPath(string devicePath)
    {
      PathHelper.InitializeDeviceMapIfNeeded();
      int num = devicePath.Length;
      while (num > 0 && (num = devicePath.LastIndexOf('\\', num - 1)) != -1)
      {
        string empty = string.Empty;
        if (PathHelper.deviceMap.TryGetValue(devicePath.Substring(0, num), out empty))
          return empty + devicePath.Substring(num);
      }
      return string.Empty;
    }

    private static void InitializeDeviceMapIfNeeded()
    {
      lock (PathHelper.syncObject)
      {
        if (PathHelper.deviceMap != null)
          return;
        Dictionary<string, string> dictionary = PathHelper.BuildDeviceMap();
        Interlocked.CompareExchange<Dictionary<string, string>>(ref PathHelper.deviceMap, dictionary, (Dictionary<string, string>) null);
      }
    }

    private static Dictionary<string, string> BuildDeviceMap()
    {
      string[] logicalDrives = Environment.GetLogicalDrives();
      Dictionary<string, string> dictionary = new Dictionary<string, string>(logicalDrives.Length);
      StringBuilder lpTargetPath = new StringBuilder(260);
      foreach (string str in logicalDrives)
      {
        string lpDeviceName = str.Substring(0, 2);
        NativeMethods.QueryDosDevice(lpDeviceName, lpTargetPath, 260);
        dictionary.Add(PathHelper.NormalizeDeviceName(lpTargetPath.ToString()), lpDeviceName);
      }
      dictionary.Add("\\Device\\LanmanRedirector\\".Substring(0, "\\Device\\LanmanRedirector\\".Length - 1), "\\");
      return dictionary;
    }

    private static string NormalizeDeviceName(string deviceName)
    {
      return string.Compare(deviceName, 0, "\\Device\\LanmanRedirector\\", 0, "\\Device\\LanmanRedirector\\".Length, StringComparison.InvariantCulture) == 0 ? "\\Device\\LanmanRedirector\\" + deviceName.Substring(deviceName.IndexOf('\\', "\\Device\\LanmanRedirector\\".Length) + 1) : deviceName;
    }
  }
}

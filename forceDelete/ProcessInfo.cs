// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.ProcessInfo
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System.Runtime.InteropServices;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  internal struct ProcessInfo
  {
    public UniqueProcess Process;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string ApplicationName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
    public string ServiceShortName;
    public ApplicationType ApplicationType;
    public uint AppStatus;
    public uint TSSessionId;
    [MarshalAs(UnmanagedType.Bool)]
    public bool Restartable;
  }
}

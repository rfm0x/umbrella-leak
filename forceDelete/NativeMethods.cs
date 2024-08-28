// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.NativeMethods
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  internal static class NativeMethods
  {
    [DllImport("ntdll.dll")]
    internal static extern NtStatus NtQuerySystemInformation(
      [In] SystemInformationClass SystemInformationClass,
      [In] IntPtr SystemInformation,
      [In] int SystemInformationLength,
      out int ReturnLength);

    [DllImport("ntdll.dll")]
    internal static extern NtStatus NtQueryObject(
      [In] IntPtr Handle,
      [In] ObjectInformationClass ObjectInformationClass,
      [In] IntPtr ObjectInformation,
      [In] int ObjectInformationLength,
      out int ReturnLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern SafeNativeHandle OpenProcess(
      [In] ProcessAccessRights dwDesiredAccess,
      [MarshalAs(UnmanagedType.Bool), In] bool bInheritHandle,
      [In] int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool DuplicateHandle(
      [In] IntPtr hSourceProcessHandle,
      [In] IntPtr hSourceHandle,
      [In] IntPtr hTargetProcessHandle,
      out SafeNativeHandle lpTargetHandle,
      [In] int dwDesiredAccess,
      [MarshalAs(UnmanagedType.Bool), In] bool bInheritHandle,
      [In] DuplicateHandleOptions dwOptions);

    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetCurrentProcess();

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern int GetProcessId([In] IntPtr Process);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool CloseHandle([In] IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern int QueryDosDevice(
      [In] string lpDeviceName,
      [Out] StringBuilder lpTargetPath,
      [In] int ucchMax);

    [DllImport("kernel32.dll")]
    internal static extern uint GetCurrentThreadId();

    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetCurrentThread();

    [DllImport("kernel32.dll")]
    internal static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);

    [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
    internal static extern int RmStartSession(
      out uint pSessionHandle,
      int dwSessionFlags,
      string strSessionKey);

    [DllImport("rstrtmgr.dll")]
    internal static extern int RmEndSession(uint pSessionHandle);

    [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
    internal static extern int RmRegisterResources(
      uint pSessionHandle,
      uint nFiles,
      string[] rgsFilenames,
      uint nApplications,
      UniqueProcess[] rgApplications,
      uint nServices,
      string[] rgsServiceNames);

    [DllImport("rstrtmgr.dll")]
    internal static extern int RmGetList(
      uint dwSessionHandle,
      out uint pnProcInfoNeeded,
      ref uint pnProcInfo,
      [In, Out] ProcessInfo[] rgAffectedApps,
      ref uint lpdwRebootReasons);
  }
}

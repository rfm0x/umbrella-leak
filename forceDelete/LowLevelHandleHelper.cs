// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.LowLevelHandleHelper
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  internal static class LowLevelHandleHelper
  {
    public static bool IsFileHandle(IntPtr handle, int processId)
    {
      return LowLevelHandleHelper.GetHandleTypeToken(handle, processId).Equals("File");
    }

    public static string GetFileNameFromHandle(IntPtr handle, int processId)
    {
      string fileName;
      if (!LowLevelHandleHelper.GetFileNameFromHandle(handle, processId, out fileName))
        return string.Empty;
      string dosPath = PathHelper.ConvertDevicePathToDosPath(fileName);
      return dosPath.Length == 0 ? fileName : dosPath;
    }

    private static string GetHandleTypeToken(IntPtr handle, int processId)
    {
      IntPtr currentProcess = NativeMethods.GetCurrentProcess();
      bool flag = processId != NativeMethods.GetProcessId(currentProcess);
      SafeNativeHandle safeNativeHandle = (SafeNativeHandle) null;
      SafeNativeHandle lpTargetHandle = (SafeNativeHandle) null;
      try
      {
        if (flag)
        {
          safeNativeHandle = NativeMethods.OpenProcess(ProcessAccessRights.ProcessDuplicateHandle, true, processId);
          if (NativeMethods.DuplicateHandle(safeNativeHandle.DangerousGetHandle(), handle, currentProcess, out lpTargetHandle, 0, false, DuplicateHandleOptions.SameAccess))
            handle = lpTargetHandle.DangerousGetHandle();
        }
        return LowLevelHandleHelper.GetHandleTypeToken(handle);
      }
      finally
      {
        if (flag)
        {
          safeNativeHandle?.Close();
          lpTargetHandle?.Close();
        }
      }
    }

    private static string GetHandleTypeToken(IntPtr handle)
    {
      int ReturnLength;
      if (NativeMethods.NtQueryObject(handle, ObjectInformationClass.ObjectTypeInformation, IntPtr.Zero, 0, out ReturnLength) == NtStatus.InvalidHandle)
        return string.Empty;
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          num = Marshal.AllocHGlobal(ReturnLength);
        }
        if (NativeMethods.NtQueryObject(handle, ObjectInformationClass.ObjectTypeInformation, num, ReturnLength, out ReturnLength) == NtStatus.Success)
          return Marshal.PtrToStringUni((IntPtr) ((long) num + 96L));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
      return string.Empty;
    }

    private static bool GetFileNameFromHandle(IntPtr handle, int processId, out string fileName)
    {
      IntPtr currentProcess = NativeMethods.GetCurrentProcess();
      bool flag = processId != NativeMethods.GetProcessId(currentProcess);
      SafeNativeHandle safeNativeHandle = (SafeNativeHandle) null;
      SafeNativeHandle lpTargetHandle = (SafeNativeHandle) null;
      try
      {
        if (flag)
        {
          safeNativeHandle = NativeMethods.OpenProcess(ProcessAccessRights.ProcessDuplicateHandle, true, processId);
          if (NativeMethods.DuplicateHandle(safeNativeHandle.DangerousGetHandle(), handle, currentProcess, out lpTargetHandle, 0, false, DuplicateHandleOptions.SameAccess))
            handle = lpTargetHandle.DangerousGetHandle();
        }
        return LowLevelHandleHelper.GetFileNameFromHandle(handle, out fileName, 200);
      }
      finally
      {
        if (flag)
        {
          safeNativeHandle?.Close();
          lpTargetHandle?.Close();
        }
      }
    }

    private static bool GetFileNameFromHandle(IntPtr handle, out string fileName, int wait)
    {
      LowLevelHandleHelper.FileNameFromHandleWorker fromHandleWorker = new LowLevelHandleHelper.FileNameFromHandleWorker(handle);
      Thread thread = new Thread(new ThreadStart(fromHandleWorker.DoWork));
      thread.Start();
      if (thread.Join(wait))
      {
        fileName = fromHandleWorker.FileName;
        return true;
      }
      fileName = string.Empty;
      return false;
    }

    private static bool GetFileNameFromHandle(IntPtr handle, out string fileName)
    {
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        int ReturnLength = 512;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          num = Marshal.AllocHGlobal(ReturnLength);
        }
        NtStatus ntStatus = NativeMethods.NtQueryObject(handle, ObjectInformationClass.ObjectNameInformation, num, ReturnLength, out ReturnLength);
        if (ntStatus == NtStatus.BufferOverflow)
        {
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
          }
          finally
          {
            Marshal.FreeHGlobal(num);
            num = Marshal.AllocHGlobal(ReturnLength);
          }
          ntStatus = NativeMethods.NtQueryObject(handle, ObjectInformationClass.ObjectNameInformation, num, ReturnLength, out ReturnLength);
        }
        if (ntStatus == NtStatus.Success)
        {
          fileName = Marshal.PtrToStringUni((IntPtr) ((int) num + 8), (ReturnLength - 9) / 2);
          return fileName.Length != 0;
        }
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
      fileName = string.Empty;
      return false;
    }

    private class FileNameFromHandleWorker
    {
      private SafeNativeHandle nativeThreadId;

      public FileNameFromHandleWorker(IntPtr handle)
      {
        this.Handle = handle;
        this.FileName = string.Empty;
        this.RetValue = false;
        this.nativeThreadId = (SafeNativeHandle) null;
      }

      public IntPtr Handle { get; private set; }

      public string FileName { get; set; }

      public bool RetValue { get; set; }

      public void DoWork()
      {
        Thread.BeginThreadAffinity();
        NativeMethods.GetCurrentProcess();
        NativeMethods.GetCurrentThread();
        string fileName;
        bool fileNameFromHandle = LowLevelHandleHelper.GetFileNameFromHandle(this.Handle, out fileName);
        this.FileName = fileName;
        this.RetValue = fileNameFromHandle;
      }

      public bool Abort()
      {
        return this.nativeThreadId != null && !this.nativeThreadId.IsInvalid && NativeMethods.TerminateThread(this.nativeThreadId.DangerousGetHandle(), 0U);
      }
    }
  }
}

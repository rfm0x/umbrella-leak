// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.FileDeleter
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UmbrellaDesign.forceDelete;

#nullable disable
namespace UmbrellaDesign
{
  internal sealed class FileDeleter
  {
    private ProcessHandleSnapshot snapshot;

    public FileDeleter() => this.snapshot = new ProcessHandleSnapshot();

    public bool Delete(string fileName)
    {
      try
      {
        string fullPath = Path.GetFullPath(fileName);
        this.RemoveReadOnlyAttribute(fullPath);
        this.DeleteTheFile(fullPath);
        return true;
      }
      catch
      {
      }
      return false;
    }

    public void Unlocker(string fileName)
    {
      try
      {
        this.TryHarderDelete(fileName, false);
      }
      catch
      {
      }
    }

    private bool TryHarderDelete(string absoluteFileName, bool finaly_delete)
    {
      foreach (int processId in SystemHelper.IsWindowsVistaOrNewer() ? UsedFileDetector.GetProcesses(absoluteFileName) : SystemHelper.GetProcesses())
      {
        foreach (IntPtr handle in this.snapshot.GetHandles(processId))
        {
          string fileNameFromHandle = LowLevelHandleHelper.GetFileNameFromHandle(handle, processId);
          if (absoluteFileName.Equals(fileNameFromHandle, StringComparison.OrdinalIgnoreCase))
          {
            this.CloseHandleInRemoteProcess(processId, handle);
            break;
          }
        }
        Process currentProcess = Process.GetCurrentProcess();
        Process processById = Process.GetProcessById(processId);
        foreach (ProcessModule module in (ReadOnlyCollectionBase) processById.Modules)
        {
          if (absoluteFileName.Equals(module.FileName, StringComparison.OrdinalIgnoreCase) && currentProcess.Id != processId)
          {
            processById.Kill();
            processById.WaitForExit();
          }
        }
      }
      if (finaly_delete)
        File.Delete(absoluteFileName);
      return true;
    }

    private bool RemoveReadOnlyAttribute(string fileName)
    {
      FileAttributes attributes = File.GetAttributes(fileName);
      if ((attributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
        return true;
      File.SetAttributes(fileName, attributes & ~FileAttributes.ReadOnly);
      return true;
    }

    private bool DeleteTheFile(string absoluteFileName)
    {
      try
      {
        File.Delete(absoluteFileName);
        return true;
      }
      catch
      {
        return this.TryHarderDelete(absoluteFileName, true);
      }
    }

    private void CloseHandleInRemoteProcess(int processId, IntPtr handle)
    {
      SafeNativeHandle safeNativeHandle = NativeMethods.OpenProcess(ProcessAccessRights.ProcessDuplicateHandle, true, processId);
      if (safeNativeHandle.IsInvalid)
        return;
      IntPtr handle1 = safeNativeHandle.DangerousGetHandle();
      IntPtr currentProcess = NativeMethods.GetCurrentProcess();
      SafeNativeHandle lpTargetHandle = (SafeNativeHandle) null;
      if (NativeMethods.DuplicateHandle(handle1, handle, currentProcess, out lpTargetHandle, 0, false, DuplicateHandleOptions.CloseSource))
        NativeMethods.CloseHandle(lpTargetHandle.DangerousGetHandle());
      NativeMethods.CloseHandle(handle1);
    }
  }
}

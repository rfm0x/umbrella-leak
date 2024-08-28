// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.ProcessHandleSnapshot
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  internal class ProcessHandleSnapshot
  {
    private Dictionary<int, List<IntPtr>> snapshot;

    public ProcessHandleSnapshot()
    {
      this.snapshot = new Dictionary<int, List<IntPtr>>();
      IEnumerator<SystemHandleEntry> enumerator = new OpenFilesEnumerator().GetEnumerator();
      while (enumerator.MoveNext())
      {
        SystemHandleEntry current = enumerator.Current;
        int ownerPid = current.OwnerPid;
        IntPtr handleValue = (IntPtr) (int) current.HandleValue;
        if (LowLevelHandleHelper.IsFileHandle(handleValue, ownerPid) && !this.IgnoreSystemHandleEntry(current))
        {
          if (!this.snapshot.ContainsKey(ownerPid))
            this.snapshot.Add(ownerPid, new List<IntPtr>());
          List<IntPtr> numList;
          if (this.snapshot.TryGetValue(ownerPid, out numList))
            numList.Add(handleValue);
        }
      }
    }

    public ReadOnlyCollection<IntPtr> GetHandles(Process p) => this.GetHandles(p.Id);

    public ReadOnlyCollection<IntPtr> GetHandles(int processId)
    {
      List<IntPtr> list;
      return this.snapshot.TryGetValue(processId, out list) ? new ReadOnlyCollection<IntPtr>((IList<IntPtr>) list) : new ReadOnlyCollection<IntPtr>((IList<IntPtr>) new List<IntPtr>(0));
    }

    private bool IgnoreSystemHandleEntry(SystemHandleEntry entry)
    {
      if (SystemHelper.IsWindowsVistaOrNewer())
        return false;
      return entry.AccessMask == 1180063 || entry.AccessMask == 1704351 || entry.AccessMask == 1180041 || entry.AccessMask == 1048576;
    }
  }
}

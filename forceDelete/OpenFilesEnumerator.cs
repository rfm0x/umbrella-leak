// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.OpenFilesEnumerator
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  internal sealed class OpenFilesEnumerator : IEnumerable<SystemHandleEntry>, IEnumerable
  {
    public IEnumerator<SystemHandleEntry> GetEnumerator()
    {
      int length = 65536;
      NtStatus ret;
      do
      {
        IntPtr ptr = IntPtr.Zero;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
          }
          finally
          {
            ptr = Marshal.AllocHGlobal(length);
          }
          int ReturnLength;
          ret = NativeMethods.NtQuerySystemInformation(SystemInformationClass.SystemHandleInformation, ptr, length, out ReturnLength);
          switch (ret)
          {
            case NtStatus.InfoLengthMismatch:
              length = ReturnLength + (int) ushort.MaxValue & -65536;
              goto label_12;
            case NtStatus.Success:
              int handleCount = IntPtr.Size == 4 ? Marshal.ReadInt32(ptr) : (int) Marshal.ReadInt64(ptr);
              int offset = IntPtr.Size;
              int size = Marshal.SizeOf(typeof (SystemHandleEntry));
              for (int i = 0; i < handleCount; ++i)
              {
                yield return (SystemHandleEntry) Marshal.PtrToStructure((IntPtr) ((long) ptr + (long) offset), typeof (SystemHandleEntry));
                offset += size;
              }
              break;
          }
        }
        finally
        {
          Marshal.FreeHGlobal(ptr);
        }
label_12:;
      }
      while (ret == NtStatus.InfoLengthMismatch);
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}

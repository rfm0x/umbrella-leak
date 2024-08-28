// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.SafeNativeHandle
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using Microsoft.Win32.SafeHandles;
using System.Security.Permissions;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
  internal sealed class SafeNativeHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeNativeHandle()
      : base(true)
    {
    }

    protected override bool ReleaseHandle() => NativeMethods.CloseHandle(this.handle);
  }
}

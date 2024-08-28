// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.SystemHandleEntry
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System;
using System.Text;

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  internal struct SystemHandleEntry
  {
    public int OwnerPid;
    public byte ObjectType;
    public byte HandleFlags;
    public short HandleValue;
    public IntPtr ObjectPointer;
    public int AccessMask;

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("OwnerPid={0}, ", (object) this.OwnerPid);
      stringBuilder.AppendFormat("ObjectType={0:X02}, ", (object) this.ObjectType);
      stringBuilder.AppendFormat("HandleFlags={0:X02}, ", (object) this.HandleFlags);
      stringBuilder.AppendFormat("HandleValue={0}, ", (object) this.HandleValue);
      stringBuilder.AppendFormat("AccessMask={0:X04}", (object) this.AccessMask);
      return stringBuilder.ToString();
    }
  }
}

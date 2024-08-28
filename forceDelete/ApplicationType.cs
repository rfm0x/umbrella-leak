// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.forceDelete.ApplicationType
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

#nullable disable
namespace UmbrellaDesign.forceDelete
{
  internal enum ApplicationType
  {
    RmUnknownApp = 0,
    RmMainWindow = 1,
    RmOtherWindow = 2,
    RmService = 3,
    RmExplorer = 4,
    RmConsole = 5,
    RmCritical = 1000, // 0x000003E8
  }
}

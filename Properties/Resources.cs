// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.Properties.Resources
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace UmbrellaDesign.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (UmbrellaDesign.Properties.Resources.resourceMan == null)
          UmbrellaDesign.Properties.Resources.resourceMan = new ResourceManager("UmbrellaDesign.Properties.Resources", typeof (UmbrellaDesign.Properties.Resources).Assembly);
        return UmbrellaDesign.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => UmbrellaDesign.Properties.Resources.resourceCulture;
      set => UmbrellaDesign.Properties.Resources.resourceCulture = value;
    }
  }
}

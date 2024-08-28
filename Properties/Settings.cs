// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.Properties.Settings
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace UmbrellaDesign.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.6.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string login
    {
      get => (string) this[nameof (login)];
      set => this[nameof (login)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string password
    {
      get => (string) this[nameof (password)];
      set => this[nameof (password)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string custom_params
    {
      get => (string) this[nameof (custom_params)];
      set => this[nameof (custom_params)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string Version
    {
      get => (string) this[nameof (Version)];
      set => this[nameof (Version)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string cheat_id
    {
      get => (string) this[nameof (cheat_id)];
      set => this[nameof (cheat_id)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("-1")]
    public int server_id
    {
      get => (int) this[nameof (server_id)];
      set => this[nameof (server_id)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool hwid_spoofer_enabled
    {
      get => (bool) this[nameof (hwid_spoofer_enabled)];
      set => this[nameof (hwid_spoofer_enabled)] = (object) value;
    }
  }
}

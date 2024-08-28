// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.Window1
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using FontAwesome.WPF;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using UmbrellaDesign.Properties;

#nullable disable
namespace UmbrellaDesign
{
  public class Window1 : Window, IComponentConnector, IStyleConnector
  {
    private const string loaderDllName = "LoaderKernel.dll";
    private bool subscriber;
    private DispatcherTimer log_timer;
    private Dictionary<int, Window1.cheat_info> kv_launch = new Dictionary<int, Window1.cheat_info>();
    public static System.Timers.Timer save_timer = (System.Timers.Timer) null;
    public static string[] names_Type = new string[6]
    {
      "both",
      "enemy",
      "ally",
      "snipe_both",
      "snipe_enemy",
      "snipe_ally"
    };
    internal Grid main_form;
    internal Button close_btn;
    internal Button minimaze_btn;
    internal Image logo_image;
    internal TextBlock first_caption;
    internal TextBlock second_caption;
    internal Grid black_list_grid;
    internal DataGrid dataGrid;
    internal Grid main_grid;
    internal ListBox cheat_list_box;
    internal TextBox startup_dota_parameters;
    internal CheckBox HwidSpooferCheckbox;
    internal Grid cheat_log;
    internal RichTextBox log_window;
    internal ImageBrush controlPanel_logo;
    internal Grid m_activate_promo;
    internal ImageBrush key_logo;
    internal Grid m_exit;
    internal ImageBrush exit_logo;
    internal TextBlock m_name;
    internal TextBlock m_expire;
    internal StackPanel sp_status;
    internal TextBlock m_status_label;
    internal TextBlock m_status;
    internal StackPanel sp_update_data;
    internal TextBlock m_update_data;
    internal Button start_game_btn;
    internal ImageAwesome loading_ico;
    internal TextBlock launch_button;
    private bool _contentLoaded;

    [DllImport("LoaderKernel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr logs_handler();

    public static string logs_handler_get() => Marshal.PtrToStringAnsi(Window1.logs_handler());

    [DllImport("LoaderKernel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr get_cheatList();

    public static string cheatList_get() => Marshal.PtrToStringAnsi(Window1.get_cheatList());

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern bool launch_game(string custom_params, string cheat_id, string game);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern bool launch_game_v2(string custom_params, string cheat_id, string game);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern bool launch_task(
      string target_process,
      string cheat_id,
      int flags,
      string custom_param);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern bool wait_process(string target_process, int need_count, int timeout);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int check_driver(string _target, bool check);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int kill_process(string _target, bool check);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int check_connect();

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int connect_client();

    [DllImport("LoaderKernel.dll", EntryPoint = "refresh_session", CallingConvention = CallingConvention.StdCall)]
    private static extern int refresh_session_dll();

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int auth(string login, string password);

    private void loadCustomSettings()
    {
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        if (MainWindow.json_config_data == null)
          return;
        JObject jsonConfigData = MainWindow.json_config_data;
        Customization.loadIcon((Window) this, "ico", jsonConfigData);
        Customization.loadImage(this.logo_image, "logo_cp", jsonConfigData);
        Customization.loadEllipseImage(this.controlPanel_logo, "default_avatar", jsonConfigData);
        Customization.loadTextBlock(this.first_caption, "first_caption", jsonConfigData);
        Customization.loadTextBlock(this.second_caption, "second_caption", jsonConfigData);
      }));
    }

    private void log_handler(object sender, EventArgs e)
    {
      string log_item = Window1.logs_handler_get();
      if (log_item == null)
        return;
      log_item = Encoding.ASCII.GetString(Convert.FromBase64String(log_item));
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        if (log_item.ToLower().IndexOf("hwid") != -1 || log_item.ToLower().IndexOf("error") != -1)
        {
          if (log_item.ToLower().IndexOf("hwid") != -1)
          {
            this.m_expire.Text = "Invalid HWID";
            this.m_expire.Foreground = (Brush) new SolidColorBrush(Color.FromArgb((byte) 190, (byte) 240, (byte) 0, (byte) 0));
            this.m_expire.FontWeight = FontWeights.Normal;
          }
          TextRange textRange = new TextRange(this.log_window.Document.ContentEnd, this.log_window.Document.ContentEnd);
          textRange.Text = log_item + "\n";
          DependencyProperty foregroundProperty = TextElement.ForegroundProperty;
          textRange.ApplyPropertyValue(TextElement.ForegroundProperty, (object) Brushes.Red);
          textRange.ApplyPropertyValue(TextElement.FontWeightProperty, (object) FontWeights.Normal);
        }
        else
          this.log_window.AppendText(log_item + "\n");
      }));
    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
      return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();
    }

    private void updateSubData()
    {
      string CheatName1 = "Umbrella [DotA2]";
      string CheatName2 = "AquaHook [DotA2]";
      if (MainWindow.json_config_data != null)
      {
        object obj1 = (object) ((JToken) MainWindow.json_config_data).SelectToken("NamesHack.CheatName1");
        // ISSUE: reference to a compiler-generated field
        if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Window1.\u003C\u003Eo__23.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target1 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p1 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Window1.\u003C\u003Eo__23.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__0.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__0, obj1, (object) null);
        string str1;
        if (!target1((CallSite) p1, obj2))
        {
          str1 = CheatName1;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Window1.\u003C\u003Eo__23.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          str1 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__2.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__2, obj1);
        }
        CheatName1 = str1;
        object obj3 = (object) ((JToken) MainWindow.json_config_data).SelectToken("NamesHack.CheatName2");
        // ISSUE: reference to a compiler-generated field
        if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Window1.\u003C\u003Eo__23.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target2 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p4 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__4;
        // ISSUE: reference to a compiler-generated field
        if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Window1.\u003C\u003Eo__23.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__3.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__3, obj3, (object) null);
        string str2;
        if (!target2((CallSite) p4, obj4))
        {
          str2 = CheatName2;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Window1.\u003C\u003Eo__23.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          str2 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__5.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__5, obj3);
        }
        CheatName2 = str2;
      }
      string s = Window1.cheatList_get();
      string str3;
      try
      {
        JObject.Parse(s);
        str3 = s;
      }
      catch (Exception ex)
      {
        str3 = Encoding.ASCII.GetString(Convert.FromBase64String(s));
      }
      JObject json_cheat_list = JObject.Parse(str3);
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        DateTime dateTime1;
        if (JToken.op_Explicit(json_cheat_list["exp"]) == 0L)
        {
          this.subscriber = true;
          this.m_expire.Text = "Expire date: Never";
        }
        else if (JToken.op_Explicit(json_cheat_list["exp"]) == -1L)
        {
          this.m_expire.Text = "No subscription";
        }
        else
        {
          this.subscriber = true;
          TextBlock expire = this.m_expire;
          dateTime1 = Window1.UnixTimeStampToDateTime((double) JToken.op_Explicit(json_cheat_list["exp"]));
          string str4 = "Expire date: " + dateTime1.ToString("d");
          expire.Text = str4;
        }
        if (JToken.op_Explicit(json_cheat_list["avatar"]) != "null")
        {
          BitmapImage bitmapImage = new BitmapImage();
          bitmapImage.BeginInit();
          bitmapImage.UriSource = new Uri(JToken.op_Explicit(json_cheat_list["avatar"]));
          bitmapImage.EndInit();
          this.controlPanel_logo.ImageSource = (ImageSource) bitmapImage;
        }
        this.m_name.Text = MainWindow.login;
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToLower();
        foreach (JToken jtoken in (JArray) json_cheat_list["list"])
        {
          object obj5 = (object) jtoken.SelectToken("name");
          object obj6 = (object) jtoken.SelectToken("key");
          object obj7 = (object) jtoken.SelectToken("game");
          object obj8 = (object) jtoken.SelectToken("last_update");
          object obj9 = (object) jtoken.SelectToken("status");
          object obj10 = (object) jtoken.SelectToken("is_free");
          // ISSUE: reference to a compiler-generated field
          if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__12 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Window1.\u003C\u003Eo__23.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target3 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__12.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p12 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__12;
          // ISSUE: reference to a compiler-generated field
          if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Window1.\u003C\u003Eo__23.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj11 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__6.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__6, obj10, (object) null);
          // ISSUE: reference to a compiler-generated field
          if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__9 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Window1.\u003C\u003Eo__23.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          object obj12;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (!Window1.\u003C\u003Eo__23.\u003C\u003Ep__9.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__9, obj11))
          {
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__8 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool, object> target4 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__8.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool, object>> p8 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__8;
            object obj13 = obj11;
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__7 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (bool), typeof (Window1)));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            int num = Window1.\u003C\u003Eo__23.\u003C\u003Ep__7.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__7, obj10) ? 1 : 0;
            obj12 = target4((CallSite) p8, obj13, num != 0);
          }
          else
            obj12 = obj11;
          object obj14 = obj12;
          // ISSUE: reference to a compiler-generated field
          if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__11 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Window1.\u003C\u003Eo__23.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          object obj15;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (!Window1.\u003C\u003Eo__23.\u003C\u003Ep__11.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__11, obj14))
          {
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__10 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            obj15 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__10.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__10, obj14, !MainWindow.display_free_products);
          }
          else
            obj15 = obj14;
          if (!target3((CallSite) p12, obj15))
          {
            if (MainWindow.json_config_data != null)
              ((JToken) MainWindow.json_config_data).SelectToken("first_caption.Text.value");
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__13 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (Window1)));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.cheat_list_box.Items.Add((object) Window1.\u003C\u003Eo__23.\u003C\u003Ep__13.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__13, obj5).Replace("CheatName1", CheatName1).Replace("CheatName2", CheatName2));
            Window1.cheat_info cheatInfo = new Window1.cheat_info();
            cheatInfo.useAntivac = false;
            ref Window1.cheat_info local1 = ref cheatInfo;
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__14 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            string str5 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__14.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__14, obj6);
            local1.key = str5;
            ref Window1.cheat_info local2 = ref cheatInfo;
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__15 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            string str6 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__15.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__15, obj7);
            local2.game = str6;
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__20 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__20 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool> target5 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__20.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool>> p20 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__20;
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__16 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj16 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__16.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__16, obj8, (object) null);
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__19 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__19 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            object obj17;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (!Window1.\u003C\u003Eo__23.\u003C\u003Ep__19.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__19, obj16))
            {
              // ISSUE: reference to a compiler-generated field
              if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__18 == null)
              {
                // ISSUE: reference to a compiler-generated field
                Window1.\u003C\u003Eo__23.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, bool, object> target6 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__18.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, bool, object>> p18 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__18;
              object obj18 = obj16;
              // ISSUE: reference to a compiler-generated field
              if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__17 == null)
              {
                // ISSUE: reference to a compiler-generated field
                Window1.\u003C\u003Eo__23.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              int num = Window1.\u003C\u003Eo__23.\u003C\u003Ep__17.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__17, obj8) != "" ? 1 : 0;
              obj17 = target6((CallSite) p18, obj18, num != 0);
            }
            else
              obj17 = obj16;
            int num1;
            if (!target5((CallSite) p20, obj17))
            {
              num1 = 0;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__21 == null)
              {
                // ISSUE: reference to a compiler-generated field
                Window1.\u003C\u003Eo__23.\u003C\u003Ep__21 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              num1 = int.Parse(Window1.\u003C\u003Eo__23.\u003C\u003Ep__21.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__21, obj8));
            }
            int num2 = num1;
            dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime dateTime2 = dateTime1.AddSeconds((double) num2);
            cheatInfo.update_date = dateTime2;
            ref Window1.cheat_info local3 = ref cheatInfo;
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__22 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__22 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            string str7 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__22.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__22, obj9);
            local3.status = str7;
            object obj19 = (object) jtoken.SelectToken("os_bit");
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__23 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__23 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj20 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__23.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__23, obj19, (object) null);
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__27 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__27 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (!Window1.\u003C\u003Eo__23.\u003C\u003Ep__27.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__27, obj20))
            {
              // ISSUE: reference to a compiler-generated field
              if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__26 == null)
              {
                // ISSUE: reference to a compiler-generated field
                Window1.\u003C\u003Eo__23.\u003C\u003Ep__26 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, bool> target7 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__26.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, bool>> p26 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__26;
              // ISSUE: reference to a compiler-generated field
              if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__25 == null)
              {
                // ISSUE: reference to a compiler-generated field
                Window1.\u003C\u003Eo__23.\u003C\u003Ep__25 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, bool, object> target8 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__25.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, bool, object>> p25 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__25;
              object obj21 = obj20;
              // ISSUE: reference to a compiler-generated field
              if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__24 == null)
              {
                // ISSUE: reference to a compiler-generated field
                Window1.\u003C\u003Eo__23.\u003C\u003Ep__24 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              int num3 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__24.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__24, obj19) == "null" ? 1 : 0;
              object obj22 = target8((CallSite) p25, obj21, num3 != 0);
              if (!target7((CallSite) p26, obj22))
              {
                ref Window1.cheat_info local4 = ref cheatInfo;
                // ISSUE: reference to a compiler-generated field
                if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__28 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  Window1.\u003C\u003Eo__23.\u003C\u003Ep__28 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                string str8 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__28.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__28, obj19);
                local4.os_bit = str8;
                goto label_74;
              }
            }
            cheatInfo.os_bit = MainWindow.GetOSBit();
label_74:
            if (cheatInfo.game == "custom_rules")
            {
              cheatInfo.custom_Rules = new List<Window1.custom_rule>();
              JArray jarray = (JArray) jtoken.SelectToken("custom_rules");
              int num4 = 0;
              foreach (JToken child in ((JToken) jarray).Children())
              {
                Window1.custom_rule customRule = new Window1.custom_rule();
                customRule.name = JToken.op_Explicit(child.SelectToken("name"));
                customRule.type = JToken.op_Explicit(child.SelectToken("type"));
                customRule.step = num4++;
                object obj23 = (object) child.SelectToken("inject_flags");
                // ISSUE: reference to a compiler-generated field
                if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__30 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  Window1.\u003C\u003Eo__23.\u003C\u003Ep__30 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                Func<CallSite, object, bool> target9 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__30.Target;
                // ISSUE: reference to a compiler-generated field
                CallSite<Func<CallSite, object, bool>> p30 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__30;
                // ISSUE: reference to a compiler-generated field
                if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__29 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  Window1.\u003C\u003Eo__23.\u003C\u003Ep__29 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj24 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__29.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__29, obj23, (object) null);
                if (target9((CallSite) p30, obj24))
                {
                  ref Window1.custom_rule local5 = ref customRule;
                  // ISSUE: reference to a compiler-generated field
                  if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__31 == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    Window1.\u003C\u003Eo__23.\u003C\u003Ep__31 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
                  }
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  int num5 = int.Parse(Window1.\u003C\u003Eo__23.\u003C\u003Ep__31.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__31, obj23));
                  local5.inject_flags = num5;
                }
                else
                  customRule.inject_flags = 32772;
                customRule.target_process = JToken.op_Explicit(child.SelectToken("target_process"));
                if (customRule.target_process == "_antivac_")
                  cheatInfo.useAntivac = true;
                customRule.hash = JToken.op_Explicit(child.SelectToken("key"));
                object obj25 = (object) child.SelectToken("custom_param");
                // ISSUE: reference to a compiler-generated field
                if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__33 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  Window1.\u003C\u003Eo__23.\u003C\u003Ep__33 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                Func<CallSite, object, bool> target10 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__33.Target;
                // ISSUE: reference to a compiler-generated field
                CallSite<Func<CallSite, object, bool>> p33 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__33;
                // ISSUE: reference to a compiler-generated field
                if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__32 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  Window1.\u003C\u003Eo__23.\u003C\u003Ep__32 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Window1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj26 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__32.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__32, obj25, (object) null);
                if (target10((CallSite) p33, obj26))
                {
                  ref Window1.custom_rule local6 = ref customRule;
                  // ISSUE: reference to a compiler-generated field
                  if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__34 == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    Window1.\u003C\u003Eo__23.\u003C\u003Ep__34 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
                  }
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  string str9 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__34.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__34, obj25);
                  local6.custom_param = str9;
                }
                else
                  customRule.custom_param = "";
                cheatInfo.custom_Rules.Add(customRule);
              }
            }
            this.kv_launch[this.cheat_list_box.Items.Count - 1] = cheatInfo;
            string cheatId = Settings.Default.cheat_id;
            // ISSUE: reference to a compiler-generated field
            if (Window1.\u003C\u003Eo__23.\u003C\u003Ep__35 == null)
            {
              // ISSUE: reference to a compiler-generated field
              Window1.\u003C\u003Eo__23.\u003C\u003Ep__35 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Window1)));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            string str10 = Window1.\u003C\u003Eo__23.\u003C\u003Ep__35.Target((CallSite) Window1.\u003C\u003Eo__23.\u003C\u003Ep__35, obj6);
            if (cheatId == str10)
            {
              this.sp_update_data.Visibility = Visibility.Visible;
              this.sp_status.Visibility = Visibility.Visible;
              TextBlock updateData = this.m_update_data;
              dateTime1 = cheatInfo.update_date.ToLocalTime();
              string str11 = dateTime1.ToString();
              updateData.Text = str11;
              this.m_status.Text = cheatInfo.status != null ? cheatInfo.status : "UnKnOwN";
              this.m_status_label.Visibility = Visibility.Visible;
              this.cheat_list_box.SelectedIndex = this.cheat_list_box.Items.Count - 1;
            }
          }
        }
        if (this.cheat_list_box.SelectedIndex != -1 || this.cheat_list_box.Items.Count <= 0)
          return;
        this.cheat_list_box.SelectedIndex = 0;
      }));
    }

    public static void save_blacklist(object source, ElapsedEventArgs e)
    {
      string contents = "";
      foreach (Window1.toxicData toxicGridItem in (Collection<Window1.toxicData>) Window1.toxicGridItems)
      {
        if (toxicGridItem.Type == -1)
          toxicGridItem.Type = 0;
        if (toxicGridItem.SteamID != null)
          contents = contents + toxicGridItem.SteamID.Trim() + "|" + Window1.names_Type[toxicGridItem.Type] + "@" + (toxicGridItem.Comment == null ? "" : toxicGridItem.Comment.Trim()) + "\n";
      }
      System.IO.File.WriteAllText("./toxics.data", contents);
      Window1.save_timer = (System.Timers.Timer) null;
    }

    public static void saveCollection()
    {
      if (Window1.save_timer != null)
      {
        Window1.save_timer.Stop();
        Window1.save_timer = (System.Timers.Timer) null;
      }
      Window1.save_timer = new System.Timers.Timer(800.0);
      Window1.save_timer.Elapsed += new ElapsedEventHandler(Window1.save_blacklist);
      Window1.save_timer.AutoReset = false;
      Window1.save_timer.Start();
    }

    private void loadBlackList()
    {
      try
      {
        StreamReader streamReader = new StreamReader("./toxics.data");
        string str1 = "";
        while (str1 != null)
        {
          str1 = streamReader.ReadLine();
          if (str1 != null)
          {
            int length = str1.IndexOf("|");
            int num1 = str1.IndexOf("@");
            string str2 = str1.Substring(0, length);
            string str3 = str1.Substring(length + 1, num1 - length - 1);
            string str4 = str1.Substring(num1 + 1);
            int num2;
            switch (str3)
            {
              case "both":
                num2 = 0;
                break;
              case "enemy":
                num2 = 1;
                break;
              case "ally":
                num2 = 2;
                break;
              case "snipe_both":
                num2 = 3;
                break;
              case "snipe_enemy":
                num2 = 4;
                break;
              case "snipe_ally":
                num2 = 5;
                break;
              default:
                num2 = 0;
                break;
            }
            if (str2 != null)
              Window1.toxicGridItems.Add(new Window1.toxicData()
              {
                SteamID = str2,
                Type = num2,
                Comment = str4
              });
          }
          else
            break;
        }
        streamReader.Close();
      }
      catch
      {
      }
    }

    private void on_load_component()
    {
      this.loadBlackList();
      this.dataGrid.ItemsSource = (IEnumerable) Window1.toxicGridItems;
      this.startup_dota_parameters.Text = Settings.Default.custom_params;
      this.HwidSpooferCheckbox.IsChecked = new bool?(Settings.Default.hwid_spoofer_enabled);
      string logsData = MainWindow.logs_data;
      char[] chArray = new char[1]{ '\n' };
      foreach (string str in logsData.Split(chArray))
      {
        if (str.Length != 0)
        {
          if (str.ToLower().IndexOf("hwid") != -1 || str.ToLower().IndexOf("error") != -1)
          {
            TextRange textRange = new TextRange(this.log_window.Document.ContentEnd, this.log_window.Document.ContentEnd);
            textRange.Text = str + "\n";
            DependencyProperty foregroundProperty = TextElement.ForegroundProperty;
            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, (object) Brushes.Red);
            textRange.ApplyPropertyValue(TextElement.FontWeightProperty, (object) FontWeights.Normal);
          }
          else
            this.log_window.AppendText(str + "\n");
        }
      }
      this.log_timer = new DispatcherTimer();
      this.log_timer.Tick += new EventHandler(this.log_handler);
      this.log_timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
      this.log_timer.Start();
      this.updateSubData();
      ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed;
    }

    public Window1()
    {
      this.InitializeComponent();
      this.loadCustomSettings();
      Window1.toxicGridItems = new ObservableCollection<Window1.toxicData>();
      this.Dispatcher.Invoke((Action) (() => this.on_load_component()));
    }

    public static ObservableCollection<Window1.toxicData> toxicGridItems { get; private set; }

    private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

    private void auth_client()
    {
      string s_login = MainWindow.login;
      string s_password = MainWindow.password;
      new Thread((ThreadStart) (() =>
      {
        if (Window1.connect_client() != 1 || Window1.auth(WebUtility.UrlEncode(s_login), WebUtility.UrlEncode(s_password)) != 0)
          return;
        this.updateSubData();
      })).Start();
    }

    private void refresh_session()
    {
      if (Window1.refresh_session_dll() != 0)
        return;
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        this.sp_update_data.Visibility = Visibility.Collapsed;
        this.sp_status.Visibility = Visibility.Collapsed;
      }));
      this.updateSubData();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      Window1 window1 = this;
      new PromoModal().ShowDialog();
      if (Window1.check_connect() != 1)
        return;
      // ISSUE: reference to a compiler-generated method
      window1.Dispatcher.InvokeAsync(new Action(window1.\u003CButton_Click\u003Eb__40_0));
      await Task.Delay(800);
      window1.refresh_session();
      // ISSUE: reference to a compiler-generated method
      window1.Dispatcher.InvokeAsync(new Action(window1.\u003CButton_Click\u003Eb__40_1));
    }

    private void Minimaze_btn_Click(object sender, RoutedEventArgs e)
    {
      this.WindowState = WindowState.Minimized;
    }

    public void exit()
    {
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        Settings.Default.Save();
        this.Close();
        Environment.Exit(0);
      }));
    }

    private void Close_btn_Click(object sender, RoutedEventArgs e) => this.exit();

    private void Log_window_TextChanged(object sender, TextChangedEventArgs e)
    {
      this.log_window.Focus();
      this.log_window.CaretPosition = this.log_window.Document.ContentEnd;
      this.log_window.ScrollToEnd();
    }

    private void startCustomRules(string param, Window1.cheat_info info)
    {
      this.Dispatcher.InvokeAsync<Task>((Func<Task>) (async () =>
      {
        if (Window1.check_connect() == 1)
        {
          info.custom_Rules.Sort((Comparison<Window1.custom_rule>) ((x, y) => x.step.CompareTo(y.step)));
          bool bRulesExecuting = true;
          bool bWriteStatus = false;
          bool bShowStep = true;
          bool bAutoClose = true;
          string sCustomLogPre = "";
          foreach (Window1.custom_rule customRule in info.custom_Rules)
          {
            Window1.custom_rule item = customRule;
            if (bRulesExecuting)
            {
              if (bShowStep)
                this.log_window.AppendText("Step " + item.step.ToString() + " started...\n");
              string type = item.type;
              if (type != null)
              {
                switch (type.Length)
                {
                  case 6:
                    switch (type[0])
                    {
                      case 'd':
                        if (type == "driver")
                          break;
                        goto label_79;
                      case 'i':
                        if (type == "inject")
                          break;
                        goto label_79;
                      default:
                        goto label_79;
                    }
                    if (item.target_process != null && item.hash != null)
                    {
                      bool flag = await Task.Run<bool>((Func<bool>) (() => Window1.launch_task(item.target_process, item.hash, item.inject_flags, item.custom_param)));
                      if (bWriteStatus && !(bWriteStatus = false))
                      {
                        this.log_window.AppendText(sCustomLogPre + flag.ToString() + "\n");
                        sCustomLogPre = "";
                      }
                      if (flag)
                      {
                        bRulesExecuting = false;
                        goto label_80;
                      }
                      else
                        goto label_80;
                    }
                    else
                    {
                      this.log_window.AppendText("#Unknown error[inject]#\n");
                      goto label_80;
                    }
                  case 10:
                    if (type == "custom_log")
                    {
                      TextRange textRange = new TextRange(this.log_window.Document.ContentEnd, this.log_window.Document.ContentEnd);
                      textRange.Text = item.target_process;
                      DependencyProperty foregroundProperty = TextElement.ForegroundProperty;
                      if (item.custom_param.Length > 0)
                        textRange.ApplyPropertyValue(TextElement.ForegroundProperty, (object) (Brush) new BrushConverter().ConvertFrom((object) item.custom_param));
                      textRange.ApplyPropertyValue(TextElement.FontWeightProperty, (object) FontWeights.Normal);
                      goto label_80;
                    }
                    else
                      break;
                  case 12:
                    switch (type[0])
                    {
                      case 'c':
                        if (type == "custom_sleep")
                        {
                          await Task.Delay(int.Parse(item.target_process));
                          goto label_80;
                        }
                        else
                          break;
                      case 'k':
                        if (type == "kill_process")
                        {
                          int num = await Task.Run<int>((Func<int>) (() => Window1.kill_process(item.target_process, false)));
                          if (bWriteStatus && !(bWriteStatus = false))
                          {
                            this.log_window.AppendText(sCustomLogPre + num.ToString() + "\n");
                            sCustomLogPre = "";
                          }
                          switch (num)
                          {
                            case -1:
                              bRulesExecuting = false;
                              goto label_80;
                            default:
                              goto label_80;
                          }
                        }
                        else
                          break;
                      case 'w':
                        if (type == "wait_process")
                        {
                          if (item.target_process != null)
                          {
                            int count_process = 1;
                            int timeout = 0;
                            string _target_process;
                            if (item.target_process.IndexOf("#") != -1)
                            {
                              _target_process = item.target_process.Substring(0, item.target_process.IndexOf("#"));
                              string s = item.target_process.Substring(item.target_process.IndexOf("#") + 1);
                              if (s.IndexOf("@") != -1)
                              {
                                count_process = int.Parse(s.Substring(0, s.IndexOf("@")));
                                timeout = int.Parse(s.Substring(s.IndexOf("@") + 1));
                              }
                              else
                                count_process = int.Parse(s);
                            }
                            else
                              _target_process = item.target_process;
                            string _target_process_name = _target_process;
                            if (_target_process.IndexOf("|") != -1)
                            {
                              _target_process_name = _target_process.Substring(_target_process.IndexOf("|") + 1);
                              _target_process = _target_process.Substring(0, _target_process.IndexOf("|"));
                            }
                            if (_target_process_name != "")
                              this.log_window.AppendText("Waiting for the start of the process: " + _target_process_name + (timeout == 0 ? "\n" : " for " + Convert.ToInt32(timeout / 1000).ToString()) + " seconds\n");
                            else
                              this.log_window.AppendText("Waiting for the start of the process\n");
                            if (await Task.Run<bool>((Func<bool>) (() => Window1.wait_process(_target_process, count_process, timeout))))
                            {
                              this.log_window.AppendText("Process found!\n");
                              goto label_80;
                            }
                            else
                            {
                              if (_target_process_name != "")
                                this.log_window.AppendText("Process " + _target_process_name + " was not found\n");
                              else
                                this.log_window.AppendText("Target process was not found\n");
                              bRulesExecuting = false;
                              goto label_80;
                            }
                          }
                          else
                            goto label_80;
                        }
                        else
                          break;
                    }
                    break;
                  case 13:
                    if (type == "launcher_kill")
                    {
                      this.exit();
                      return;
                    }
                    break;
                  case 14:
                    switch (type[0])
                    {
                      case 'c':
                        if (type == "custom_log_pre")
                        {
                          sCustomLogPre = item.target_process;
                          bWriteStatus = true;
                          goto label_80;
                        }
                        else
                          break;
                      case 'e':
                        if (type == "exclude_driver")
                        {
                          int num = await Task.Run<int>((Func<int>) (() => Window1.check_driver(item.target_process, true)));
                          if (bWriteStatus && !(bWriteStatus = false))
                          {
                            this.log_window.AppendText(sCustomLogPre + num.ToString() + "\n");
                            sCustomLogPre = "";
                          }
                          switch (num)
                          {
                            case -1:
                              this.log_window.AppendText("#Unknown error checking driver availability#\n");
                              bRulesExecuting = false;
                              goto label_80;
                            case 1:
                              this.log_window.AppendText("[ALERT] Need to unload driver: " + item.name + "\n");
                              bRulesExecuting = false;
                              goto label_80;
                            default:
                              goto label_80;
                          }
                        }
                        else
                          break;
                      case 'p':
                        if (type == "protect_inject")
                        {
                          if (item.target_process != null && item.hash != null)
                          {
                            bool flag = await Task.Run<bool>((Func<bool>) (() => Window1.launch_game(param != null ? param : "", item.hash, item.target_process)));
                            if (bWriteStatus && !(bWriteStatus = false))
                            {
                              this.log_window.AppendText(sCustomLogPre + flag.ToString() + "\n");
                              sCustomLogPre = "";
                            }
                            if (flag)
                            {
                              bRulesExecuting = false;
                              goto label_80;
                            }
                            else
                              goto label_80;
                          }
                          else
                          {
                            this.log_window.AppendText("#Unknown error[protect_inject]#\n");
                            goto label_80;
                          }
                        }
                        else
                          break;
                    }
                    break;
                  case 16:
                    if (type == "enabled_next_log")
                    {
                      bWriteStatus = true;
                      goto label_80;
                    }
                    else
                      break;
                  case 17:
                    if (type == "protect_inject_v2")
                    {
                      if (item.target_process != null && item.hash != null)
                      {
                        bool flag = await Task.Run<bool>((Func<bool>) (() => Window1.launch_game_v2(param != null ? param : "", item.hash, item.target_process)));
                        if (bWriteStatus && !(bWriteStatus = false))
                        {
                          this.log_window.AppendText(sCustomLogPre + flag.ToString() + "\n");
                          sCustomLogPre = "";
                        }
                        if (flag)
                        {
                          bRulesExecuting = false;
                          goto label_80;
                        }
                        else
                          goto label_80;
                      }
                      else
                      {
                        this.log_window.AppendText("#Unknown error[protect_inject]#\n");
                        goto label_80;
                      }
                    }
                    else
                      break;
                  case 19:
                    if (type == "toggle_visible_step")
                    {
                      bShowStep = item.target_process == "" ? !bShowStep : Convert.ToBoolean(item.target_process);
                      goto label_80;
                    }
                    else
                      break;
                  case 26:
                    if (type == "toggle_auto_close_launcher")
                    {
                      bAutoClose = item.target_process == "" ? !bAutoClose : Convert.ToBoolean(item.target_process);
                      goto label_80;
                    }
                    else
                      break;
                }
              }
label_79:
              this.log_window.AppendText("#Unknown Error#\n");
label_80:
              await Task.Delay(400);
            }
          }
          await Task.Delay(1000);
          if (bRulesExecuting & bAutoClose)
            this.exit();
          sCustomLogPre = (string) null;
        }
        else
        {
          new MainWindow().Show();
          this.Close();
        }
        this.Dispatcher.InvokeAsync((Action) (() =>
        {
          this.start_game_btn.IsEnabled = true;
          ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed;
        }));
      }));
    }

    private void startGame(string param, Window1.cheat_info info)
    {
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        if (Window1.check_connect() == 1)
        {
          new Thread((ThreadStart) (() =>
          {
            if (!Window1.launch_game(param != null ? param : "", info.key, info.game))
              this.exit();
            else
              this.Dispatcher.InvokeAsync((Action) (() =>
              {
                this.start_game_btn.IsEnabled = true;
                ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed;
              }));
          })).Start();
        }
        else
        {
          new MainWindow().Show();
          this.Close();
        }
      }));
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      if (this.cheat_list_box.SelectedIndex == -1)
        this.log_window.AppendText(this.subscriber ? "[Loader] Software not selected!\n" : "[Loader] No subscription or cheat not selected!\n");
      else if (this.kv_launch[this.cheat_list_box.SelectedIndex].os_bit != MainWindow.GetCurrentProcessBit())
      {
        this.log_window.AppendText("[Loader] Software requires " + this.kv_launch[this.cheat_list_box.SelectedIndex].os_bit + " OS!\n");
      }
      else
      {
        string text = this.startup_dota_parameters.Text;
        try
        {
          // ISSUE: variable of a compiler-generated type
          Settings settings = Settings.Default;
          bool? isChecked = this.HwidSpooferCheckbox.IsChecked;
          bool flag = true;
          int num = isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0;
          settings.hwid_spoofer_enabled = num != 0;
          Settings.Default.custom_params = text;
          Settings.Default.cheat_id = this.kv_launch[this.cheat_list_box.SelectedIndex].key.ToString();
          Settings.Default.Save();
        }
        catch
        {
        }
        this.Dispatcher.InvokeAsync((Action) (() =>
        {
          this.start_game_btn.IsEnabled = false;
          ((UIElement) this.loading_ico).Visibility = Visibility.Visible;
        }));
        if (this.kv_launch[this.cheat_list_box.SelectedIndex].game == "custom_rules")
          this.startCustomRules(text, this.kv_launch[this.cheat_list_box.SelectedIndex]);
        else
          this.startGame(text, this.kv_launch[this.cheat_list_box.SelectedIndex]);
      }
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        new MainWindow().Show();
        this.log_timer.Stop();
        this.Close();
      }));
    }

    private bool IsNull<T>(T value) => (object) value == null;

    private void Cheat_list_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      try
      {
        this.sp_update_data.Visibility = Visibility.Visible;
        this.sp_status.Visibility = Visibility.Visible;
        TextBlock updateData = this.m_update_data;
        Window1.cheat_info cheatInfo = this.kv_launch[this.cheat_list_box.SelectedIndex];
        string str = this.kv_launch[this.cheat_list_box.SelectedIndex].update_date.ToLocalTime().ToString();
        updateData.Text = str;
        this.m_status.Text = this.kv_launch[this.cheat_list_box.SelectedIndex].status != null ? this.kv_launch[this.cheat_list_box.SelectedIndex].status : "UnKnOwN";
        this.m_status_label.Visibility = Visibility.Visible;
        if (this.kv_launch[this.cheat_list_box.SelectedIndex].useAntivac)
          this.HwidSpooferCheckbox.Visibility = Visibility.Visible;
        else
          this.HwidSpooferCheckbox.Visibility = Visibility.Hidden;
      }
      catch
      {
      }
    }

    private void BtnView_Click(object sender, RoutedEventArgs e)
    {
      Window1.toxicGridItems.Remove(this.dataGrid.SelectedItem as Window1.toxicData);
      this.dataGrid.CanUserAddRows = false;
      this.dataGrid.CanUserAddRows = true;
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
      this.main_grid.Visibility = this.main_grid.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
      this.black_list_grid.Visibility = this.black_list_grid.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }

    private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
    {
      string str = "";
      foreach (Window1.toxicData toxicGridItem in (Collection<Window1.toxicData>) Window1.toxicGridItems)
        str = str + toxicGridItem.SteamID + "|" + toxicGridItem.Type.ToString() + "@" + toxicGridItem.Comment + "\n";
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      Window1.saveCollection();
    }

    private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
    }

    private void DataGrid_KeyUp(object sender, KeyEventArgs e) => Window1.saveCollection();

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/UmbrellaLoader;component/usercontrol.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.main_form = (Grid) target;
          break;
        case 2:
          ((UIElement) target).MouseLeftButtonDown += new MouseButtonEventHandler(this.Grid_MouseLeftButtonDown);
          break;
        case 3:
          this.close_btn = (Button) target;
          this.close_btn.Click += new RoutedEventHandler(this.Close_btn_Click);
          break;
        case 4:
          this.minimaze_btn = (Button) target;
          this.minimaze_btn.Click += new RoutedEventHandler(this.Minimaze_btn_Click);
          break;
        case 5:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click_3);
          break;
        case 6:
          this.logo_image = (Image) target;
          break;
        case 7:
          this.first_caption = (TextBlock) target;
          break;
        case 8:
          this.second_caption = (TextBlock) target;
          break;
        case 9:
          this.black_list_grid = (Grid) target;
          break;
        case 10:
          this.dataGrid = (DataGrid) target;
          this.dataGrid.SelectionChanged += new SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
          break;
        case 12:
          this.main_grid = (Grid) target;
          break;
        case 13:
          this.cheat_list_box = (ListBox) target;
          this.cheat_list_box.SelectionChanged += new SelectionChangedEventHandler(this.Cheat_list_box_SelectionChanged);
          break;
        case 14:
          this.startup_dota_parameters = (TextBox) target;
          break;
        case 15:
          this.HwidSpooferCheckbox = (CheckBox) target;
          break;
        case 16:
          this.cheat_log = (Grid) target;
          break;
        case 17:
          this.log_window = (RichTextBox) target;
          this.log_window.TextChanged += new TextChangedEventHandler(this.Log_window_TextChanged);
          break;
        case 18:
          this.controlPanel_logo = (ImageBrush) target;
          break;
        case 19:
          this.m_activate_promo = (Grid) target;
          break;
        case 20:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 21:
          this.key_logo = (ImageBrush) target;
          break;
        case 22:
          this.m_exit = (Grid) target;
          break;
        case 23:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click_2);
          break;
        case 24:
          this.exit_logo = (ImageBrush) target;
          break;
        case 25:
          this.m_name = (TextBlock) target;
          break;
        case 26:
          this.m_expire = (TextBlock) target;
          break;
        case 27:
          this.sp_status = (StackPanel) target;
          break;
        case 28:
          this.m_status_label = (TextBlock) target;
          break;
        case 29:
          this.m_status = (TextBlock) target;
          break;
        case 30:
          this.sp_update_data = (StackPanel) target;
          break;
        case 31:
          this.m_update_data = (TextBlock) target;
          break;
        case 32:
          this.start_game_btn = (Button) target;
          this.start_game_btn.Click += new RoutedEventHandler(this.Button_Click_1);
          break;
        case 33:
          this.loading_ico = (ImageAwesome) target;
          break;
        case 34:
          this.launch_button = (TextBlock) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IStyleConnector.Connect(int connectionId, object target)
    {
      if (connectionId != 11)
        return;
      ((ButtonBase) target).Click += new RoutedEventHandler(this.BtnView_Click);
    }

    public struct custom_rule
    {
      public string name;
      public string hash;
      public string target_process;
      public string custom_param;
      public string type;
      public int inject_flags;
      public int step;
    }

    public struct cheat_info
    {
      public string key;
      public string game;
      public string os_bit;
      public DateTime update_date;
      public string status;
      public bool useAntivac;
      public List<Window1.custom_rule> custom_Rules;
    }

    private enum TypeToInt
    {
      both,
      enemy,
      ally,
    }

    public class toxicData : INotifyPropertyChanged
    {
      private string _SteamID;
      private int _Type = -1;
      private string _Comment;

      public event PropertyChangedEventHandler PropertyChanged;

      public void OnPropertyChanged(string prop)
      {
        Window1.saveCollection();
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged == null)
          return;
        propertyChanged((object) this, new PropertyChangedEventArgs(prop));
      }

      public string SteamID
      {
        get => this._SteamID;
        set
        {
          if (this._SteamID == value)
            return;
          if (this._SteamID != null)
          {
            this._SteamID = value;
            this.OnPropertyChanged(nameof (SteamID));
          }
          else
            this._SteamID = value;
        }
      }

      public int Type
      {
        get => this._Type;
        set
        {
          if (this._Type == value)
            return;
          if (this._Type != -1)
          {
            this._Type = value;
            this.OnPropertyChanged(nameof (Type));
          }
          else
            this._Type = value;
        }
      }

      public string Comment
      {
        get => this._Comment;
        set
        {
          if (this._Comment == value)
            return;
          if (this._Comment != null)
          {
            this._Comment = value;
            this.OnPropertyChanged(nameof (Comment));
          }
          else
            this._Comment = value;
        }
      }
    }
  }
}

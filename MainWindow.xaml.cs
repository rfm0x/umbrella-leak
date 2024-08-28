// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.MainWindow
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using FontAwesome.WPF;
using HttpProgress;
using Ionic.Zip;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Device.Location;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using UmbrellaDesign.Properties;

#nullable disable
namespace UmbrellaDesign
{
  public partial class MainWindow : Window, IComponentConnector
  {
    public static string softVersion = "2.4.2";
    private const string loaderDllName = "LoaderKernel.dll";
    private const string host_main = "http://dist.uc.zone";
    private const string host_second = "http://dist.umbrella-team.space";
    private bool use_second;
    public static string logs_data = "";
    public string site_url = "rem";
    public string group_url = "rem";
    public static JObject json_config_data = (JObject) null;
    private System.Timers.Timer timer_log = new System.Timers.Timer();
    private int download_status;
    private string download_name = "";
    private int prev_dowload_percent;
    private string current_download_name = "";
    public static string login;
    public static string password;
    internal Button close_btn;
    internal Button minimaze_btn;
    internal Image logo_image;
    internal TextBlock first_caption;
    internal TextBlock second_caption;
    internal ComboBox server_combo;
    internal Grid load_activity;
    internal ProgressBar pbStatus;
    internal Grid login_activity;
    internal TextBox login_tb;
    internal PasswordBox password_tb;
    internal Grid button_login;
    internal Button login_btn;
    internal ImageAwesome loading_ico;
    internal Grid cheat_log;
    internal RichTextBox log_window;
    private bool _contentLoaded;

    [DllImport("kernel32", SetLastError = true)]
    private static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int connect_client(int server_id);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int check_connect();

    [DllImport("LoaderKernel.dll", EntryPoint = "get_game_path", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr get_game_path_handlers(string game);

    public static string get_game_path(string game)
    {
      return Marshal.PtrToStringAnsi(MainWindow.get_game_path_handlers(game));
    }

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr logs_handler();

    public static string logs_handler_get() => Marshal.PtrToStringAnsi(MainWindow.logs_handler());

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int auth(string login, string password);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int auth_free();

    public static bool display_free_products { get; set; } = true;

    private void log_handler(object sender, EventArgs e)
    {
      string log_item = MainWindow.logs_handler_get();
      if (log_item == null)
        return;
      log_item = Encoding.ASCII.GetString(Convert.FromBase64String(log_item));
      this.Dispatcher.Invoke((Action) (() =>
      {
        if (log_item.ToLower().IndexOf("error") != -1)
        {
          TextRange textRange = new TextRange(this.log_window.Document.ContentEnd, this.log_window.Document.ContentEnd);
          textRange.Text = log_item + "\n";
          DependencyProperty foregroundProperty = TextElement.ForegroundProperty;
          textRange.ApplyPropertyValue(TextElement.ForegroundProperty, (object) Brushes.Red);
          textRange.ApplyPropertyValue(TextElement.FontWeightProperty, (object) FontWeights.Normal);
        }
        else
        {
          this.log_window.AppendText(log_item + "\n");
          MainWindow.logs_data = MainWindow.logs_data + log_item + "\n";
        }
      }));
    }

    private void loadConfig()
    {
      try
      {
        string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!System.IO.File.Exists(Path.Combine(directoryName, "config.json")))
          return;
        using (StreamReader streamReader = new StreamReader(Path.Combine(directoryName, "config.json")))
        {
          string end = streamReader.ReadToEnd();
          try
          {
            MainWindow.json_config_data = JObject.Parse(end);
          }
          catch
          {
          }
        }
      }
      catch
      {
      }
    }

    private void loadCustomSettings()
    {
      this.Dispatcher.Invoke((Action) (() =>
      {
        try
        {
          JObject jsonConfigData = MainWindow.json_config_data;
          if (MainWindow.json_config_data != null)
          {
            MainWindow.display_free_products = false;
            object obj1 = (object) ((JToken) jsonConfigData).SelectToken("site_url");
            // ISSUE: reference to a compiler-generated field
            if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MainWindow), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool> target1 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__1.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool>> p1 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__1;
            // ISSUE: reference to a compiler-generated field
            if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MainWindow), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj2 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__0.Target((CallSite) MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__0, obj1, (object) null);
            string str1;
            if (!target1((CallSite) p1, obj2))
            {
              str1 = this.site_url;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__2 == null)
              {
                // ISSUE: reference to a compiler-generated field
                MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MainWindow)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              str1 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__2.Target((CallSite) MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__2, obj1);
            }
            this.site_url = str1;
            object obj3 = (object) ((JToken) jsonConfigData).SelectToken("group_url");
            // ISSUE: reference to a compiler-generated field
            if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__4 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MainWindow), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool> target2 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__4.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool>> p4 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__4;
            // ISSUE: reference to a compiler-generated field
            if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__3 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MainWindow), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj4 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__3.Target((CallSite) MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__3, obj3, (object) null);
            string str2;
            if (!target2((CallSite) p4, obj4))
            {
              str2 = this.group_url;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__5 == null)
              {
                // ISSUE: reference to a compiler-generated field
                MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MainWindow)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              str2 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__5.Target((CallSite) MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__5, obj3);
            }
            this.group_url = str2;
            object obj5 = (object) ((JToken) jsonConfigData).SelectToken("display_free_products");
            // ISSUE: reference to a compiler-generated field
            if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__7 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MainWindow), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool> target3 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__7.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool>> p7 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__7;
            // ISSUE: reference to a compiler-generated field
            if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__6 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MainWindow), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj6 = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__6.Target((CallSite) MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__6, obj5, (object) null);
            int num;
            if (!target3((CallSite) p7, obj6))
            {
              num = MainWindow.display_free_products ? 1 : 0;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__8 == null)
              {
                // ISSUE: reference to a compiler-generated field
                MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (bool), typeof (MainWindow)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              num = MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__8.Target((CallSite) MainWindow.\u003C\u003Eo__24.\u003C\u003Ep__8, obj5) ? 1 : 0;
            }
            MainWindow.display_free_products = num != 0;
          }
          Customization.loadIcon((Window) this, "ico", jsonConfigData);
          Customization.loadImage(this.logo_image, "logo_main", jsonConfigData);
          Customization.loadTextBlock(this.first_caption, "first_caption", jsonConfigData);
          Customization.loadTextBlock(this.second_caption, "second_caption", jsonConfigData);
        }
        catch (Exception ex)
        {
          this.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("[Launcher] Error loading custom settings: " + ex.Message + "\n")));
        }
      }));
    }

    private void setServer(int server_id)
    {
      this.server_combo.Dispatcher.Invoke((Action) (() => this.server_combo.SelectedIndex = server_id));
      Settings.Default.server_id = server_id;
      Settings.Default.Save();
    }

    private async void selectServer()
    {
      int selected_server = 1;
      try
      {
        IPHostEntry server_0_ip = Dns.GetHostEntry("fr1.uc.zone");
        IPHostEntry server_1_ip = Dns.GetHostEntry("msk1.uc.zone");
        IPHostEntry server_2_ip = Dns.GetHostEntry("hk1.uc.zone");
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpClient httpClient = new HttpClient();
        string self_json_data = await httpClient.GetStringAsync("https://ipwhois.app/json/");
        string data_0 = await httpClient.GetStringAsync("https://ipwhois.app/json/" + server_0_ip.AddressList[0]?.ToString());
        string data_1 = await httpClient.GetStringAsync("https://ipwhois.app/json/" + server_1_ip.AddressList[0]?.ToString());
        string stringAsync = await httpClient.GetStringAsync("https://ipwhois.app/json/" + server_2_ip.AddressList[0]?.ToString());
        JObject jobject1 = JObject.Parse(self_json_data);
        JObject jobject2 = JObject.Parse(data_0);
        JObject jobject3 = JObject.Parse(data_1);
        JObject jobject4 = JObject.Parse(stringAsync);
        GeoCoordinate geoCoordinate = new GeoCoordinate(Convert.ToDouble(jobject1["latitude"].ToString()), Convert.ToDouble(jobject1["longitude"].ToString()));
        double distanceTo1 = geoCoordinate.GetDistanceTo(new GeoCoordinate(Convert.ToDouble(jobject2["latitude"].ToString()), Convert.ToDouble(jobject2["longitude"].ToString())));
        double distanceTo2 = geoCoordinate.GetDistanceTo(new GeoCoordinate(Convert.ToDouble(jobject3["latitude"].ToString()), Convert.ToDouble(jobject3["longitude"].ToString())));
        double distanceTo3 = geoCoordinate.GetDistanceTo(new GeoCoordinate(Convert.ToDouble(jobject4["latitude"].ToString()), Convert.ToDouble(jobject4["longitude"].ToString())));
        double num = double.MaxValue;
        if (distanceTo1 < num)
        {
          num = distanceTo1;
          selected_server = 0;
        }
        if (distanceTo2 < num)
        {
          num = distanceTo2;
          selected_server = 1;
        }
        if (distanceTo3 < num)
          selected_server = 2;
        server_0_ip = (IPHostEntry) null;
        server_1_ip = (IPHostEntry) null;
        server_2_ip = (IPHostEntry) null;
        httpClient = (HttpClient) null;
        self_json_data = (string) null;
        data_0 = (string) null;
        data_1 = (string) null;
      }
      catch (Exception ex)
      {
      }
      this.setServer(selected_server);
    }

    private void loadAppSettings()
    {
      this.Dispatcher.Invoke((Action) (() =>
      {
        try
        {
          this.login_tb.Text = Settings.Default.login;
          this.password_tb.Password = Settings.Default.password;
          int serverId = Settings.Default.server_id;
          if (serverId == -1)
            this.selectServer();
          else
            this.server_combo.SelectedIndex = serverId;
        }
        catch (ConfigurationErrorsException ex)
        {
          System.IO.File.Delete(((ConfigurationException) ex.InnerException).Filename);
          this.selectServer();
        }
        ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed;
      }));
    }

    private void KIllSteam()
    {
      try
      {
        List<string> stringList = new List<string>();
        stringList.Add("dota2");
        stringList.Add("steam");
        stringList.Add("steamservice");
        stringList.Add("steamwebhelper");
        string processName1 = Process.GetCurrentProcess().ProcessName;
        stringList.Add(processName1);
        foreach (string processName2 in stringList)
        {
          foreach (Process process in Process.GetProcessesByName(processName2))
          {
            if (process.ProcessName == processName1)
            {
              if (process.Id != Process.GetCurrentProcess().Id)
                process.Kill();
            }
            else
              process.Kill();
          }
        }
        Thread.Sleep(1000);
      }
      catch
      {
      }
    }

    public string checkMD5(string filename)
    {
      using (MD5 md5 = MD5.Create())
      {
        if (!System.IO.File.Exists(filename))
          return "";
        using (FileStream inputStream = System.IO.File.OpenRead(filename))
        {
          byte[] hash = md5.ComputeHash((Stream) inputStream);
          inputStream.Close();
          return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
      }
    }

    public void checkRename()
    {
      try
      {
        string location = Assembly.GetEntryAssembly().Location;
        foreach (FileInfo file in new DirectoryInfo(location.Substring(0, location.LastIndexOf("\\") + 1)).GetFiles())
        {
          if (file.Name.IndexOf("UmbrellaKernel.dll") != -1 && System.IO.File.Exists(file.FullName))
            System.IO.File.Delete(file.FullName);
          if (file.Name.IndexOf(".bak") != -1 && System.IO.File.Exists(file.FullName))
            System.IO.File.Delete(file.FullName);
        }
      }
      catch
      {
      }
    }

    private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
    {
      this.Dispatcher.Invoke((Action) (() =>
      {
        if (!(this.download_name != "") || e.ProgressPercentage <= this.download_status + 4)
          return;
        this.download_status = e.ProgressPercentage;
        this.log_window.AppendText("[Updater] " + this.download_name + " load is " + this.download_status.ToString() + "%\r");
      }));
    }

    private void DownloadFileCallBack2(object sender, AsyncCompletedEventArgs e)
    {
      this.Dispatcher.Invoke((Action) (() =>
      {
        if (e.Cancelled)
          this.log_window.AppendText("[Updater] " + this.download_name + " download was cancelled!\r");
        else if (e.Error != null)
          this.log_window.AppendText("[Updater] " + this.download_name + " error downloading: " + e.Error.Message + " !\r");
        else
          this.log_window.AppendText("[Updater] " + this.download_name + " successfully loaded!\r");
        this.download_status = 0;
        this.download_name = "";
      }));
    }

    private void go_main()
    {
      this.Dispatcher.Invoke((Action) (() =>
      {
        this.load_activity.Visibility = Visibility.Hidden;
        this.button_login.Visibility = Visibility.Visible;
        this.login_activity.Visibility = Visibility.Visible;
      }));
    }

    private static async Task<long?> PingAsync(string hostname)
    {
      int timeout = 3000;
      Ping ping = new Ping();
      try
      {
        PingReply pingReply = await ping.SendPingAsync(hostname, timeout);
        return pingReply.Status != IPStatus.Success ? new long?() : new long?(pingReply.RoundtripTime);
      }
      catch (Exception ex)
      {
        return new long?();
      }
    }

    public static string fix_link(string input, bool fix)
    {
      return fix ? input.Replace("dist.uc.zone", "dist.umbrella-team.space") : input;
    }

    public async void check_update2()
    {
      MainWindow mainWindow1 = this;
      // ISSUE: reference to a compiler-generated method
      mainWindow1.Dispatcher.Invoke(new Action(mainWindow1.\u003Ccheck_update2\u003Eb__41_0));
      string base_url = "http://dist.uc.zone";
      string version_info = "/version_info_x64.json";
      // ISSUE: reference to a compiler-generated method
      Progress<ICopyProgress> progress_callback = new Progress<ICopyProgress>(new Action<ICopyProgress>(mainWindow1.\u003Ccheck_update2\u003Eb__41_1));
      try
      {
        Task<long?> task_main = MainWindow.PingAsync("http://dist.uc.zone");
        Task<long?> task_second = MainWindow.PingAsync("http://dist.umbrella-team.space");
        long?[] nullableArray = await Task.WhenAll<long?>(task_main, task_second);
        long? result1 = task_main.Result;
        long? result2 = task_second.Result;
        if (!result1.HasValue)
        {
          mainWindow1.use_second = true;
          base_url = "http://dist.umbrella-team.space";
        }
        if (result1.HasValue && result2.HasValue)
        {
          long? nullable1 = result2;
          long? nullable2 = result1;
          if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          {
            mainWindow1.use_second = true;
            base_url = "http://dist.umbrella-team.space";
          }
        }
        // ISSUE: reference to a compiler-generated method
        mainWindow1.Dispatcher.Invoke(new Action(mainWindow1.\u003Ccheck_update2\u003Eb__41_3));
        task_main = (Task<long?>) null;
        task_second = (Task<long?>) null;
      }
      catch (Exception ex)
      {
      }
      JObject json = (JObject) null;
      HttpClient httpClient = new HttpClient();
      httpClient.BaseAddress = new Uri(base_url);
      httpClient.DefaultRequestHeaders.Add("umbrella-updater", "1.0.0");
      try
      {
        try
        {
          string stringAsync = await httpClient.GetStringAsync(version_info);
          mainWindow1.Dispatcher.Invoke((Action) (() => { }));
          json = JObject.Parse(stringAsync);
        }
        catch (Exception ex1)
        {
          MainWindow mainWindow = mainWindow1;
          Exception ex = ex1;
          mainWindow1.Dispatcher.Invoke((Action) (() =>
          {
            mainWindow.log_window.AppendText("[Launcher] caught exception, trying second\n");
            mainWindow.log_window.AppendText("[Launcher] " + ex.Message + "\n");
          }));
          if (mainWindow1.use_second)
          {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://dist.uc.zone");
            httpClient.DefaultRequestHeaders.Add("umbrella-updater", "1.0.0");
            mainWindow1.use_second = false;
          }
          else
          {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://dist.umbrella-team.space");
            httpClient.DefaultRequestHeaders.Add("umbrella-updater", "1.0.0");
            mainWindow1.use_second = true;
          }
          json = JObject.Parse(await httpClient.GetStringAsync(version_info));
        }
        if (json["success"] == null)
        {
          // ISSUE: reference to a compiler-generated method
          mainWindow1.Dispatcher.Invoke(new Action(mainWindow1.\u003Ccheck_update2\u003Eb__41_4));
          mainWindow1.go_main();
          base_url = (string) null;
          version_info = (string) null;
          progress_callback = (Progress<ICopyProgress>) null;
          json = (JObject) null;
          httpClient = (HttpClient) null;
          return;
        }
        JArray jarray = JArray.Parse(json["files"].ToString());
        string exePath = Assembly.GetExecutingAssembly().Location;
        string fileName = Path.GetFileName(exePath);
        string directoryPath = Path.GetDirectoryName(exePath);
        foreach (JToken jtoken in jarray)
        {
          string str1 = jtoken[(object) "name"].ToString();
          MemoryStream downloadStream;
          FileStream fs;
          string dll_path;
          switch (str1)
          {
            case ".exe":
              string lower1 = jtoken[(object) "md5"].ToString().ToLower();
              string lower2 = mainWindow1.checkMD5(exePath).ToLower();
              Uri uri1 = new Uri(jtoken[(object) "link"].ToString());
              string str2 = lower2;
              if (lower1 != str2)
              {
                mainWindow1.current_download_name = str1;
                downloadStream = new MemoryStream();
                HttpResponseMessage async = await HttpClientExtensions.GetAsync(httpClient, uri1.AbsolutePath, (Stream) downloadStream, (IProgress<ICopyProgress>) progress_callback, new CancellationToken());
                mainWindow1.prev_dowload_percent = 0;
                if (async == null || !async.IsSuccessStatusCode)
                  throw new Exception(async.ReasonPhrase);
                mainWindow1.KIllSteam();
                string str3 = exePath + ".bak";
                System.IO.File.Move(exePath, exePath + ".bak");
                fs = new FileStream(fileName, FileMode.Create);
                try
                {
                  await downloadStream.CopyToAsync((Stream) fs);
                  fs.Close();
                  // ISSUE: reference to a compiler-generated method
                  mainWindow1.Dispatcher.Invoke(new Action(mainWindow1.\u003Ccheck_update2\u003Eb__41_7));
                  ProcessStartInfo Info = new ProcessStartInfo();
                  Info.Arguments = "/C choice /C Y /N /D Y /T 2 & START \"\" \"" + Assembly.GetEntryAssembly().Location + "\"";
                  Info.WindowStyle = ProcessWindowStyle.Hidden;
                  Info.CreateNoWindow = true;
                  Info.FileName = "cmd.exe";
                  mainWindow1.Dispatcher.Invoke((Action) (() =>
                  {
                    Process.Start(Info);
                    Process.GetCurrentProcess().Kill();
                  }));
                  base_url = (string) null;
                  version_info = (string) null;
                  progress_callback = (Progress<ICopyProgress>) null;
                  json = (JObject) null;
                  httpClient = (HttpClient) null;
                  return;
                }
                finally
                {
                  fs?.Dispose();
                }
              }
              else
                continue;
            case "LoaderKernel.dll":
              dll_path = directoryPath + "\\" + str1;
              if (jtoken[(object) "md5"].ToString().ToLower() != mainWindow1.checkMD5(dll_path).ToLower())
              {
                Uri uri2 = new Uri(jtoken[(object) "link"].ToString());
                mainWindow1.current_download_name = str1;
                downloadStream = new MemoryStream();
                HttpResponseMessage async = await HttpClientExtensions.GetAsync(httpClient, uri2.AbsolutePath, (Stream) downloadStream, (IProgress<ICopyProgress>) progress_callback, new CancellationToken());
                mainWindow1.prev_dowload_percent = 0;
                if (async == null || !async.IsSuccessStatusCode)
                  throw new Exception(async.ReasonPhrase);
                mainWindow1.KIllSteam();
                string sourceFileName = dll_path;
                string destFileName = dll_path + ".bak";
                try
                {
                  System.IO.File.Move(sourceFileName, destFileName);
                }
                catch
                {
                }
                fs = new FileStream(dll_path, FileMode.Create);
                try
                {
                  await downloadStream.CopyToAsync((Stream) fs);
                  fs.Close();
                  // ISSUE: reference to a compiler-generated method
                  mainWindow1.Dispatcher.Invoke(new Action(mainWindow1.\u003Ccheck_update2\u003Eb__41_9));
                  ProcessStartInfo Info = new ProcessStartInfo();
                  Info.Arguments = "/C choice /C Y /N /D Y /T 2 & START \"\" \"" + Assembly.GetEntryAssembly().Location + "\"";
                  Info.WindowStyle = ProcessWindowStyle.Hidden;
                  Info.CreateNoWindow = true;
                  Info.FileName = "cmd.exe";
                  mainWindow1.Dispatcher.Invoke((Action) (() =>
                  {
                    Process.Start(Info);
                    Process.GetCurrentProcess().Kill();
                  }));
                  base_url = (string) null;
                  version_info = (string) null;
                  progress_callback = (Progress<ICopyProgress>) null;
                  json = (JObject) null;
                  httpClient = (HttpClient) null;
                  return;
                }
                finally
                {
                  fs?.Dispose();
                }
              }
              else
              {
                dll_path = (string) null;
                continue;
              }
            default:
              if (str1.EndsWith(".zip"))
              {
                string str4 = directoryPath;
                dll_path = directoryPath;
                if (jtoken[(object) "type"].ToString() == "in_game")
                {
                  string gamePath = MainWindow.get_game_path("dota2");
                  if (gamePath != null && !(gamePath == ""))
                    dll_path = gamePath.Substring(0, gamePath.IndexOf("\\game\\"));
                  else
                    continue;
                }
                string file_path = str4 + "\\" + str1;
                if (!(jtoken[(object) "md5"].ToString().ToLower() == mainWindow1.checkMD5(file_path).ToLower()))
                {
                  try
                  {
                    Uri uri3 = new Uri(jtoken[(object) "link"].ToString());
                    mainWindow1.current_download_name = str1;
                    downloadStream = new MemoryStream();
                    HttpResponseMessage async = await HttpClientExtensions.GetAsync(httpClient, uri3.AbsolutePath, (Stream) downloadStream, (IProgress<ICopyProgress>) progress_callback, new CancellationToken());
                    mainWindow1.prev_dowload_percent = 0;
                    if (async == null || !async.IsSuccessStatusCode)
                      throw new Exception(async.ReasonPhrase);
                    fs = new FileStream(file_path, FileMode.Create);
                    try
                    {
                      await downloadStream.CopyToAsync((Stream) fs);
                      fs.Close();
                      using (ZipFile zipFile = ZipFile.Read(file_path))
                      {
                        ExtractExistingFileAction existingFileAction = (ExtractExistingFileAction) 1;
                        zipFile.ExtractAll(dll_path, existingFileAction);
                      }
                    }
                    finally
                    {
                      fs?.Dispose();
                    }
                    fs = (FileStream) null;
                    downloadStream = (MemoryStream) null;
                  }
                  catch (Exception ex2)
                  {
                    MainWindow mainWindow = mainWindow1;
                    Exception ex = ex2;
                    if (System.IO.File.Exists(file_path))
                      System.IO.File.Delete(file_path);
                    mainWindow1.Dispatcher.Invoke((Action) (() => mainWindow.log_window.AppendText("[Launcher] " + ex.Message + "\n")));
                  }
                  dll_path = (string) null;
                  file_path = (string) null;
                  continue;
                }
                continue;
              }
              continue;
          }
        }
        exePath = (string) null;
        fileName = (string) null;
        directoryPath = (string) null;
      }
      catch (Exception ex3)
      {
        MainWindow mainWindow = mainWindow1;
        Exception ex = ex3;
        mainWindow1.Dispatcher.Invoke((Action) (() => mainWindow.log_window.AppendText("[Launcher] failed to update " + ex.Message + " \n")));
        mainWindow1.go_main();
        base_url = (string) null;
        version_info = (string) null;
        progress_callback = (Progress<ICopyProgress>) null;
        json = (JObject) null;
        httpClient = (HttpClient) null;
        return;
      }
      mainWindow1.go_main();
      base_url = (string) null;
      version_info = (string) null;
      progress_callback = (Progress<ICopyProgress>) null;
      json = (JObject) null;
      httpClient = (HttpClient) null;
    }

    public async void check_update()
    {
      MainWindow mainWindow = this;
      mainWindow.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("[Launcher] Checking update.\n")));
      string contents = "";
      try
      {
        string update_url = "http://dist.uc.zone/version_info_x64.json";
        Task<long?> task_main = MainWindow.PingAsync("http://dist.uc.zone");
        Task<long?> task_second = MainWindow.PingAsync("http://dist.umbrella-team.space");
        long?[] nullableArray = await Task.WhenAll<long?>(task_main, task_second);
        long? result1 = task_main.Result;
        long? result2 = task_second.Result;
        if (!result1.HasValue)
        {
          mainWindow.use_second = true;
          update_url = "http://dist.umbrella-team.space/version_info_x64.json";
        }
        if (result1.HasValue && result2.HasValue)
        {
          long? nullable1 = result2;
          long? nullable2 = result1;
          if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          {
            mainWindow.use_second = true;
            update_url = "http://dist.umbrella-team.space/version_info_x64.json";
          }
        }
        mainWindow.Dispatcher.Invoke((Action) (() =>
        {
          if (this.use_second)
            this.log_window.AppendText("[Launcher] Selected second server\n");
          else
            this.log_window.AppendText("[Launcher] Selected main server\n");
        }));
        using (StreamReader streamReader = new StreamReader(WebRequest.Create(update_url).GetResponse().GetResponseStream(), Encoding.UTF8))
          contents = streamReader.ReadToEnd();
        update_url = (string) null;
        task_main = (Task<long?>) null;
        task_second = (Task<long?>) null;
      }
      catch (Exception ex)
      {
        mainWindow.Dispatcher.Invoke((Action) (() =>
        {
          this.log_window.AppendText("[Launcher] " + ex.Message);
          this.log_window.AppendText("[Launcher] Error checking update, сheck for Updates Manually!\n");
          this.log_window.AppendText("[Launcher] Current version " + MainWindow.softVersion + "!\n");
          this.log_window.AppendText("[Launcher] Please visit own resource:\n");
          this.log_window.AppendText("[Launcher] Site: " + this.site_url + "\n");
          this.log_window.AppendText("[Launcher] VK group: " + this.group_url + "\n");
        }));
        mainWindow.go_main();
        return;
      }
      JArray token0;
      try
      {
        JObject jobject = JObject.Parse(contents);
        if (jobject["success"] == null)
        {
          mainWindow.Dispatcher.Invoke((Action) (() =>
          {
            this.log_window.AppendText("[Launcher] Error checking update, сheck for Updates Manually!\n");
            this.log_window.AppendText("[Launcher] Current version " + MainWindow.softVersion + "!\n");
            this.log_window.AppendText("[Launcher] Please visit own resource:\n");
            this.log_window.AppendText("[Launcher] Site: " + this.site_url + "\n");
            this.log_window.AppendText("[Launcher] VK group: " + this.group_url + "\n");
          }));
          mainWindow.go_main();
          return;
        }
        token0 = JArray.Parse(jobject["files"].ToString());
      }
      catch
      {
        string now_date = DateTime.Now.ToString().Replace(':', '_').Replace('.', '_').Replace(' ', '_');
        System.IO.File.WriteAllText("./launcher_logs/html_answer_" + now_date + ".txt", contents);
        mainWindow.Dispatcher.Invoke((Action) (() =>
        {
          this.log_window.AppendText("[Launcher] Error checking update, сheck for Updates Manually!\n");
          this.log_window.AppendText("[Launcher] Current version " + MainWindow.softVersion + "!\n");
          this.log_window.AppendText("[Launcher] Please visit own resource:\n");
          this.log_window.AppendText("[Launcher] Site: " + this.site_url + "\n");
          this.log_window.AppendText("[Launcher] VK group: " + this.group_url + "\n");
          this.log_window.AppendText("\n[Launcher] Please, send log file to the support team!\n");
          this.log_window.AppendText("[Launcher] Log file: launcher_logs/html_answer_" + now_date + ".txt !\n");
        }));
        mainWindow.go_main();
        return;
      }
      bool flag1 = false;
      string path = Assembly.GetEntryAssembly().Location;
      string str1 = path.Substring(path.LastIndexOf("\\") + 1);
      path = path.Substring(0, path.LastIndexOf("\\") + 1);
      for (int index = 0; index < ((JContainer) token0).Count; ++index)
      {
        if (token0[index][(object) "type"].ToString() == "main")
        {
          if (token0[index][(object) "name"].ToString() == ".exe")
          {
            string lower = mainWindow.checkMD5(path + str1).ToLower();
            if (token0[index][(object) "md5"].ToString().ToLower() != lower)
            {
              mainWindow.KIllSteam();
              System.IO.File.Move(path + str1, path + str1 + ".bak");
              WebClient webClient = new WebClient();
              TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
              webClient.Headers.Add("umbrella-updater", "1.0.0");
              try
              {
                mainWindow.download_name = str1;
                mainWindow.download_status = 0;
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(mainWindow.DownloadProgressCallback);
                webClient.DownloadFileCompleted += (AsyncCompletedEventHandler) ((sender, e) => this.Dispatcher.Invoke((Action) (() =>
                {
                  if (e.Cancelled)
                  {
                    this.log_window.AppendText("[Updater] " + this.download_name + " download was cancelled!\r");
                    tcs.SetResult(false);
                  }
                  else if (e.Error != null)
                  {
                    this.log_window.AppendText("[Updater] " + this.download_name + " error downloading: " + e.Error.Message + " !\r");
                    tcs.SetException(e.Error);
                  }
                  else
                  {
                    this.log_window.AppendText("[Updater] " + this.download_name + " successfully loaded!\r");
                    tcs.SetResult(true);
                  }
                  this.download_status = 0;
                  this.download_name = "";
                })));
                webClient.DownloadFileAsync(new Uri(MainWindow.fix_link(token0[index][(object) "link"].ToString(), mainWindow.use_second)), path + str1);
                tcs.Task.Wait();
                if (token0[index][(object) "md5"].ToString().ToLower() == mainWindow.checkMD5(path + str1).ToLower())
                {
                  flag1 = true;
                }
                else
                {
                  if (System.IO.File.Exists(path + str1))
                    System.IO.File.Delete(path + str1);
                  if (System.IO.File.Exists(path + str1 + ".bak"))
                    System.IO.File.Move(path + str1 + ".bak", path + str1);
                  mainWindow.Dispatcher.Invoke((Action) (() =>
                  {
                    this.log_window.AppendText("[Launcher] Error download update, сheck for Updates Manually!\n");
                    this.log_window.AppendText("[Launcher] Current version " + MainWindow.softVersion + "!\n");
                    this.log_window.AppendText("[Launcher] Please visit own resource:\n");
                    this.log_window.AppendText("[Launcher] Site: " + this.site_url + "\n");
                    this.log_window.AppendText("[Launcher] VK group: " + this.group_url + "\n");
                  }));
                }
              }
              catch (WebException ex)
              {
                mainWindow.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("[WebclientError] " + ex.StackTrace + "\r")));
                mainWindow.go_main();
                return;
              }
            }
          }
          else if (token0[index][(object) "name"].ToString() == "LoaderKernel.dll")
          {
            string lower = mainWindow.checkMD5(path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString()).ToLower();
            if (token0[index][(object) "md5"].ToString().ToLower() != lower)
            {
              mainWindow.KIllSteam();
              if (System.IO.File.Exists(path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString()))
                System.IO.File.Move(path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString(), path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString() + ".bak");
              WebClient webClient = new WebClient();
              try
              {
                mainWindow.download_name = token0[index][(object) "name"].ToString();
                mainWindow.download_status = 0;
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(mainWindow.DownloadProgressCallback);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(mainWindow.DownloadFileCallBack2);
                webClient.DownloadFileAsync(new Uri(MainWindow.fix_link(token0[index][(object) "link"].ToString(), mainWindow.use_second)), path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString());
                do
                  ;
                while (webClient.IsBusy);
                if (token0[index][(object) "md5"].ToString().ToLower() == mainWindow.checkMD5(path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString()).ToLower())
                {
                  flag1 = true;
                }
                else
                {
                  if (System.IO.File.Exists(path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString()))
                    System.IO.File.Delete(path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString());
                  if (System.IO.File.Exists(path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString() + ".bak"))
                    System.IO.File.Move(path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString() + ".bak", path + token0[index][(object) "path"].ToString() + token0[index][(object) "name"].ToString());
                  mainWindow.Dispatcher.Invoke((Action) (() =>
                  {
                    this.log_window.AppendText("[Launcher] Error download update, сheck for Updates Manually!\n");
                    this.log_window.AppendText("[Launcher] Current version " + MainWindow.softVersion + "!\n");
                    this.log_window.AppendText("[Launcher] Please visit own resource:\n");
                    this.log_window.AppendText("[Launcher] Site: " + this.site_url + "\n");
                    this.log_window.AppendText("[Launcher] VK group: " + this.group_url + "\n");
                  }));
                }
              }
              catch (WebException ex)
              {
                mainWindow.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("[WebclientError] " + ex.StackTrace + "\r")));
                mainWindow.go_main();
                return;
              }
            }
          }
        }
      }
      if (flag1)
      {
        mainWindow.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("[Launcher] Update download succesfully, now the application will restart!\n")));
        ProcessStartInfo Info = new ProcessStartInfo();
        Info.Arguments = "/C choice /C Y /N /D Y /T 2 & START \"\" \"" + Assembly.GetEntryAssembly().Location + "\"";
        Info.WindowStyle = ProcessWindowStyle.Hidden;
        Info.CreateNoWindow = true;
        Info.FileName = "cmd.exe";
        mainWindow.Dispatcher.Invoke((Action) (() =>
        {
          Process.Start(Info);
          Process.GetCurrentProcess().Kill();
        }));
      }
      else
      {
        for (int i = 0; i < ((JContainer) token0).Count; ++i)
        {
          if (token0[i][(object) "type"].ToString() != "main")
          {
            string lower = mainWindow.checkMD5(path + token0[i][(object) "path"].ToString() + token0[i][(object) "name"].ToString()).ToLower();
            if (token0[i][(object) "md5"].ToString().ToLower() != lower)
            {
              mainWindow.KIllSteam();
              if (System.IO.File.Exists(path + token0[i][(object) "path"].ToString() + token0[i][(object) "name"].ToString()))
                System.IO.File.Delete(path + token0[i][(object) "path"].ToString() + token0[i][(object) "name"].ToString());
              WebClient webClient = new WebClient();
              try
              {
                mainWindow.download_name = token0[i][(object) "name"].ToString();
                mainWindow.download_status = -1;
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(mainWindow.DownloadProgressCallback);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(mainWindow.DownloadFileCallBack2);
                await Task.Run((Action) (() => webClient.DownloadFileAsync(new Uri(MainWindow.fix_link(token0[i][(object) "link"].ToString(), this.use_second)), path + token0[i][(object) "path"].ToString() + token0[i][(object) "name"].ToString())));
                do
                  ;
                while (webClient.IsBusy);
                if (token0[i][(object) "name"].ToString().IndexOf(".zip") != -1)
                {
                  string str2 = path;
                  bool flag2 = token0[i][(object) "type"].ToString() == "in_game";
                  if (flag2)
                  {
                    try
                    {
                      string gamePath = MainWindow.get_game_path("dota2");
                      str2 = gamePath.Substring(0, gamePath.IndexOf("\\game\\"));
                    }
                    catch (Exception ex)
                    {
                      mainWindow.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("\r" + ex.StackTrace + "\r")));
                      if (System.IO.File.Exists(path + token0[i][(object) "path"].ToString() + token0[i][(object) "name"].ToString()))
                        System.IO.File.Delete(path + token0[i][(object) "path"].ToString() + token0[i][(object) "name"].ToString());
                      continue;
                    }
                  }
                  if (flag2)
                  {
                    if (str2 == "")
                      continue;
                  }
                  try
                  {
                    using (ZipArchive zipArchive = ZipFile.OpenRead(Path.Combine(path + token0[i][(object) "path"].ToString() + token0[i][(object) "name"].ToString())))
                    {
                      foreach (ZipArchiveEntry entry in zipArchive.Entries)
                      {
                        if (entry.FullName.EndsWith("/", StringComparison.OrdinalIgnoreCase))
                        {
                          if (!Directory.Exists(Path.Combine(Path.Combine(str2 + token0[i][(object) "path"].ToString()), entry.FullName)))
                            Directory.CreateDirectory(Path.Combine(Path.Combine(str2 + token0[i][(object) "path"].ToString()), entry.FullName));
                        }
                        else
                        {
                          try
                          {
                            entry.ExtractToFile(Path.Combine(Path.Combine(str2 + token0[i][(object) "path"].ToString()), entry.FullName), true);
                          }
                          catch
                          {
                          }
                        }
                      }
                    }
                    mainWindow.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("[UnZip] " + token0[i][(object) "name"].ToString() + " unzip successfully\r")));
                  }
                  catch (Exception ex)
                  {
                    mainWindow.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("\r" + ex.StackTrace + "\r")));
                  }
                }
              }
              catch (WebException ex)
              {
                mainWindow.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("[WebclientError] " + ex.StackTrace + "\r")));
                mainWindow.go_main();
                return;
              }
            }
          }
        }
        mainWindow.go_main();
      }
    }

    public static string GetOSBit() => Environment.Is64BitOperatingSystem ? "x64" : "x32";

    public static string GetCurrentProcessBit() => Environment.Is64BitProcess ? "x64" : "x32";

    public MainWindow()
    {
      this.InitializeComponent();
      new Thread((ThreadStart) (() =>
      {
        try
        {
          this.checkRename();
          this.check_update2();
          if (!Directory.Exists("./scripts"))
            Directory.CreateDirectory("./scripts");
          this.timer_log.Elapsed += new ElapsedEventHandler(this.log_handler);
          this.timer_log.Interval = 20.0;
          this.timer_log.AutoReset = true;
          this.timer_log.Enabled = true;
        }
        catch (Exception ex)
        {
          this.Dispatcher.Invoke((Action) (() => this.log_window.AppendText("[Launcher] " + ex.Message + "\n")));
        }
      })).Start();
      new Thread((ThreadStart) (() =>
      {
        string lower = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToLower();
        this.loadConfig();
        this.loadCustomSettings();
        this.Dispatcher.Invoke((Action) (() =>
        {
          this.load_activity.Visibility = Visibility.Visible;
          this.button_login.Visibility = Visibility.Hidden;
          this.login_activity.Visibility = Visibility.Hidden;
        }));
        if (MainWindow.json_config_data != null)
          ((JToken) MainWindow.json_config_data).SelectToken("first_caption.Text.value");
        if (lower.IndexOf("procheat") != -1)
        {
          try
          {
            if (!Directory.Exists("./launcher_logs"))
              Directory.CreateDirectory("./launcher_logs");
          }
          catch
          {
          }
        }
        this.loadAppSettings();
      })).Start();
    }

    public void exit()
    {
      this.Dispatcher.Invoke((Action) (() =>
      {
        Settings.Default.Save();
        foreach (ProcessModule module in (ReadOnlyCollectionBase) Process.GetCurrentProcess().Modules)
        {
          if (module.ModuleName == "LoaderKernel.dll")
            MainWindow.FreeLibrary(module.BaseAddress);
        }
        this.Close();
        Environment.Exit(0);
      }));
    }

    private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

    private void Close_btn_Click(object sender, RoutedEventArgs e) => this.exit();

    private void Minimaze_btn_Click(object sender, RoutedEventArgs e)
    {
      this.WindowState = WindowState.Minimized;
    }

    private void btn_s_enabled()
    {
      this.Dispatcher.Invoke((Action) (() => this.login_btn.IsEnabled = true));
    }

    private void btn_s_disabled()
    {
      this.Dispatcher.Invoke((Action) (() => this.login_btn.IsEnabled = false));
    }

    private async void auth_client()
    {
      MainWindow mainWindow = this;
      mainWindow.btn_s_disabled();
      mainWindow.Dispatcher.Invoke((Action) (() => ((UIElement) this.loading_ico).Visibility = Visibility.Visible));
      string s_login = mainWindow.login_tb.Text;
      string s_password = mainWindow.password_tb.Password;
      int _server_id = Settings.Default.server_id;
      if (_server_id == 3)
        _server_id = 9;
      else if (_server_id == 4)
        _server_id = 10;
      if (await Task.Run<int>((Func<int>) (() => MainWindow.connect_client(_server_id))) == 1)
      {
        try
        {
          if (await Task.Run<int>((Func<int>) (() => MainWindow.auth(WebUtility.UrlEncode(s_login), WebUtility.UrlEncode(s_password)))) == 0)
          {
            Settings.Default.login = s_login;
            MainWindow.login = s_login;
            Settings.Default.password = s_password;
            MainWindow.password = s_password;
            Settings.Default.Save();
            mainWindow.btn_s_enabled();
            mainWindow.login_btn.Dispatcher.Invoke((Action) (() =>
            {
              this.timer_log.Stop();
              this.timer_log.Close();
              new Window1().Show();
              this.Close();
            }));
          }
          else
          {
            mainWindow.btn_s_enabled();
            ((DispatcherObject) mainWindow.loading_ico).Dispatcher.Invoke((Action) (() => ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed));
          }
        }
        catch (Exception ex)
        {
          mainWindow.btn_s_enabled();
          ((DispatcherObject) mainWindow.loading_ico).Dispatcher.Invoke((Action) (() => ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed));
          mainWindow.log_window.Dispatcher.Invoke((Action) (() => this.log_window.AppendText(ex.Message + "\r")));
        }
      }
      else
      {
        mainWindow.btn_s_enabled();
        ((DispatcherObject) mainWindow.loading_ico).Dispatcher.Invoke((Action) (() => ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed));
      }
    }

    private void Login_btn_Click(object sender, RoutedEventArgs e) => this.auth_client();

    private void Log_window_TextChanged(object sender, TextChangedEventArgs e)
    {
      this.log_window.Focus();
      this.log_window.CaretPosition = this.log_window.Document.ContentEnd;
      this.log_window.ScrollToEnd();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (this.server_combo.SelectedIndex == -1)
        return;
      Settings.Default.server_id = this.server_combo.SelectedIndex;
      Settings.Default.Save();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/UmbrellaLoader;component/mainwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((UIElement) target).MouseLeftButtonDown += new MouseButtonEventHandler(this.Grid_MouseLeftButtonDown);
          break;
        case 2:
          this.close_btn = (Button) target;
          this.close_btn.Click += new RoutedEventHandler(this.Close_btn_Click);
          break;
        case 3:
          this.minimaze_btn = (Button) target;
          this.minimaze_btn.Click += new RoutedEventHandler(this.Minimaze_btn_Click);
          break;
        case 4:
          this.logo_image = (Image) target;
          break;
        case 5:
          this.first_caption = (TextBlock) target;
          break;
        case 6:
          this.second_caption = (TextBlock) target;
          break;
        case 7:
          this.server_combo = (ComboBox) target;
          this.server_combo.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
          break;
        case 8:
          this.load_activity = (Grid) target;
          break;
        case 9:
          this.pbStatus = (ProgressBar) target;
          break;
        case 10:
          this.login_activity = (Grid) target;
          break;
        case 11:
          this.login_tb = (TextBox) target;
          break;
        case 12:
          this.password_tb = (PasswordBox) target;
          break;
        case 13:
          this.button_login = (Grid) target;
          break;
        case 14:
          this.login_btn = (Button) target;
          this.login_btn.Click += new RoutedEventHandler(this.Login_btn_Click);
          break;
        case 15:
          this.loading_ico = (ImageAwesome) target;
          break;
        case 16:
          this.cheat_log = (Grid) target;
          break;
        case 17:
          this.log_window = (RichTextBox) target;
          this.log_window.TextChanged += new TextChangedEventHandler(this.Log_window_TextChanged);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.PromoModal
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using FontAwesome.WPF;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

#nullable disable
namespace UmbrellaDesign
{
  public partial class PromoModal : Window, IComponentConnector
  {
    private const string loaderDllName = "LoaderKernel.dll";
    internal Image logo_image;
    internal Button close_btn;
    internal TextBox promocode;
    internal Button promo_btn;
    internal ImageAwesome loading_ico;
    private bool _contentLoaded;

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern bool activate_promocode(string promocode);

    [DllImport("LoaderKernel.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int check_connect();

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
        Customization.loadImage(this.logo_image, "logo_promo", jsonConfigData);
      }));
    }

    public PromoModal()
    {
      this.InitializeComponent();
      this.loadCustomSettings();
      ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed;
      this.promocode.Focus();
    }

    private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

    private void Close_btn_Click(object sender, RoutedEventArgs e) => this.Close();

    private void Promo_btn_Click(object sender, RoutedEventArgs e)
    {
      string promo = this.promocode.Text;
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        this.promo_btn.IsEnabled = false;
        ((UIElement) this.loading_ico).Visibility = Visibility.Visible;
      }));
      new Thread((ThreadStart) (() =>
      {
        if (PromoModal.check_connect() == 1)
        {
          if (PromoModal.activate_promocode(WebUtility.UrlEncode(promo)))
          {
            Thread.Sleep(800);
            this.Dispatcher.InvokeAsync((Action) (() => this.Close()));
          }
          else
            this.Dispatcher.InvokeAsync((Action) (() =>
            {
              this.promo_btn.IsEnabled = true;
              ((UIElement) this.loading_ico).Visibility = Visibility.Collapsed;
            }));
        }
        else
          this.Dispatcher.InvokeAsync((Action) (() =>
          {
            new MainWindow().Show();
            this.Close();
          }));
      })).Start();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/UmbrellaLoader;component/promomodal.xaml", UriKind.Relative));
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
          this.logo_image = (Image) target;
          break;
        case 3:
          this.close_btn = (Button) target;
          this.close_btn.Click += new RoutedEventHandler(this.Close_btn_Click);
          break;
        case 4:
          this.promocode = (TextBox) target;
          break;
        case 5:
          this.promo_btn = (Button) target;
          this.promo_btn.Click += new RoutedEventHandler(this.Promo_btn_Click);
          break;
        case 6:
          this.loading_ico = (ImageAwesome) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}

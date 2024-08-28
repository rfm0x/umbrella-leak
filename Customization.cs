// Decompiled with JetBrains decompiler
// Type: UmbrellaDesign.Customization
// Assembly: UmbrellaLoader, Version=13.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 704CD7CC-C1AA-4E05-BDBF-D9D71A8ACA15
// Assembly location: C:\Users\admin-pc\Desktop\UmbrellaLoader.exe

using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#nullable disable
namespace UmbrellaDesign
{
  internal class Customization
  {
    private static FontStyle getFontStyle(int id)
    {
      switch (id)
      {
        case 0:
          return FontStyles.Normal;
        case 1:
          return FontStyles.Oblique;
        case 2:
          return FontStyles.Italic;
        default:
          return FontStyles.Normal;
      }
    }

    private static FontWeight getFontWeight(int id)
    {
      switch (id)
      {
        case 0:
          return FontWeights.Thin;
        case 1:
          return FontWeights.ExtraLight;
        case 2:
          return FontWeights.UltraLight;
        case 3:
          return FontWeights.Light;
        case 4:
          return FontWeights.Normal;
        case 5:
          return FontWeights.Regular;
        case 6:
          return FontWeights.Medium;
        case 7:
          return FontWeights.DemiBold;
        case 8:
          return FontWeights.SemiBold;
        case 9:
          return FontWeights.Bold;
        case 10:
          return FontWeights.ExtraBold;
        case 11:
          return FontWeights.UltraBold;
        case 12:
          return FontWeights.Black;
        case 13:
          return FontWeights.Heavy;
        case 14:
          return FontWeights.ExtraBlack;
        case 15:
          return FontWeights.UltraBlack;
        default:
          return FontWeights.Normal;
      }
    }

    public static void loadImage(Image element, string name, JObject data)
    {
      if (data == null)
        return;
      object obj1 = (object) ((JToken) data).SelectToken(name + ".source");
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target = Customization.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p1 = Customization.\u003C\u003Eo__2.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = Customization.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) Customization.\u003C\u003Eo__2.\u003C\u003Ep__0, obj1, (object) null);
      if (!target((CallSite) p1, obj2))
        return;
      BitmapImage bitmapImage1 = new BitmapImage();
      bitmapImage1.BeginInit();
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      switch (Customization.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) Customization.\u003C\u003Eo__2.\u003C\u003Ep__2, obj1))
      {
        case 0:
          object obj3 = (object) ((JToken) data).SelectToken(name + ".url");
          BitmapImage bitmapImage2 = bitmapImage1;
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Customization)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          Uri uri1 = new Uri(Customization.\u003C\u003Eo__2.\u003C\u003Ep__3.Target((CallSite) Customization.\u003C\u003Eo__2.\u003C\u003Ep__3, obj3));
          bitmapImage2.UriSource = uri1;
          break;
        case 1:
          object obj4 = (object) ((JToken) data).SelectToken(name + ".url");
          string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          BitmapImage bitmapImage3 = bitmapImage1;
          string path1 = directoryName;
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Customization)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          string path2 = Customization.\u003C\u003Eo__2.\u003C\u003Ep__4.Target((CallSite) Customization.\u003C\u003Eo__2.\u003C\u003Ep__4, obj4);
          Uri uri2 = new Uri(Path.Combine(path1, path2));
          bitmapImage3.UriSource = uri2;
          break;
      }
      bitmapImage1.EndInit();
      if (element == null)
        return;
      element.Source = (ImageSource) bitmapImage1;
    }

    public static void loadEllipseImage(ImageBrush element, string name, JObject data)
    {
      if (data == null)
        return;
      object obj1 = (object) ((JToken) data).SelectToken(name + ".source");
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target = Customization.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p1 = Customization.\u003C\u003Eo__3.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = Customization.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) Customization.\u003C\u003Eo__3.\u003C\u003Ep__0, obj1, (object) null);
      if (!target((CallSite) p1, obj2))
        return;
      BitmapImage bitmapImage1 = new BitmapImage();
      bitmapImage1.BeginInit();
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      switch (Customization.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) Customization.\u003C\u003Eo__3.\u003C\u003Ep__2, obj1))
      {
        case 0:
          object obj3 = (object) ((JToken) data).SelectToken(name + ".url");
          BitmapImage bitmapImage2 = bitmapImage1;
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Customization)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          Uri uri1 = new Uri(Customization.\u003C\u003Eo__3.\u003C\u003Ep__3.Target((CallSite) Customization.\u003C\u003Eo__3.\u003C\u003Ep__3, obj3));
          bitmapImage2.UriSource = uri1;
          break;
        case 1:
          object obj4 = (object) ((JToken) data).SelectToken(name + ".url");
          string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          BitmapImage bitmapImage3 = bitmapImage1;
          string path1 = directoryName;
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Customization)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          string path2 = Customization.\u003C\u003Eo__3.\u003C\u003Ep__4.Target((CallSite) Customization.\u003C\u003Eo__3.\u003C\u003Ep__4, obj4);
          Uri uri2 = new Uri(Path.Combine(path1, path2));
          bitmapImage3.UriSource = uri2;
          break;
      }
      bitmapImage1.EndInit();
      if (element == null)
        return;
      element.ImageSource = (ImageSource) bitmapImage1;
    }

    public static void loadTextBlock(TextBlock element, string name, JObject data)
    {
      if (data == null)
        return;
      object obj1 = (object) ((JToken) data).SelectToken(name + ".Text.value");
      TextBlock textBlock1 = element;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (Customization)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target1 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p3 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target2 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p1 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__0, obj1, (object) null);
      object obj3;
      if (!target2((CallSite) p1, obj2))
      {
        obj3 = (object) element.Text;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        obj3 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__2, obj1);
      }
      string str = target1((CallSite) p3, obj3);
      textBlock1.Text = str;
      object obj4 = (object) ((JToken) data).SelectToken(name + ".FontFamily.value");
      TextBlock textBlock2 = element;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target3 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p5 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__5;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__4.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__4, obj4, (object) null);
      FontFamily fontFamily;
      if (!target3((CallSite) p5, obj5))
      {
        fontFamily = element.FontFamily;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__7 = CallSite<Func<CallSite, Type, object, FontFamily>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, Type, object, FontFamily> target4 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, Type, object, FontFamily>> p7 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__7;
        Type type = typeof (FontFamily);
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__6.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__6, obj4);
        fontFamily = target4((CallSite) p7, type, obj6);
      }
      textBlock2.FontFamily = fontFamily;
      object obj7 = (object) ((JToken) data).SelectToken(name + ".Padding.value");
      TextBlock textBlock3 = element;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target5 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__9.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p9 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__9;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj8 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__8.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__8, obj7, (object) null);
      Thickness thickness;
      if (!target5((CallSite) p9, obj8))
      {
        thickness = element.Padding;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int> target6 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__11.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int>> p11 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__11;
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__10.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__10, obj7, 0);
        double left = (double) target6((CallSite) p11, obj9);
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int> target7 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__13.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int>> p13 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__13;
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__12.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__12, obj7, 1);
        double top = (double) target7((CallSite) p13, obj10);
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int> target8 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__15.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int>> p15 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__15;
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj11 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__14.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__14, obj7, 2);
        double right = (double) target8((CallSite) p15, obj11);
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__17 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int> target9 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__17.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int>> p17 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__17;
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj12 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__16.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__16, obj7, 3);
        double bottom = (double) target9((CallSite) p17, obj12);
        thickness = new Thickness(left, top, right, bottom);
      }
      textBlock3.Padding = thickness;
      object obj13 = (object) ((JToken) data).SelectToken(name + ".FontStyles.value");
      TextBlock textBlock4 = element;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__19 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__19 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target10 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__19.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p19 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__19;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__18 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj14 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__18.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__18, obj13, (object) null);
      FontStyle fontStyle;
      if (!target10((CallSite) p19, obj14))
      {
        fontStyle = element.FontStyle;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__20 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__20 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        fontStyle = Customization.getFontStyle(Customization.\u003C\u003Eo__4.\u003C\u003Ep__20.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__20, obj13));
      }
      textBlock4.FontStyle = fontStyle;
      object obj15 = (object) ((JToken) data).SelectToken(name + ".FontSize.value");
      TextBlock textBlock5 = element;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__22 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__22 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target11 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__22.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p22 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__22;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__21 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__21 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj16 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__21.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__21, obj15, (object) null);
      double num1;
      if (!target11((CallSite) p22, obj16))
      {
        num1 = element.FontSize;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__23 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__23 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        num1 = (double) Customization.\u003C\u003Eo__4.\u003C\u003Ep__23.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__23, obj15);
      }
      textBlock5.FontSize = num1;
      object obj17 = (object) ((JToken) data).SelectToken(name + ".FontWeight.value");
      TextBlock textBlock6 = element;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__25 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__25 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target12 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__25.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p25 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__25;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__24 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__24 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj18 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__24.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__24, obj17, (object) null);
      FontWeight fontWeight;
      if (!target12((CallSite) p25, obj18))
      {
        fontWeight = element.FontWeight;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__26 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__26 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        fontWeight = Customization.getFontWeight(Customization.\u003C\u003Eo__4.\u003C\u003Ep__26.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__26, obj17));
      }
      textBlock6.FontWeight = fontWeight;
      object obj19 = (object) ((JToken) data).SelectToken(name + ".VerticalAlignment.value");
      TextBlock textBlock7 = element;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__28 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__28 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target13 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__28.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p28 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__28;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__27 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__4.\u003C\u003Ep__27 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj20 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__27.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__27, obj19, (object) null);
      int num2;
      if (!target13((CallSite) p28, obj20))
      {
        num2 = (int) element.VerticalAlignment;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (Customization.\u003C\u003Eo__4.\u003C\u003Ep__29 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Customization.\u003C\u003Eo__4.\u003C\u003Ep__29 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        num2 = Customization.\u003C\u003Eo__4.\u003C\u003Ep__29.Target((CallSite) Customization.\u003C\u003Eo__4.\u003C\u003Ep__29, obj19);
      }
      textBlock7.VerticalAlignment = (VerticalAlignment) num2;
    }

    public static void loadIcon(Window element, string name, JObject data)
    {
      if (data == null)
        return;
      object obj1 = (object) ((JToken) data).SelectToken(name + ".source");
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p1 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) Customization.\u003C\u003Eo__5.\u003C\u003Ep__0, obj1, (object) null);
      if (!target1((CallSite) p1, obj2))
        return;
      // ISSUE: reference to a compiler-generated field
      if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Customization.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (Customization)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      switch (Customization.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) Customization.\u003C\u003Eo__5.\u003C\u003Ep__2, obj1))
      {
        case 0:
          object obj3 = (object) ((JToken) data).SelectToken(name + ".url");
          Window window1 = element;
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Customization)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          BitmapFrame bitmapFrame = BitmapFrame.Create(new Uri(Customization.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) Customization.\u003C\u003Eo__5.\u003C\u003Ep__3, obj3)));
          window1.Icon = (ImageSource) bitmapFrame;
          break;
        case 1:
          object obj4 = (object) ((JToken) data).SelectToken(name + ".url");
          string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__5.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (Customization)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          string path2 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__4.Target((CallSite) Customization.\u003C\u003Eo__5.\u003C\u003Ep__4, obj4);
          object obj5 = (object) new Uri(Path.Combine(directoryName, path2));
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__5.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target2 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__7.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p7 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__7;
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__5.\u003C\u003Ep__6 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Exists", (IEnumerable<Type>) null, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target3 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__6.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p6 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__6;
          Type type = typeof (File);
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__5.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "LocalPath", typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj6 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__5.Target((CallSite) Customization.\u003C\u003Eo__5.\u003C\u003Ep__5, obj5);
          object obj7 = target3((CallSite) p6, type, obj6);
          if (!target2((CallSite) p7, obj7))
            break;
          Window window2 = element;
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__9 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__5.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, ImageSource>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (ImageSource), typeof (Customization)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, ImageSource> target4 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__9.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, ImageSource>> p9 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__9;
          // ISSUE: reference to a compiler-generated field
          if (Customization.\u003C\u003Eo__5.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Customization.\u003C\u003Eo__5.\u003C\u003Ep__8 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Create", (IEnumerable<Type>) null, typeof (Customization), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj8 = Customization.\u003C\u003Eo__5.\u003C\u003Ep__8.Target((CallSite) Customization.\u003C\u003Eo__5.\u003C\u003Ep__8, typeof (BitmapFrame), obj5);
          ImageSource imageSource = target4((CallSite) p9, obj8);
          window2.Icon = imageSource;
          break;
      }
    }
  }
}

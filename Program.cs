// Decompiled with JetBrains decompiler
// Type: LidlAvailabilityNotifierUI.Program
// Assembly: LidlAvailabilityNotifierUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5865371B-D735-43D4-A5D6-EC04D94FE962
// Assembly location: C:\Users\Bartek\Downloads\LidlAvailabilityNotifierUI.exe

using System;
using System.Windows.Forms;

namespace LidlAvailabilityNotifierUI
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}

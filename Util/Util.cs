﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Windows;

namespace YTY.amt
{
  public static class Util
  {
    private const string CSIDDIGITS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!-._~";
    private static readonly DateTime UNIXTIMESTAMPBASE = new DateTime(1970, 1, 1, 0, 0, 0);
    private static readonly int NUMCSIDDIGITS;
    private static readonly MD5 md5 = MD5.Create();
    private static readonly SHA1 sha1 = SHA1.Create();

    static Util()
    {
      NUMCSIDDIGITS = CSIDDIGITS.Length;
    }

    public static DateTime FromUnixTimestamp(int unixTimestamp)
    {
      return UNIXTIMESTAMPBASE + TimeSpan.FromSeconds(unixTimestamp);
    }

    public static uint Csid2Int(string csid)
    {
      var weight = 1;
      return (uint)csid.Aggregate(0, (sum, digit) =>
      {
        sum += CSIDDIGITS.IndexOf(digit) * weight;
        weight *= NUMCSIDDIGITS;
        return sum;
      });
    }

    public static string Int2Csid(int value)
    {
      var ret = string.Empty;
      do
      {
        ret += CSIDDIGITS[value % NUMCSIDDIGITS];
        value = value / NUMCSIDDIGITS;
      } while (value > 0);
      return ret;
    }

    public static string GetFileMd5(string fileName)
    {
      return BitConverter.ToString(md5.ComputeHash(File.ReadAllBytes(fileName))).Replace("-", string.Empty).ToLower();
    }

    public static string GetFileSha1(string fileName)
    {
      return BitConverter.ToString(sha1.ComputeHash(File.ReadAllBytes(fileName))).Replace("-", string.Empty).ToLower();
    }

    public static IEnumerable<Size> GetScreenResolutions()
    {
      return Inner().Distinct().OrderByDescending(s => s.Width).ThenByDescending(s => s.Height);

      IEnumerable<Size> Inner()
      {
        var dm = new DEVMODE();
        var i = 0;
        while (EnumDisplaySettings(null, i++, ref dm))
          yield return new Size(dm.dmPelsWidth, dm.dmPelsHeight);
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DEVMODE
    {
      private const int CCHDEVICENAME = 0x20;
      private const int CCHFORMNAME = 0x20;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
      public string dmDeviceName;
      public short dmSpecVersion;
      public short dmDriverVersion;
      public short dmSize;
      public short dmDriverExtra;
      public int dmFields;
      public int dmPositionX;
      public int dmPositionY;
      public ScreenOrientation dmDisplayOrientation;
      public int dmDisplayFixedOutput;
      public short dmColor;
      public short dmDuplex;
      public short dmYResolution;
      public short dmTTOption;
      public short dmCollate;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
      public string dmFormName;
      public short dmLogPixels;
      public int dmBitsPerPel;
      public int dmPelsWidth;
      public int dmPelsHeight;
      public int dmDisplayFlags;
      public int dmDisplayFrequency;
      public int dmICMMethod;
      public int dmICMIntent;
      public int dmMediaType;
      public int dmDitherType;
      public int dmReserved1;
      public int dmReserved2;
      public int dmPanningWidth;
      public int dmPanningHeight;
    }

    [DllImport("user32")]
    private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
  }
}
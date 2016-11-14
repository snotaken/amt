﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace YTY.amt
{
  public class ConfigModel : INotifyPropertyChanged
  {
    private string hawkempirePath;
    private int currentGameVersion;

    public string HawkempirePath
    {
      get { return hawkempirePath; }
      set
      {
        hawkempirePath = value;
        DAL.SaveHawkempirePath(this);
        OnPropertyChanged(nameof(HawkempirePath));
      }
    }

    public int CurrentGameVersion
    {
      get { return currentGameVersion; }
      set
      {
        currentGameVersion = value;
        
      }
    }

    public ConfigModel() { }

    public ConfigModel(string hawkempirePath)
    {
      this.hawkempirePath = hawkempirePath;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
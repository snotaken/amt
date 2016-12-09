﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Forms;

namespace YTY.amt
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
      My.App.Shutdown();
    }

    private void btnSetHawkempirePath_Click(object sender, RoutedEventArgs e)
    {
      var fbd = new FolderBrowserDialog();
      if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
      txbHawkempirePath.Text = fbd.SelectedPath;
    }

    private void TextBox_Error(object sender, ValidationErrorEventArgs e)
    {
      Debug.WriteLine(e.Action);
    }
  }
}
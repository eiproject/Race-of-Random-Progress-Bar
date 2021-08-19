using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using ProgressBarRace.Model;

namespace ProgressBarRace {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    private TheBar _bar111;
    private TheBar _bar112;
    private TheBar _bar121;
    private TheBar _bar122;
    private TheBar _bar21;
    private TheBar _bar22;
    private TheBar _bar31;
    private TheBar _bar32;
    public MainWindow() {
      InitializeComponent();
      InitProgressBar();
    }

    private void StartButton(object sender, RoutedEventArgs e) {
      Thread t1 = new Thread(() => _bar111.StartProgress());
      t1.Start();
      Thread t2 = new Thread(() => _bar112.StartProgress());
      t2.Start();
      Thread t3 = new Thread(() => _bar121.StartProgress());
      t3.Start();
      Thread t4 = new Thread(() => _bar122.StartProgress());
      t4.Start();
    }

    private void InitProgressBar() {
      _bar111 = new TheBar(Bar111);
      _bar112 = new TheBar(Bar112);
      _bar121 = new TheBar(Bar121);
      _bar122 = new TheBar(Bar122);
      _bar21 = new TheBar(Bar21);
      _bar22 = new TheBar(Bar22);
      _bar31 = new TheBar(Bar31);
      _bar32 = new TheBar(Bar32);
    }
  }
}

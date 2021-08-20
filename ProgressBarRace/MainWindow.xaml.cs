using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private bool _isWinnerAlreadyDecided = false;
    private TheBar _bar111;
    private TheBar _bar112;
    private TheBar _bar121;
    private TheBar _bar122;
    private TheBar _bar21;
    private TheBar _bar22;
    private TheBar _bar31;
    private TheBar _bar32;

    private Thread _threadBar111;
    private Thread _threadBar112;
    private Thread _threadBar121;
    private Thread _threadBar122;
    private Thread _threadBar21;
    private Thread _threadBar22;
    private Thread _threadBar31;
    private Thread _threadBar32;

    private Thread _threadFirstRace;
    private Thread _threadSecondRace;
    private Thread _threadFinalRace;
    private Thread _threadListenerFinalRace;

    private Thread _secondRaceTeam1;
    private Thread _secondRaceTeam2;

    private Thread _threadFinalRaceTeam1;
    private Thread _threadFinalRaceTeam2;

    private Thread _threadListenerFinalRaceTeam1;
    private Thread _threadListenerFinalRaceTeam2;

    public MainWindow() {
      InitializeComponent();
      InitializeBackendComponent();
    }

    private void StartButton(object sender, RoutedEventArgs e) {
      ResetRace();
      StartRace();
      StartBtn.IsEnabled = false;
      StartBtn.Foreground = new SolidColorBrush(Colors.Gray);
    }

    private void ResetButton(object sender, RoutedEventArgs e) {
      StartBtn.IsEnabled = true;
      StartBtn.Foreground = new SolidColorBrush(Colors.White);
      MainWindow win2 = new MainWindow();
      win2.Show();
      Application.Current.MainWindow.Close();
      this.Close();
    }

    private void InitializeBackendComponent() {
      InitProgressBar();
      InitThread();
      InitRaceRound();
      InitRaceThread();
    }

    private void StartRace() {
      _threadFirstRace.Start();
      _threadSecondRace.Start();
      _threadFinalRace.Start();
      _threadListenerFinalRace.Start();
    }

    private void ResetRace() {
      // Environment.Exit(Environment.ExitCode);
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

    private void InitThread() {
      _threadBar111 = new Thread(() => _bar111.StartProgress());
      _threadBar112 = new Thread(() => _bar112.StartProgress());
      _threadBar121 = new Thread(() => _bar121.StartProgress());
      _threadBar122 = new Thread(() => _bar122.StartProgress());
      _threadBar21 = new Thread(() => _bar21.StartProgress());
      _threadBar22 = new Thread(() => _bar22.StartProgress());
      _threadBar31 = new Thread(() => _bar31.StartProgress());
      _threadBar32 = new Thread(() => _bar32.StartProgress());
    }

    private void InitRaceRound() {
      _threadFirstRace = new Thread(FirstRace);
      _threadSecondRace = new Thread(SecondRace);
      _threadFinalRace = new Thread(FinalRace);
      _threadListenerFinalRace = new Thread(ListenerFinalRace);
    }

    private void InitRaceThread() {
      _secondRaceTeam1 = new Thread(SecondRaceTeam1);
      _secondRaceTeam2 = new Thread(SecondRaceTeam2);
      _threadFinalRaceTeam1 = new Thread(FinalRaceTeam1);
      _threadFinalRaceTeam2 = new Thread(FinalRaceTeam2);
      _threadListenerFinalRaceTeam1 = new Thread(ListenerFinalRaceTeam1);
      _threadListenerFinalRaceTeam2 = new Thread(ListenerFinalRaceTeam2);
    }

    private void FirstRace() {
      _threadBar111.Start();
      _threadBar112.Start();
      _threadBar121.Start();
      _threadBar122.Start();
    }

    private void SecondRace() {
      _secondRaceTeam1.Start();
      _secondRaceTeam2.Start();
    }

    private void FinalRace() {
      _threadFinalRaceTeam1.Start();
      _threadFinalRaceTeam2.Start();
    }

    private void ListenerFinalRace() {
      _threadListenerFinalRaceTeam1.Start();
      _threadListenerFinalRaceTeam2.Start();
    }

    private void SecondRaceTeam1() {
      _threadBar111.Join();
      _threadBar112.Join();
      _threadBar21.Start();
    }

    private void SecondRaceTeam2() {
      _threadBar121.Join();
      _threadBar122.Join();
      _threadBar22.Start();
    }

    private void FinalRaceTeam1() {
      _secondRaceTeam1.Join();
      _threadBar21.Join();
      _threadBar31.Start();

    }

    private void FinalRaceTeam2() {
      _secondRaceTeam2.Join();
      _threadBar22.Join();
      _threadBar32.Start();
    }

    private void ListenerFinalRaceTeam1() {
      _threadFinalRaceTeam1.Join();
      _threadBar31.Join();
      ChangeWinnerToTeam1();
    }

    private void ListenerFinalRaceTeam2() {
      _threadFinalRaceTeam2.Join();
      _threadBar32.Join();
      ChangeWinnerToTeam2();
    }

    private void ChangeWinnerToTeam1() {
      if (!_isWinnerAlreadyDecided) {
        this.Dispatcher.Invoke((Action)(() => {
          WinnerBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF06B07A"));
          WinnerBtn.Foreground = new SolidColorBrush(Colors.White);
          WinnerBtn.Content = "Team 1 Win!";
          WinnerBtn.IsEnabled = true;
          _isWinnerAlreadyDecided = true;
        }));
      }
    }

    private void ChangeWinnerToTeam2() {
      if (!_isWinnerAlreadyDecided) {
        this.Dispatcher.Invoke((Action)(() => {
          WinnerBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB07A06"));
          WinnerBtn.Foreground = new SolidColorBrush(Colors.White);
          WinnerBtn.Content = "Team 2 Win!";
          WinnerBtn.IsEnabled = true;
          _isWinnerAlreadyDecided = true;
        }));
      }
    }
  }
}

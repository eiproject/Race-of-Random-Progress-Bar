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

    private Thread _threadBar111;
    private Thread _threadBar112;
    private Thread _threadBar121;
    private Thread _threadBar122;
    private Thread _threadBar21;
    private Thread _threadBar22;
    private Thread _threadBar31;
    private Thread _threadBar32;

    private Thread _firstRace;
    private Thread _secondRace;
    private Thread _finalRace;

    private Thread _firstRaceTeam1;
    private Thread _firstRaceTeam2;
    private Thread _secondRaceTeam1;
    private Thread _secondRaceTeam2;

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
    }

    private void InitializeBackendComponent() {
      InitProgressBar();
      InitThread();
      InitRaceRound();
    }

    private void StartRace() {
      _firstRace.Start();
      _secondRace.Start();
      _finalRace.Start();
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

      _threadBar111.Name = "Team 1";
      _threadBar112.Name = "Team 1";
      _threadBar121.Name = "Team 2";
      _threadBar122.Name = "Team 2";
    }

    private void InitRaceRound() {
      _firstRace = new Thread(FirstRace);
      _secondRace = new Thread(SecondRace);
      _finalRace = new Thread(FinalRace);
    }

    private void FirstRace() {
      _threadBar111.Start();
      _threadBar112.Start();
      _threadBar121.Start();
      _threadBar122.Start();
    }

    private void SecondRace() {
      _secondRaceTeam1 = new Thread(SecondRaceTeam1);
      _secondRaceTeam2 = new Thread(SecondRaceTeam2);
      _secondRaceTeam1.Start();
      _secondRaceTeam2.Start();
    }

    private void FinalRace() {
      new Thread(FinalRaceTeam1).Start();
      new Thread(FinalRaceTeam2).Start();
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
  }
}

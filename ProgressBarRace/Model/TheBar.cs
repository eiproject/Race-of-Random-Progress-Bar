using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ProgressBarRace.Model {
  public class TheBar : INotifyPropertyChanged {
    private ProgressBar _bar;
    private int _progress = 0;
    private Random _random;
    public int Progress {
      get { return _progress; }
      set {
        _progress = value;
        _bar.Value = _progress;
        OnPropertyChanged("Progress");
      }
    }
    internal TheBar(ProgressBar bar) {
      _bar = bar;
      _random = new Random();
    }

    internal void StartProgress() {
      // _progress = 0;
      while (_progress <= 1000) {
        _progress += GetRandomNumber();
        _bar.Dispatcher.Invoke(() => _bar.Value = _progress, DispatcherPriority.Background);
        Thread.Sleep(100);
      }
    }

    internal void Reset() {
      _progress = 0;
    }

    private int GetRandomNumber() {
      int a = 0;
      int b = 50;
      if (Thread.CurrentThread.Name == "Team 2") {
        b = 40;
      }
      return _random.Next(a, b);
    }

    private void OnPropertyChanged(string propertyName) {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}

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
    private int _progress;
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
      int i = 0;
      while (i <= 1000) {
        _bar.Dispatcher.Invoke(() => _bar.Value = i, DispatcherPriority.Background);
        i += GetRandomNumber();
        Thread.Sleep(100);
      }
    }

    private int GetRandomNumber() {
      return _random.Next(0, 30);
    }

    private void OnPropertyChanged(string propertyName) {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}

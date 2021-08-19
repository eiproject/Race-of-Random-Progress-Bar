using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProgressBar.Model {
  public class ProgressBar : INotifyPropertyChanged {
    private int _progress;
    public int Progress {
      get { return _progress; }
      set {
        _progress = value;
        OnPropertyChanged("Progress");
      }
    }

    private void OnPropertyChanged(string propertyName) {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}

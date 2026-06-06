using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace XamlApp1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window, INotifyPropertyChanged
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = this;

      //command definition
      ChangeValueCommand = new RoutedCommand(Guid.NewGuid().ToString(), typeof(MainWindow));
      //command binding registration
      CommandBindings.Add(new CommandBinding(ChangeValueCommand, OnChangeValueCommand));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    //classic WPF implementation
    private void OnChangeValueCommand(object sender, ExecutedRoutedEventArgs e)
    {
      Result += Convert.ToInt32(e.Parameter);
      //notify value update
      Notify("Result");
    }

    public ICommand ChangeValueCommand { get; set; }
    public int Result { get; set; }

    //value update notification
    void Notify([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}

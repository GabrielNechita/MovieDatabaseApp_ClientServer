using System;
using System.Windows;
using System.Windows.Navigation;
using MovieDatabase.UI.ViewModel;

namespace MovieDatabase.UI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private LoginViewModel _viewModel;

    public MainWindow()
    {
      InitializeComponent();

      _viewModel = new LoginViewModel();

      DataContext = _viewModel;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      if (_viewModel.IsValidLogin())
      {
      }
      else
      {

      }
    }
  }
}

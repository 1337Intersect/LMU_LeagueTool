// ===== MainWindow.xaml.cs =====
using System;
using System.Windows;

namespace LMU.RacingLeague
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        // ===== DRIVER EVENT HANDLERS =====
        private async void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.AddDriverAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nell'aggiunta del pilota: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateDriver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.UpdateDriverAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nella modifica del pilota: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteDriver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_viewModel.SelectedDriver == null)
                {
                    MessageBox.Show("Seleziona un pilota da eliminare", "Attenzione",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Sei sicuro di voler eliminare {_viewModel.SelectedDriver.FullName}?",
                    "Conferma eliminazione", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await _viewModel.DeleteDriverAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nell'eliminazione del pilota: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearDriverForm_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NewDriverFirstName = "";
            _viewModel.NewDriverLastName = "";
            _viewModel.NewDriverShortName = "";
            _viewModel.NewDriverNationality = "";
            _viewModel.NewDriverCategory = Models.DriverCategory.Bronze;
            _viewModel.SelectedDriver = null;
        }

        // ===== TEAM EVENT HANDLERS =====
        private async void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.AddTeamAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nell'aggiunta del team: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.UpdateTeamAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nella modifica del team: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_viewModel.SelectedTeam == null)
                {
                    MessageBox.Show("Seleziona un team da eliminare", "Attenzione",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Sei sicuro di voler eliminare {_viewModel.SelectedTeam.Name}?",
                    "Conferma eliminazione", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await _viewModel.DeleteTeamAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nell'eliminazione del team: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearTeamForm_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NewTeamName = "";
            _viewModel.NewTeamShortName = "";
            _viewModel.NewTeamCountry = "";
            _viewModel.NewTeamPrimaryColor = "#FF0000";
            _viewModel.SelectedTeam = null;
        }

        // ===== CAR EVENT HANDLERS =====
        private async void AddCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.AddCarAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nell'aggiunta dell'auto: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.UpdateCarAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nella modifica dell'auto: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_viewModel.SelectedCar == null)
                {
                    MessageBox.Show("Seleziona un'auto da eliminare", "Attenzione",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Sei sicuro di voler eliminare {_viewModel.SelectedCar.DisplayName}?",
                    "Conferma eliminazione", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await _viewModel.DeleteCarAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nell'eliminazione dell'auto: {ex.Message}", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearCarForm_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NewCarName = "";
            _viewModel.NewCarManufacturer = "";
            _viewModel.NewCarCategory = Models.CarCategory.LMP2;
            _viewModel.NewCarYear = DateTime.Now.Year;
            _viewModel.SelectedCar = null;
        }
    }
}
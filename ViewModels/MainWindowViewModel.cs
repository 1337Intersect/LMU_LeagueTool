using LMU.RacingLeague.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LMU.RacingLeague
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly LMUDbContext _context;

        public ObservableCollection<Driver> Drivers { get; set; }
        public ObservableCollection<Team> Teams { get; set; }
        public ObservableCollection<Car> Cars { get; set; }

        private Driver? _selectedDriver;
        public Driver? SelectedDriver
        {
            get => _selectedDriver;
            set
            {
                _selectedDriver = value;
                OnPropertyChanged();
                if (value != null)
                {
                    NewDriverFirstName = value.FirstName;
                    NewDriverLastName = value.LastName;
                    NewDriverShortName = value.ShortName ?? "";
                    NewDriverNationality = value.Nationality;
                    NewDriverCategory = value.Category;
                }
            }
        }

        private Team? _selectedTeam;
        public Team? SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;
                OnPropertyChanged();
                if (value != null)
                {
                    NewTeamName = value.Name;
                    NewTeamShortName = value.ShortName ?? "";
                    NewTeamCountry = value.Country;
                    NewTeamPrimaryColor = value.PrimaryColor ?? "#FF0000";
                }
            }
        }

        private Car? _selectedCar;
        public Car? SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged();
                if (value != null)
                {
                    NewCarName = value.Name;
                    NewCarManufacturer = value.Manufacturer;
                    NewCarCategory = value.Category;
                    NewCarYear = value.Year;
                }
            }
        }

        // Driver Form Properties
        private string _newDriverFirstName = "";
        public string NewDriverFirstName
        {
            get => _newDriverFirstName;
            set { _newDriverFirstName = value; OnPropertyChanged(); }
        }

        private string _newDriverLastName = "";
        public string NewDriverLastName
        {
            get => _newDriverLastName;
            set { _newDriverLastName = value; OnPropertyChanged(); }
        }

        private string _newDriverShortName = "";
        public string NewDriverShortName
        {
            get => _newDriverShortName;
            set { _newDriverShortName = value; OnPropertyChanged(); }
        }

        private string _newDriverNationality = "";
        public string NewDriverNationality
        {
            get => _newDriverNationality;
            set { _newDriverNationality = value; OnPropertyChanged(); }
        }

        private DriverCategory _newDriverCategory = DriverCategory.Bronze;
        public DriverCategory NewDriverCategory
        {
            get => _newDriverCategory;
            set { _newDriverCategory = value; OnPropertyChanged(); }
        }

        // Team Form Properties
        private string _newTeamName = "";
        public string NewTeamName
        {
            get => _newTeamName;
            set { _newTeamName = value; OnPropertyChanged(); }
        }

        private string _newTeamShortName = "";
        public string NewTeamShortName
        {
            get => _newTeamShortName;
            set { _newTeamShortName = value; OnPropertyChanged(); }
        }

        private string _newTeamCountry = "";
        public string NewTeamCountry
        {
            get => _newTeamCountry;
            set { _newTeamCountry = value; OnPropertyChanged(); }
        }

        private string _newTeamPrimaryColor = "#FF0000";
        public string NewTeamPrimaryColor
        {
            get => _newTeamPrimaryColor;
            set { _newTeamPrimaryColor = value; OnPropertyChanged(); }
        }

        // Car Form Properties
        private string _newCarName = "";
        public string NewCarName
        {
            get => _newCarName;
            set { _newCarName = value; OnPropertyChanged(); }
        }

        private string _newCarManufacturer = "";
        public string NewCarManufacturer
        {
            get => _newCarManufacturer;
            set { _newCarManufacturer = value; OnPropertyChanged(); }
        }

        private CarCategory _newCarCategory = CarCategory.LMP2;
        public CarCategory NewCarCategory
        {
            get => _newCarCategory;
            set { _newCarCategory = value; OnPropertyChanged(); }
        }

        private int _newCarYear = DateTime.Now.Year;
        public int NewCarYear
        {
            get => _newCarYear;
            set { _newCarYear = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel(LMUDbContext context)
        {
            _context = context;
            Drivers = new ObservableCollection<Driver>();
            Teams = new ObservableCollection<Team>();
            Cars = new ObservableCollection<Car>();

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            var drivers = await _context.Drivers.OrderBy(d => d.LastName).ToListAsync();
            var teams = await _context.Teams.OrderBy(t => t.Name).ToListAsync();
            var cars = await _context.Cars.OrderBy(c => c.Category).ThenBy(c => c.Manufacturer).ToListAsync();

            Drivers.Clear();
            Teams.Clear();
            Cars.Clear();

            foreach (var driver in drivers) Drivers.Add(driver);
            foreach (var team in teams) Teams.Add(team);
            foreach (var car in cars) Cars.Add(car);
        }

        // Driver Commands
        public async Task AddDriverAsync()
        {
            if (string.IsNullOrWhiteSpace(NewDriverFirstName) || string.IsNullOrWhiteSpace(NewDriverLastName))
                return;

            var driver = new Driver
            {
                FirstName = NewDriverFirstName.Trim(),
                LastName = NewDriverLastName.Trim(),
                ShortName = string.IsNullOrWhiteSpace(NewDriverShortName) ? null : NewDriverShortName.Trim(),
                Nationality = NewDriverNationality.Trim(),
                Category = NewDriverCategory
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            Drivers.Add(driver);
            ClearDriverForm();
        }

        public async Task UpdateDriverAsync()
        {
            if (SelectedDriver == null) return;

            SelectedDriver.FirstName = NewDriverFirstName.Trim();
            SelectedDriver.LastName = NewDriverLastName.Trim();
            SelectedDriver.ShortName = string.IsNullOrWhiteSpace(NewDriverShortName) ? null : NewDriverShortName.Trim();
            SelectedDriver.Nationality = NewDriverNationality.Trim();
            SelectedDriver.Category = NewDriverCategory;

            await _context.SaveChangesAsync();
            OnPropertyChanged(nameof(Drivers));
            ClearDriverForm();
        }

        public async Task DeleteDriverAsync()
        {
            if (SelectedDriver == null) return;

            _context.Drivers.Remove(SelectedDriver);
            await _context.SaveChangesAsync();

            Drivers.Remove(SelectedDriver);
            SelectedDriver = null;
            ClearDriverForm();
        }

        private void ClearDriverForm()
        {
            NewDriverFirstName = "";
            NewDriverLastName = "";
            NewDriverShortName = "";
            NewDriverNationality = "";
            NewDriverCategory = DriverCategory.Bronze;
            SelectedDriver = null;
        }

        // Team Commands
        public async Task AddTeamAsync()
        {
            if (string.IsNullOrWhiteSpace(NewTeamName)) return;

            var team = new Team
            {
                Name = NewTeamName.Trim(),
                ShortName = string.IsNullOrWhiteSpace(NewTeamShortName) ? null : NewTeamShortName.Trim(),
                Country = NewTeamCountry.Trim(),
                PrimaryColor = NewTeamPrimaryColor
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            Teams.Add(team);
            ClearTeamForm();
        }

        public async Task UpdateTeamAsync()
        {
            if (SelectedTeam == null) return;

            SelectedTeam.Name = NewTeamName.Trim();
            SelectedTeam.ShortName = string.IsNullOrWhiteSpace(NewTeamShortName) ? null : NewTeamShortName.Trim();
            SelectedTeam.Country = NewTeamCountry.Trim();
            SelectedTeam.PrimaryColor = NewTeamPrimaryColor;

            await _context.SaveChangesAsync();
            OnPropertyChanged(nameof(Teams));
            ClearTeamForm();
        }

        public async Task DeleteTeamAsync()
        {
            if (SelectedTeam == null) return;

            _context.Teams.Remove(SelectedTeam);
            await _context.SaveChangesAsync();

            Teams.Remove(SelectedTeam);
            SelectedTeam = null;
            ClearTeamForm();
        }

        private void ClearTeamForm()
        {
            NewTeamName = "";
            NewTeamShortName = "";
            NewTeamCountry = "";
            NewTeamPrimaryColor = "#FF0000";
            SelectedTeam = null;
        }

        // Car Commands
        public async Task AddCarAsync()
        {
            if (string.IsNullOrWhiteSpace(NewCarName) || string.IsNullOrWhiteSpace(NewCarManufacturer)) return;

            var car = new Car
            {
                Name = NewCarName.Trim(),
                Manufacturer = NewCarManufacturer.Trim(),
                Category = NewCarCategory,
                Year = NewCarYear
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            Cars.Add(car);
            ClearCarForm();
        }

        public async Task UpdateCarAsync()
        {
            if (SelectedCar == null) return;

            SelectedCar.Name = NewCarName.Trim();
            SelectedCar.Manufacturer = NewCarManufacturer.Trim();
            SelectedCar.Category = NewCarCategory;
            SelectedCar.Year = NewCarYear;

            await _context.SaveChangesAsync();
            OnPropertyChanged(nameof(Cars));
            ClearCarForm();
        }

        public async Task DeleteCarAsync()
        {
            if (SelectedCar == null) return;

            _context.Cars.Remove(SelectedCar);
            await _context.SaveChangesAsync();

            Cars.Remove(SelectedCar);
            SelectedCar = null;
            ClearCarForm();
        }

        private void ClearCarForm()
        {
            NewCarName = "";
            NewCarManufacturer = "";
            NewCarCategory = CarCategory.LMP2;
            NewCarYear = DateTime.Now.Year;
            SelectedCar = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
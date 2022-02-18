using ChipSoft.Workshop.PatientRegistration.BussinessLogic;
using ChipSoft.Workshop.PatientRegistration.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSoft.Workshop.PatientRegistration.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            SearchPatients();
            AddPatientCommand = new RelayCommand(ExecuteAddPatientCommand);
            EditPatientCommand = new RelayCommand(ExecuteEditPatientCommand, CanExecuteEditPatientCommand);
            DeletePatientCommand = new RelayCommand(ExecuteDeletePatientCommand, CanExecuteDeletePatientCommand);
            PatientConsultsCommand = new RelayCommand(ExecutePatientConsultsCommand, CanExecutePatientConsultsCommand);
            PatientConsultsAdvancedCommand = new RelayCommand(ExecutePatientConsultsAdvancedCommand);
        }

        private ConsultsViewModel _consultsViewModel = new ConsultsViewModel();
        private void ExecutePatientConsultsAdvancedCommand()
        {
            ConsultsWindow consultsWindow = new ConsultsWindow();
            consultsWindow.DataContext = _consultsViewModel;
            consultsWindow.Show();
        }

        private bool CanExecutePatientConsultsCommand()
        {
            return SelectedItem != null;
        }

        private void ExecutePatientConsultsCommand()
        {
            ConsultsWindow consultsWindow = new ConsultsWindow();
            consultsWindow.DataContext = new ConsultsViewModel(SelectedItem);
            consultsWindow.ShowDialog();
        }

        private bool CanExecuteDeletePatientCommand()
        {
            return SelectedItem != null;
        }

        private void ExecuteDeletePatientCommand()
        {
            PatientRepository.Instance.DeletePatient(SelectedItem.Id);
            SelectedItem = null;
            SearchPatients();
        }

        private void ExecuteEditPatientCommand()
        {
            var viewModel = new PatientViewModel(SelectedItem);
            var view = new PatientWindow();
            view.DataContext = viewModel;
            viewModel.View = view;
            view.ShowDialog();
            if (viewModel.Result)
            {
                PatientRepository.Instance.EditPatient(SelectedItem.Id, viewModel.FirstName, viewModel.LastName, viewModel.DateOfBirth, viewModel.Address);
                SearchPatients();
            }
        }

        private bool CanExecuteEditPatientCommand()
        {
            return SelectedItem != null;
        }

        private void ExecuteAddPatientCommand()
        {
            var viewModel = new PatientViewModel();
            var view = new PatientWindow();
            view.DataContext = viewModel;
            viewModel.View = view;
            view.ShowDialog();
            if (viewModel.Result)
            {
                PatientRepository.Instance.AddNewPatient(viewModel.FirstName, viewModel.LastName, viewModel.DateOfBirth, viewModel.Address);
                SearchPatients();
            }
        }

        private void SearchPatients()
        {
            CurrentPatients = new ObservableCollection<Patient>(PatientRepository.Instance.SearchPatients(SearchText));
        }

        string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchPatients();
            }
        }

        ObservableCollection<Patient> _currentPatients;
        public ObservableCollection<Patient> CurrentPatients
        {
            get => _currentPatients;
            set
            {
                _currentPatients = value;
                OnPropertyChanged();
            }
        }


        Patient _selectedItem;
        public Patient SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                _consultsViewModel.Patient = value;
            }
        }



        RelayCommand _addPatientCommand;
        public RelayCommand AddPatientCommand
        {
            get => _addPatientCommand;
            set
            {
                _addPatientCommand = value;
                OnPropertyChanged();
            }
        }

        RelayCommand _editPatientCommand;
        public RelayCommand EditPatientCommand
        {
            get => _editPatientCommand;
            set
            {
                _editPatientCommand = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand DeletePatientCommand { get; }
        public RelayCommand PatientConsultsCommand { get; }
        public RelayCommand PatientConsultsAdvancedCommand { get; }

        RelayCommand _removePatientCommand;
        public RelayCommand RemovePatientCommand
        {
            get => _removePatientCommand;
            set
            {
                _removePatientCommand = value;
                OnPropertyChanged();
            }
        }
    }
}

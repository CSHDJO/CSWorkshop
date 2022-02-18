using ChipSoft.Workshop.PatientRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSoft.Workshop.PatientRegistration.ViewModels
{
    class PatientViewModel : BaseViewModel
    {
        public PatientViewModel()
        {
            Title = "Patiënt toevoegen";
            DateOfBirth = DateTime.Now;
            InitializeCommands();
        }



        public PatientViewModel(Patient patient)
        {
            Title = $"Patiënt {patient.FirstName} {patient.LastName} wijzigen";
            FirstName = patient.FirstName;
            LastName = patient.LastName;
            Address = patient.Address;
            DateOfBirth = patient.DateOfBirth;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CancelCommand = new RelayCommand(() =>
            {
                View.Close();
            });
            SaveCommand = new RelayCommand(() =>
           {
               Result = true;
               View.Close();
           }, () => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName));
        }

        public bool Result { get; private set; } = false;

        string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get => _cancelCommand;
            set
            {
                _cancelCommand = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; private set; }

        string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }
        public IWindow View { get; set; }
    }
}

using ChipSoft.Workshop.PatientRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSoft.Workshop.PatientRegistration.ViewModels
{
    class ConsultViewModel : BaseViewModel
    {
        public ConsultViewModel()
        {
            Title = "Consult toevoegen";
            DateOfConsult = DateTime.Now;
            InitializeCommands();
        }

        public ConsultViewModel(Consult consult)
        {
            Title = $"Consult {consult.DateOfConsult.ToString("dd-MM-yyyy")} wijzigen";
            Reason = consult.Reason;
            //TODO:Andere properties ook overnemen
            DateOfConsult = consult.DateOfConsult;
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
           }, () => !string.IsNullOrWhiteSpace(Reason));
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

        string _reason;
        public string Reason
        {
            get => _reason;
            set
            {
                _reason = value;
                OnPropertyChanged();
            }
        }

        DateTime _dateOfConsult;
        public DateTime DateOfConsult
        {
            get => _dateOfConsult;
            set
            {
                _dateOfConsult = value;
                OnPropertyChanged();
            }
        }
        public IWindow View { get; set; }
    }
}

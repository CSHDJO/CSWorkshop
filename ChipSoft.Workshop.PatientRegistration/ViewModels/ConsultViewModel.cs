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
    class ConsultViewModel : BaseViewModel
    {
        public ConsultViewModel()
        {
            Title = "Consult toevoegen";
            DateOfConsult = DateTime.Now;
            PossibleMedications = MedicationDefinitionRepository.Instance.GetMedicationDefinitions();
            Medications = new ObservableCollection<Medication>();
            InitializeCommands();
        }

        public ConsultViewModel(Consult consult)
        {
            Title = $"Consult {consult.DateOfConsult.ToString("dd-MM-yyyy")} wijzigen";
            PossibleMedications = MedicationDefinitionRepository.Instance.GetMedicationDefinitions();
            Reason = consult.Reason;
            Consultation = consult.Consultation;
            ActionsToDo = consult.ActionsToDo;
            Medications = new ObservableCollection<Medication>(consult.Medications);
            DateOfConsult = consult.DateOfConsult;
            InitializeCommands();
        }

        public MedicationDefinition SelectedMedicationDefinition { get; set; }

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

            AddMedicationCommand = new RelayCommand(() =>
             {
                 Medication medication = new Medication();
                 medication.MedicationDefinitionId = SelectedMedicationDefinition.Id;
                 medication.Amount = 1;
                 Medications.Add(medication);

             },
            () => SelectedMedicationDefinition != null
            );

            DeleteMedicationCommand = new RelayCommand(() =>
            {
                Medications.Remove(SelectedMedication);
            },
            () => SelectedMedication != null
            );
        }

        public Medication SelectedMedication { get; set; }

        ObservableCollection<Medication> _medications;
        public ObservableCollection<Medication> Medications
        {
            get => _medications;
            set
            {
                _medications = value;
                OnPropertyChanged();
            }
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
        public RelayCommand AddMedicationCommand { get; private set; }
        public RelayCommand DeleteMedicationCommand { get; private set; }

        string _consultation;
        public string Consultation
        {
            get => _consultation;
            set
            {
                _consultation = value;
                OnPropertyChanged();
            }
        }

        string _actionsToDo;
        public string ActionsToDo
        {
            get => _actionsToDo;
            set
            {
                _actionsToDo = value;
                OnPropertyChanged();
            }
        }
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

        public List<MedicationDefinition> PossibleMedications { get; }
        public IWindow View { get; set; }
    }
}

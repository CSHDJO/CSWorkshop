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
    internal class ConsultsViewModel : BaseViewModel
    {
        public ConsultsViewModel()
        {
        }
        public ConsultsViewModel(Patient patient)
        {
            Patient = patient;

            AddConsultCommand = new RelayCommand(ExecuteAddConsultCommand, CanExecuteAddConsultCommand);
            EditConsultCommand = new RelayCommand(ExecuteEditConsultCommand, CanExecuteEditConsultCommand);
            DeleteConsultCommand = new RelayCommand(ExecuteDeleteConsultCommand, CanExecuteDeleteConsultCommand);
        }

        private bool CanExecuteAddConsultCommand()
        {
            return Patient != null;
        }

        public RelayCommand AddConsultCommand { get; }
        public RelayCommand DeleteConsultCommand { get; }
        public RelayCommand EditConsultCommand { get; }
        private bool CanExecuteDeleteConsultCommand()
        {
            return SelectedConsult != null;
        }

        private void ExecuteDeleteConsultCommand()
        {
            ConsultRepository.Instance.DeleteConsult(SelectedConsult.Id);
            SelectedConsult = null;
            LoadConsults();
        }

        private void ExecuteEditConsultCommand()
        {
            var viewModel = new ConsultViewModel(SelectedConsult);
            var view = new ConsultWindow();
            view.DataContext = viewModel;
            viewModel.View = view;
            view.ShowDialog();
            if (viewModel.Result)
            {
                ConsultRepository.Instance.EditConsult(SelectedConsult.Id, viewModel.Reason, viewModel.Consultation, viewModel.ActionsToDo, viewModel.DateOfConsult, viewModel.Medications.ToList());
                LoadConsults();
            }
        }

        private bool CanExecuteEditConsultCommand()
        {
            return SelectedConsult != null; //TODO:implement canedit
        }

        private void ExecuteAddConsultCommand()
        {
            var viewModel = new ConsultViewModel();
            var view = new ConsultWindow();
            view.DataContext = viewModel;
            viewModel.View = view;
            view.ShowDialog();
            if (viewModel.Result)
            {
                ConsultRepository.Instance.AddNewConsult(Patient, viewModel.Reason, viewModel.Consultation, viewModel.ActionsToDo, viewModel.DateOfConsult, viewModel.Medications.ToList());
                LoadConsults();
            }
        }

        Consult _selectedConsult;
        public Consult SelectedConsult
        {
            get => _selectedConsult;
            set
            {
                _selectedConsult = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<Consult> _consultsOfPatient;
        public ObservableCollection<Consult> ConsultsOfPatient
        {
            get => _consultsOfPatient;
            set
            {
                _consultsOfPatient = value;
                OnPropertyChanged();
            }
        }

        Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged();
                if (Patient != null)
                {
                    Title = "Consults van " + Patient.LastName;
                }
                else
                {
                    Title = "Consults";
                }
                LoadConsults();
            }
        }

        private void LoadConsults()
        {
            if (Patient == null)
            {
                ConsultsOfPatient = new ObservableCollection<Consult>();
            }
            else
            {
                ConsultsOfPatient = new ObservableCollection<Consult>(ConsultRepository.Instance.GetConsultsForPatient(Patient));
            }
        }
        string _title = "Consults";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
}

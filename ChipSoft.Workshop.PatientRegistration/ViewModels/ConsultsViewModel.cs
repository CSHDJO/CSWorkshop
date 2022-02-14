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
 
            LoadConsults();
        }

        private bool CanExecuteAddConsultCommand()
        {
            return Patient != null;
        }

        public RelayCommand AddConsultCommand { get; }
       

        private void ExecuteAddConsultCommand()
        {
            var viewModel = new ConsultViewModel();
            var view = new ConsultWindow();
            view.DataContext = viewModel;
            viewModel.View = view;
            view.ShowDialog();
            if (viewModel.Result)
            {
                ConsultRepository.Instance.AddNewConsult(Patient, viewModel.Reason, "", "", viewModel.DateOfConsult);//TODO: gebruik nieuwe properties
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

            }
        }

        private void LoadConsults()
        {
            //TODO:load consults
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

using ChipSoft.Workshop.PatientRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSoft.Workshop.PatientRegistration.BussinessLogic
{

    class PatientRepository
    {
        public static PatientRepository Instance { get; } = new PatientRepository();

        private List<Patient> _data;
        private PatientRepository()
        {
            _data = new List<Patient>()
            {
                new Patient()
                {
                    Id= new Guid("{8F73D6AA-AAB9-4501-97D8-D523B1C7354F}"),
                    FirstName="Hendrik",
                    LastName="de Jonge",
                    DateOfBirth= new DateTime(1993,11,17),
                    Address="TOP SECRET"
                }
            };
        }


        public void AddNewPatient(string firstName, string lastName, DateTime dateOfBirth, string address)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("firstName must have a value");
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("lastName must have a value");
            }
            var patient = new Patient()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Address = address
            };
            _data.Add(patient);
        }

        public void DeletePatient(Guid patientId)
        {
            var patient = _data.Find(b => b.Id == patientId);
            if (patient == null)
            {
                throw new ArgumentException($"cannnot find patient by Id, Id is{patientId}");

            }
            _data.Remove(patient);
        }

        public List<Patient> SearchPatients(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return GetPatients();
            }
            return _data.Where(b => b.FirstName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
            || b.LastName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
            || (b.FirstName + " " + b.LastName).Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
            || b.Address.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).Select(b => b.Clone()).ToList();
        }

        public void EditPatient(Guid patientId, string firstName, string lastName, DateTime dateOfBirth, string address)
        {

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("firstName must have a value");
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("lastName must have a value");
            }
            var patient = _data.Find(b => b.Id == patientId);
            if (patient == null)
            {
                throw new ArgumentException($"cannnot find patient by Id, Id is{patientId}");

            }

            patient.FirstName = firstName;
            patient.LastName = lastName;
            patient.DateOfBirth = dateOfBirth;
            patient.Address = address;
        }

        public List<Patient> GetPatients()
        {
            return new List<Patient>(_data.Select(b => b.Clone()));
        }
    }
}

using ChipSoft.Workshop.PatientRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSoft.Workshop.PatientRegistration.BussinessLogic
{

    class ConsultRepository
    {
        public static ConsultRepository Instance { get; } = new ConsultRepository();

        private List<Consult> _data;
        private ConsultRepository()
        {
            _data = new List<Consult>()
            {
                new Consult()
                {
                    PatientId= new Guid("{8F73D6AA-AAB9-4501-97D8-D523B1C7354F}"),
                    Id=Guid.NewGuid(),
                    Reason="Hoofdpijn",
                    Consultation = "Patiënt geeft aan erg last te hebben van zijn hoofd",
                    ActionsToDo="Paracetamol voorschrijven",
                    DateOfConsult=DateTime.Now.AddDays(-10)
                }
            };
        }


        public void AddNewConsult(Patient patient, string reason, string consultation, string actionsToDo, DateTime dateOfConsult)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException("reason must have a value");
            }
            if (string.IsNullOrWhiteSpace(consultation))
            {
                throw new ArgumentException("consultation must have a value");
            }
            var consult = new Consult()
            {
                Id = Guid.NewGuid(),
                Reason = reason,
                DateOfConsult = dateOfConsult,
                Consultation = consultation,
                ActionsToDo = actionsToDo,
                PatientId = patient.Id
            };
            _data.Add(consult);
        }

        public void EditConsult(Guid consultId, string reason, string consultation, string actionsToDo, DateTime dateOfConsult)
        {

            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException("reason must have a value");
            }
            if (string.IsNullOrWhiteSpace(consultation))
            {
                throw new ArgumentException("consultation must have a value");
            }
            var consult = _data.Find(b => b.Id == consultId);
            if (consult == null)
            {
                throw new ArgumentException($"cannnot find consult by Id, Id is {consultId}");

            }

            consult.Reason = reason;
            consult.DateOfConsult = dateOfConsult;
            consult.Consultation = consultation;
            consult.ActionsToDo = actionsToDo;
        }
        public void DeleteConsult(Guid consultId)
        {
            var consult = _data.Find(b => b.Id == consultId);
            if (consult == null)
            {
                throw new ArgumentException($"cannnot find consult by Id, Id is {consultId}");

            }
            _data.Remove(consult);
        }
        public List<Consult> GetConsultsForPatient(Patient patient)
        {
            return new List<Consult>(_data.Where(b => b.PatientId == patient.Id).Select(b => b.Clone()));
        }
    }
}

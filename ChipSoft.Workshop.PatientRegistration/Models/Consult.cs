using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSoft.Workshop.PatientRegistration.Models
{
    public class Consult
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateOfConsult { get; set; }
        public string Reason { get; set; }
        public string Consultation { get; set; }
        public string ActionsToDo { get; set; }

        public List<Medication> Medications { get; set; } = new List<Medication>();

        public Consult Clone()
        {
            Consult clonedConsult = new Consult()
            {
                Id = Id,
                PatientId = PatientId,
                DateOfConsult = DateOfConsult,
                Reason = Reason,
                ActionsToDo = ActionsToDo,
                Consultation = Consultation,
                Medications = Medications.Select(b=> new Medication() { Amount = b.Amount, MedicationDefinitionId = b.MedicationDefinitionId }).ToList()
            };
            return clonedConsult;
        }
    }
}

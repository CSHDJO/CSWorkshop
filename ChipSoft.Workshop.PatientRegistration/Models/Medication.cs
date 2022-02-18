using ChipSoft.Workshop.PatientRegistration.BussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSoft.Workshop.PatientRegistration.Models
{
    public class Medication
    {
        public Guid MedicationDefinitionId { get; set; }
        public MedicationDefinition MedicationObject
        {
            get
            {
                return MedicationDefinitionRepository.Instance.GetMedicationDefinitions().Find(b => b.Id == MedicationDefinitionId);
            }
        }

        int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
            }
        }
    }
}

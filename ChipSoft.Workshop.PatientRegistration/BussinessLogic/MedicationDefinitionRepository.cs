using ChipSoft.Workshop.PatientRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSoft.Workshop.PatientRegistration.BussinessLogic
{
    public class MedicationDefinitionRepository
    {
        private List<MedicationDefinition> _content;

        public MedicationDefinitionRepository()
        {
            _content = new List<MedicationDefinition>()
            {
                new MedicationDefinition()
                {
                    Id=Guid.NewGuid(),
                    Name="Paracetamol"
                },
                 new MedicationDefinition()
                {
                    Id=Guid.NewGuid(),
                    Name="medicatie 2"
                }
            };
        }
        public static MedicationDefinitionRepository Instance { get; } = new MedicationDefinitionRepository();
        public List<MedicationDefinition> GetMedicationDefinitions()
        {
            return _content;
        }
    }
}

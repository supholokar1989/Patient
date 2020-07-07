using MediatR;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using Patient.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.API.Application.CommandService.Command
{
    public class CreatePatientVisitCommand : IRequest<bool>
    {
        public CreatePatientVisitCommand(PatientVisitModel patientVisit)
        {
            PatientVisit = patientVisit;
        }
        public PatientVisitModel PatientVisit { get; }
    }
}

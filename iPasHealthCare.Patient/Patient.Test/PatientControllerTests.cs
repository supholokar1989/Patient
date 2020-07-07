using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Patient.API.Application.CommandService;
using Patient.API.Application.CommandService.Command;
using Patient.API.Controllers;
using Patient.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Test
{
    [TestFixture]
    class PatientControllerTests
    {
        private Mock<IMediator> _mediator;
        PatientVisitModel patientVisit_S12;
        PatientVisitModel patientVisit_S14;
        private Mock<CreatePatientVisitCommand> _createPatientVisitCommand;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            string jsonFile_S12_NewPatientAppointment = "D:\\PELITAS\\PROJECTS\\IPAS_PatientService\\iPasHealthCare.Patient\\Patient.Test\\TestData\\S12_NewPatientAppointment.json";
            string jsonFile_S14_ExistingAppointmentUpdate = "D:\\PELITAS\\PROJECTS\\IPAS_PatientService\\iPasHealthCare.Patient\\Patient.Test\\TestData\\S14_ExistingAppointmentUpdate.json";
            patientVisit_S12 = ConvertJsonToModel(jsonFile_S12_NewPatientAppointment);
            patientVisit_S14 = ConvertJsonToModel(jsonFile_S14_ExistingAppointmentUpdate);
        }

        [Test]
        public async Task PatientController_Success_Result()
        {
            _mediator.Setup(_ => _.Send(It.IsAny<CreatePatientVisitCommand>(), new System.Threading.CancellationToken()))
                .Returns(Task.FromResult<bool>(true));
            _createPatientVisitCommand = new Mock<CreatePatientVisitCommand>(patientVisit_S12);

            var sut = new PatientController(_mediator.Object);

            var result = await sut.Post(patientVisit_S12);

            Assert.IsInstanceOf<OkResult>(result);
           
        }


        [Ignore("Ignore test")]
        public PatientVisitModel ConvertJsonToModel(string json)
        {
            PatientVisitModel patientVisitModel;
            using (StreamReader r = new StreamReader(json))
            {
                string jsonResult = r.ReadToEnd();
                patientVisitModel = JsonConvert.DeserializeObject<PatientVisitModel>(jsonResult);
            }
            return patientVisitModel;
        }

        [TearDown]
        public void Cleanup()
        {

        }
    }
}

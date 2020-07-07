using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patient.Domain.Models
{
    public class PatientVisitModel
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string TenantId { get; set; }
        public float PatientVisitDocVersion { get; set; }
        public Patientvisit PatientVisit { get; set; }
        public Patient Patient { get; set; }
        public Patientcontact[] PatientContact { get; set; }
        public Visit Visit { get; set; }
        public Gaurantor Gaurantor { get; set; }
        public Insurance[] Insurance { get; set; }
        public Observation[] Observation { get; set; }
        public Diagnosis[] Diagnosis { get; set; }
        public Procedure[] Procedure { get; set; }
        public Accident Accident { get; set; }
        public Claimoccurance[] ClaimOccurance { get; set; }
        public Appointment Appointment { get; set; }
        public Transaction[] Transaction { get; set; }
    }

    public class Patientvisit
    {
        public float VersionNum { get; set; }
        public string PatientVisitId { get; set; }
        //public string TenantId { get; set; }
        public string FacilityCode { get; set; }
        public string LastEventType { get; set; }
        public string LastEventDateTime { get; set; }
        public string LastEventEmployee { get; set; }
        public string LastEventEmployeeDepartment { get; set; }
        public string PatientAccountNumber { get; set; }
        public string VisitNumber { get; set; }
        public string AppointmentId { get; set; }
        public string PatientId { get; set; }
        public string PatientIdentifier { get; set; }
    }

    public class Patient
    {
        public string PatientMedicalRecordNumber { get; set; }
        public string PatientSSN { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientMiddleInitial { get; set; }
        public string PatientDateOfBirth { get; set; }
        public string PatientGender { get; set; }
        public string PatientRace { get; set; }
        public string PatientLanguage { get; set; }
        public string MaritalStatus { get; set; }
        public string IsDeceased { get; set; }
        public string DeceasedDateTime { get; set; }
        public string PatientMultipleBirthIndicator { get; set; }
        public string PatientMultipleBirthOrder { get; set; }
        public string PatientMotherIdentifier { get; set; }
        public string PatientEmailAddress { get; set; }
        public Patientphonenumber[] PatientPhoneNumber { get; set; }
        public Patientaddress[] PatientAddress { get; set; }
        public Primarycareprovider[] PrimaryCareProvider { get; set; }
    }

    public class Patientphonenumber
    {
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }
    }

    public class Patientaddress
    {
        public string AddressType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Primarycareprovider
    {
        public string ProviderType { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
    }

    public class Visit
    {
        public string PatientVisitNumber { get; set; }
        public string PatientAccountNumber { get; set; }
        public string PreAdmissionNumber { get; set; }
        public string PatientClass { get; set; }
        public string PatientType { get; set; }
        public string FinancialClass { get; set; }
        public string CurrentBalance { get; set; }
        public string VisitAdmissionDate { get; set; }
        public string VisitDischargeDate { get; set; }
        public string VisitLengthOfStay { get; set; }
        public string VisitAdmissionType { get; set; }
        public string VisitReadmisisonIndicator { get; set; }
        public string VisitAdmissionReason { get; set; }
        public Visitlocation[] VisitLocation { get; set; }
        public Visitadmittingprovider VisitAdmittingProvider { get; set; }
        public Visitattendingprovider VisitAttendingProvider { get; set; }
        public Visitrenderingprovider VisitRenderingProvider { get; set; }
        public Visitconsultingprovider VisitConsultingProvider { get; set; }
        public string VisitNewbornIndicator { get; set; }
    }

    public class Visitadmittingprovider
    {
        public string ProviderType { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
    }

    public class Visitattendingprovider
    {
        public string ProviderType { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
    }

    public class Visitrenderingprovider
    {
        public string ProviderType { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
    }

    public class Visitconsultingprovider
    {
        public string ProviderType { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
    }

    public class Visitlocation
    {
        public string LocationType { get; set; }
        public string Facility { get; set; }
        public string Department { get; set; }
        public string Room { get; set; }
        public string Bed { get; set; }
    }

    public class Gaurantor
    {
        public string GaurantorNumber { get; set; }
        public string GaurantorRelationToPatient { get; set; }
        public string GaurantorFirstName { get; set; }
        public string GaurantorLastName { get; set; }
        public string GaurantorMiddleInitial { get; set; }
        public string GaurantorDateOfBirth { get; set; }
        public string GaurantorSSN { get; set; }
        public string GaurantorGender { get; set; }
        public string GaurantorLanguage { get; set; }
        public string GaurantorSpouseFirstName { get; set; }
        public string GaurantorSpouseLastName { get; set; }
        public Gaurantorphonenumber[] GaurantorPhoneNumber { get; set; }
        public string GaurantorEmail { get; set; }
        public Gaurantoraddress[] GaurantorAddress { get; set; }
        public string GaurantorEmployerName { get; set; }
        public Gaurantoremployeraddress[] GaurantorEmployerAddress { get; set; }
    }

    public class Gaurantorphonenumber
    {
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }
    }

    public class Gaurantoraddress
    {
        public string AddressType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Gaurantoremployeraddress
    {
        public string AddressType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Accident
    {
        public string AccidentDateTime { get; set; }
        public string AccidentCode { get; set; }
        public string AccidentLocation { get; set; }
        public string AutoAccidentState { get; set; }
        public string AccidentJobRelatedIndicator { get; set; }
        public string AccidentDeathIndicator { get; set; }
        public Accidentaddress AccidentAddress { get; set; }
    }

    public class Accidentaddress
    {
        public string AddressType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Appointment
    {
        public string AppointmentId { get; set; }
        public string AppointmentStatus { get; set; }
        public string AppointmentType { get; set; }
        public string AppointmentStartDate { get; set; }
        public string AppointmentEndDate { get; set; }
        public string AppointmentDuration { get; set; }
        public string AppointmentDurationUnits { get; set; }
        public string AppointmentReason { get; set; }
        public Note[] Note { get; set; }
        public Appointmentservice[] AppointmentService { get; set; }
        public Appointmentresource[] AppointmentResource { get; set; }
        public Appointmentlocation[] AppointmentLocation { get; set; }
        public Appointmentpersonnel[] AppointmentPersonnel { get; set; }
    }

    public class Note
    {
        public string NoteType { get; set; }
        public string NoteSource { get; set; }
        public string NoteText { get; set; }
    }

    public class Appointmentservice
    {
        public string ServiceID { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceDuration { get; set; }
        public string ServiceDurationUnits { get; set; }
        public Note1[] Note { get; set; }
    }

    public class Note1
    {
        public string NoteType { get; set; }
        public string NoteSource { get; set; }
        public string NoteText { get; set; }
    }

    public class Appointmentresource
    {
        public string ResourceID { get; set; }
        public string ResourceType { get; set; }
        public string ResourceQuantity { get; set; }
        public string ResourceQuantityUnits { get; set; }
        public string ResourceDate { get; set; }
        public string ResourceDuration { get; set; }
        public string ResourceDurationUnits { get; set; }
        public Note2[] Note { get; set; }
    }

    public class Note2
    {
        public string NoteType { get; set; }
        public string NoteSource { get; set; }
        public string NoteText { get; set; }
    }

    public class Appointmentlocation
    {
        public string LocationPointOfCare { get; set; }
        public string LocationFacility { get; set; }
        public string LocationBuilding { get; set; }
        public string LocationFloor { get; set; }
        public string LocationRoom { get; set; }
        public string LocationBed { get; set; }
        public string LocationType { get; set; }
        public string LocationGroup { get; set; }
        public string LocationDate { get; set; }
        public string LocationDuration { get; set; }
        public string LocationDurationUnits { get; set; }
        public Note3[] Note { get; set; }
    }

    public class Note3
    {
        public string NoteType { get; set; }
        public string NoteSource { get; set; }
        public string NoteText { get; set; }
    }

    public class Appointmentpersonnel
    {
        public string PersonnelResourceRole { get; set; }
        public string PersonalNumber { get; set; }
        public string PersonnelFirstName { get; set; }
        public string PersonnelLastName { get; set; }
        public string PersonellPrefix { get; set; }
        public string PersonnelSuffix { get; set; }
        public string PersonnelResourceDate { get; set; }
        public string PersonnelResourceDuration { get; set; }
        public string PersonnelResourceDurationUnits { get; set; }
        public Note4[] Note { get; set; }
    }

    public class Note4
    {
        public string NoteType { get; set; }
        public string NoteSource { get; set; }
        public string NoteText { get; set; }
    }

    public class Patientcontact
    {
        public string ContactType { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactMiddleInitial { get; set; }
        public string ContactRelationToPatient { get; set; }
        public Contactphonenumber[] ContactPhoneNumber { get; set; }
        public Contactaddress[] ContactAddress { get; set; }
    }

    public class Contactphonenumber
    {
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }
    }

    public class Contactaddress
    {
        public string AddressType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Insurance
    {
        public string InsurancePlanId { get; set; }
        public string InsurancePlanType { get; set; }
        public string InsurancePlanName { get; set; }
        public string InsuranceMemberNumber { get; set; }
        public string InsuranceCompanyNumber { get; set; }
        public string InsuranceCompanyName { get; set; }
        public Insurancecompanyaddress InsuranceCompanyAddress { get; set; }
        public Insurancecompanyphonenumber InsuranceCompanyPhoneNumber { get; set; }
        public string InsuranceGroupNumber { get; set; }
        public string InsuranceGroupName { get; set; }
        public string InsurancePlanEffectiveDate { get; set; }
        public string InsurancePlanExpirationDate { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string InsuranceAgreementType { get; set; }
        public string InsuranceCoverageType { get; set; }
        public Insured Insured { get; set; }
    }

    public class Insurancecompanyaddress
    {
        public string AddressType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Insurancecompanyphonenumber
    {
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }
    }

    public class Insured
    {
        public string Insuredid { get; set; }
        public string InsuredIdType { get; set; }
        public string InsuredResponsibilityLevel { get; set; }
        public string InsuredRelationToPatient { get; set; }
        public string InsuredFirstName { get; set; }
        public string InsuredLastName { get; set; }
        public string InsuredMiddleInitial { get; set; }
        public string InsuredDateOfBirth { get; set; }
        public string InsuredSSN { get; set; }
        public string InsuredGender { get; set; }
        public Insuredaddress InsuredAddress { get; set; }
        public string InsuredGroupEmployerId { get; set; }
        public string InsuredGroupEmployerName { get; set; }
    }

    public class Insuredaddress
    {
        public string AddressType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Observation
    {
        public string ObservationType { get; set; }
        public string ObservationNumber { get; set; }
        public object[] ObservationValue { get; set; }
        public string ObservationUnits { get; set; }
        public string ObservationRange { get; set; }
        public object[] ObservationAbnormalFlag { get; set; }
        public string ObservationProbability { get; set; }
        public object[] ObservationNatureOfAbnormalTest { get; set; }
        public string ObservationResultStatus { get; set; }
        public string ObservationLastNormalResultDate { get; set; }
        public string ObservationProducerId { get; set; }
        public Observationresponsibleobserver ObservationResponsibleObserver { get; set; }
    }

    public class Observationresponsibleobserver
    {
        public string ProviderType { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public string ObservationMethod { get; set; }
    }

    public class Diagnosis
    {
        public string DiagnosisType { get; set; }
        public string DiagnosisCode { get; set; }
        public string DiagnosisDescription { get; set; }
        public string DiagnosisDateTime { get; set; }
        public string DiagnosisCondition { get; set; }
        public string DiagnosisRank { get; set; }
        public string DiagnosisRole { get; set; }
        public Diagnosisclinician[] DiagnosisClinician { get; set; }
        public string DiagnosisClassification { get; set; }
        public string DiagnosisConfidentialIndicator { get; set; }
        public string DiagnosisAssertationDateTime { get; set; }
    }

    public class Diagnosisclinician
    {
        public string ProviderType { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
    }

    public class Procedure
    {
        public string ProcedureNumber { get; set; }
        public string ProcedureType { get; set; }
        public string ProcedureCode { get; set; }
        public string ProcedureDate { get; set; }
        public string ProcedureModifier { get; set; }
        public Procedureprovider[] ProcedureProvider { get; set; }
        public string ProcedureAthesiaCode { get; set; }
        public string ProcedureAnthesiaMinutes { get; set; }
        public string ProcedurePriority { get; set; }
        public string ProcedureAssociatedDiagnosisCode { get; set; }
    }

    public class Procedureprovider
    {
        public string ProviderType { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
    }

    public class Claimoccurance
    {
        public string OccuranceCode { get; set; }
        public string OccuranceDate { get; set; }
    }

    public class Transaction
    {
        public string MessageId { get; set; }
        public string FacilityCode { get; set; }
        public string EventType { get; set; }
        public string EventDateTime { get; set; }
        public string EmployeeType { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }

}

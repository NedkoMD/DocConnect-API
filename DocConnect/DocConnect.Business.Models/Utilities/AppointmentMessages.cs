namespace DocConnect.Business.Models.Utilities
{
    public static class AppointmentMessages
    {
        public const string AppointmentNotFoundMessage = "Appointment does not exist in the database";

        public const string AppointmentTimeSlotNotInWorkingHours = "Appointment time does not fit into working hours";

        public const string AppointmentOutOfDateMessage = "You can only make an appointment for the next 30 days starting from tomorrow";

        public const string PatientAppointmentHourAlreadyTaken = "You already have an appointment for this period";

        public const string DoctorAppointmentHourAlreadyTaken = "Doctor already has an appointment for this period";

        
    }
}

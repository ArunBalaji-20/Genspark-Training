namespace FirstAPI.Models
{
    // public class Patient
    // {
    //     public int Id { get; set; }
    //     public string Name { get; set; } = string.Empty;

    //     public int Age { get; set; }

    //     public int Contact { get; set; }
    // }

      public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ICollection<Appointment>? Appointments { get; set; }
    }
}


namespace ShopAPI.DTOs
{
    public class SpecializationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public decimal Premium { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public List<SpecializationDto> Specializations { get; set; } = new();
    }

    public class SpecializationWithCountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DoctorCount { get; set; }
    }

    public class DepartmentStatisticsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TotalExpenses { get; set; }
        public decimal AverageSalary { get; set; }
        public int DoctorCount { get; set; }
    }

    public class DepartmentWithSpecializationsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> Specializations { get; set; } = new();
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorGenericRepository.Model
{
    public partial class Employee : IEntity
    {
        [NotMapped]
        public int Id => EmployeeId;

        public int EmployeeId { get; set; }
        public string EmployeeGovtId { get; set; }
        public string EmployeeName { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public decimal? Salary { get; set; }
        public string Designation { get; set; }
        public bool? HasDependents { get; set; }
    }
}

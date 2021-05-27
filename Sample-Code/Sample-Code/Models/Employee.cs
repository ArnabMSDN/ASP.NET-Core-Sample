using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Code.Models
{
    public class Employee :BaseEntity
    {
        [Key]
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }              
        public string BankAccountNo { get; set; }
    }

    public class EmployeeViewModel
    {
        public int? EmpId { get; set; }

        [Required]
        public string EmpName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string BankAccountNo { get; set; }
    }
}

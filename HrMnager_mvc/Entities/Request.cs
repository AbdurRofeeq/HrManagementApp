using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Enums;

namespace HrMnager_mvc.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateApproved { get; set; }
        public int? ApprovedById { get; set; }
        public HrManager? ApprovedBy { get; set; }
        public DateTime LeaveStartDate { get; set; } = default!;
        public DateTime LeaveEndDate { get; set; } = default!;
    }
}
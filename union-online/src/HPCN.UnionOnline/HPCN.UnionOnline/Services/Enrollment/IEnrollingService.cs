using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IEnrollingService
    {
        bool IsReadyForEnrolling(Enrollment enrollment);
        Task<bool> ExceedsMaxCountOfEnrollees(Enrollment enrollment);
        Task<bool> IsAlreadyEnrolled(string employeeNo, Enrollment enrollment);
        Task<Enrolling> CreateAsync(Guid enrollmentId, string employeeNo, string emailAddress, string name, string phoneNumber, IDictionary<string, string> fieldInputs, Guid userId, string createdBy);
    }
}

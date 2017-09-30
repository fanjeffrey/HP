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
        Task<Enrolling> GetEnrollingIncludingEnrollmentAndFieldInputsAsync(Guid enrollingId);
        Task<List<Enrolling>> GetEnrollingsAsync(Guid userId);
        Task<Dictionary<Guid, int>> GetCountOfEnrollingsInEnrollments(IEnumerable<Guid> enrollmentIds);
        Task<Enrolling> CreateAsync(Guid enrollmentId, string employeeNo, IDictionary<string, string> fieldInputs, Guid userId, string createdBy);
        Task<Enrolling> UpdateAsync(Guid enrollingId, string employeeNo, Dictionary<string, string> fieldInputs, Guid userId, string updatedBy);
        Task CancelAsync(Guid enrollingId);
    }
}

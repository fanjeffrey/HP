using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IEnrollmentActivityService
    {
        Task<bool> ExistsAsync(string name);
        Task<EnrollmentActivity> CreateAsync(string name, DateTime beginTime, DateTime endTime, string description, string creator);
        Task<int> CountAsync(string keyword);
        Task<IList<EnrollmentActivity>> SearchAsync(string keyword, int pageIndex, int pageSize);
    }
}

using HPCN.UnionOnline.Models;
using System;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IPointsService
    {
        Task<double> CalculatePointsTotal(Guid userId);
        double CalculatePointsTotal(User user);
        Task<double> CalculatePointsBalance(Guid userId);
        Task<double> CalculateConsumedPoints(Guid userId);
    }
}

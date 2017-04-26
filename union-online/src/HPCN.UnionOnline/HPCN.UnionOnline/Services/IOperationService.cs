using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCN.UnionOnline.Site.ViewModels;
using HPCN.UnionOnline.Models;

namespace HPCN.UnionOnline.Services
{
    public interface IOperationService
    {
        Task LogProductCreation(Guid productId, string username);
        Task LogProductUpdate(Guid id, string v);
    }
}

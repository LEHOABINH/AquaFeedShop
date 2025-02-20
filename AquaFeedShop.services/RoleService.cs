using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Transactions;
using AquaFeedShop.core.Interfaces;
using AquaFeedShop.core.Models;

using AquaFeedShop.services.Interfaces;
using AquaFeedShop.shared.Extensions;
using AquaFeedShop.shared.Models.Request;
using AquaFeedShop.shared.Models.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Task = System.Threading.Tasks.Task;

namespace AquaFeedShop.services
{
    public class RoleService : IRoleService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<Role?> GetRoleById(int roleId)
        {
            var role = _unitOfWork.Roles.GetByIDAsync(roleId);
            return role;
        }

    }
}

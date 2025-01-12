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
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> GetAllProduct()
        {
            var productList = await _unitOfWork.Products.GetAsync(
                includeProperties: "Category,Supplier"
            );

            var result = productList.Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.Price,
                p.Stock,
                p.Unit,
                p.Image,
                p.Description,
                Category = new
                {
                    p.Category?.CategoryId,
                    p.Category?.CategoryName
                },
                Supplier = new
                {
                    p.Supplier?.SupplierId,
                    p.Supplier?.SupplierName
                }
            });

            return result;
        }


    }
}

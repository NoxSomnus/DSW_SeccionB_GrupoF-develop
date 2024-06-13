using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.Queries;

namespace UCABPagaloTodoMS.Application.RefactoringMethods
{
    public class AllRequiredFieldsByOption
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;

        public AllRequiredFieldsByOption(IUCABPagaloTodoDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<List<PaymentRequiredFieldsResponse>> requiredfields(AllRequiredFieldsByPaymentOptionQuery request)
        {


            var fields = _dbContext.PaymentRequiredFieldEntities.Where(c => c.PaymentOptionId == request.PaymentOptionId).Select(c => new PaymentRequiredFieldsResponse()
            {
                PaymentOptionId = c.PaymentOptionId,
                RequiredFieldId = c.Id,
                FieldName = c.FieldName,
                isNumber = c.isNumber,
                isString = c.isString,
                Length = c.Length
            });
            return await fields.ToListAsync();
            
        }
    }
}

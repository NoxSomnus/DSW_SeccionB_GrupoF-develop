using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Models;

namespace UCABPagaloTodoMS.Application.Mappers
{
    public class ConciliationfileConfigMapper
    {
        public static ConciliationFileConfigureEntity MapRequestToEntity(AddConciliationFileConfigRequest request)
        {
            var entity = new ConciliationFileConfigureEntity()
            {
                IncludeDni = request.IncludeDni,
                IncludeName = request.IncludeName,
                IncludeLastname = request.IncludeLastname,
                IncludeUsername = request.IncludeUsername,
                IncludeEmail = request.IncludeEmail,
                IncludePhoneNumber = request.IncludePhoneNumber,
                IncludeAmount = request.IncludeAmount,
                IncludeBillDate = request.IncludeBillDate,
                IncludeContractnumber = request.IncludeContractnumber,
                ProviderId = request.ProviderId,
                ServiceId = request.ServiceId
            };
            return entity;
        }
        public static ConciliacionFileConfiguration MapentityModel(ConciliationFileConfigureEntity request)
        {
            var entity = new ConciliacionFileConfiguration()
            {
                IncludeDni = request.IncludeDni,
                IncludeName = request.IncludeName,
                IncludeLastname = request.IncludeLastname,
                IncludeUsername = request.IncludeUsername,
                IncludeEmail = request.IncludeEmail,
                IncludePhoneNumber = request.IncludePhoneNumber,
                IncludeAmount = request.IncludeAmount,
                IncludeBillDate = request.IncludeBillDate,
                
            };
            return entity;
        }
    }
}

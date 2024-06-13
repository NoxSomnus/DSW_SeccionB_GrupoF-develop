using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Models;

namespace UCABPagaloTodoMS.Core.Interfaces
{
    public interface IConciliationFileBuilder
    {
        IConciliationFileBuilder IncludeDni ();
        IConciliationFileBuilder IncludeName ();
        IConciliationFileBuilder IncludeLastName ();
        IConciliationFileBuilder IncludeUserName ();
        IConciliationFileBuilder IncludeEmail ();
        IConciliationFileBuilder IncludePhoneNumber ();
        //datos de bill
        IConciliationFileBuilder IncludeAmount ();
        IConciliationFileBuilder IncludeBillDate ();
        IConciliationFileBuilder IncludeContractNumber ();
        byte[] Build(List<BillEntity> bills, Guid providerID, Guid serviceID, ConciliacionFileConfiguration configuration, IUCABPagaloTodoDbContext _dbContext);
    }
}

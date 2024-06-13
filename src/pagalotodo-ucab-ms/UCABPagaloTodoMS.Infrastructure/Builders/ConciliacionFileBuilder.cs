﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Interfaces;
using UCABPagaloTodoMS.Core.Models;

namespace UCABPagaloTodoMS.Infrastructure.Builders
{
    public class ConciliacionFileBuilder : IConciliationFileBuilder
    {
        private bool _includeDni ;
        private bool _includeName ;
        private bool _includeLastName ;
        private bool _includeUserName ;
        private bool _includeEmail ;
        private bool _includePhoneNumber ;
        //datos de bill
        private bool _includeAmount ;
        private bool _includeBillDate ;
        private bool _includeContractNumber ;

        public IConciliationFileBuilder IncludeDni()
        {
            _includeDni = true;
            return this;
        }
        public IConciliationFileBuilder IncludeName()
        {
            _includeName = true;
            return this;
        }
        public IConciliationFileBuilder IncludeLastName()
        {
            _includeLastName = true;
            return this;
        }
        public IConciliationFileBuilder IncludeUserName()
        {
            _includeUserName = true;
            return this;
        }
        public IConciliationFileBuilder IncludeEmail()
        {
            _includeEmail = true;
            return this;
        }
        public IConciliationFileBuilder IncludePhoneNumber()
        {
            _includePhoneNumber = true;
            return this;
        }
        public IConciliationFileBuilder IncludeAmount()
        {
            _includeAmount = true;
            return this;
        }
        public IConciliationFileBuilder IncludeBillDate()
        {
            _includeBillDate = true;
            return this;
        }
        public IConciliationFileBuilder IncludeContractNumber()
        {
            _includeContractNumber = true;
            return this;
        }
        public byte[] Build(List<BillEntity> bills, Guid providerID, Guid serviceID, ConciliacionFileConfiguration configuration, IUCABPagaloTodoDbContext _dbContext)
        {
            var lines = new List<string>();

            foreach (var bill in bills)
            {
                if (bill.Service.Id != serviceID || bill.Service.Provider.Id != providerID) continue;

                var line = new StringBuilder();

                if (configuration.IncludeDni) line.Append($"{bill.User.Dni},");
                if (configuration.IncludeName) line.Append($"{bill.User.Name},");
                if (configuration.IncludeLastname) line.Append($"{bill.User.Lastname},");
                if (configuration.IncludeUsername) line.Append($"{bill.User.Username},");
                if (configuration.IncludeEmail) line.Append($"{bill.User.Email},");
                if (configuration.IncludePhoneNumber) line.Append($"{bill.User.PhoneNumber},");
                if (configuration.IncludeAmount) line.Append($"{bill.Amount},");
                if (configuration.IncludeBillDate) line.Append($"{bill.Date.ToString("dd/MM/yyyy")},");


                lines.Add(line.ToString());
            }

            return Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lines));
        }

    }
}

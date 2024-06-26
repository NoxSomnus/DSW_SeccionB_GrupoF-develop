﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class SendConciliationfileQuery : IRequest<bool>
    {
        public SendFileConciliationRequest _request;
        public SendConciliationfileQuery(SendFileConciliationRequest request)
        {
            _request = request;
        }
    }
}



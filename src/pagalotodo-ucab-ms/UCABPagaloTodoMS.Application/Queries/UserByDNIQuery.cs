using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class UserByDNIQuery : IRequest<OneUserResponse>
    {
        public string? Dni { get; set; }

        public UserByDNIQuery(string dni)
        {
            Dni = dni;
        }
    }
}

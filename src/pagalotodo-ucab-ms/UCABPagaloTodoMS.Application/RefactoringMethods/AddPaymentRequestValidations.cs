using Microsoft.EntityFrameworkCore;
using RestSharp.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.RefactoringMethods
{
    public class AddPaymentRequestValidations
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;

        public AddPaymentRequestValidations(IUCABPagaloTodoDbContext dbContext) {
            _dbContext = dbContext;
        }

        public bool validate(AddPaymentCommand request) {
            var user = _dbContext.UserEntities.FirstOrDefault(c => c.Id == request._request.UserId);

            try
            {
                if (user == null)
                {
                    throw new UserIdNotFoundException("Error: El usuario no existe");
                }

                if (user is ProviderEntity || user is AdminEntity)
                {
                    throw new UserIsNotCustomerException("Error: Para hacer un pago debe ser un usuario consumidor");
                }
                //busca el servicio para ver si existe en la BD
                var service = _dbContext.ServiceEntities.FirstOrDefault(s => s.Id == request._request.ServiceId);

                if (service == null)
                {
                    throw new ServiceNotFoundException("Error: No existe el servicio");
                }

                var option = _dbContext.PaymentOptionEntities.Where(s => s.Id == request._request.PaymentOptionId && s.ServiceId == request._request.ServiceId)
                    .FirstOrDefault();

                if (option == null)
                {
                    throw new PaymentOptionNotFoundException("Error: No existe la opcion de pago");
                }

                if (option.Status != "Activo")
                {
                    throw new PaymentOptionIsNotActiveException("Error: El metodo de pago no esta activo");
                }

                //Verificaciones para que cuando sea el servicio sea de tipo telefonia, no reciba en el request un contractnumber
                //y si no es un servicio de telefonia entonces que el request no reciba un numero de telefono


                /*if (service.TypeService != "Telefonia")
                {
                    if (request._request.PhoneNumber is null or "")
                    {
                        // Si el servicio no requiere un número de teléfono, establece el valor de PhoneNumber en null o en una cadena vacía
                        request._request.PhoneNumber = ""; // o request._request.PhoneNumber = null;
                    }
                    else
                    {
                        throw new InvalidRequestFormatException("Error: El servicio no requiere un número de teléfono, solo requiere un número de contrato");
                    }
                }

                if (service.TypeService == "Telefonia")
                {
                    if (request._request.ContractNumber == null || request._request.ContractNumber == "")
                    {
                        // Si el servicio de telefonia no tiene un número de contrato, establece el valor de ContractNumber en null o en una cadena vacía
                        request._request.ContractNumber = ""; // o request._request.ContractNumber = null;
                    }
                    else
                    {
                        throw new InvalidRequestFormatException("Error: El servicio de telefonia no tiene un numero de contrato");
                    }
                }*/
            }
            catch (UserNotFoundException ex)
            {
                throw ex;
            }
            catch (UserIsNotCustomerException ex)
            {
                throw;
            }
            catch (ServiceNotFoundException ex)
            {
                throw;
            }
            catch (PaymentOptionNotFoundException ex)
            {
                throw;
            }
            catch (PaymentOptionIsNotActiveException ex)
            {
                throw;
            }
            catch (InvalidRequestFormatException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
    }
}




    
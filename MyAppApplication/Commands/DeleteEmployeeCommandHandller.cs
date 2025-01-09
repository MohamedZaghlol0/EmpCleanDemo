using MediatR;
using MyAppDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppApplication.Commands
{
    public class DeleteEmployeeCommandHandller
    {
        public record DeleteEmpComm(Guid EmployeeId) : IRequest<bool>;

        public class DeleteEmpHandler(IEmpRepo empRepo)
            : IRequestHandler<DeleteEmpComm, bool>
        {
            public async Task<bool> Handle(DeleteEmpComm request, CancellationToken cancellationToken)
            {
                return await empRepo.DeleteEmployeeAsync(request.EmployeeId);

            }
        }
    }

}

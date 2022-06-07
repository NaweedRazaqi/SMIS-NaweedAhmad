using App.Application.Lookup.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Queries
{
    public class GetStudentRelativeType : IRequest<List<StudentRelativeTypeModel>>
    {
        public int? Id { get; set; }

    }

    public class GetStudentRelativeTypeHandler : IRequestHandler<GetStudentRelativeType, List<StudentRelativeTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetStudentRelativeTypeHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<StudentRelativeTypeModel>> Handle(GetStudentRelativeType request, CancellationToken cancellationToken)
        {
            List<StudentRelativeTypeModel> list = new List<StudentRelativeTypeModel>();
            var query = Context.RetativesTypes.AsQueryable();

            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }

            list = await(from o in query
                         select new StudentRelativeTypeModel
                         {
                             Id = o.Id,
                             Name = o.Name,
                             NameDari = o.NameDari,
                             NamePashto = o.NamePashto
                             

                         }).ToListAsync(cancellationToken);
            return list;
        }
    }
}

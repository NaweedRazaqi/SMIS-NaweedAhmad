using App.Application.Prf.Models;
using App.Application.Student.Prf.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Prf.Queries
{
    public class SearchStudentRelativeQuery : IRequest<List<StudentRelativeModel>>
    {
        public int? Id { get; set; }
        public long? ProfileId { get; set; }

    }

    public class SearchStudentRelativeQueryHandler : IRequestHandler<SearchStudentRelativeQuery, List<StudentRelativeModel>>
    {
        private readonly AppDbContext context;
        public SearchStudentRelativeQueryHandler(AppDbContext mContext)
        {
            context = mContext;
        }

        public async Task<List<StudentRelativeModel>> Handle(SearchStudentRelativeQuery request, CancellationToken cancellationToken)
        {


            var query = context.Relatives.AsQueryable();

            if (request.Id.HasValue)
            {
                query = query.Where(r => r.Id == request.Id);
            }
            if (!request.Id.HasValue && !request.ProfileId.HasValue)
            {
                return new List<StudentRelativeModel>();
            }
         
            if (request.ProfileId.HasValue)
            {
                query = query.Where(p => p.ProfileId == request.ProfileId);
            }


            return await query.Include(p => p.Profile)
                              .Include(p => p.RelativeType)
                .Select(e => new StudentRelativeModel
            {
                Id = e.Id,
                //Name = e.Name,
                RelativeTypeId = e.RelativeTypeId,
                RelativeTypeName = e.RelativeType.NameDari
            }).ToListAsync();
        }
    }
}

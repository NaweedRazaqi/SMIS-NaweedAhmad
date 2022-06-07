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


    public class SearchStudentParentsDetails : IRequest<List<StudentParentDetailsModel>>
    {
        public int? Id { get; set; }
        public decimal? ProfileId { get; set; }
        public int? JobLocationId { get; set; }
    }

    public class StudentParentJobQueryHandler : IRequestHandler<SearchStudentParentsDetails, List<StudentParentDetailsModel>>
    {

        private readonly AppDbContext context;
        public StudentParentJobQueryHandler(AppDbContext mContext)
        {
            context = mContext;
        }

        public async Task<List<StudentParentDetailsModel>> Handle(SearchStudentParentsDetails request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue && !request.ProfileId.HasValue)
            {
                return new List<StudentParentDetailsModel>();
            }
            var query = context.Relatives.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(r => r.Id == request.Id);
            }
            if (request.ProfileId.HasValue)
            {
                query = query.Where(p => p.ProfileId == request.ProfileId);
            }
            return await query.Include(p => p.Profile)
           .Select(e => new StudentParentDetailsModel
           {
               Id = e.Id,
               ProfileId = e.ProfileId,
               RelativeTypeId = e.RelativeTypeId,
               RelativeTypeText = e.RelativeType.NameDari,
               JobLocationId = e.JobLocationId,
               JobLocationName = e.JobLocation.Dari,
               GurrenterName = e.GurrenterName,
               GurrenterFatherName = e.GurrenterFatherName,
               ProfessionTypeId = e.ProfessionTypeId,
               ProfesionName = e.ProfessionType.Dari,
               RelativeName = e.RelativeName,
               Phone = e.Phone
               
                
           }).ToListAsync();

        }
    }
}

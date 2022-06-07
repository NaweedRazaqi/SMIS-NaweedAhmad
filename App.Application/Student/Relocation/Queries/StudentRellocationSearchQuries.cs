
using App.Application.Student.Relocation.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Relocation.Queries
{


    public class StudentRellocationSearchQuries : IRequest<List<SearchRellocationModel>>
    {
        public long? Id { get; set; }
        public long? ProfileId { get; set; }

    }

    public class SearchStudentRelativeQueryHandler : IRequestHandler<StudentRellocationSearchQuries, List<SearchRellocationModel>>
    {
        private readonly AppDbContext context;
        public SearchStudentRelativeQueryHandler(AppDbContext mContext)
        {
            context = mContext;
        }

        public async Task<List<SearchRellocationModel>> Handle(StudentRellocationSearchQuries request, CancellationToken cancellationToken)
        {
            var query = context.Rellocations.AsQueryable();

            if (request.Id.HasValue)
            {
                query = query.Where(r => r.Id == request.Id);
            }
            if (!request.Id.HasValue && !request.ProfileId.HasValue)
            {
                return new List<SearchRellocationModel>();
            }

            if (request.ProfileId.HasValue)
            {
                query = query.Where(p => p.ProfileId == request.ProfileId);
            }
            return await query.Include(p => p.Profile)

               .Select(e => new SearchRellocationModel
               {
                   Id = e.Id,
                   NewAssasNumber = e.NewAssasNumber,
                   OldAssasNumber = e.OldAssasNumber,
                   NewSchoolId = e.NewSchoolId,
                   OldSchoolId = e.OldSchoolId,
                   SchoolLocationId = e.SchoolLocationId,
                   NewSchoolNameText = e.NewSchool.Dari,
                   OldSchoolNameText = e.OldSchool.Dari,
                   SchoolLocationNameText = e.SchoolLocation.Dari,
                   ProfileName = e.Profile.FirstName,
                   District = e.District,
                   DistrictName = e.DistrictNavigation.Dari,
                  SchoolTypeId = e.SchoolTypeId
               }).ToListAsync();
        }
    }
}
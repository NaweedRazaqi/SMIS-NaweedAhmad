
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
    public class SearchStudentClassQuery : IRequest<List<SearchStudentClassModel>>
    {
        public int? ID { get; set; }
        public decimal? ProfileID { get; set; }
    }


    public class SearchStudentClassQueryHandler:IRequestHandler<SearchStudentClassQuery , List<SearchStudentClassModel>>
    {
        private readonly AppDbContext context;
        public SearchStudentClassQueryHandler(AppDbContext mContext)
        {
            context = mContext;
        }
        public async Task<List<SearchStudentClassModel>> Handle(SearchStudentClassQuery request, CancellationToken cancellationToken)
        {
            if (!request.ID.HasValue && !request.ProfileID.HasValue)
            {
                return new List<SearchStudentClassModel>();
            }
            var query = context.StudentClasses.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.ProfileID.HasValue)
            {
                query = query.Where(e => e.ProfileId == request.ProfileID);
            }

            return await query.Include(e => e.Profile)
                .Include(e => e.ClassType)
                .Include(e=>e.ClassManagement)
                .Select(e => new SearchStudentClassModel
                {
                    Id=e.Id,
                    ProfileId=e.ProfileId,
                    ClassTypeId=e.ClassTypeId,
                    ClassTypeName=e.ClassType.DariName,
                    ClassManagementId=e.ClassManagementId,
                    ClassManagementName=e.ClassManagement.Name,
                    
                    
                }).ToListAsync();
        }
    }
}

using App.Application.Student.Examination.Result.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Examination.Result.Queries
{

    public class SearchStudentResultQuery : IRequest<List<SearchStudentNewResultModel>>
    {
        public long? Id { get; set; }
        public short? ClassTypeId { get; set; }
        public short? ClassManagementId { get; set; }
        public int? ResultId { get; set; }

    }

    public class SearchStudentResultQueryHandler : IRequestHandler<SearchStudentResultQuery, List<SearchStudentNewResultModel>>
    {
        private readonly AppDbContext context;
        public SearchStudentResultQueryHandler(AppDbContext mContext)
        {
            context = mContext;
        }

        public async Task<List<SearchStudentNewResultModel>> Handle(SearchStudentResultQuery request, CancellationToken cancellationToken)
        {
            var query = context.StudentResults.AsQueryable();

            if (request.Id.HasValue)
            {
                query = query.Where(R => R.Id == request.Id);
            }


            if (request.ClassTypeId.HasValue)
            {
                query = query.Where(R => R.ClassTypeId == request.ClassTypeId);
            }
            if (request.ClassManagementId.HasValue)
            {
                query = query.Where(R => R.ClassManagementId == request.ClassManagementId);
            }
            if (request.ResultId.HasValue)
            {
                query = query.Where(R => R.ResultId == request.ResultId);
            }

            return await query.Select(R => new SearchStudentNewResultModel
            {
                Id = R.Id,
                ProfileId = R.ProfileId,
                ClassTypeId = R.ClassTypeId,
                ClassManagementId = R.ClassManagementId,
                ResultId = R.ResultId,
                Total = R.Total,
                ClassManagementText = R.ClassManagement.Name,
                ClassTypeText = R.ClassType.DariName,
                ResultText = R.Result.DariName,
                IsActive = R.IsActive,
                StudentName = R.Profile.FirstName + " "  + R.Profile.LastName,
                FatherName = R.Profile.FatherName


            }).Where(s => s.IsActive == null).ToListAsync();
        }
    }
}
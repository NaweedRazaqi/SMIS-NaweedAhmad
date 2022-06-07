
using App.Application.Student.Registration.Model;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Registration.Queries
{

    public class SearchStudentRegistrationQueries : IRequest<List<StudentRegisterationModel>>
    {
        public int? Id { get; set; }
        public long? ProfileId { get; set; }
        public short? SchoolCategoryId { get; set; }
        public short? SchoolId { get; set; }
        public int? StudentAssassNumber { get; set; }
    }



    public class SearchStudentRegistrationQueriesHandler : IRequestHandler<SearchStudentRegistrationQueries, List<StudentRegisterationModel>>
    {

        private readonly AppDbContext context;
        public SearchStudentRegistrationQueriesHandler(AppDbContext mContext)
        {
            context = mContext;
        }

        public async Task<List<StudentRegisterationModel>> Handle(SearchStudentRegistrationQueries request, CancellationToken cancellationToken)
        {
           
            
            if (!request.Id.HasValue && !request.ProfileId.HasValue)
            {
                return new List<StudentRegisterationModel>();
            }
            var query = context.StudentRegisterations.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(r => r.Id == request.Id);
            }
            if (request.ProfileId.HasValue)
            {
                query = query.Where(p => p.ProfileId == request.ProfileId);
            }
            if (request.SchoolCategoryId.HasValue)
            {
                query = query.Where(p => p.SchoolCategoryId == request.SchoolCategoryId);
            }
            if (request.SchoolId.HasValue)
            {
                query = query.Where(p => p.SchoolId == request.SchoolId);
            }
            if (request.StudentAssassNumber.HasValue)
            {
                query = query.Where(p => p.StudentAssassNumber == request.StudentAssassNumber);
            }
            return await query
           .Select(e => new StudentRegisterationModel
           {
               Id = e.Id,
               ProfileId = e.ProfileId,
               ClassTypeId = e.ClassTypeId,
               SchoolId = e.SchoolId,
               SchoolTypeId = e.SchoolTypeId,
               StudentAssassNumber = e.StudentAssassNumber,
               ClassManagementId = e.ClassManagementId,
               ClassTypeText = e.ClassType.DariName,
               SchoolName = e.School.Dari,
               ClassmanagementName = e.ClassManagement.Name,
               SchoolTypeName = e.SchoolType.NameDari,
               ProvinceId = e.ProvinceId,
               PdistrictId = e.PdistrictsId,
               SchoolCategoryId = e.SchoolCategoryId,
               ProvinceText = e.Province.Title,
               DistrictText = e.Pdistricts.Name,
               SchoolCattext = e.SchoolCategory.Name
           }).ToListAsync();

        }

    }
}
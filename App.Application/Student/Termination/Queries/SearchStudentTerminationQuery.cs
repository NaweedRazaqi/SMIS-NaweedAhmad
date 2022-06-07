using App.Application.Prf.Models;
using App.Application.Student.Prf.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
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


    public class SearchStudentTerminationQuery : IRequest<List<StudentsTerminationModel>>
    {
        public int? Id { get; set; }
        public long? ProfileId { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? ClassTypeId { get; set; }
    }

    public class SearchStudentTerminationQueryHandler : IRequestHandler<SearchStudentTerminationQuery, List<StudentsTerminationModel>>
    {

        private readonly AppDbContext context;
        public SearchStudentTerminationQueryHandler(AppDbContext mContext)
        {
            context = mContext;
        }

        public async Task<List<StudentsTerminationModel>> Handle(SearchStudentTerminationQuery request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue && !request.ProfileId.HasValue)
            {
                return new List<StudentsTerminationModel>();
            }
            var query = context.Terminations.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(r => r.Id == request.Id);
            }
            if (request.ProfileId.HasValue)
            {
                query = query.Where(p => p.ProfileId == request.ProfileId);
            }
            if (request.ClassTypeId.HasValue)
            {
                query = query.Where(p => p.ClassTypeId == request.ClassTypeId);
            }
            return await query.Include(p => p.Profile)
               .Select(e => new StudentsTerminationModel
               {
                   Id = e.Id,
                   Reasons = e.Reasons,
                   Fine = e.Fine,
                   TerminationDate = e.TerminationDate,
                   DocumentNo = e.DocumentNo,
                   DocumentTypeId = e.DocumentTypeId,
                   TerminationDateShamsi= PersianDate.GetFormatedString(e.TerminationDate),
                   ClassTypeId = e.ClassTypeId,
                   ClassTypeText = e.ClassType.DariName

               }).ToListAsync();

        }
    }
}

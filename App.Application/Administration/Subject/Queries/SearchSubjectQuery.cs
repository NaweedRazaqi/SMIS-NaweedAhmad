using App.Application.Subject.Models;
using App.Persistence.Context;
using Clean.Common.Service;
using Clean.Persistence.Identity;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Subject.Queries
{
    public class SearchSubjectQuery : IRequest<IEnumerable<SearchSubjectModel>>
    {
            public long? Id { get; set; }
            public string Name { get; set; }
            public string NameEng { get; set; }
            public string Remarks { get; set; }
            public int StatusId { get; set; }
            public short? SchoolTypeId { get; set; }
        public class SearchSubjectQueryHandler : IRequestHandler<SearchSubjectQuery, IEnumerable<SearchSubjectModel>>
        {
            private readonly AppDbContext context;
            private ICurrentUser User;

            private AppIdentityDbContext IDContext;

            public SearchSubjectQueryHandler(AppDbContext context, ICurrentUser currentUser, AppIdentityDbContext idContext)
            {
                this.context = context;
                User = currentUser;
                IDContext = idContext;
            }
            public async Task<IEnumerable<SearchSubjectModel>> Handle(SearchSubjectQuery request, CancellationToken cancellationToken)
            {
                var query = context.SubjectManagements.AsQueryable();
                if (request.Id.HasValue)
                {
                    query = query.Where(e => e.Id == request.Id.Value);
                }
                if (!String.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(e => e.Name == request.Name);
                }
                if (request.SchoolTypeId.HasValue)
                {
                    query = query.Where(e => e.SchoolTypeId == request.SchoolTypeId);
                }

                if (!String.IsNullOrEmpty(request.NameEng))
                {
                    query = query.Where(e => EF.Functions.Like(e.NameEng, String.Concat("%", request.NameEng, "%")));
                }

                if (!String.IsNullOrEmpty(request.Remarks))
                {
                    query = query.Where(e => EF.Functions.Like(e.Remarks, String.Concat("%", request.Remarks, "%")));
                }

                return await query.Select(p => new SearchSubjectModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    NameEng = p.NameEng,
                    Remarks = p.Remarks,
                    StatusId = p.StatusId,
                    ViewOrder = p.ViewOrder,
                    StatusName = p.Status.Dari,
                    SchoolTypeId = p.SchoolTypeId,
                    SchoolTypeName = p.SchoolType.NameDari
                }).ToListAsync();
            }
        }
    }
}

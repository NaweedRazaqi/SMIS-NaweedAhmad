using App.Application.Subject.Models;
using App.Application.Teacher.Models;
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

namespace App.Application.Teacher.Queries
{
    public class SearchTeacherQuery : IRequest<IEnumerable<SearchTeacherModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }

        public class SearchAcasQueryHandler : IRequestHandler<SearchTeacherQuery, IEnumerable<SearchTeacherModel>>
        {
            private readonly AppDbContext context;
            private ICurrentUser User;

            private AppIdentityDbContext IDContext;

            public SearchAcasQueryHandler(AppDbContext context, ICurrentUser currentUser, AppIdentityDbContext idContext)
            {
                this.context = context;
                User = currentUser;
                IDContext = idContext;
            }
            public async Task<IEnumerable<SearchTeacherModel>> Handle(SearchTeacherQuery request, CancellationToken cancellationToken)
            {
                var query = context.Teachers.AsQueryable();
                if (request.Id != 0)
                {
                    query = query.Where(e => e.Id == request.Id);
                }
                if (!String.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(e => e.Name == request.Name);
                }

                if (!String.IsNullOrEmpty(request.FatherName))
                {
                    query = query.Where(e => EF.Functions.Like(e.FatherName, String.Concat("%", request.FatherName, "%")));
                }

                if (!String.IsNullOrEmpty(request.FatherName))
                {
                    query = query.Where(e => EF.Functions.Like(e.FatherName, String.Concat("%", request.FatherName, "%")));
                }

                return await query.Select(p => new SearchTeacherModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    FullName = p.FatherName,
                    FatherName = p.FatherName,
                    Phone = p.Phone,
                    Email = p.Email,
                   // GenderId = p.GenderId,
                    GenderName = p.Gender.Name
                }).ToListAsync();
            }
        }
    }
}

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
    public class GetTeacherList : IRequest<List<TeacherModel>>
    {
        public int? ID { get; set; }
    }
    public class GetTeacherListHandler : IRequestHandler<GetTeacherList, List<TeacherModel>>
    {
        private AppDbContext Context { get; set; }
        public GetTeacherListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<TeacherModel>> Handle(GetTeacherList request, CancellationToken cancellationToken)
        {
            var query = Context.Teachers.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new TeacherModel
            {
                Id = e.Id,
                Name = e.Name,
                FatherName = e.FatherName
            }).ToListAsync();
        }
    }
}

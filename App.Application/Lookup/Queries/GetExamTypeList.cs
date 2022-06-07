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
    public class GetExamTypeList : IRequest<List<ExamTypeModel>>
    {
        public int? Id { get; set; }
    }

    public class GetExamTypeListHandler : IRequestHandler<GetExamTypeList, List<ExamTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetExamTypeListHandler(AppDbContext context)
        {
            Context = context;
        }



        public async Task<List<ExamTypeModel>> Handle(GetExamTypeList request, CancellationToken cancellationToken)
        {


            var query = Context.ExamTypes.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id);
            }
            return await query.Select(e => new ExamTypeModel
            {
                Id = e.Id,
                Dari = e.Dari,
                Pashto = e.Pashto
            }).ToListAsync();
        }

    
    }
}

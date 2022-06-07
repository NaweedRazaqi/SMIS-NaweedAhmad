
using App.Application.Student.Prf.Models;
using App.Persistence.Context;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Examination.Queries
{
    public class SearchStudentListQuery : IRequest<IEnumerable<SearchProfileModel>>
    {

        public decimal? ID { get; set; }
    }

    public class SearchStudentListQueryHandler : IRequestHandler<SearchStudentListQuery, IEnumerable<SearchProfileModel>>
    {
        private readonly AppDbContext context;

        private AppIdentityDbContext IDContext;

        public SearchStudentListQueryHandler(AppDbContext context, AppIdentityDbContext idContext)
        {
            this.context = context;
          
            IDContext = idContext;
        }

        public async Task<IEnumerable<SearchProfileModel>> Handle(SearchStudentListQuery request, CancellationToken cancellationToken)
        {
            var query = context.Profiles.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID.Value);
            }

            return (await query.Select(p => new SearchProfileModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                //SchoolId = p.SchoolId,


            })

               .OrderBy(x => x.Id).ToListAsync()).ToList();

        }
    }

}

using App.Application.Student.Examination.StudentUpgrade.Models;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Examination.StudentUpgrade.Queries
{
    public class SearchStudentUpgradeQuery : IRequest<IEnumerable<StudentUpgradeSearch>>
    {
        public int? Id { get; set; }
        public int? ProfileId { get; set; }
    }

    public class SearchStudentUpgradeQueryHandler : IRequestHandler<SearchStudentUpgradeQuery, IEnumerable<StudentUpgradeSearch>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;


        public SearchStudentUpgradeQueryHandler(AppDbContext context, ICurrentUser currentUser)
        {
            this.context = context;
            User = currentUser;

        }

        public async Task<IEnumerable<StudentUpgradeSearch>> Handle(SearchStudentUpgradeQuery request, CancellationToken cancellationToken)
        {
            var query = context.ClassUpgradations.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id.Value);
            }

            return (await query.OrderBy(x => x.Id).Select(SC => new StudentUpgradeSearch
            {
                Id = SC.Id,
                ProfileId = SC.ProfileId,
                ClassTypeId = SC.ClassTypeId,
                ClassManagementId = SC.ClassManagementId


            }).ToListAsync());
        }
    }
}
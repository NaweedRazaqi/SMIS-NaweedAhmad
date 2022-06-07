using App.Application.Teacher.Models;
using App.Application.Teacher.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Teacher.Commands
{
    public class CreateTeacherCommand : IRequest<List<SearchTeacherModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
    }
    public class CreateSubjectCommandHandler : IRequestHandler<CreateTeacherCommand, List<SearchTeacherModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;
        public CreateSubjectCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }
        public async Task<List<SearchTeacherModel>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            int CurrentUserId = await currentUser.GetUserId();
            var Teacher = request.Id != 0 ? context.Teachers.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Teacher();
            IEnumerable<SearchTeacherModel> result = new List<SearchTeacherModel>();
            Teacher.Name = request.Name.Trim();
            Teacher.FatherName = request.FatherName;
            Teacher.Phone = request.Phone;
            Teacher.Email = request.Email;
            Teacher.GenderId = request.GenderId;

            if (request.Id == 0)
            {
                Teacher.ModifiedBy = "";
                Teacher.ModifiedOn = DateTime.Now;
                Teacher.CreatedBy = CurrentUserId;
                Teacher.CreatedOn = DateTime.Now;
                context.Teachers.Add(Teacher);
            }
            else
            {
                Teacher.ModifiedBy += "," + CurrentUserId; ;
                Teacher.ModifiedOn = DateTime.Now;
            }
            await context.SaveChangesAsync();
            result = await mediator.Send(new SearchTeacherQuery() { Id = Teacher.Id });
            return result.ToList();

        }
    }
}

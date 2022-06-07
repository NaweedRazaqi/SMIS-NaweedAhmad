
using App.Application.Student.Prf.Models;
using App.Application.Student.Prf.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Prf.Commands
{
    class StudentTermination
    {
    }
    public class StudentTerminationCommand : IRequest<List<StudentsTerminationModel>>
    {

        public int Id { get; set; }
        public string Reasons { get; set; }
        public int? Fine { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DocumentNo { get; set; }
        public int ProfileId { get; set; }
        public short ClassTypeId { get; set; }


    }

    public class StudentTerminationCommandHandler : IRequestHandler<StudentTerminationCommand, List<StudentsTerminationModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public StudentTerminationCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }

        public async Task<List<StudentsTerminationModel>> Handle(StudentTerminationCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<StudentsTerminationModel> result = new List<StudentsTerminationModel>();
            var StudentTermination = request.Id != 0 ? context.Terminations.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Termination();
            int CurrentUserId = await currentUser.GetUserId();

            StudentTermination.Id = request.Id;
            StudentTermination.ProfileId = request.ProfileId;
            StudentTermination.Reasons = request.Reasons;
            StudentTermination.Fine = request.Fine;
            StudentTermination.TerminationDate = request.TerminationDate;
            StudentTermination.DocumentNo = request.DocumentNo;
            StudentTermination.DocumentTypeId = request.DocumentTypeId;
            StudentTermination.ClassTypeId = request.ClassTypeId;

            if (request.Id != 0)
            {
                StudentTermination.ModifiedOn = DateTime.Now; ;
                StudentTermination.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                StudentTermination.CreatedBy = CurrentUserId;
                StudentTermination.CreatedOn = DateTime.Now;
                context.Terminations.Add(StudentTermination);
            }

            await context.SaveChangesAsync();

            result = await mediator.Send(new SearchStudentTerminationQuery() { Id = StudentTermination.Id });

            return result.ToList();
        }
    }
}


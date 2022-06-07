
using App.Application.Student.Prf.Models;
using App.Application.Student.Prf.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Prf.Commands
{
    public class StudentHealthReportCommand : IRequest<List<StudentHealthReportModel>>
    {

        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }


    public class StudentHealthReportCommandHandler : IRequestHandler<StudentHealthReportCommand, List<StudentHealthReportModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public StudentHealthReportCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }

        public async Task<List<StudentHealthReportModel>> Handle(StudentHealthReportCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<StudentHealthReportModel> result = new List<StudentHealthReportModel>();
            var StudentHealthReport = request.Id != 0 ? context.StudentHealthReports.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.StudentHealthReport();
            int CurrentUserId = await currentUser.GetUserId();

            StudentHealthReport.Id = request.Id;
            StudentHealthReport.ProfileId = request.ProfileId;
            StudentHealthReport.Description = request.Description;
            StudentHealthReport.AttachmentPath = request.AttachmentPath;
            if (request.Id != 0)
            {
                StudentHealthReport.ModifiedOn = DateTime.Now; ;
                StudentHealthReport.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                StudentHealthReport.CreatedBy = CurrentUserId;
                StudentHealthReport.CreatedOn = DateTime.Now;
                context.StudentHealthReports.Add(StudentHealthReport);
            }
            await context.SaveChangesAsync();

            result = await mediator.Send(new SearchStudentHealthReportQuery() { Id = StudentHealthReport.Id });

            return result.ToList();
        }
    }
}

using App.Application.Student.ScheduleExam.Models;
using App.Application.Student.ScheduleExam.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Examination.ScheduleExam.Commands
{
    public class CreateExamScheduleCommand : IRequest<List<SearchScheduleExamModel>>
    {

        public int Id { get; set; }
        public string ExmName { get; set; }
        public short SubjectId { get; set; }
        public short SchoolTypeId { get; set; }
        public int StudentClassId { get; set; }
        public short ClassTypeId { get; set; }
        public short? ClassManagementId { get; set; }
        public TimeSpan? ExamTimeStart { get; set; }
        public TimeSpan? ExamTimeEnd { get; set; }
        public DateTime? ExamDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }

    public class CreateExamScheduleCommandHandler : IRequestHandler<CreateExamScheduleCommand, List<SearchScheduleExamModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public CreateExamScheduleCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }

        public async Task<List<SearchScheduleExamModel>> Handle(CreateExamScheduleCommand request, CancellationToken cancellationToken)
        {

            IEnumerable<SearchScheduleExamModel> result = new List<SearchScheduleExamModel>();
            var ScheduleExam = request.Id != 0 ? context.ScheduleExams.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.ScheduleExam();
            int CurrentUserId = await currentUser.GetUserId();

            ScheduleExam.Id = request.Id;
            ScheduleExam.ExmName = request.ExmName;
            ScheduleExam.ClassTypeId = request.ClassTypeId;
            ScheduleExam.SubjectId = request.SubjectId;
            ScheduleExam.ClassManagementId = request.ClassManagementId;
            ScheduleExam.StudentClassId = request.StudentClassId;
            ScheduleExam.ExamTimeStart = request.ExamTimeStart;
            ScheduleExam.ExamTimeEnd = request.ExamTimeEnd;
            ScheduleExam.ExamDate = request.ExamDate;
            ScheduleExam.SchoolTypeId = request.SchoolTypeId;

            if (request.Id != 0)
            {
                ScheduleExam.ModifiedOn = DateTime.Now; ;
                ScheduleExam.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                ScheduleExam.CreatedBy = CurrentUserId;
                ScheduleExam.CreatedOn = DateTime.Now;
                context.ScheduleExams.Add(ScheduleExam);
            }

            await context.SaveChangesAsync();

            result = await mediator.Send(new SearchExamScheduleQueries() { Id = ScheduleExam.Id });

            return result.ToList();
        }
    }
}

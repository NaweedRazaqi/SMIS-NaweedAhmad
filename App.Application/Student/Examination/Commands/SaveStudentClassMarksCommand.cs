
using App.Application.Student.Examination.Models;
using App.Domain.Entity.prf;
using App.Persistence.Context;
using Clean.Common.Exceptions;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Examination.Commands
{
    public class SaveStudentClassMarksCommand : IRequest<List<SchoolStudentClassMarksModel>>
    {

        public ScoreModel[] ScoreModel { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ScoreModel
    {
        public long StudentClassId { get; set; }
        public List<short?> StudentSubjectId { get; set; }
        public List<int> Marks { get; set; }
        public List<long> MarkId { get; set; }
        public int ExampTypeId { get; set; }
    }


    public class SaveStudentClassMarksCommandHandler : IRequestHandler<SaveStudentClassMarksCommand, List<SchoolStudentClassMarksModel>>
    {

        private readonly AppDbContext context;
        private readonly ICurrentUser currentUser;
        private readonly IMediator mediator;

        public SaveStudentClassMarksCommandHandler(AppDbContext context, ICurrentUser user , IMediator mediator)
        {
            this.context = context;
            this.currentUser = user;
            this.mediator = mediator;
        }

        public async Task<List<SchoolStudentClassMarksModel>> Handle(SaveStudentClassMarksCommand request, CancellationToken cancellationToken)
        {

            
            
            int CurrentUserId = await currentUser.GetUserId();

            for (var Counter = 0; Counter < request.ScoreModel.Count(); Counter++)
            {
             
                    for (var InnerCounter = 0; InnerCounter < request.ScoreModel[Counter].Marks.Count(); InnerCounter++)
                    {
                        var HighSchoolStudentClassMarks = context.StudentClassMarks.Where(sc => sc.Id == request.ScoreModel[Counter].MarkId[InnerCounter]).Count();
                        var studentMarks = request.ScoreModel[Counter].MarkId[InnerCounter] != 0 ? context.StudentClassMarks.Where(e => e.StudentClassId == request.ScoreModel[Counter].StudentClassId && e.Id == request.ScoreModel[Counter].MarkId[InnerCounter]).Single() : new Domain.Entity.prf.StudentClassMarks();
                        if (request.ScoreModel[Counter].Marks[InnerCounter] != 0)
                        { // does not allow 0's to be saved in table
                          //if (request.ScoreModel[Counter].Marks[InnerCounter] !> 40 && request.ScoreModel[Counter].ExampTypeId != 2  ) {
                            if (HighSchoolStudentClassMarks <= 1)
                            {
                                studentMarks = new StudentClassMarks
                                {
                                    Marks = Convert.ToInt32(request.ScoreModel[Counter].Marks[InnerCounter]),
                                    SubjectId = (short)(request.ScoreModel[Counter].StudentSubjectId[InnerCounter]),
                                    CreatedBy = CurrentUserId,
                                    StudentClassId = Convert.ToInt32(request.ScoreModel[Counter].StudentClassId),
                                    ExamTypeId = Convert.ToInt32(request.ScoreModel[Counter].ExampTypeId),
                                    CreatedOn = DateTime.Now
                                };
                                await context.StudentClassMarks.AddAsync(studentMarks);
                                await context.SaveChangesAsync();
                            }
                            else
                            {
                                var subjectMarks = context.StudentClassMarks.Where(e => e.Id == request.ScoreModel[Counter].MarkId[InnerCounter] && e.SubjectId == request.ScoreModel[Counter].StudentSubjectId[InnerCounter]).Single();
                                subjectMarks.Marks = Convert.ToInt32(request.ScoreModel[Counter].Marks[InnerCounter]);
                                await context.SaveChangesAsync();
                            }


                        }

                    
                }
            }

            return null;
        }
    }
}

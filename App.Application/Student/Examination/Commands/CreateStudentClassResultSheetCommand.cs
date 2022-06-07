using App.Application.Student.Examination.Models;
using App.Application.Student.Examination.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Examination.Commands
{
    public class CreateStudentClassResultSheetCommand : IRequest<List<StudentClassResultSheetModel>>
    {
        public int? Id { get; set; }
        public short StudentSchoolId { get; set; }
        public short ClassTypeId { get; set; }
        public short ClassManagementId { get; set; }
        public int? DocumentTypeId { get; set; }
        public short SchoolTypeId { get; set; }


    }

    public class CreateStudentClassResultSheetCommandHandler : IRequestHandler<CreateStudentClassResultSheetCommand, List<StudentClassResultSheetModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;
        public CreateStudentClassResultSheetCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }
        public async Task<List<StudentClassResultSheetModel>> Handle(CreateStudentClassResultSheetCommand request, CancellationToken cancellationToken)
        {

            int CurrentUserId = await currentUser.GetUserId();
            var ClassResult = request.Id.HasValue ? context.StudentClassResultSheets.Where(R=> R.Id == request.Id).Single() : new Domain.Entity.prf.StudentClassResultSheet();
            var ownerid = context.UserOwners.Where(o => o.UserId == CurrentUserId).Select(w => w.OwnerId).SingleOrDefault();
        
            IEnumerable<StudentClassResultSheetModel> result = new List<StudentClassResultSheetModel>();
           // ClassResult.Id = request.Id;
            ClassResult.StudentSchoolId = request.StudentSchoolId;
            ClassResult.ClassTypeId = request.ClassTypeId;
            ClassResult.ClassManagementId = request.ClassManagementId;
            ClassResult.DocumentTypeId = 5;
            ClassResult.SchoolTypeId = request.SchoolTypeId;
            ClassResult.OwnerId = ownerid;
         

            if (request.Id.HasValue)
            {
                ClassResult.ModifiedOn = DateTime.Now; ;
                ClassResult.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                ClassResult.CreatedBy = CurrentUserId;
                ClassResult.CreatedOn = DateTime.Now;
                context.StudentClassResultSheets.Add(ClassResult);
            }
            await context.SaveChangesAsync();

            result = await mediator.Send(new SearchStudentClassResultSheetQuery() { Id = ClassResult.Id });
            return result.ToList();
        }
    }
}

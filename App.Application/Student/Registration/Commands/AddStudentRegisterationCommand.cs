
using App.Application.Student.Registration.Model;
using App.Application.Student.Registration.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Registration.Commands
{
    public class AddStudentRegisterationCommand : IRequest<List<StudentRegisterationModel>>
    {

        public int Id { get; set; }
        public int ProfileId { get; set; }
        public short SchoolTypeId { get; set; }
        public short? SchoolCategoryId { get; set; }
        public short SchoolId { get; set; }
        public short ClassTypeId { get; set; }
        public short ClassManagementId { get; set; }
        public int? StudentAssassNumber { get; set; }
        public short? ProvinceId { get; set; }
        public short? PdistrictId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
    }

    public class AddStudentRegisterationCommandHandler : IRequestHandler<AddStudentRegisterationCommand, List<StudentRegisterationModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public AddStudentRegisterationCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }

        public async Task<List<StudentRegisterationModel>> Handle(AddStudentRegisterationCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<StudentRegisterationModel> result = new List<StudentRegisterationModel>();
            var RegisterStudent = request.Id != 0 ? context.StudentRegisterations.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.StudentRegisteration();
            int CurrentUserId = await currentUser.GetUserId();

            RegisterStudent.ProfileId = request.ProfileId;
            RegisterStudent.SchoolTypeId = request.SchoolTypeId;
            RegisterStudent.SchoolId = request.SchoolId;
            RegisterStudent.ClassTypeId = request.ClassTypeId;
            RegisterStudent.ClassManagementId = request.ClassManagementId;
            RegisterStudent.StudentAssassNumber = request.StudentAssassNumber;
            RegisterStudent.SchoolCategoryId = request.SchoolCategoryId;
            RegisterStudent.ProvinceId = request.ProvinceId;
            RegisterStudent.PdistrictsId = request.PdistrictId;




            if (request.Id != 0)
            {
                RegisterStudent.ModifiedOn = DateTime.Now; ;
                RegisterStudent.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                RegisterStudent.CreatedBy = (short)CurrentUserId;
                RegisterStudent.CreatedOn = DateTime.Now;
                context.StudentRegisterations.Add(RegisterStudent);


                var studentClass = new Domain.Entity.prf.StudentClass
                {
                    ProfileId = request.ProfileId,
                    ClassManagementId = request.ClassManagementId,
                    ClassTypeId = request.ClassTypeId,
                    IsActive = false,
                    SchoolId = request.SchoolId,
                    CreatedBy = await currentUser.GetUserId(),
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "",
                    ModifiedOn = DateTime.Now
                };

                context.StudentClasses.Add(studentClass);
            }

            await context.SaveChangesAsync();


            result = await mediator.Send(new SearchStudentRegistrationQueries() { Id = RegisterStudent.Id });

            return result.ToList();
        }
    }
}

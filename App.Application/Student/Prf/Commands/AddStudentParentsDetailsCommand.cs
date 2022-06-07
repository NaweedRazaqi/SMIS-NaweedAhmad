using App.Application.Student.Prf.Models;
using App.Application.Student.Prf.Queries;
using App.Persistence.Context;
using Clean.Common.Exceptions;
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


    public class AddStudentParentsDetailsCommand : IRequest<List<StudentParentDetailsModel>>
    {

        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int RelativeTypeID { get; set; }
        public string PositionName { get; set; }
        public int? JobLocationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string GurrenterName { get; set; }
        public string GurrenterFatherName { get; set; }
        public int? ProfessionTypeId { get; set; }
        public string RelativeName { get; set; }
        public string Phone { get; set; }
    }
    public class AddStudentParentJobCommandHandler : IRequestHandler<AddStudentParentsDetailsCommand, List<StudentParentDetailsModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public AddStudentParentJobCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }

        public async Task<List<StudentParentDetailsModel>> Handle(AddStudentParentsDetailsCommand request, CancellationToken cancellationToken)
        {

            bool mo = IsPhoneNumber(request.Phone.ToString());

            if (!mo)
            {
                throw new BusinessRulesException("شماره  درست نیست! شماره با 07 شروع.باید 10 عدد باشد");
            }
            IEnumerable<StudentParentDetailsModel> result = new List<StudentParentDetailsModel>();
            var StudentRelatives = request.Id != 0 ? context.Relatives.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Relatives();
            int CurrentUserId = await currentUser.GetUserId();

            StudentRelatives.Id = request.Id;
            StudentRelatives.ProfileId = request.ProfileId;
            StudentRelatives.RelativeTypeId = request.RelativeTypeID;
            StudentRelatives.JobLocationId = request.JobLocationId;
            StudentRelatives.GurrenterName = request.GurrenterName;
            StudentRelatives.GurrenterFatherName = request.GurrenterFatherName;
            StudentRelatives.ProfessionTypeId = request.ProfessionTypeId;
            StudentRelatives.RelativeName = request.RelativeName;
            StudentRelatives.Phone = request.Phone;
            


            if (request.Id != 0)
            {
                StudentRelatives.ModifiedOn = DateTime.Now; ;
                StudentRelatives.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                StudentRelatives.CreatedBy = CurrentUserId;
                StudentRelatives.CreatedOn = DateTime.Now;
                context.Relatives.Add(StudentRelatives);
            }

            await context.SaveChangesAsync();

            result = await mediator.Send(new SearchStudentParentsDetails() { Id = StudentRelatives.Id });

            return result.ToList();
        }

        public static bool IsPhoneNumber(string number)
        {

            return number[0] == '0' && number[1] == '7' && number.Length == 10 && IsDigit(number);

        }
        static bool IsDigit(string Input)
        {
            foreach (char c in Input)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}

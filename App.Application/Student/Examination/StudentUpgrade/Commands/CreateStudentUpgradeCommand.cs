using App.Application.Student.Examination.StudentUpgrade.Models;
using App.Application.Student.Examination.StudentUpgrade.Queries;
using App.Domain.Entity.prf;
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

namespace App.Application.Student.Examination.StudentUpgrade.Commands
{
    public class CreateStudentUpgradeCommand : IRequest<List<StudentUpgradeSearch>>
    {

        public ClassUpgrade[] ClassUpgrade { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ClassUpgrade
    {
        public short? ClassTypeId { get; set; }
        public short? ClassManagementId { get; set; }
        public List<int?> ProfileId { get; set; }
         public int? Id { get; set; }
    }

    public class AddStudentUpgradeCommandHandler : IRequestHandler<CreateStudentUpgradeCommand, List<StudentUpgradeSearch>>
    {

        private readonly AppDbContext context;
        private readonly ICurrentUser currentUser;
        private readonly IMediator mediator;

        public AddStudentUpgradeCommandHandler(AppDbContext context, ICurrentUser user, IMediator mediator)
        {
            this.context = context;
            this.currentUser = user;
            this.mediator = mediator;
        }

        public async Task<List<StudentUpgradeSearch>> Handle(CreateStudentUpgradeCommand request, CancellationToken cancellationToken)
        {
            int CurrentUserId = await currentUser.GetUserId();
          

            IEnumerable<StudentUpgradeSearch> result = new List<StudentUpgradeSearch>();
            for (var Counter = 0; Counter < request.ClassUpgrade.Count(); Counter++)
            {

                for (var InnerCounter = 0; InnerCounter < request.ClassUpgrade[Counter].ProfileId.Count(); InnerCounter++)
                {

                    // Extracting ProfileId
                    var ProfileIDEx = context.StudentResults.Where(R => R.Id == request.ClassUpgrade[Counter].ProfileId[InnerCounter]).Select(R => R.ProfileId).SingleOrDefault();
                    var AddStudentUpgrade = request.ClassUpgrade[Counter].Id.HasValue ? context.ClassUpgradations.Where(C => C.Id == request.ClassUpgrade[Counter].Id).Single() : new Domain.Entity.prf.ClassUpgradation();
                  
                    if (request.ClassUpgrade[Counter].ClassTypeId > 12)
                    {
                        throw new BusinessRulesException("از صنف دوازدهم به بالا اتقاع داده نمی شود!");
                    }
                    else
                    {
                        AddStudentUpgrade.ProfileId = ProfileIDEx;
                        AddStudentUpgrade.ClassTypeId = request.ClassUpgrade[Counter].ClassTypeId;
                        AddStudentUpgrade.ClassManagementId = request.ClassUpgrade[Counter].ClassManagementId;

                        if (AddStudentUpgrade.Id.HasValue)
                        {
                            AddStudentUpgrade.ModifiedOn = Convert.ToInt32(DateTime.Now);
                            AddStudentUpgrade.ModifiedBy = "," + CurrentUserId;
                        }
                        else
                        {
                            AddStudentUpgrade.CreatedOn = DateTime.Now;
                            AddStudentUpgrade.CreatedBy = CurrentUserId;

                        }
                        context.ClassUpgradations.Add(AddStudentUpgrade);
                        await context.SaveChangesAsync();

                        // updating student result table.
                        var studentResutl = context.StudentResults.Where(s => s.ProfileId == ProfileIDEx && s.Id == request.ClassUpgrade[Counter].ProfileId[InnerCounter]).SingleOrDefault();

                        if (studentResutl.IsActive == null)
                        {
                            studentResutl.IsActive = false;
                            context.StudentResults.Update(studentResutl);
                            await context.SaveChangesAsync();
                        }

                        // insertion of upgrated class for student in student class.
                        var studentclass = new Domain.Entity.prf.StudentClass();
                        for (var counter1 = 0; counter1 < request.ClassUpgrade.Count(); Counter++)
                        {
                            for( var inncounter1 = 0; inncounter1 < request.ClassUpgrade[inncounter1].ProfileId.Count(); inncounter1++)
                            {
                                var schoolid = context.StudentClasses.Where(s => s.ProfileId == ProfileIDEx).Select(s => s.SchoolId).FirstOrDefault();
                                studentclass.ProfileId = ProfileIDEx;
                                studentclass.ClassTypeId = request.ClassUpgrade[counter1].ClassTypeId;
                                studentclass.ClassManagementId = (short)request.ClassUpgrade[counter1].ClassManagementId;
                                studentclass.SchoolId = schoolid;
                                studentclass.IsActive = false;
                                studentclass.CreatedOn = DateTime.Now;
                                studentclass.CreatedBy = CurrentUserId;                              
                                context.StudentClasses.Add(studentclass);
                                await context.SaveChangesAsync();
                            }
                        }
                        result = await mediator.Send(new SearchStudentUpgradeQuery() { Id = AddStudentUpgrade.Id });
                    }

                }
                
            }
            

          
            return result.ToList();
        }
    }
}


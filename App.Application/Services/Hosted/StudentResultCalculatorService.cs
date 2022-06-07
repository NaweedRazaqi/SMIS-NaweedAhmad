using App.Application.Student.Examination.Result.Models;
using App.Domain.Entity.prf;
using App.Persistence.Context;
using Clean.Common.Exceptions;
using Clean.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Services.Hosted
{
    public class StudentResultCalculatorService : BackgroundService
    {

        private IServiceProvider Provider { get; }
        private ILogger<StudentResultCalculatorService> Logger { get; }
        private System.Timers.Timer MainTimer { get; }

        public StudentResultCalculatorService(IServiceProvider provider, ILogger<StudentResultCalculatorService> logger)
        {
            MainTimer = new System.Timers.Timer();
            Provider = provider;
            Logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            MainTimer.Interval = TimeSpan.FromSeconds(300).TotalMilliseconds;
            MainTimer.Elapsed += MainTimer_Elapsed;
            MainTimer.Start();
            return Task.CompletedTask;
        }

        private void MainTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Logger.LogInformation("Service Is Called {0}", DateTime.Now);
            MainTimer.Stop();
            try
            {

                Logger.LogWarning("Students marks calculation services is running!");
                var scope = Provider.CreateScope();
                var Context = scope.ServiceProvider.GetRequiredService<AppDbContext>();


                var query = Context.StudentClasses.AsQueryable();
                var query1 = Context.StudentRegisterations.AsQueryable();
                var query2 = Context.StudentClassMarks.AsQueryable();
               

                Context.StudentRegisterations.ToList().ForEach(h =>
               {
                   var query4 = (
                                   from q1 in Context.Profiles
                                   join q2 in Context.StudentClasses on q1.Id equals q2.ProfileId
                                   join q3 in Context.StudentRegisterations on q2.ProfileId equals q3.ProfileId
                                   join q4 in Context.ClassTypes on q2.ClassTypeId equals q4.Id
                                   join q5 in Context.ClassManagements on q2.ClassManagementId equals q5.Id
                                 

                                   where q2.IsActive == false

                                  
                                   select new SearchStudentResultModel
                                   {
                                       Id = q2.Id,
                                       ProfileId = q2.ProfileId,
                                       ClassManagementID = (short)q5.Id,
                                       ClassTypeId = q4.Id,
                                   }).ToList();

                   for (int t = 0; t < query4.Count; t++)
                   {
                       var score = Context.StudentClassMarks.Where(e => e.StudentClassId == query4[t].Id).ToList();
                       var stusub = Context.StudentClassMarks.Where(s => s.StudentClassId == query4[t].Id && s.ExamTypeId == 1).Select(st => st.SubjectId).ToList();

                       int resultype =0;
                       var marks = 0;
                       var Average = 0;
                       for (int i = 0; i < score.Count; i++)
                       {
                           marks = (int)(marks + score[t].Marks);

                       }
                       // Exam calculation Creteria
                       if (marks == 0)
                       {
                           resultype = 2;
                       }
                       else if (marks > 0)
                       {
                           Average = marks / stusub.Count;

                           if (Average > 0 && Average < 45)
                           {
                               resultype = 3;
                           }
                           else
                           {
                               resultype = 1;
                           }
                       }
                       
                       if(marks == 0) {
                          
                           throw new BusinessRulesException("the student need to give exam first");
                       }
                       else
                       {
                           var newResult = new Domain.Entity.prf.StudentResult
                           {
                               ProfileId = query4[t].ProfileId,
                               ClassTypeId = query4[t].ClassTypeId,
                               ClassManagementId = query4[t].ClassManagementID,
                               Total = marks,
                               ResultId = resultype,
                               CreatedOn = DateTime.Now,
                               ModifiedBy = "",
                               ModifiedOn = DateTime.Now

                           };

                           try
                           {
                               Context.StudentResults.Add(newResult);
                               Context.SaveChanges();
                               try
                               {
                                   StudentClass studentClasses = new StudentClass();
                                   studentClasses = Context.StudentClasses.Where(e => e.Id == query4[t].Id).SingleOrDefault();

                                   studentClasses.IsActive = true;

                                   Context.SaveChanges();
                               }
                               catch (Exception e)
                               {

                               }
                           }
                           catch (Exception ex)
                           {
                               //return new JsonResult(CustomMessages.FabricateException(ex));
                           }
                       }
                       
                   }
               });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error Processing");
            }
            finally
            {
                MainTimer.Start();
            }
        }
    }
}


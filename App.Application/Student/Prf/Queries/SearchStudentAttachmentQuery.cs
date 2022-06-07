
using App.Application.Student.Prf.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Prf.Queries
{
    public class SearchStudentAttachmentQuery : IRequest<List<StudentAttachmentModel>>
    {
        public long? Id { get; set; }
        public int RecordId { get; set; }
        public int DocumentTypeId { get; set; }
        public long ProfileId { get; set; }
        public int ScreenId { get; set; }

    }

    public class StudentAttachmentSearchQueryHandler : IRequestHandler<SearchStudentAttachmentQuery, List<StudentAttachmentModel>>
    {

        private readonly AppDbContext _context;
       
        public StudentAttachmentSearchQueryHandler(AppDbContext context)
        {
            _context = context;
        }
       

        public async Task<List<StudentAttachmentModel>> Handle(SearchStudentAttachmentQuery request, CancellationToken cancellationToken)
        {
            List<StudentAttachmentModel> result = new List<StudentAttachmentModel>();
            var query = _context.Documents.AsQueryable();

          
            
             if (!request.Id.HasValue)
            {
                query = query.Where(e => e.RecordId == request.ProfileId && e.ScreenId == request.ScreenId);
            }
            else
            {
                query = query.Where( e=> e.Id == request.Id);
            }


            return await query.Select(d => new StudentAttachmentModel
            {
                Id = d.Id,
                Description = d.Description,
                RecordId = d.RecordId,
                DocumentTypeId = d.DocumentTypeId,
                DocumentTypeText = d.DocumentType.Name,
                Path = d.Path,
                DocumentNumber = d.DocumentNumber,
                DocumentSource = d.DocumentSource,
                DocumentDate = d.DocumentDate,
                LastDownloadDate = d.LastDownloadDate,
                DocumentDateShamsi = PersianDate.GetFormatedString(d.DocumentDate),
                DownloadDateText = PersianDate.GetFormatedString(d.LastDownloadDate),
                UploadDateText = PersianDate.GetFormatedString(d.UploadDate)
                //DocumentDateShamsi = string.Concat(PersianDate.Convert(d.DocumentDate).Year,"/",
                //PersianDate.Convert(d.DocumentDate).Month, "/", PersianDate.Convert(d.DocumentDate).Day)
            }).ToListAsync();

        }
    }
}
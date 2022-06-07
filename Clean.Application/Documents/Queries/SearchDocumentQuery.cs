using Clean.Application.Documents.Models;
using Clean.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Clean.Common.Dates;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Documents.Queries
{
    public class SearchDocumentQuery : IRequest<List<SearchedDocumentModel>>
    {

        public long? Id { get; set; }
        public int RecordId { get; set; }
        public string ModuleId { get; set; }
        public string Item { get; set; }

    }


    public class SearchDocumentQueryHandler : IRequestHandler<SearchDocumentQuery, List<SearchedDocumentModel>>
    {

        private readonly BaseContext _context;
        //private readonly IMediator _mediator;

        public SearchDocumentQueryHandler(BaseContext context/*, IMediator mediator*/)
        {
            _context = context;
            //_mediator = mediator;
        }

        public async Task<List<SearchedDocumentModel>> Handle(SearchDocumentQuery request, CancellationToken cancellationToken)
        {
            List<SearchedDocumentModel> result = new List<SearchedDocumentModel>();
            var query = _context.Documents.AsQueryable();

            if (request.Id != null)
            {
                query = query.Where(e => e.Id == request.Id);
            }
            else /*if (request.RecordId != null)*/
            {
                query = query.Where(e => e.RecordId == request.RecordId && e.FileName == request.Item);
            }

            return await query.Select(d => new SearchedDocumentModel
            {
                Id = d.Id,
                Description = d.Description,
                RecordId = d.RecordId,
                DocumentTypeId = d.DocumentTypeId,
                DocumentTypeText = d.DocumentType.Name,
                Path = d.Path,
                DocumentNumber = d.DocumentNumber,
                DocumentSource = d.DocumentSource,
                DocumentDateShamsi = PersianDate.GetFormatedString(d.DocumentDate),
                DownloadDateText = PersianDate.GetFormatedString(d.LastDownloadDate),
                UploadDateText = PersianDate.GetFormatedString(d.UploadDate)
            }).ToListAsync() ; 

        }
    }
}

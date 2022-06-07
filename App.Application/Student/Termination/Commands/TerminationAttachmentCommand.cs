using App.Application.Student.Termination.Models;
using App.Application.Student.Termination.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace App.Application.Student.Termination.Commands
{


    public class TerminationAttachmentCommand : IRequest<List<SearchTerAttachmentModel>>
    {

        public long? Id { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string Module { get; set; }
        public string Item { get; set; }
        public long RecordId { get; set; }
        public string Root { get; set; }
        public string Path { get; set; }
        public string EncryptionKey { get; set; }
        public string ReferenceNo { get; set; }
        public int? StatusId { get; set; }
        public int? ScreenId { get; set; }
        public string Description { get; set; }
        public int DocumentTypeId { get; set; }
        public long ProfileId { get; set; }
        public DateTime? LastDownloadDate { get; set; }
        public string FileName { get; set; }
        public string DocumentNumber { get; set; }
        public string Documentsource { get; set; }
        public DateTime DocumentDate { get; set; }
        public int? BranchId { get; set; }//just for account opening
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class TerminationAttachmentCommandHandler : IRequestHandler<TerminationAttachmentCommand, List<SearchTerAttachmentModel>>
    {

        private readonly AppDbContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public TerminationAttachmentCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;

        }

        public async Task<List<SearchTerAttachmentModel>> Handle(TerminationAttachmentCommand request, CancellationToken cancellationToken)
        {
            string path = request.Path;
            string FileName = Path.GetFileName(path); // extracting filename 
            var ContentType = Path.GetExtension(path); // extracting file extension
            int CurrentUserId = await _currentUser.GetUserId();
            List<SearchTerAttachmentModel> Result = new List<SearchTerAttachmentModel>();
            if (request.Id == null || request.Id == default(decimal))
            {

                var TerminationAttachment = new Clean.Domain.Entity.doc.Documents()
                {

                    ContentType = ContentType,
                    UploadDate = DateTime.Now,
                    Path = request.Path,
                    RecordId = request.ProfileId,
                    Root = request.Root,
                    DocumentTypeId = request.DocumentTypeId,
                    DocumentDate = request.DocumentDate,
                    DocumentNumber = request.DocumentNumber,
                    DocumentSource = request.Documentsource,
                    Description = request.Description,
                    EncryptionKey = request.EncryptionKey,
                    FileName = FileName,
                    ScreenId = request.ScreenId,
                    LastDownloadDate = DateTime.Now,

                };

                _context.Documents.Add(TerminationAttachment);

                await _context.SaveChangesAsync(cancellationToken);

                Result = await _mediator.Send(new SearchTerAttachmentQuery() { Id = TerminationAttachment.Id });


            }
            else
            {

                var TerminationAttachment = (from p in _context.Documents
                                         where p.Id == request.Id
                                         select p).First();

                TerminationAttachment.ContentType = ContentType;
                TerminationAttachment.UploadDate = request.UploadDate;
                TerminationAttachment.Path = request.Path;
                TerminationAttachment.RecordId = request.ProfileId;
                TerminationAttachment.Root = request.Root;
                TerminationAttachment.UploadDate = request.UploadDate;
                TerminationAttachment.DocumentTypeId = request.DocumentTypeId;
                TerminationAttachment.DocumentDate = request.DocumentDate;
                TerminationAttachment.DocumentNumber = request.DocumentNumber;
                TerminationAttachment.DocumentSource = request.Documentsource;
                TerminationAttachment.Description = request.Description;
                TerminationAttachment.EncryptionKey = request.EncryptionKey;
                TerminationAttachment.FileName = FileName;
                TerminationAttachment.ScreenId = request.ScreenId;
                TerminationAttachment.LastDownloadDate = request.LastDownloadDate;

                await _context.SaveChangesAsync();

                Result = await _mediator.Send(new SearchTerAttachmentQuery() { Id = TerminationAttachment.Id });

            }
            return Result;
        }
    }
}

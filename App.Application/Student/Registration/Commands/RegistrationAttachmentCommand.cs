using App.Application.Prf.Models;
using App.Application.Prf.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Prf.Commands
{
    public class RegistrationAttachmentCommand : IRequest<List<RegisterationAttachmentModel>>
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


    public class RegistrationAttachmentCommandHandler : IRequestHandler<RegistrationAttachmentCommand, List<RegisterationAttachmentModel>>
    {

        private readonly AppDbContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public RegistrationAttachmentCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;

        }

        public async Task<List<RegisterationAttachmentModel>> Handle(RegistrationAttachmentCommand request, CancellationToken cancellationToken)
        {

            string path = request.Path;
            string FileName = Path.GetFileName(path); // extracting filename 
            var ContentType = Path.GetExtension(path); // extracting file extension
            int CurrentUserId = await _currentUser.GetUserId();
            List<RegisterationAttachmentModel> Result = new List<RegisterationAttachmentModel>();
            if (request.Id == null || request.Id == default(decimal))
            {

                var StudentAttachment = new Clean.Domain.Entity.doc.Documents()
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

                _context.Documents.Add(StudentAttachment);

                await _context.SaveChangesAsync(cancellationToken);

                Result = await _mediator.Send(new SearchRegisterationAttachmentQuery() { Id = StudentAttachment.Id });


            }
            else
            {

                var StudentAttachment = (from p in _context.Documents
                                         where p.Id == request.Id
                                         select p).First();

                StudentAttachment.ContentType = ContentType;
                StudentAttachment.UploadDate = request.UploadDate;
                StudentAttachment.Path = request.Path;
                StudentAttachment.RecordId = request.ProfileId;
                StudentAttachment.Root = request.Root;
                StudentAttachment.UploadDate = request.UploadDate;
                StudentAttachment.DocumentTypeId = request.DocumentTypeId;
                StudentAttachment.DocumentDate = request.DocumentDate;
                StudentAttachment.DocumentNumber = request.DocumentNumber;
                StudentAttachment.DocumentSource = request.Documentsource;
                StudentAttachment.Description = request.Description;
                StudentAttachment.EncryptionKey = request.EncryptionKey;
                StudentAttachment.FileName = FileName;
                StudentAttachment.ScreenId = request.ScreenId;
                StudentAttachment.LastDownloadDate = request.LastDownloadDate;

                await _context.SaveChangesAsync();

                Result = await _mediator.Send(new SearchRegisterationAttachmentQuery() { Id = StudentAttachment.Id });

            }
            return Result;
        }
    }
}


   
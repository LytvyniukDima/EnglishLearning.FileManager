using System.IO;
using System.Linq;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Extensions;
using EnglishLearning.FileManager.Web.ViewModels;
using FluentValidation;
using static EnglishLearning.FileManager.Application.Constants.FileConstants;

namespace EnglishLearning.FileManager.Web.Validators
{
    public class FileCreateViewModelValidator : AbstractValidator<FileCreateViewModel>
    {
        private static readonly string[] AcceptedFileExtensions = ArchiveFileExtensions
            .Concat(TextFileExtensions)
            .ToArray();
        
        private readonly IFileService _fileService;

        private readonly IFolderService _folderService;
        
        public FileCreateViewModelValidator(
            IFileService fileService,
            IFolderService folderService)
        {
            _fileService = fileService;
            _folderService = folderService;
            
            RuleFor(x => x.UploadedFile).NotNull();

            RuleFor(x => x.UploadedFile.FileName.GetFileExtension())
                 .NotNull()
                 .Must(x => AcceptedFileExtensions.Contains(x))
                 .WithMessage("Only txt, csv, zip files are accepted");

            When(
                x => TextFileExtensions.Contains(x.UploadedFile.FileName.GetFileExtension()), 
                () =>
                {
                    RuleFor(x => x)
                        .MustAsync(async (x, cancellation) =>
                        {
                            var filesInFolder = await _fileService.GetAllByFolderId(x.FolderId);

                            return filesInFolder.All(file => file.Name != x.Name);
                        })
                        .WithMessage("Name not unique");
                });
            
            When(
                x => ArchiveFileExtensions.Contains(x.UploadedFile.FileName.GetFileExtension()), 
                () =>
                {
                    RuleFor(x => x)
                        .MustAsync(async (x, cancellation) =>
                        {
                            var foldersInFolder = await _folderService.GetChildFoldersAsync(x.FolderId);

                            return foldersInFolder.All(file => file.Name != x.Name);
                        })
                        .WithMessage("Folder name not unique");
                });
        }
    }
}

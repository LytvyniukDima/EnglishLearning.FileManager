using System.Linq;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Web.Constants;
using EnglishLearning.FileManager.Web.ViewModels;
using FluentValidation;

namespace EnglishLearning.FileManager.Web.Validators
{
    public class FileCreateViewModelValidator : AbstractValidator<FileCreateViewModel>
    {
        private static readonly string[] ArchiveContentTypes = new[]
        {
            ContentTypes.Zip,
        };

        private static readonly string[] TextFileContentTypes = new[]
        {
            ContentTypes.Txt,
            ContentTypes.Csv,
        };

        private static readonly string[] AcceptedContentTypes = ArchiveContentTypes
            .Concat(TextFileContentTypes)
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

            RuleFor(x => x.UploadedFile.ContentType)
                .Must(x => AcceptedContentTypes.Contains(x))
                .WithMessage("Only txt, csv, zip files are accepted");

            When(
                x => TextFileContentTypes.Contains(x.UploadedFile.ContentType), 
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
                x => ArchiveContentTypes.Contains(x.UploadedFile.ContentType), 
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

using AutoMapper;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Models.ContentEditing;

namespace Umbraco.Web.Models.Mapping
{
    internal class DefaultTemplateResolver : IValueResolver<IContent, ContentItemDisplay, string>
    {
        private readonly IFileService _fileService;

        public DefaultTemplateResolver(IFileService fileService)
        {
            _fileService = fileService ?? throw new System.ArgumentNullException(nameof(fileService));
        }

        public string Resolve(IContent source, ContentItemDisplay destination, string destMember, ResolutionContext context)
        {
            if (source?.TemplateId == null) return null;

            var alias = _fileService.GetTemplate(source.TemplateId.Value).Alias;

            //set default template if template isn't set
            if (string.IsNullOrEmpty(alias))
                alias = source.ContentType.DefaultTemplate == null
                    ? string.Empty
                    : source.ContentType.DefaultTemplate.Alias;

            return alias;
        }
    }
}

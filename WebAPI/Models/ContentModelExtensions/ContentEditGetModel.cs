using System.Collections.Generic;

namespace WebApi.Models.ContentModelExtensions
{
    public class ContentEditGetModel
    {
        public ContentBaseModel Content { get; set; }
        public IEnumerable<ContentTypeModel> ContentType { get; set; }
    }
}
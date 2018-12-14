using System.Collections.Generic;

namespace WebApi.Models.ContentModelExtensions
{
    public class ContentCreateGetModel
    {
        public IEnumerable<ContentTypeModel> ContentType { get; set; }
    }
}
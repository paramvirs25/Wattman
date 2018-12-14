using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.AppConstants
{
    public class ContentType
    {
        public const string Video = "Video";
        public const string Audio = "Audio";

        public static List<ContentTypeModel> GetAll()
        {
            List<ContentTypeModel> ct = new List<ContentTypeModel>
            {
                new ContentTypeModel() { ContentTypeName = Video},
                new ContentTypeModel() { ContentTypeName = Audio}
            };

            return ct;
        }
    }
}

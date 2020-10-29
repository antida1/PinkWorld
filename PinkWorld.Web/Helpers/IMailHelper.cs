using PinkWorld.Common.Responses;

namespace PinkWorld.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }

}

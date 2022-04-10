using Shopping.Common;

namespace Shopping.Interface
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}

using System;

namespace Project.ProjectDataBase.Domain
{
    public class CaptchaCode
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}

using Newtonsoft.Json;

namespace PinkWorld.Web.Data.Entities
{
    public class UserAnswer
    {

        public int Id { get; set; }

        [JsonIgnore]
        public Answer Answer { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}

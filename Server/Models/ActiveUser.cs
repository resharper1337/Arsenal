namespace Server.Models
{
    public class ActiveUser: ConnectionForm
    {

        public string ConnectionId { get; set; }

        public DateTime LastActive { get; set; }

    }
}

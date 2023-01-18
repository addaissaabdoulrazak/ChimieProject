namespace ChimieProject.Models.Entities
{
    public class Message
    {
        public bool etat { get; set; }
        public string message { get; set; }
        public Message(bool e, string m)
        {
            this.etat = e;
            this.message = m;
        }
    }
}

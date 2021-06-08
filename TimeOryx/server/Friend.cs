namespace MobileClient
{
    public class Friend
    {
        public int Id { get; set; }
       // [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            Friend friend = obj as Friend;
            return this.Id == friend.Id;
        }
    }
}
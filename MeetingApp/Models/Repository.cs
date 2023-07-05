namespace MeetingApp.Models
{
    public static class Repository
    {
        private static List<UserInfo> _users = new();

        static Repository()
        {
            _users.Add(new UserInfo() { Id=1, Name = "Ali", Email = "abc@gmail.com", Phone = "11111", WillAttend=true });
            _users.Add(new UserInfo() { Id=2, Name = "Ahmet", Email = "abcd@gmail.com", Phone = "22222", WillAttend=false });
            _users.Add(new UserInfo() { Id=3, Name = "Canan", Email = "abce@gmail.com", Phone = "33333", WillAttend=true });
        }

        public static List<UserInfo> Users {
            get {
                return _users;
            }
        }

        public static void CreateUser(UserInfo user)
        {
            user.Id = Users.Count + 1;
            _users.Add(user);
        }        

        public static UserInfo? GetById(int id)
        {
            return _users.FirstOrDefault(user => user.Id == id);
        }
    }
}
using Movies.ViewModels.Enums;

namespace Movies.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public int? Subscription { private get; set; }
        public SubscriptionType SubscriptionType => (SubscriptionType)Subscription;
    }
}

namespace Framework.Models.Account
{
    public class RegisterRequest<TSubscriberType, TSubscriberPlan ,TPerson>
        where TSubscriberType : struct
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
        public TSubscriberType? SubscriberType { get; set; } = default(TSubscriberType);
        public TSubscriberPlan? SubscriberPlan { get; set; } = default(TSubscriberPlan);
        public TPerson? Person { get; set; }
    }
}


namespace DocConnect.Business.Models.Options
{
    public class EmailOptions
    {
        public string ConnectionString { get; set; }

        public string Sender { get; set; }

        public string ConfirmEmailRoute { get; set; }

        public string ResetPasswordRoute { get; set; }
    }
}

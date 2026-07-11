namespace CustomerCommLib
{
    // Class under test: depends on IMailSender through constructor injection
    // so a mock can be substituted during unit testing.
    public class CustomerComm
    {
        private readonly IMailSender _mailSender;

        public CustomerComm(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public bool SendMailToCustomer()
        {
            // Actual logic goes here - define message and mail address
            _mailSender.SendMail("cust123@abc.com", "Some Message");
            return true;
        }
    }
}

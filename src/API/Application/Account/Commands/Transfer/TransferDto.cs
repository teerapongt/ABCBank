namespace API.Application.Account.Commands.Transfer
{
    public class TransferDto
    {
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public decimal Transfer { get; set; }
    }
}
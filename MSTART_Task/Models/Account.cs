namespace MSTART_Task.Models
{
    public class Account
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public DateTime Server_DateTime { get; set; }
        public DateTime DateTime_UTC { get; set; }
        public DateTime Update_DateTime_UTC { get; set; }
        public string Account_Number { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; }
        public User User { get; set; }
    }
}

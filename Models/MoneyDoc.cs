using System;

namespace MyTetingApp.Models
{
    public class MoneyDoc
    {
        public int Id { get; set; }
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public string Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string System_Id { get; set; }
        public int ValCurs_Id { get; set; }
        public DateTime DateOfUpload { get; set; }
    }
}

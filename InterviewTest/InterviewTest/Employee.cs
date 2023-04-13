using System;

namespace InterviewTest
{
    public class Employee
    {
        public int TableID { get; set; }
        public int Id { get; set; }
        public int DivisionNO { get; set; }
        public string _DivisionNO
        {
            get
            {
                if (DivisionNO == 1)
                    return Constant.MainCompany;
                else if (DivisionNO == 2)
                    return Constant.ServiceCompany;
                else if (DivisionNO == 3)
                    return Constant.PartsCompany;
                else
                    return string.Empty;
            }
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Email { get { return $"{FirstName.Substring(0, 1)}{LastName}@{_DivisionNO}.com"; } }
        public string AddressA { get; set; }
        public string AddressB { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public DateTime DateIntegrated { get; set; }
    }
}

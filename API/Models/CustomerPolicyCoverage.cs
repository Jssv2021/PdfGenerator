using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CustomerPolicyCoverage
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public int CoverageId { get; set; }
        public double PersonalPropertyCoverage { get; set; }
        public double PropertyDeduction { get; set; }
        public double PersonalLiabilityLimit { get; set; }
        public double DamageToPropertyOfOthers { get; set; }
        public int PolicyNumber { get; set; }
        public DateTime PolicyEffectiveDate { get; set; }
        public DateTime PolicyExpiryDate { get; set; }
        public string PaymentOption { get; set; }
        public double TotalAmount { get; set; }
        public string Active { get; set; }
    }

}


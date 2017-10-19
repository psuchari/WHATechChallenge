using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public static class Evalulator
    {
        public static bool IsStakeUnusual(Int32 stake, Double averageStake)
        {
            return (stake >= averageStake * 10);
        }

        public static bool IsStakeHighlyUnusual(Int32 stake, Double averageStake)
        {
            return (stake >= averageStake * 30);
        }

        public static bool IsHighPayoutBet(Int32 payout)
        {
            return (payout >= 1000);
        }
  
        public static bool IsCustomerRisky(Int32 customer, List<Int32> RiskyCustomers)
        {
            return (RiskyCustomers.Count > 0 && RiskyCustomers.Exists(x => x == customer));
        }
    }
}
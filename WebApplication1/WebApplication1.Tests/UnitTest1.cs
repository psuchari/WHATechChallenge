using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApplication1.Tests
{
    [TestClass]
    public class UnitTestEvalulator
    {
        [TestMethod]
        public void TestNormalStake()
        {
            Int32 stake = 100;
            double averageStake = 50;

            Assert.AreEqual(false, Evalulator.IsStakeUnusual(stake, averageStake), "Normal stake not determined correctly");
        }

        [TestMethod]
        public void TestUnusualStake()
        {
            Int32 stake = 100;
            double averageStake = 10;

            Assert.AreEqual(true, Evalulator.IsStakeUnusual(stake, averageStake), "Unsual stake not determined correctly");
        }


        [TestMethod]
        public void TestHighlyUnusualStake()
        {
            Int32 stake = 1000;
            double averageStake = 10;

            Assert.AreEqual(true, Evalulator.IsStakeUnusual(stake, averageStake), "Highly Unsual stake not determined correctly");
        }

        [TestMethod]
        public void TestNormalPayout()
        {
            Int32 payout = 100;

            Assert.AreEqual(false, Evalulator.IsHighPayoutBet(payout), "Normal payout not determined correctly");
        }

        [TestMethod]
        public void TestHighPayout()
        {
            Int32 payout = 2000;

            Assert.AreEqual(true, Evalulator.IsHighPayoutBet(payout), "High payout not determined correctly");
        }

        [TestMethod]
        public void TestRiskyCustomer()
        {
            Int32 customer = 1;
            List<Int32> riskyCustomers = new List<Int32> { 1 };

            Assert.AreEqual(true, Evalulator.IsCustomerRisky(customer, riskyCustomers), "Risky customer not determined correctly");
        }

        [TestMethod]
        public void TestNormalCustomer()
        {
            Int32 customer = 2;
            List<Int32> riskyCustomers = new List<Int32> { 1 };

            Assert.AreEqual(false, Evalulator.IsCustomerRisky(customer, riskyCustomers), "Normal customer not determined correctly");
        }

    }
}

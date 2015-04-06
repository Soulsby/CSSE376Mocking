using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
    
        [TestMethod]
        public void TestThatCarDoesGetCarLocation()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            
            String carLocation = "Atlanta";
            String anotherCarLocation = "Miami";

            Expect.Call(mockDB.getCarLocation(112)).Return(carLocation);
            Expect.Call(mockDB.getCarLocation(998)).Return(anotherCarLocation);

            mocks.ReplayAll();

            Car target = new Car(3);

            target.Database = mockDB;

            String result;

            result = target.getCarLocation(112);
            Assert.AreEqual(carLocation, result);

            result = target.getCarLocation(998);
            Assert.AreEqual(anotherCarLocation, result);

            mocks.VerifyAll();
        }

    [TestMethod]
        public void TestThatCarDoesGetMileageFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            int Miles = new Int32();
            Miles = 100;

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDatabase.Miles = Miles;
            var target = ObjectMother.BMW();
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(mileage, Miles);

            mocks.VerifyAll();
        }
	}
}

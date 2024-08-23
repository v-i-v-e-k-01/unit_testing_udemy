using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TestNinja.Mocking;

using Moq;

namespace TestNinja.UnitTests.Mocking
{

    [TestFixture]
    public class BookingHelperTests
    {
        Mock<IBookingRepository> _repository;
        Booking _booking;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IBookingRepository>();
            _booking = new Booking
            {
                Reference = "bookingReference",
                Status = "bookingStatus",
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Now.AddDays(2),
                Id = 1
            };
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeExistingBooking_ReturnsEmptyString()
        {
            _repository.Setup(b => b.GetActiveBookings(_booking.Id)).Returns(new List<Booking>
            {
                new Booking
                {
                    Reference = "bookingReference",
                    Status = "bookingStatus",
                    ArrivalDate = new DateTime(2025,1,15,14,0,0),
                    DepartureDate = new DateTime(2025,1,20,10,0,0),
                    Id = 2
                }
            }.AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(_booking, _repository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));

        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesAfterExistingBooking_ReturnsEmptyString()
        {
            _repository.Setup(b => b.GetActiveBookings(_booking.Id)).Returns(new List<Booking>
            {
                new Booking
                {
                    Reference = "bookingReference",
                    Status = "bookingStatus",
                    ArrivalDate = Before(_booking.ArrivalDate, days: 3),
                    DepartureDate = Before(_booking.ArrivalDate, days: 2),
                    Id = 2
                }
            }.AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(_booking, _repository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));

        }

        [Test]
        public void OverlappingBookingsExist_BookingStartBetweenExistingBookingArrivalAndDepartureDate_ReturnsOverlappingReference()
        {
            _repository.Setup(b => b.GetActiveBookings(_booking.Id)).Returns(new List<Booking>
            {
                new Booking
                {
                    Reference = "bookingReference",
                    Status = "bookingStatus",
                    ArrivalDate = Before(_booking.ArrivalDate, days: 2 ),
                    DepartureDate = After(_booking.ArrivalDate, days: 2),
                    Id = 2
                }
            }.AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(_booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingEndsBetweenExistingBookingArrivalAndDepartureDate_ReturnsOverlappingReference()
        {
            _repository.Setup(b => b.GetActiveBookings(_booking.Id)).Returns(new List<Booking>
            {
                new Booking
                {
                    Reference = "bookingReference",
                    Status = "bookingStatus",
                    ArrivalDate = Before(_booking.DepartureDate, days: 2 ),
                    DepartureDate = After(_booking.DepartureDate, days: 2),
                    Id = 2
                }
            }.AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(_booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndEndsInMiddleOfExistingBookingArrivalAndDepartureDate_ReturnsOverlappingReference()
        {
            _repository.Setup(b => b.GetActiveBookings(_booking.Id)).Returns(new List<Booking>
            {
                new Booking
                {
                    Reference = "bookingReference",
                    Status = "bookingStatus",
                    ArrivalDate = Before(_booking.ArrivalDate, days: 2 ),
                    DepartureDate = After(_booking.DepartureDate, days: 2),
                    Id = 2
                }
            }.AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(_booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBokingsExist_BookingStartBeforeAndEndsAfterExistingBooking_ReturnsOverlappingReference()
        {
            _repository.Setup(b => b.GetActiveBookings(_booking.Id)).Returns(new List<Booking>
            {
                new Booking
                {
                    Reference = "bookingReference",
                    Status = "bookingStatus",
                    ArrivalDate = After(_booking.ArrivalDate, days: 1 ),
                    DepartureDate = Before(_booking.DepartureDate, days: 1),
                    Id = 2
                }
            }.AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(_booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingsOverlapButNewBookingCancelled_ReturnsEmptyString()
        {
            _booking.Status = "Cancelled";

            _repository.Setup(b => b.GetActiveBookings(_booking.Id)).Returns(new List<Booking>
            {
                new Booking
                {
                    Reference = "bookingReference",
                    Status = "bookingStatus",
                    ArrivalDate = After(_booking.ArrivalDate, days: 1 ),
                    DepartureDate = Before(_booking.DepartureDate, days: 1),
                    Id = 2
                }
            }.AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(_booking, _repository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        private DateTime Before(DateTime givenDate, int days = 1)
        {
            return givenDate.AddDays(-days);
        }

        private DateTime After(DateTime givenDate, int days = 1)
        {
            return givenDate.AddDays(days);
        }

    }
}

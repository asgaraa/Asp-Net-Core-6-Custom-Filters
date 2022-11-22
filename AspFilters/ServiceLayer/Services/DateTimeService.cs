using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class DateTimeService : IDateTimeService
    {
        private DateTime _dateTime;
        public DateTime dateTime;
        public DateTimeService()
        {

            _dateTime = DateTime.Now;

            _dateTime = dateTime;
        }
        public DateTime GetCurrentDateTime()
        {
            return dateTime;
        }
    }
}

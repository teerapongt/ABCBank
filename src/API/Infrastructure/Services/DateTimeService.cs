using System;
using API.Application.Common.Interfaces;

namespace API.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Heka.Domain.Models.Appointment;

namespace Heka.Service.Services.Helpers.ExtentionMethods
{
    public static class ExtentionsMethods
    {
        public static string GetEnumDisplayName(this AppointmentStatus enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
        }
    }
}

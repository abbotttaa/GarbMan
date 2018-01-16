using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GarbMan.Models
{
    public class PickUpDaysModels
    {
        [Key]
        public int Id { get; set; }

        public string User { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public int Zip { get; set; }

        [Display(Name = "Address Number")]
        public int AddressNum { get; set; }

        public string Address { get; set; }

        [Display(Name = "Pick Up Day")]
        public DayOfWeek PickUpDay { get; set; }

        [Display(Name = "Vacation Starts")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string Start { get; set; }

        [Display(Name = "Vacation Ends")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string End { get; set; }

        [Display(Name = "Single Day Exemption")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string OneDay { get; set; }

        public int Balance { get; set; }


    }
}
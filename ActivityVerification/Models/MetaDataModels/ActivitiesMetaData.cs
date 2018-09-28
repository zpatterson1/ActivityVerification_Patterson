using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityVerification.Models
{
    [MetadataType(typeof(ActivityMetaData))]
    public partial class Activity
    {
        public bool TypeEmpty
        {
            get
            {
                return (Type == 0);
            }
        }
    }
    public partial class ActivityMetaData
    {
        [Display(Name ="Case ID:")]
        public int Id { get; set; }
        [Display(Name = "Created By")]
        public string UserID { get; set; }
        [Display(Name = "Description:")]
        public string Description { get; set; }
        [Display(Name = "Activity Type:")]
        public int Type { get; set; }
        [Display(Name = "Verified:")]
        public bool Verified { get; set; }
        [Display(Name = "Reason:")]
        public Nullable<int> RecognitionReason { get; set; }
        [Display(Name = "Activity Name:")]
        public string Name { get; set; }
        [Display(Name = "Last Updated:")]
        public System.DateTime Updated { get; set; }
    }
}
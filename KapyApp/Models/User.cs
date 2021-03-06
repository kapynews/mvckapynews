//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KapyApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.ActionRecords = new HashSet<ActionRecord>();
            this.ActionRecords1 = new HashSet<ActionRecord>();
            this.Comments = new HashSet<Comment>();
            this.User_Source = new HashSet<User_Source>();
            this.Categories = new HashSet<Category>();
        }
    
        public int userId { get; set; }

        [Required(ErrorMessage = "Please provide a valid username", AllowEmptyStrings = false)]
        public string userName { get; set; }

        [Required(ErrorMessage = "Please provide a valid Email address", AllowEmptyStrings = false)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string userEmail { get; set; }

        [Required(ErrorMessage = "Please provide a password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6 character long.")]
        public string password { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable <int> roleId { get; set; }


        [Required(ErrorMessage = "How often would you like to recieve the email notification (0-7)", AllowEmptyStrings = false)]
        
        public Nullable<int> notifyFrequency { get; set; }
        public Nullable<bool> isNotified { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActionRecord> ActionRecords { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActionRecord> ActionRecords1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Source> User_Source { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }
    }
}

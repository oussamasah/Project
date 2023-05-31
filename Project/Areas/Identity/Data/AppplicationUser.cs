using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Project.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppplicationUser class
public class AppplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName ="nvarchar(100)")]
    public String Fullname { get; set; }


    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    [EnumDataType(typeof(MyEnum))]
    public String Role { get; set; }
}
public enum MyEnum
{
    Admin,
    Manager,
    Member
}

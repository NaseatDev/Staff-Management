using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StaffManagement.Service.Models;

public partial class Staff
{
    [Key]
    [StringLength(8)]
    public string StaffId { get; set; } = null!;

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public int Gender { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }
}

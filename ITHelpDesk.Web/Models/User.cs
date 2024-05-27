using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITHelpDesk.Web.Models
{
    public partial class User
    {
        public User()
        {
            TicketAssignedTos = new HashSet<Ticket>();
            TicketCreatedBies = new HashSet<Ticket>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        public string AzureAdObjectId { get; set; } = null!;
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [InverseProperty("AssignedTo")]
        public virtual ICollection<Ticket> TicketAssignedTos { get; set; }
        [InverseProperty("CreatedBy")]
        public virtual ICollection<Ticket> TicketCreatedBies { get; set; }
    }
}

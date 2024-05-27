using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITHelpDesk.Web.Models
{
    public partial class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(500)]
        public string? Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResolvedDate { get; set; }
        public TicketStatus Status { get; set; }
        public int? AssignedToId { get; set; }
        public int CreatedById { get; set; }

        [ForeignKey("AssignedToId")]
        [InverseProperty("TicketAssignedTos")]
        public virtual User? AssignedTo { get; set; }
        [ForeignKey("CreatedById")]
        [InverseProperty("TicketCreatedBies")]
        public virtual User CreatedBy { get; set; } = null!;
    }
    public enum TicketStatus
    {
        Open,
        InProgress,
        ReOpened,
        Resolved,
        Closed
    }
}

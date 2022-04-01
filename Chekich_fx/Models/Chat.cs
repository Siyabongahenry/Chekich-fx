using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public enum ChatStatus { Read,NotRead}
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public int SRInfoId { get; set; }
        public SenderReceiverInfo SRInfo { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public ChatStatus ChatStatus { get; set; }
        
    }
}

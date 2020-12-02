using System;

namespace PG.SharedKernel.Model
{
    public class BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int Void { get; set; }
        public DateTime VoidDate { get; set; }
    }
}

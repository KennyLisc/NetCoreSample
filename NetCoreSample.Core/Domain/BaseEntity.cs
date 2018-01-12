using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace NetCoreSample.Core.Domain
{
    [DataContract]
    public abstract partial class BaseEntity : BaseSimpleEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        //public virtual Guid Id { get; set; }

        [StringLength(128)]
        public string CreateUserId { get; set; }

        /// <summary>
        /// The time when the entity was first created.
        /// </summary>
        public DateTime? CreateDate { get; set; }

        [StringLength(128)]
        public string LastModifierUserId { get; set; }

        /// <summary>
        /// The time when the entity was last saved/updated.
        /// </summary>
        public DateTime? LastModificationDate { get; set; }

        public int? VersionNo { get; set; }

        public Guid? TransactionId { get; set; }

    }
}

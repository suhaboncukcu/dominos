namespace Dominos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Product
    {
        public virtual int ProductID { get; set; }

        public virtual string ProductName { get; set; }

        public virtual string ProductImage { get; set; }

        public virtual decimal ProductPrice { get; set; }
    }
}

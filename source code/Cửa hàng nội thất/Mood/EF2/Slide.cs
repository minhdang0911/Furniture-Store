namespace Mood.EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public long Id { get; set; }

        public string Image { get; set; }

        public int DisPlayOrder { get; set; }

        public string Title { get; set; }
        public string Link { get; set; }

        public bool Status { get; set; }
    }
}

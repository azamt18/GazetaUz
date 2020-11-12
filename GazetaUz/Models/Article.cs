using System;

namespace GazetaUz.Models
{
    public class Article
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Section Section { get; set; }
    }
}
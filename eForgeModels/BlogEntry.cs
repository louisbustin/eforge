using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace eForgeModels {
    public class BlogEntry {

        public int BlogEntryId { get; set; }
        public int BlogEntryCategoryId { get; set; }
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Body { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Subject { get; set; }
        public string Summary { get; set; }
        public int UserId { get; set; }
        public string LinkText { get; set; }
        public virtual User Author { get; set; }
        public virtual BlogEntryCategory Category { get; set; }

        public BlogEntry() {
        }

    }
}

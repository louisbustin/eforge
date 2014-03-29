using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eForgeModels {
    public class BlogEntryCategory {
        public int BlogEntryCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<BlogEntry> BlogEntries { get; set; }

        public BlogEntryCategory() {
            BlogEntries = new HashSet<BlogEntry>();
        }

    }
}

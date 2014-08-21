using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eForgeModels {
    public class Image {

        public int ImageId { get; set; }
        public byte[] ImageData { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string AltText { get; set; }
        public Guid ImageGuid { get; set; }
    }
}

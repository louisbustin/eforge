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

        public virtual string GetImageTag(int? width = null, int? height = null) {
                        //<img src="/Images/GetImage?imageGuid=@item.ImageGuid" width="100" height="100"/>

            return string.Format("<img src=\"{0}\" {1} {2} />", 
                GetImageLink(), 
                width.HasValue ? string.Format("width=\"{0}\"", width.ToString()) : "",
                height.HasValue ? string.Format("height=\"{0}\"", width.ToString()) : ""
                );
        }
        public virtual string GetImageLink() {
            return string.Format("/Images/GetImage?imageGuid={0}", ImageGuid);
        }
    }
}

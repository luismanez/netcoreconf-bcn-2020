using System;
using System.Collections.Generic;
using System.Text;

namespace Instanyam.Jobs.Models
{
    public enum ProcessImageResultType
    {
        Processed,
        Rejected
    }

    public class ProcessImageResult
    {
        public string ImageName { get; set; }
        public string ImageSize { get; set; }
        public List<string> ImageTags { get; set; }
        public ProcessImageResultType ResultType { get; set; }
        public string ErrorDetail { get; set; }
    }
}

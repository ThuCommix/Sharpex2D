using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSIntegration.Content
{
    public class Project
    {
        public string SourceFolder { get; set; }

        public string TargetFolder { get; set; }

        public string ContentPipeline { set; get; }

        public DateTime? FileLastChanged { set; get; }

        public bool IsError { set; get; }
    }
}

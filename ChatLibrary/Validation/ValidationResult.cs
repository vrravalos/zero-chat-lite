﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLibrary.Validation
{
    public struct ValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

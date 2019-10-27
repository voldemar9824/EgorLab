using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLab.Models
{
    public class BaseModelValidationResult
    {
        private StringBuilder _errorBuilder = new StringBuilder();

        public bool IsValid { get; private set; } = true;
        public string Errors { get => _errorBuilder.ToString().Trim(); }

        public void Append(string error)
        {
            IsValid = false;
            _errorBuilder.AppendLine(error);
        }
    }
}

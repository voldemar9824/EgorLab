using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgorLab.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName{ get; set; }
        public int Age { get; set; }

        public Person()
        {
            Id = new Guid();
        }

        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();
            if (string.IsNullOrWhiteSpace(Name))
            {
                validationResult.Append($"Name cannot be empty");
            }
            else
            {
                if (!char.IsUpper(Name.FirstOrDefault()))
                {
                    validationResult.Append($"Name {Name} should start from capital letter");
                }
            }
            if (string.IsNullOrWhiteSpace(SurName))
            {
                validationResult.Append($"SurName cannot be empty");
            }
            else
            {
                if (!char.IsUpper(SurName.FirstOrDefault()))
                {
                    validationResult.Append($"SurName {SurName} should start from capital letter");
                }
            }
            if (Age < 18)
            {
                validationResult.Append($"FullYears should not be under 18");
            }            
            return validationResult;
        }
        public override string ToString()
        {
            return $"{Name} {SurName} age {Age}";
        }
    }
}


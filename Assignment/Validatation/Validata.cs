using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Assignment.Validatation
{
    public class Validata:ValidationAttribute
    {
        public override bool IsValid(object obj)
        {
            var isNumber = "[1-9]";

            if (Regex.IsMatch((string)obj, isNumber))
            {
                return false;
            }
            else return true;

        }
    }
}

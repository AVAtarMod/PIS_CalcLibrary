using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
    public class Expression
    {
        [Display(Name = "Expression")]
        public string ExpressionString { get; set; }
        [Display(Name = "Result")]
        public double Result { get; set; }
    }
}

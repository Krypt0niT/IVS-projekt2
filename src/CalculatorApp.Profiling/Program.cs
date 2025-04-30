using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;

Console.WriteLine("Hello, World!");


// najprv nahrat cele do listu po koniec suboru

// vyratat priemer

// zlozit vzorec

var sum = new SumOperation(
    new List<Term>() 
    { 
        new AbsoluteMember(1),
        new AbsoluteMember(2),
    }
);


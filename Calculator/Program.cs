
using System;

class Program
{
	static void Main(string[] args)
	{
	Console.WriteLine("Simple Console Calculator");
	Console.WriteLine("Enter an expression (e.g., (2+2/2)-37 or 34+21-10*2/5):");
		string input = Console.ReadLine();

		try
		{
			double result = EvaluateExpression(input);
			Console.WriteLine($"Result: {result}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}

	// Simple expression evaluator for +, -, *, /
	static double EvaluateExpression(string expr)
	{
		if (string.IsNullOrWhiteSpace(expr))
			throw new ArgumentException("Input is empty");

		// Remove spaces
		expr = expr.Replace(" ", "");

	// Use DataTable.Compute for simplicity (n(ot for production use)
	// Supports +, -, *, /, and parentheses (brackets)
	// For more robust parsing, a real parser should be used
	var table = new System.Data.DataTable();
	object result = table.Compute(expr, "");
	return Convert.ToDouble(result);
	}
}

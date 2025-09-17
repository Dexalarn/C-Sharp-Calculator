
using System;

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Simple Console Calculator");
		Console.WriteLine("Enter an expression (e.g., (2+2/2)-37, 2^3, or 34+21-10*2/5):");
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

	
	static double EvaluateExpression(string expr)
	{
		if (string.IsNullOrWhiteSpace(expr))
			throw new ArgumentException("Input is empty");

		// Remove spaces
		expr = expr.Replace(" ", "");

		// Handle power operator (^), since DataTable.Compute does not support it
		expr = EvaluatePowers(expr);

		var table = new System.Data.DataTable();
		object result = table.Compute(expr, "");
		return Convert.ToDouble(result);
	}

	// Evaluates all ^ (power) operations in the expression string
	static string EvaluatePowers(string expr)
	{
		// Regex to find patterns like number^number (e.g., 2^3, (2+1)^4)
		var regex = new System.Text.RegularExpressions.Regex(@"(\([^()]+\)|[\d.]+)\^([\d.]+)");
		while (regex.IsMatch(expr))
		{
			expr = regex.Replace(expr, match =>
			{
				string left = match.Groups[1].Value;
				string right = match.Groups[2].Value;
				double baseVal = 0;
				// Evaluate left side if it's a parenthesis
				if (left.StartsWith("(") && left.EndsWith(")"))
				{
					var table = new System.Data.DataTable();
					baseVal = Convert.ToDouble(table.Compute(left, ""));
				}
				else
				{
					baseVal = Convert.ToDouble(left);
				}
				double expVal = Convert.ToDouble(right);
				double powResult = Math.Pow(baseVal, expVal);
				return powResult.ToString();
			});
		}
		return expr;
	}
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Action.Extensions
{
	public static class ExtensionMethodsSys
	{
		public static DateTime MonthStart(this DateTime pDateTime)
		{
			return new DateTime(pDateTime.Year, pDateTime.Month, 1);
		}

		/// <summary>
		///     Calcula a data que representa o fim do mês da data de referência
		/// </summary>
		/// <param name="pDateTime">Data de referência</param>
		/// <returns>DateTime representando o fim do mês</returns>
		public static DateTime MonthEnd(this DateTime pDateTime)
		{
			return new DateTime(pDateTime.Year, pDateTime.Month, DateTime.DaysInMonth(pDateTime.Year, pDateTime.Month));
		}

		/// <summary>
		///     Calcula a data que representa o início do ano da data de referência
		/// </summary>
		/// <param name="pDateTime">Data de referência</param>
		/// <returns>DateTime representando o início do ano</returns>
		public static DateTime YearStart(this DateTime pDateTime)
		{
			return new DateTime(pDateTime.Year, 1, 1);
		}

		/// <summary>
		///     Calcula a data que representa o fim do ano da data de referência
		/// </summary>
		/// <param name="pDateTime">Data de referência</param>
		/// <returns>DateTime representando o fim do ano</returns>
		public static DateTime YearEnd(this DateTime pDateTime)
		{
			return new DateTime(pDateTime.Year, 12, 31);
		}

		public static string RemoveCaracters(this string str, params char[] caracteres)
		{
			if (str == null) return str;
			caracteres.ToList().ForEach(c => str = str.Replace(c.ToString(), ""));
			return str;
		}

		public static List<string> GetErrors(this ModelStateDictionary pModel)
		{
			List<string> errors = new List<string>();

			if (!pModel.IsValid)
			{
				try {
					var values = pModel.Values;
					foreach (var value in values)
						foreach (var error in value.Errors)
							errors.Add(error.ErrorMessage);
				}
				catch { }

				if (errors.Count == 0)
					errors.Add("Modelo não é válido.");
			}

			return errors;
		}
	}
}
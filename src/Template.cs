using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SystemEx
{
	public static class Template
	{
		public static string TransformToText<TemplateType>(IDictionary<string, object> parameters) where TemplateType : new()
		{
			var TemplateInstance = new TemplateType();
			typeof(TemplateType).GetProperty("Session").SetValue(TemplateInstance, parameters, null);
			typeof(TemplateType).GetMethod("Initialize").Invoke(TemplateInstance, null);

			var Result = (string)typeof(TemplateType).GetMethod("TransformText").Invoke(TemplateInstance, null);
			var ResultBuilder = new StringBuilder(Result.Length);
			using (var ResultReader = new StringReader(Result))
			{
				var line = ResultReader.ReadLine();
				while (line != null)
				{
					ResultBuilder.AppendLine(line);
					line = ResultReader.ReadLine();
				}
			}

			return ResultBuilder.ToString();
		}
	}
}


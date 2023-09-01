
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.LivingDocumentation
{
    internal class TemplateFiller
    {
        public static void CreateTargetFile(string content, string title, string templatePath, string targetFileName)
        {
            var template = ReadTemplate(templatePath);
            var text = Evaluate(template, title, content);
            Write(targetFileName, text);
        }

        private static void Write(string fileFullName, string content)
        {
            using (var streamWriter = new StreamWriter(fileFullName))
            {
                streamWriter.Write(content);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        private static string Evaluate(string template, string title, string content)
        {
            return template.Replace("{0}", title).Replace("{1}", content);
        }

        public static string ReadTemplate(string templatePath)
        {
            using (var streamReader = new StreamReader(templatePath))
            {
                return streamReader.ReadToEnd();
            };
        }
    }
}

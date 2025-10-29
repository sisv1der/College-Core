using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wordroller;

namespace Cerberus
{
    public static class PrintHelper
    {
        private static string tempDirectory = "test.";

        public static string GetTempDirectory()
        {
            if (tempDirectory != null) return tempDirectory;
            var path = Path.Combine(Path.GetTempPath(), "Wordroller.Tests");
            Directory.CreateDirectory(path);
            tempDirectory = path;
            return tempDirectory;
        }

        public static WordDocument CreateNewDocument()
        {
            return new WordDocument(CultureInfo.GetCultureInfo("ru-ru"));
        }

        public static WordDocument LoadDocumentFromResource(string resourceName)
        {
            using (var stream = GetResourceStream(resourceName))
            {
                return new WordDocument(stream);
            }
        }

        public static Stream GetResourceStream(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) throw new ArgumentException($"Embedded resource {resourceName} not found", nameof(resourceName));
            return stream;
        }
    }
}

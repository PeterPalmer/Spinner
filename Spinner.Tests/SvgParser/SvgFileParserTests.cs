using System;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.SvgParser;

namespace Spinner.Tests
{
	[TestClass]
	public class SvgFileParserTests
	{
		[TestMethod]
		public void ParseFile()
		{
			string svgXml = String.Concat("<svg id='chrome' xmlns='http://www.w3.org/2000/svg' version='1.1'>",
											"<path fill='#FFFFFF' stroke='none' d='M12.34,56.78' />",
											"<path fill='#000000' stroke='none' d='M12.34,56.78' /></svg>");
			XmlTextReader reader = new XmlTextReader(new StringReader(svgXml));
			var parser = new SvgFileParser();

			var result = parser.ParsePathsXml(reader).ToList();

			Assert.AreEqual(2, result.Count());
			Assert.AreEqual("#FFFFFF", result[0].Fill);
			Assert.AreEqual("M12.34,56.78", result[1].Data);
		}
	}
}

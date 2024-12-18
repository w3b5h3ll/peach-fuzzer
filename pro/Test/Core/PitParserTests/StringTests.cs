﻿

using System.IO;
using NUnit.Framework;
using Peach.Core;
using Peach.Core.Analyzers;
using Peach.Core.IO;
using Peach.Core.Test;

namespace Peach.Pro.Test.Core.PitParserTests
{
	[TestFixture]
	[Quick]
	[Peach]
	public class StringTests
	{
		// TODO - Unicode and everything 

		[Test]
		public void SimpleStringTest()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"ascii\" value=\"abc\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.ascii, str.stringType);
			Assert.AreEqual("abc", (string)str.DefaultValue);

			BitwiseStream value = str.Value;
			Assert.AreEqual(3, value.Length);
			Assert.AreEqual(Encoding.ASCII.GetBytes("abc"), value.ToArray());
		}

		[Test]
		public void Utf7Test()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"utf7\" value=\"abc\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf7, str.stringType);
			Assert.AreEqual("abc", (string)str.DefaultValue);

			BitwiseStream value = str.Value;
			Assert.AreEqual(Encoding.UTF7.GetBytes("abc"), value.ToArray());
		}

		[Test]
		public void Utf8Test()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"utf8\" value=\"abc\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf8, str.stringType);
			Assert.AreEqual("abc", (string)str.DefaultValue);

			BitwiseStream value = str.Value;
			Assert.AreEqual(Encoding.UTF8.GetBytes("abc"), value.ToArray());
		}

		[Test]
		public void Utf16Test()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"utf16\" value=\"abc\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf16, str.stringType);
			Assert.AreEqual("abc", (string)str.DefaultValue);

			BitwiseStream value = str.Value;
			Assert.AreEqual(Encoding.Unicode.GetBytes("abc"), value.ToArray());
		}

		[Test]
		public void Utf16BeTest()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"utf16be\" value=\"abc\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf16be, str.stringType);
			Assert.AreEqual("abc", (string)str.DefaultValue);

			BitwiseStream value = str.Value;
			Assert.AreEqual(Encoding.BigEndianUnicode.GetBytes("abc"), value.ToArray());
		}

		[Test]
		public void Utf32Test()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"utf32\" value=\"abc\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf32, str.stringType);
			Assert.AreEqual("abc", (string)str.DefaultValue);

			BitwiseStream value = str.Value;
			Assert.AreEqual(Encoding.UTF32.GetBytes("abc"), value.ToArray());
		}

		[Test]
		public void Utf32BeTest()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"utf32be\" value=\"abc\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf32be, str.stringType);
			Assert.AreEqual("abc", (string)str.DefaultValue);

			BitwiseStream value = str.Value;
			Assert.AreEqual(Encoding.BigEndianUTF32.GetBytes("abc"), value.ToArray());
		}

		[Test]
		public void HexStringTest()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" valueType=\"hex\" value=\"0x0a\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf8, str.stringType);
			Assert.AreEqual(Variant.VariantType.String, str.DefaultValue.GetVariantType());
			Assert.AreEqual("\n", (string)str.DefaultValue);
		}

		[Test]
		public void HexStringTest2()
		{
			// 0xaa 0xbb is not a valid ascii string, should throw
			
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" valueType=\"hex\" value=\"0xaa 0xbb\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();

			Assert.Throws<PeachException>(delegate(){
				parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			});
		}

		[Test]
		public void Utf32StringTest()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"utf32\" value=\"Hello\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf32, str.stringType);
			Assert.AreEqual("Hello", (string)str.DefaultValue);

			BitwiseStream value = str.Value;
			Assert.AreEqual(20, value.Length);
			Assert.AreEqual(Encoding.UTF32.GetBytes("Hello"), value.ToArray());
		}

		[Test]
		public void HexStringUtf32Test()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"utf32\" valueType=\"hex\" value=\"48 00 00 00 65 00 00 00 6c 00 00 00 6c 00 00 00 6f 00 00 00\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf32, str.stringType);
			Assert.AreEqual(Variant.VariantType.String, str.DefaultValue.GetVariantType());
			Assert.AreEqual("Hello", (string)str.DefaultValue);
		}

		private void DoExpand(string enc, string lenType, int len, char? pad, bool nullTerm, string expected, int finalLen, string value="value=\"Hello\"")
		{
			string padChar = "";
			if (pad.HasValue)
				padChar = string.Format("padCharacter=\"{0}\"", pad.Value);

			string template = @"
<Peach>
	<DataModel name=""TheDataModel"">
		<String {5} type=""{0}"" lengthType=""{1}"" length=""{2}"" {3} nullTerminated=""{4}""/>
	</DataModel>
</Peach>";
			string xml = string.Format(template, enc, lenType, len, padChar, nullTerm.ToString().ToLower(), value);

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;
			Assert.AreNotEqual(null, str);

			Assert.AreEqual(expected, (string)str.DefaultValue);
			Assert.AreEqual(expected, (string)str.InternalValue);

			var val = str.Value.ToArray();

			Assert.AreEqual(finalLen, val.Length);
			Assert.AreEqual(expected, Encoding.GetEncoding(enc).GetString(val));
		}

		[Test]
		public void ExpandTests()
		{
			Assert.Throws<PeachException>(delegate() {
				DoExpand("ascii", "bits", 5, '_', true, "", 0);
			});

			DoExpand("ascii", "bytes", 10, '_', true, "Hello____\0", 10);
			DoExpand("ascii", "bits", 80, '_', true, "Hello____\0", 10);
			DoExpand("utf32", "bytes", 40, '_', true, "Hello____\0", 40);

			Assert.Throws<PeachException>(delegate() {
				DoExpand("utf32", "bytes", 21, '_', true, "", 0);
			});

			Assert.Throws<PeachException>(delegate()
			{
				DoExpand("utf32", "bytes", 43, '_', true, "", 0);
			});

			DoExpand("utf8", "chars", 10, '_', true, "Hello____\0", 10);
			DoExpand("utf16", "chars", 10, '_', true, "Hello____\0", 20);
			DoExpand("utf16be", "chars", 10, '_', true, "Hello____\0", 20);
			DoExpand("utf32", "chars", 10, '_', true, "Hello____\0", 40);

			DoExpand("ascii", "chars", 10, '_', false, "Hello_____", 10);
			DoExpand("ascii", "chars", 10, '_', true, "Hello____\0", 10);
			DoExpand("ascii", "chars", 6, '_', false, "Hello_", 6);
			DoExpand("ascii", "chars", 6, '_', true, "Hello\0", 6);
			DoExpand("ascii", "chars", 5, '_', false, "Hello", 5);

			Assert.Throws<PeachException>(delegate() {
				DoExpand("ascii", "chars", 5, '_', true, "", 0);
			});

			Assert.Throws<PeachException>(delegate() {
				DoExpand("ascii", "chars", 4, '_', true, "", 0);
			});

			DoExpand("ascii", "chars", 5, 'a', false, "aaaaa", 5, "");
			DoExpand("ascii", "chars", 5, 'a', true, "aaaa\0", 5, "");
			DoExpand("ascii", "bytes", 5, null, true, "\0\0\0\0\0", 5, "");
			DoExpand("utf16", "chars", 5, null, false, "\0\0\0\0\0", 10, "");
		}

		[Test]
		public void HexStringPad()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"ascii\" length=\"10\" valueType=\"hex\" value=\"48 65 6c 6c 6f\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.ascii, str.stringType);
			Assert.AreEqual(Variant.VariantType.String, str.DefaultValue.GetVariantType());
			Assert.AreEqual("Hello\0\0\0\0\0", (string)str.DefaultValue);
		}

		[Test]
		public void StringPadNull()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"ascii\" length=\"10\" padCharacter=\"a\" nullTerminated=\"true\" valueType=\"hex\" value=\"48 65 6c 6c 6f\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.ascii, str.stringType);
			Assert.AreEqual(Variant.VariantType.String, str.DefaultValue.GetVariantType());
			Assert.AreEqual("Helloaaaa\0", (string)str.DefaultValue);
		}

		[Test]
		public void StringTooLong()
		{
			string xml = "<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"ascii\" length=\"4\" value=\"Hello\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();

			Assert.Throws<PeachException>(delegate()
			{
				parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			});
		}

		[Test]
		public void HexStringTooLong()
		{
			string xml = "<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String name=\"TheString\" type=\"ascii\" length=\"4\" valueType=\"hex\" value=\"48 65 6c 6c 6f\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			
			Assert.Throws<PeachException>(delegate() {
				parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			});
		}

		[Test]
		public void NullTerm()
		{
			string xml = "<Peach>\n" +
				"	<DataModel name=\"TheDataModel\">" +
				"		<String nullTerminated=\"true\" value=\"Hello World\"/>" +
				"	</DataModel>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str = dom.dataModels[0][0] as Peach.Core.Dom.String;

			Assert.AreNotEqual(null, str);
			Assert.AreEqual(Peach.Core.Dom.StringType.utf8, str.stringType);
			Assert.AreEqual(Variant.VariantType.String, str.DefaultValue.GetVariantType());
			Assert.AreEqual("Hello World", (string)str.DefaultValue);
			Assert.AreEqual(Encoding.ASCII.GetBytes("Hello World\0"), str.Value.ToArray());
		}

		[Test]
		public void EscapeChars()
		{
			string xml = @"
<Peach>
	<DataModel name='DM1'>
		<String value='1\\t2\\r\\n'/>
		<String value='1\t2\r\n'/>
		<String value='1	2
'/>
		<String value='1	2
\t
\\n'/>

	</DataModel>
</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			Peach.Core.Dom.String str1 = dom.dataModels[0][0] as Peach.Core.Dom.String;
			Peach.Core.Dom.String str2 = dom.dataModels[0][1] as Peach.Core.Dom.String;
			Peach.Core.Dom.String str3 = dom.dataModels[0][2] as Peach.Core.Dom.String;
			Peach.Core.Dom.String str4 = dom.dataModels[0][3] as Peach.Core.Dom.String;

			Assert.AreEqual("1\\t2\\r\\n", (string)str1.DefaultValue);
			Assert.AreEqual("1\t2\r\n", (string)str2.DefaultValue);
			Assert.AreEqual("1\t2\r\n", (string)str3.DefaultValue);
			Assert.AreEqual("1\t2\r\n\t\r\n\\n", (string)str4.DefaultValue);
		}
	}
}

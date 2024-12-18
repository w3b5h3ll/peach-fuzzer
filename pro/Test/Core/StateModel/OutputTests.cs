﻿using System.Collections.Generic;
using System.IO;
using NLog;
using NUnit.Framework;
using Peach.Core;
using Peach.Core.Analyzers;
using Peach.Core.Test;
using Peach.Pro.Core.Publishers;

namespace Peach.Pro.Test.Core.StateModel
{
	class MemoryStreamPublisher : Peach.Core.Publishers.StreamPublisher
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
		protected override NLog.Logger Logger { get { return logger; } }

		public MemoryStreamPublisher(MemoryStream stream)
			: base(new Dictionary<string, Variant>())
		{
			Name = "Pub";
			this.stream = stream;
		}
	}

	[TestFixture]
	[Quick]
	[Peach]
	class OutputTests : DataModelCollector
	{
		[Test]
		public void Test1()
		{
			string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
				"<Peach>" +
				"   <DataModel name=\"TheDataModel1\">" +
				"       <String value=\"Hello World!\"/>" +
				"   </DataModel>" +

				"   <StateModel name=\"TheStateModel\" initialState=\"InitialState\">" +
				"       <State name=\"InitialState\">" +
				"           <Action name=\"Action1\" type=\"output\">" +
				"               <DataModel ref=\"TheDataModel1\"/>" +
				"           </Action>" +
				"       </State>" +
				"   </StateModel>" +

				"   <Test name=\"Default\">" +
				"       <StateModel ref=\"TheStateModel\"/>" +
				"       <Publisher class=\"Null\"/>" +
				"       <Strategy class=\"RandomDeterministic\"/>" +
				"   </Test>" +
				"</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));

			MemoryStream stream = new MemoryStream();
			dom.tests[0].publishers[0] = new MemoryStreamPublisher(stream);

			RunConfiguration config = new RunConfiguration();
			config.singleIteration = true;

			Engine e = new Engine(this);
			e.startFuzzing(dom, config);

			byte [] buff = new byte[stream.Length];

			stream.Position = 0;
			stream.Read(buff, 0, buff.Length);

			Assert.AreEqual(ASCIIEncoding.ASCII.GetBytes("Hello World!"), buff);
		}

		[Test]
		public void RecursiveStates()
		{
			// When using recursive states, ensure the data model is reset
			// so that mutations from the previous actions don't carry
			// over to subsequent runs.  When slrup is used to set a value
			// in a different state, that value will be cleared when the state
			// is re-entered.

			string xml = @"
<Peach>
	<DataModel name='Foo'>
		<String name='str1' value='Foo Data Model' mutable='false'/>
	</DataModel>

	<DataModel name='DM'>
		<Number name='num' size='8' mutable='false'>
			<Fixup class='SequenceIncrementFixup'>
				<Param name='Offset' value='0'/>
			</Fixup>
		</Number>
		<String name='str1' value='Hello'/>
		<String name='str2' value='World'/>
	</DataModel>

	<StateModel name='SM' initialState='Initial'>
		<State name='Initial'>
			<Action type='output'>
				<DataModel ref='Foo'/>
			</Action>
			<Action type='slurp' valueXpath='//Foo/str1' setXpath='//DM/str2'/>
			<Action type='changeState' ref='Send'/>
		</State>

		<State name='Send'>
			<Action type='output'>
				<DataModel ref='DM'/>
			</Action>
			<Action type='changeState' ref='Send' when='int(state.actions[0].dataModel[&quot;num&quot;].InternalValue) &lt; 5'/>
		</State>

	</StateModel>

	<Test name='Default'>
		<StateModel ref='SM'/>
		<Publisher class='Null'/>
		<Strategy class='Random'>
			<Param name='MaxFieldsToMutate' value='1'/>
		</Strategy>
	</Test>
</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			dom.tests[0].includedMutators = new List<string>();
			dom.tests[0].includedMutators.Add("StringStatic");

			RunConfiguration config = new RunConfiguration();
			config.range = true;
			config.rangeStart = 1;
			config.rangeStop = 10;

			Engine e = new Engine(this);
			e.startFuzzing(dom, config);


			Assert.AreEqual(66, dataModels.Count);

			for (int i = 1; i < 6; ++i)
			{
				Assert.AreEqual(i, (int)dataModels[i][0].InternalValue);
				Assert.AreEqual("Hello", (string)dataModels[i][1].InternalValue);
				if (i == 1)
					Assert.AreEqual("Foo Data Model", (string)dataModels[i][2].InternalValue);
				else
					Assert.AreEqual("World", (string)dataModels[i][2].InternalValue);
			}

			for (int i = 6; i < dataModels.Count; i += 6)
			{
				int count = 0;
				for (int j = 1; j < 6; ++j)
				{
					Assert.AreEqual(j, (int)dataModels[i + j][0].InternalValue);

					string str1 = (string)dataModels[i + j][1].InternalValue;
					string str2 = (string)dataModels[i + j][2].InternalValue;

					string exp = j == 1 ? "Foo Data Model" : "World";

					if (str1 != "Hello")
						++count;
					if (str2 != exp)
						++count;
				}
				Assert.AreEqual(1, count);
			}
		}

		[Test]
		public void RecursiveStates2()
		{
			// When using recursive states, ensure the data model is reset
			// so that mutations from the previous actions don't carry
			// over to subsequent runs.  When slrup is used to set a value
			// in the same state, that value should be maintained.

			string xml = @"
<Peach>
	<DataModel name='Foo'>
		<String name='str1' value='Foo Data Model' mutable='false'/>
	</DataModel>

	<DataModel name='DM'>
		<Number name='num' size='8' mutable='false'>
			<Fixup class='SequenceIncrementFixup'>
				<Param name='Offset' value='0'/>
			</Fixup>
		</Number>
		<String name='str1' value='Hello'/>
		<String name='str2' value='World'/>
	</DataModel>

	<StateModel name='SM' initialState='Send'>
		<State name='Send'>
			<Action type='output'>
				<DataModel ref='Foo'/>
			</Action>
			<Action type='slurp' valueXpath='//Foo/str1' setXpath='//DM/str2'/>
			<Action type='output'>
				<DataModel ref='DM'/>
			</Action>
			<Action type='changeState' ref='Send' when='int(state.actions[2].dataModel[&quot;num&quot;].InternalValue) &lt; 5'/>
		</State>

	</StateModel>

	<Test name='Default'>
		<StateModel ref='SM'/>
		<Publisher class='Null'/>
		<Strategy class='Random'>
			<Param name='MaxFieldsToMutate' value='1'/>
		</Strategy>
	</Test>
</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));
			dom.tests[0].includedMutators = new List<string>();
			dom.tests[0].includedMutators.Add("StringStatic");

			RunConfiguration config = new RunConfiguration();
			config.range = true;
			config.rangeStart = 1;
			config.rangeStop = 10;

			Engine e = new Engine(this);
			e.startFuzzing(dom, config);

			Assert.AreEqual(110, dataModels.Count);

			for (int i = 1; i < 10; i+=2)
			{
				Assert.AreEqual((i+1)/2, (int)dataModels[i][0].InternalValue);
				Assert.AreEqual("Hello", (string)dataModels[i][1].InternalValue);
				Assert.AreEqual("Foo Data Model", (string)dataModels[i][2].InternalValue);
			}

			for (int i = 10; i < dataModels.Count; i += 10)
			{
				int count = 0;
				for (int j = 1; j < 6; ++j)
				{
					Assert.AreEqual(j, (int)dataModels[i + (2 * j - 1)][0].InternalValue);

					string str1 = (string)dataModels[i + (2 * j - 1)][1].InternalValue;
					string str2 = (string)dataModels[i + (2 * j - 1)][2].InternalValue;

					if (str1 != "Hello")
						++count;
					if (str2 != "Foo Data Model")
						++count;

					Assert.IsTrue(true);
				}
				Assert.AreEqual(1, count);
			}

		}

		[Test]
		public void NonMutableElements()
		{
			string xml = @"
<Peach>
	<DataModel name='DM'>
		<Number name='num1' occurs='1' size='8' mutable='false'/>
		<Number name='num2' occurs='1' size='8' mutable='false'/>
		<Number name='num3' occurs='1' size='16' mutable='true'/>
		<Number name='num4' occurs='1' size='32' mutable='false'/>
	</DataModel>

	<StateModel name='SM' initialState='Send'>
		<State name='Send'>
			<Action type='output'>
				<DataModel ref='DM'/>
			</Action>
		</State>
	</StateModel>

	<Test name='Default'>
		<StateModel ref='SM'/>
		<Publisher class='Null'/>
		<Strategy class='Random'/>
	</Test>
</Peach>";

			PitParser parser = new PitParser();
			Peach.Core.Dom.Dom dom = parser.asParser(null, new MemoryStream(ASCIIEncoding.ASCII.GetBytes(xml)));

			RunConfiguration config = new RunConfiguration();
			config.range = true;
			config.rangeStart = 1;
			config.rangeStop = 10;

			Engine e = new Engine(this);
			e.startFuzzing(dom, config);

			foreach (var item in allStrategies)
				Assert.True(item.EndsWith(".num3") || item.EndsWith(".num3_0"));
		}

	}
}

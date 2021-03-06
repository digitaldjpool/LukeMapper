﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Documents;

namespace LukeMapper
{
    public class ExampleMethods
    {
        public static Document MapperFunction(Poco obj)
        {
            var doc = new Document();

            //doc.Add(new Field("PropInt", obj.PropInt.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("PropNullInt", obj.PropNullInt.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("PropString", obj.PropString, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("PropDt", LukeMapper.ToDateString(obj.PropDt), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            //doc.Add(new Field("Int", obj.Int.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("NullInt", obj.NullInt.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("String", obj.String, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("Dt", LukeMapper.ToDateString(obj.Dt), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            //doc.Add(new Field("NullDt", obj.NullDt.HasValue ? LukeMapper.ToDateString(obj.NullDt.Value) : "", Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("PropNullDt", obj.PropNullDt.HasValue ? LukeMapper.ToDateString(obj.PropNullDt.Value) : "", Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            //doc.Add(new Field("Char", obj.Char.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("PropChar", obj.PropChar.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            
            //doc.Add(new Field("NullChar", obj.NullChar.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //doc.Add(new Field("PropNullChar", obj.PropNullChar.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            doc.Add(new Field("Bl", obj.Bl.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            doc.Add(new Field("PropBl", obj.PropBl.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            doc.Add(new Field("NullBl", obj.NullBl.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            doc.Add(new Field("PropNullBl", obj.PropNullBl.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            return doc;
        }

        public static Document CustomMapperFunction(TestCustomSerializerClass obj)
        {
            var doc = new Document();

            doc.Add(new Field("CustomList", TestCustomSerializerClass.CustomListToString(obj.PropStringList), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            return doc;
        }

        public static TestCustomSerializerClass CustomDeserializeFunction(Document doc)
        {
            var obj = new TestCustomSerializerClass();

            obj.PropStringList = TestCustomSerializerClass.StringToCustomList(doc.Get("CustomList"));

            return obj;
        }

        public static TestCustomSerializerClass CustomMapperFunction2(Document doc)
        {
            var obj = new TestCustomSerializerClass();

            obj.PropStringList = doc.Get("PropStringList").Split(new[] { "," }, StringSplitOptions.None).ToList();

            obj.StringList = doc.Get("StringList").Split(new[] { "," }, StringSplitOptions.None).ToList();

            obj.IntList = doc.Get("IntList").Split(new[] {","}, StringSplitOptions.None)
                .Select(int.Parse)
                .ToList();

            obj.PropIntList = doc.Get("PropIntList").Split(new[] { "," }, StringSplitOptions.None)
                .Select(int.Parse)
                .ToList();

            return obj;
        }

        public static Document CustomMapperFunction3(TestCustomSerializerClass obj)
        {
            var doc = new Document();

            doc.Add(new Field("PropStringList", string.Join(",", obj.PropStringList), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            doc.Add(new Field("StringList", string.Join(",", obj.StringList), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            doc.Add(new Field("IntList", string.Join(",", obj.IntList), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            doc.Add(new Field("PropIntList", string.Join(",", obj.PropIntList), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            return doc;
        }

    }

    public class TestCustomSerializerClass
    {
        public int Id { get; set; }

        public List<string> StringList; 
        public List<string> PropStringList { get; set; }

        public List<int> IntList;
        public List<int> PropIntList { get; set; }
            
        [LukeSerializer("CustomList")]
        public static string CustomListToString(List<string> list)
        {
            return "";
        }
        [LukeDeserializer("CustomList")]
        public static List<string> StringToCustomList(string serialized)
        {
            return new List<string>();
        }
    }

    public class Poco
    {
        public int Int;
        public int PropInt { get; set; }

        public int? NullInt;
        public int? PropNullInt { get; set; }

        public string String;
        public string PropString { get; set; }

        public DateTime Dt;
        public DateTime PropDt { get; set; }

        public DateTime? NullDt;
        public DateTime? PropNullDt { get; set; }

        public char Char;
        public char PropChar { get; set; }

        public char? NullChar;
        public char? PropNullChar { get; set; }

        public bool Bl;
        public bool PropBl { get; set; }

        public bool? NullBl;
        public bool? PropNullBl { get; set; }

    }
}

﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZenCoding.Test
{
    [TestClass]
    public class PlaceHold
    {
        private ZenCoding.Parser _parser;

        [TestInitialize]
        public void Initialize()
        {
            _parser = new ZenCoding.Parser();
        }

        [TestMethod]
        public void PlaceHoldBasic()
        {
            string result = _parser.Parse("place", ZenType.HTML);
            string expected = "<img src=\"http://place-hold.it/30x30/\" alt=\"\" />";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PlaceHoldFormat()
        {
            string result = _parser.Parse("place-30-png", ZenType.HTML);
            string expected = "<img src=\"http://place-hold.it/30x30/png/\" alt=\"\" />";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PlaceHoldText()
        {
            string result = _parser.Parse("place-120x1879-jpeg-t=Some%20Random%20Text=", ZenType.HTML);
            string expected = "http://place-hold.it/120x1879/jpeg&text=Some%20Random%20Text="; // Text is: "Some Random Text="

            StringAssert.Contains(result, expected);
        }

        [TestMethod]
        public void PlaceHoldColors()
        {
            string result = _parser.Parse("place-150x240-jpeg-EEEDDD-888222-t=This%20is%20some%20text!", ZenType.HTML);

            string expected = "http://place-hold.it/150x240/jpeg/EEEDDD/888222&text=This%20is%20some%20text!";

            StringAssert.Contains(result, expected);
        }

        [TestMethod]
        public void PlaceHoldOverflowedDimensions()
        {
            string result = _parser.Parse("place-20000x3599-t=SomeRandomText", ZenType.HTML);
            string expected = "<img src=\"http://place-hold.it/3560x678&text=SomeRandomText\" alt=\"\" />"; // the allowed bound is 0-1920

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PlaceHoldWithAttributes()
        {
            string result = _parser.Parse("place[alt=\"tag's here\" title=\"picture title\" data-foo=\"bar\"]", ZenType.HTML);
            string expected = "<img src=\"http://place-hold.it/30x30/\" alt=\"tag's here\" title=\"picture title\" data-foo=\"bar\" />";

            Assert.AreEqual(expected, result);
        }

    }
}

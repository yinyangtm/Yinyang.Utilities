using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yinyang.Utilities.Test
{

    [TestClass]
    public class ExtensionStringTests
    {
        [TestMethod]
        public void LengthEqualTest()
        {
            var str = "12345";
            Assert.IsFalse(str.LengthEqual(4));
            Assert.IsTrue(str.LengthEqual(5));
            Assert.IsFalse(str.LengthEqual(6));

            str = "１２３４５";
            Assert.IsFalse(str.LengthEqual(4));
            Assert.IsTrue(str.LengthEqual(5));
            Assert.IsFalse(str.LengthEqual(6));

            str = "あいうえお";
            Assert.IsFalse(str.LengthEqual(4));
            Assert.IsTrue(str.LengthEqual(5));
            Assert.IsFalse(str.LengthEqual(6));

            str = string.Empty;
            Assert.IsFalse(str.LengthEqual(-1));
            Assert.IsTrue(str.LengthEqual(0));
            Assert.IsFalse(str.LengthEqual(1));

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.LengthEqual(0));

        }

        [TestMethod]
        public void LengthUnderTest()
        {
            var str = "12345";
            Assert.IsFalse(str.LengthUnder(4));
            Assert.IsFalse(str.LengthUnder(5));
            Assert.IsTrue(str.LengthUnder(6));

            str = "１２３４５";
            Assert.IsFalse(str.LengthUnder(4));
            Assert.IsFalse(str.LengthUnder(5));
            Assert.IsTrue(str.LengthUnder(6));

            str = "あいうえお";
            Assert.IsFalse(str.LengthUnder(4));
            Assert.IsFalse(str.LengthUnder(5));
            Assert.IsTrue(str.LengthUnder(6));

            str = string.Empty;
            Assert.IsFalse(str.LengthUnder(-1));
            Assert.IsFalse(str.LengthUnder(0));
            Assert.IsTrue(str.LengthUnder(1));

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.LengthUnder(0));

        }

        [TestMethod]
        public void LengthOverTest()
        {
            var str = "12345";
            Assert.IsTrue(str.LengthOver(4));
            Assert.IsFalse(str.LengthOver(5));
            Assert.IsFalse(str.LengthOver(6));

            str = "１２３４５";
            Assert.IsTrue(str.LengthOver(4));
            Assert.IsFalse(str.LengthOver(5));
            Assert.IsFalse(str.LengthOver(6));

            str = "あいうえお";
            Assert.IsTrue(str.LengthOver(4));
            Assert.IsFalse(str.LengthOver(5));
            Assert.IsFalse(str.LengthOver(6));

            str = string.Empty;
            Assert.IsTrue(str.LengthOver(-1));
            Assert.IsFalse(str.LengthOver(0));
            Assert.IsFalse(str.LengthOver(1));

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.LengthOver(0));

        }

        [TestMethod]
        public void LengthRangeTest()
        {
            var str = "12345";
            Assert.IsTrue(str.LengthRange(1, 6));
            Assert.IsTrue(str.LengthRange(1, 5));
            Assert.IsFalse(str.LengthRange(1, 4));
            Assert.IsTrue(str.LengthRange(4, 10));
            Assert.IsTrue(str.LengthRange(5, 10));
            Assert.IsFalse(str.LengthRange(6, 10));

            str = "１２３４５";
            Assert.IsTrue(str.LengthRange(1, 6));
            Assert.IsTrue(str.LengthRange(1, 5));
            Assert.IsFalse(str.LengthRange(1, 4));
            Assert.IsTrue(str.LengthRange(4, 10));
            Assert.IsTrue(str.LengthRange(5, 10));
            Assert.IsFalse(str.LengthRange(6, 10));

            str = "あいうえお";
            Assert.IsTrue(str.LengthRange(1, 6));
            Assert.IsTrue(str.LengthRange(1, 5));
            Assert.IsFalse(str.LengthRange(1, 4));
            Assert.IsTrue(str.LengthRange(4, 10));
            Assert.IsTrue(str.LengthRange(5, 10));
            Assert.IsFalse(str.LengthRange(6, 10));

            str = string.Empty;
            Assert.IsTrue(str.LengthRange(-10, 1));
            Assert.IsTrue(str.LengthRange(-10, 0));
            Assert.IsFalse(str.LengthRange(-10, -1));
            Assert.IsTrue(str.LengthRange(-1, 10));
            Assert.IsTrue(str.LengthRange(0, 10));
            Assert.IsFalse(str.LengthRange(1, 10));

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.LengthRange(-10, 1));
        }

        [TestMethod]
        public void IsNumberTest()
        {
            var str = "12345";
            Assert.IsTrue(str.IsNumber());

            str = "12345-";
            Assert.IsFalse(str.IsNumber());

            str = "12a345";
            Assert.IsFalse(str.IsNumber());

            str = "１２３４５";
            Assert.IsFalse(str.IsNumber());

            str = "12あ345";
            Assert.IsFalse(str.IsNumber());

            str = string.Empty;
            Assert.IsFalse(str.IsNumber());

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.IsNumber());
        }

        [TestMethod]
        public void IsHalfWidthAlphanumericTest()
        {
            var str = "abc123";
            Assert.IsTrue(str.IsHalfWidthAlphanumeric());

            str = "abc123-";
            Assert.IsFalse(str.IsHalfWidthAlphanumeric());

            str = "abc-123";
            Assert.IsFalse(str.IsHalfWidthAlphanumeric());

            str = "abc０123";
            Assert.IsFalse(str.IsHalfWidthAlphanumeric());

            str = "abcあ123";
            Assert.IsFalse(str.IsHalfWidthAlphanumeric());

            str = string.Empty;
            Assert.IsFalse(str.IsHalfWidthAlphanumeric());

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.IsHalfWidthAlphanumeric());
        }

        [TestMethod]
        public void IsHalfWidthAlphanumericSymbolTest()
        {
            var str = "abc123!\"#$%&'()=~|`*+><_?>}";
            Assert.IsTrue(str.IsHalfWidthAlphanumericSymbol());

            str = "ＡＢＣ１２３";
            Assert.IsFalse(str.IsHalfWidthAlphanumericSymbol());

            str = "abc-123$%亜&'()=~|`*";
            Assert.IsFalse(str.IsHalfWidthAlphanumericSymbol());

            str = "abc０123$%&'()=~|`*";
            Assert.IsFalse(str.IsHalfWidthAlphanumericSymbol());

            str = "abcあ123$%&'()=~|`*";
            Assert.IsFalse(str.IsHalfWidthAlphanumericSymbol());

            str = string.Empty;
            Assert.IsFalse(str.IsHalfWidthAlphanumericSymbol());

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.IsHalfWidthAlphanumericSymbol());
        }

        [TestMethod]
        public void IsJapanPostalCodeTest()
        {
            var str = "123-4567";
            Assert.IsTrue(str.IsJapanPostalCode());

            str = "1-234567";
            Assert.IsFalse(str.IsJapanPostalCode());

            str = "１２３－４５６";
            Assert.IsFalse(str.IsJapanPostalCode());

            str = "1234567";
            Assert.IsFalse(str.IsJapanPostalCode());

            str = string.Empty;
            Assert.IsFalse(str.IsJapanPostalCode());

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.IsJapanPostalCode());
        }

        [TestMethod]
        public void IsRegexMatchTest()
        {
            var str = "123-4567";
            Assert.IsTrue(str.IsRegexMatch(@"\d"));
            Assert.IsTrue(str.IsRegexMatch(@"[0-9]"));
            Assert.IsTrue(str.IsRegexMatch(@"[!-~]"));
            Assert.IsTrue(str.IsRegexMatch(@"[!-/:-@¥[-`{-~]"));
            Assert.IsFalse(str.IsRegexMatch(@"[あ-ん]"));

            str = "あかさたな";
            Assert.IsFalse(str.IsRegexMatch(@"\d"));
            Assert.IsFalse(str.IsRegexMatch(@"[0-9]"));
            Assert.IsFalse(str.IsRegexMatch(@"[!-~]"));
            Assert.IsFalse(str.IsRegexMatch(@"[!-/:-@¥[-`{-~]"));
            Assert.IsTrue(str.IsRegexMatch(@"[あ-ん]"));

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.IsRegexMatch(@"^[0-9]{3}-[0-9]{4}$"));
        }

        [TestMethod]
        public void IsRegexIsMatchTest()
        {
            var str = "123-4567";
            Assert.IsTrue(str.IsRegexIsMatch(@"^[0-9]{3}-[0-9]{4}$"));

            str = "1-234567";
            Assert.IsFalse(str.IsRegexIsMatch(@"^[0-9]{3}-[0-9]{4}$"));

            str = "１２３－４５６";
            Assert.IsFalse(str.IsRegexIsMatch(@"^[0-9]{3}-[0-9]{4}$"));

            str = "1234567";
            Assert.IsFalse(str.IsRegexIsMatch(@"^[0-9]{3}-[0-9]{4}$"));

            str = string.Empty;
            Assert.IsFalse(str.IsRegexIsMatch(@"^[0-9]{3}-[0-9]{4}$"));

            str = null;
            Assert.ThrowsExactly<ArgumentNullException>(() => str.IsRegexIsMatch(@"^[0-9]{3}-[0-9]{4}$"));
        }

        [TestMethod]
        public void NewLines()
        {
            var testtext = "Hello\r\nWorld!\r\n";
            var removed = testtext.RemoveLineBreaks();
            Assert.AreEqual("HelloWorld!", removed);
            var linuxText = testtext.ToLinuxLineEndings();
            Assert.AreEqual("Hello\nWorld!\n", linuxText);
            var windowsText = linuxText.ToWindowsLineEndings();
            Assert.AreEqual(testtext, windowsText);
        }
    }
}

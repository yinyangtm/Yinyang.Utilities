using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yinyang.Utilities.Test
{
    [TestClass]
    public class StrConvHelperTests
    {
        /// <summary>
        ///     半角英数字＋スペース → 全角変換
        /// </summary>
        [TestMethod]
        public void TestToWide_AsciiCharacters()
        {
            var input = "abc 123";
            var expected = "ａｂｃ　１２３";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToWide);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     半角カナ＋濁点結合 → 全角カナ（濁音）
        /// </summary>
        [TestMethod]
        public void TestToWide_KanaCharacters_WithDakuten()
        {
            var input = "ｶﾞｷﾞｸﾞｹﾞｺﾞ";
            var expected = "ガギグゲゴ";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToWide);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     半角カナ＋濁点合成なし → カ＋濁点
        /// </summary>
        [TestMethod]
        public void TestToWide_KanaCharacters_WithoutDakuten()
        {
            var input = "ｶﾞｷﾞｸﾞｹﾞｺﾞ";
            var expected = "カ゛キ゛ク゛ケ゛コ゛";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToWide, false);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     小書き文字・記号・長音「ｰ」のテスト
        /// </summary>
        [TestMethod]
        public void TestToWide_SpecialKana()
        {
            var input = "ｧｨｩｪｫｬｭｮｯ｡｢｣､･ｰ";
            var expected = "ァィゥェォャュョッ。「」、・ー";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToWide);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     半濁点対応（パ行）
        /// </summary>
        [TestMethod]
        public void TestToWide_HanDakutenCharacters()
        {
            var input = "ﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ";
            var expected = "パピプペポ";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToWide);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     半濁点対応（合成なし）
        /// </summary>
        [TestMethod]
        public void TestToWide_HanDakutenCharacters_NoMerge()
        {
            var input = "ﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ";
            var expected = "ハ゜ヒ゜フ゜ヘ゜ホ゜";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToWide, false);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     半角スペースが全角スペースになるか
        /// </summary>
        [TestMethod]
        public void TestToWide_SpaceHandling()
        {
            var input = "abc def";
            var expected = "ａｂｃ　ｄｅｆ";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToWide);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     全角英数字＋カタカナ → 半角変換
        /// </summary>
        [TestMethod]
        public void TestToNarrow_Normal()
        {
            var input = "ＡＢＣ１２３ アイウエオ";
            var expected = "ABC123 ｱｲｳｴｵ";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToNarrow);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     全角スペースが半角スペースになるか
        /// </summary>
        [TestMethod]
        public void TestToNarrow_SpaceHandling()
        {
            var input = "ＡＢＣ　１２３";
            var expected = "ABC 123";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToNarrow);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     カタカナ → ひらがな変換
        /// </summary>
        [TestMethod]
        public void TestToHiragana_Normal()
        {
            var input = "アイウエオ";
            var expected = "あいうえお";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToHiragana);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     ひらがな → カタカナ変換
        /// </summary>
        [TestMethod]
        public void TestToKatakana_Normal()
        {
            var input = "あいうえお";
            var expected = "アイウエオ";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToKatakana);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     半角カタカナ＋漢字＋半角数字＋スペース → カタカナと数字とスペースだけ全角、漢字そのまま
        /// </summary>
        [TestMethod]
        public void TestToWide_WithKanji()
        {
            var input = "ｱｲｳｴｵ 日本語 123";
            var expected = "アイウエオ　日本語　１２３";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToWide);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     全角カタカナ＋漢字＋全角数字＋スペース → カタカナと数字とスペースだけ半角、漢字そのまま
        /// </summary>
        [TestMethod]
        public void TestToNarrow_WithKanji()
        {
            var input = "アイウエオ　日本語　１２３";
            var expected = "ｱｲｳｴｵ 日本語 123";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToNarrow);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     全角カタカナ＋漢字 → カタカナだけひらがな化、漢字そのまま
        /// </summary>
        [TestMethod]
        public void TestToHiragana_WithKanji()
        {
            var input = "アイウエオ 日本語";
            var expected = "あいうえお 日本語";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToHiragana);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     ひらがな＋漢字 → ひらがなだけカタカナ化、漢字そのまま
        /// </summary>
        [TestMethod]
        public void TestToKatakana_WithKanji()
        {
            var input = "あいうえお 日本語";
            var expected = "アイウエオ 日本語";
            var actual = StrConvHelper.StrConv(input, StrConvMode.ToKatakana);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     ｳﾞ系
        /// </summary>
        [TestMethod]
        public void TestToWide_SpecialDakuten()
        {
            Assert.AreEqual("ヴ", StrConvHelper.StrConv("ｳﾞ", StrConvMode.ToWide));
            Assert.AreEqual("ヴァ", StrConvHelper.StrConv("ｳﾞｧ", StrConvMode.ToWide));
            Assert.AreEqual("ヴィ", StrConvHelper.StrConv("ｳﾞｨ", StrConvMode.ToWide));
            Assert.AreEqual("ヴェ", StrConvHelper.StrConv("ｳﾞｪ", StrConvMode.ToWide));
            Assert.AreEqual("ヴォ", StrConvHelper.StrConv("ｳﾞｫ", StrConvMode.ToWide));
        }

        /// <summary>
        ///     想定外のモードで例外発生
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidStrConvMode_Throws()
        {
            _ = StrConvHelper.StrConv("test", (StrConvMode)999);
        }
    }
}

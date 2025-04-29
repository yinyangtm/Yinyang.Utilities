using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yinyang.Utilities
{
    /// <summary>
    ///     変換モード
    /// </summary>
    public enum StrConvMode
    {
        /// <summary>
        ///     半角→全角変換
        /// </summary>
        ToWide,

        /// <summary>
        ///     全角→半角変換
        /// </summary>
        ToNarrow,

        /// <summary>
        ///     カタカナ→ひらがな変換
        /// </summary>
        ToHiragana,

        /// <summary>
        ///     ひらがな→カタカナ変換
        /// </summary>
        ToKatakana
    }

    /// <summary>
    ///     文字列変換ヘルパー
    /// </summary>
    public static class StrConvHelper
    {
        /// <summary>
        ///     半角カタカナ→全角カタカナ変換マッピング
        /// </summary>
        private static readonly Dictionary<char, char> HankakuToZenkakuKana = new Dictionary<char, char>
        {
            // 小書き文字
            { 'ｧ', 'ァ' },
            { 'ｨ', 'ィ' },
            { 'ｩ', 'ゥ' },
            { 'ｪ', 'ェ' },
            { 'ｫ', 'ォ' },
            { 'ｬ', 'ャ' },
            { 'ｭ', 'ュ' },
            { 'ｮ', 'ョ' },
            { 'ｯ', 'ッ' },
            // 通常文字
            { 'ｱ', 'ア' },
            { 'ｲ', 'イ' },
            { 'ｳ', 'ウ' },
            { 'ｴ', 'エ' },
            { 'ｵ', 'オ' },
            { 'ｶ', 'カ' },
            { 'ｷ', 'キ' },
            { 'ｸ', 'ク' },
            { 'ｹ', 'ケ' },
            { 'ｺ', 'コ' },
            { 'ｻ', 'サ' },
            { 'ｼ', 'シ' },
            { 'ｽ', 'ス' },
            { 'ｾ', 'セ' },
            { 'ｿ', 'ソ' },
            { 'ﾀ', 'タ' },
            { 'ﾁ', 'チ' },
            { 'ﾂ', 'ツ' },
            { 'ﾃ', 'テ' },
            { 'ﾄ', 'ト' },
            { 'ﾅ', 'ナ' },
            { 'ﾆ', 'ニ' },
            { 'ﾇ', 'ヌ' },
            { 'ﾈ', 'ネ' },
            { 'ﾉ', 'ノ' },
            { 'ﾊ', 'ハ' },
            { 'ﾋ', 'ヒ' },
            { 'ﾌ', 'フ' },
            { 'ﾍ', 'ヘ' },
            { 'ﾎ', 'ホ' },
            { 'ﾏ', 'マ' },
            { 'ﾐ', 'ミ' },
            { 'ﾑ', 'ム' },
            { 'ﾒ', 'メ' },
            { 'ﾓ', 'モ' },
            { 'ﾔ', 'ヤ' },
            { 'ﾕ', 'ユ' },
            { 'ﾖ', 'ヨ' },
            { 'ﾗ', 'ラ' },
            { 'ﾘ', 'リ' },
            { 'ﾙ', 'ル' },
            { 'ﾚ', 'レ' },
            { 'ﾛ', 'ロ' },
            { 'ﾜ', 'ワ' },
            { 'ｦ', 'ヲ' },
            { 'ﾝ', 'ン' },
            { '｡', '。' },
            { '｢', '「' },
            { '｣', '」' },
            { '､', '、' },
            { '･', '・' },
            { 'ｰ', 'ー' }
        };

        /// <summary>
        ///     全角カタカナ→半角カタカナ変換マッピング
        /// </summary>
        private static readonly Dictionary<char, char> ZenkakuToHankakuKana = new Dictionary<char, char>();

        /// <summary>
        ///     濁点・半濁点結合変換マッピング
        /// </summary>
        private static readonly Dictionary<(char, char), char> DakutenMap = new Dictionary<(char, char), char>
        {
            { ('ｶ', 'ﾞ'), 'ガ' },
            { ('ｷ', 'ﾞ'), 'ギ' },
            { ('ｸ', 'ﾞ'), 'グ' },
            { ('ｹ', 'ﾞ'), 'ゲ' },
            { ('ｺ', 'ﾞ'), 'ゴ' },
            { ('ｻ', 'ﾞ'), 'ザ' },
            { ('ｼ', 'ﾞ'), 'ジ' },
            { ('ｽ', 'ﾞ'), 'ズ' },
            { ('ｾ', 'ﾞ'), 'ゼ' },
            { ('ｿ', 'ﾞ'), 'ゾ' },
            { ('ﾀ', 'ﾞ'), 'ダ' },
            { ('ﾁ', 'ﾞ'), 'ヂ' },
            { ('ﾂ', 'ﾞ'), 'ヅ' },
            { ('ﾃ', 'ﾞ'), 'デ' },
            { ('ﾄ', 'ﾞ'), 'ド' },
            { ('ﾊ', 'ﾞ'), 'バ' },
            { ('ﾋ', 'ﾞ'), 'ビ' },
            { ('ﾌ', 'ﾞ'), 'ブ' },
            { ('ﾍ', 'ﾞ'), 'ベ' },
            { ('ﾎ', 'ﾞ'), 'ボ' },
            { ('ﾊ', 'ﾟ'), 'パ' },
            { ('ﾋ', 'ﾟ'), 'ピ' },
            { ('ﾌ', 'ﾟ'), 'プ' },
            { ('ﾍ', 'ﾟ'), 'ペ' },
            { ('ﾎ', 'ﾟ'), 'ポ' }
        };

        /// <summary>
        ///     特殊な濁点・半濁点結合変換マッピング
        /// </summary>
        private static readonly Dictionary<string, string> SpecialDakutenMap = new Dictionary<string, string>
        {
            { "ｳﾞ", "ヴ" },
            { "ｳﾞｧ", "ヴァ" },
            { "ｳﾞｨ", "ヴィ" },
            { "ｳﾞｪ", "ヴェ" },
            { "ｳﾞｫ", "ヴォ" }
        };

        /// <summary>
        ///     初期化
        /// </summary>
        static StrConvHelper()
        {
            foreach (var pair in HankakuToZenkakuKana.Where(pair => !ZenkakuToHankakuKana.ContainsKey(pair.Value)))
            {
                ZenkakuToHankakuKana[pair.Value] = pair.Key;
            }
        }

        /// <summary>
        ///     文字列変換
        /// </summary>
        /// <param name="text">変換する文字列</param>
        /// <param name="mode">変換モード</param>
        /// <param name="combineDakuten">濁点/半濁点合成</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string StrConv(string text, StrConvMode mode, bool combineDakuten = true)
        {
            switch (mode)
            {
                case StrConvMode.ToWide:
                    return ToWide(text, combineDakuten);
                case StrConvMode.ToNarrow:
                    return ToNarrow(text);
                case StrConvMode.ToHiragana:
                    return ToHiragana(text);
                case StrConvMode.ToKatakana:
                    return ToKatakana(text);
                default:
                    throw new ArgumentException("Unknown conversion mode.");
            }
        }

        /// <summary>
        ///     半角→全角変換
        /// </summary>
        /// <param name="text"></param>
        /// <param name="combineDakuten"></param>
        /// <returns></returns>
        private static string ToWide(string text, bool combineDakuten)
        {
            var sb = new StringBuilder();
            var i = 0;
            while (i < text.Length)
            {
                var remaining = text.Length - i;

                var matchedSpecial = false;
                if (remaining >= 2)
                {
                    var maxCheckLength = Math.Min(3, remaining); // ｳﾞｧみたいに3文字あるかも
                    var segment = text.Substring(i, maxCheckLength);

                    foreach (var special in SpecialDakutenMap.Where(special => segment.StartsWith(special.Key)))
                    {
                        sb.Append(special.Value);
                        i += special.Key.Length;
                        matchedSpecial = true;
                        break;
                    }
                }

                if (matchedSpecial)
                {
                    continue;
                }

                var c = text[i];

                if (c == 0x20) // 半角スペース
                {
                    sb.Append('\u3000');
                }
                else if (c == 'ﾞ') // 半角濁点
                {
                    sb.Append('\u309B');
                }
                else if (c == 'ﾟ') // 半角半濁点
                {
                    sb.Append('\u309C');
                }
                else if (c >= 0x21 && c <= 0x7E)
                {
                    sb.Append((char)(c + 0xFEE0));
                }
                else if (HankakuToZenkakuKana.TryGetValue(c, out var zenkakuKana))
                {
                    if (combineDakuten && i + 1 < text.Length)
                    {
                        var next = text[i + 1];
                        if (next == 'ﾞ' || next == 'ﾟ')
                        {
                            if (DakutenMap.TryGetValue((c, next), out var combined))
                            {
                                sb.Append(combined);
                                i += 2;
                                continue;
                            }
                        }
                    }

                    sb.Append(zenkakuKana);
                }
                else
                {
                    sb.Append(c);
                }

                i++;
            }

            return sb.ToString();
        }

        /// <summary>
        ///     全角→半角変換
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ToNarrow(string text)
        {
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                if (c == '\u3000') // 全角スペース
                {
                    sb.Append('\u0020'); // 半角スペースに変換
                }
                else if (c >= 0xFF01 && c <= 0xFF5E)
                {
                    sb.Append((char)(c - 0xFEE0)); // 全角英数字→半角
                }
                else if (ZenkakuToHankakuKana.TryGetValue(c, out var hankakuKana))
                {
                    sb.Append(hankakuKana);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     カタカナ→ひらがな変換
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ToHiragana(string text)
        {
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                if (c >= 0x30A1 && c <= 0x30F6)
                {
                    sb.Append((char)(c - 0x60)); // カタカナ→ひらがな
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     ひらがな→カタカナ変換
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ToKatakana(string text)
        {
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                if (c >= 0x3041 && c <= 0x3096)
                {
                    sb.Append((char)(c + 0x60)); // ひらがな→カタカナ
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}

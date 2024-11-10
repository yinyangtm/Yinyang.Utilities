using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yinyang.Utilities
{
    /// <summary>
    ///     ExtensionString
    /// </summary>
    public static class ExtensionString
    {
        /// <summary>
        ///     Check if the lengths match.
        /// </summary>
        /// <remarks>文字列の長さが指定値を一致するか比較します。</remarks>
        /// <param name="str">string</param>
        /// <param name="length">length</param>
        /// <returns>result</returns>
        /// <exception cref="ArgumentNullException">string is null</exception>
        public static bool LengthEqual(this string str, int length)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return str.Length == length;
        }

        /// <summary>
        ///     Check if the length is under.
        /// </summary>
        /// <remarks>文字列の長さが指定値を超か比較します。</remarks>
        /// <param name="str">string</param>
        /// <param name="length">length</param>
        /// <returns>result</returns>
        /// <exception cref="ArgumentNullException">string is null</exception>
        public static bool LengthUnder(this string str, int length)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return str.Length < length;
        }

        /// <summary>
        ///     Check if the length is over.
        /// </summary>
        /// <remarks>文字列の長さが指定値を未満か比較します。</remarks>
        /// <param name="str">string</param>
        /// <param name="length">length</param>
        /// <returns>result</returns>
        /// <exception cref="ArgumentNullException">string is null</exception>
        public static bool LengthOver(this string str, int length)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return length < str.Length;
        }

        /// <summary>
        ///     Check if the length is range.
        /// </summary>
        /// <remarks>文字列の長さが指定値未満、指定値超か比較します。</remarks>
        /// <param name="str">string</param>
        /// <param name="minLength">min length</param>
        /// <param name="maxLength">max length</param>
        /// <returns>result</returns>
        /// <exception cref="ArgumentNullException">string is null</exception>
        public static bool LengthRange(this string str, int minLength, int maxLength)
        {
            if (maxLength <= minLength)
            {
                throw new ArgumentNullException();
            }

            return !(str.LengthUnder(minLength) || str.LengthOver(maxLength));
        }

        /// <summary>
        ///     is a number
        /// </summary>
        /// <remarks>文字列が数値であるか比較します。</remarks>
        /// <param name="str">string</param>
        /// <returns>result</returns>
        /// <exception cref="ArgumentNullException">string is null</exception>
        public static bool IsNumber(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return Regex.IsMatch(str, @"^[0-9]+$");
        }

        /// <summary>
        ///     is a half-width alphanumeric
        /// </summary>
        /// <remarks>文字列が半角英数字であるか比較します。</remarks>
        /// <param name="str">string</param>
        /// <returns>result</returns>
        public static bool IsHalfWidthAlphanumeric(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return Regex.IsMatch(str, @"^[0-9a-zA-Z]+$");
        }

        /// <summary>
        ///     is a half-width alphanumeric symbol
        /// </summary>
        /// <remarks>文字列が半角英数字記号であるか比較します。</remarks>
        /// <param name="str">string</param>
        /// <returns>result</returns>
        public static bool IsHalfWidthAlphanumericSymbol(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return Regex.IsMatch(str, @"^[!-~]+$");
        }

        /// <summary>
        ///     is a japan postalcode
        /// </summary>
        /// <remarks>文字列が日本の郵便番号形式であるか比較します。</remarks>
        /// <param name="str">string</param>
        /// <returns>result</returns>
        public static bool IsJapanPostalCode(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return Regex.IsMatch(str, @"^[0-9]{3}-[0-9]{4}$");
        }

        /// <summary>
        ///     is a regex match
        /// </summary>
        /// <remarks>指定した正規表現に一致する箇所が見つかるかどうか示します。</remarks>
        /// <param name="str">string</param>
        /// <param name="pattern">pattern</param>
        /// <returns>result</returns>
        public static bool IsRegexMatch(this string str, string pattern)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return Regex.Match(str, pattern, RegexOptions.ECMAScript).Success;
        }

        /// <summary>
        ///     is a regex is match
        /// </summary>
        /// <remarks>指定した正規表現に一致する箇所が見つかるかどうか示します。</remarks>
        /// <param name="str">string</param>
        /// <param name="pattern">pattern</param>
        /// <returns>result</returns>
        public static bool IsRegexIsMatch(this string str, string pattern)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        ///    Removes all line breaks (both \r and \n) from the given string.
        /// </summary>
        /// <remarks>文字列からすべての改行コード（\r および \n）を削除します。</remarks>
        /// <param name="str">The input string from which line breaks will be removed.</param>
        /// <returns>A new string without any line breaks.</returns>
        public static string RemoveLineBreaks(this string str)
        {
            return str.Replace("\r", "").Replace("\n", "");
        }

        /// <summary>
        /// Converts all Windows-style line endings (\r\n) to Linux-style line endings (\n) in the given string.
        /// </summary>
        /// <remarks>Windows形式の改行コード（\r\n）をLinux形式の改行コード（\n）に変換します。</remarks>
        /// <param name="str">The input string with Windows-style line endings.</param>
        /// <returns>A new string with all line endings converted to Linux-style (\n).</returns>
        public static string ToLinuxLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n");
        }

        /// <summary>
        /// Converts all Linux-style line endings (\n) to Windows-style line endings (\r\n) in the given string.
        /// </summary>
        /// <remarks>Linux形式の改行コード（\n）をWindows形式の改行コード（\r\n）に変換します。</remarks>
        /// <param name="str">The input string with Linux-style line endings.</param>
        /// <returns>A new string with all line endings converted to Windows-style (\r\n).</returns>
        public static string ToWindowsLineEndings(this string str)
        {
            return str.Replace("\n", "\r\n").Replace("\r\r\n", "\r\n");
        }

        /// <summary>
        /// Writes the string content to a specified file with the specified encoding, overwriting if the file already exists.
        /// </summary>
        /// <remarks>エンコードを指定して指定されたファイルパスに文字列を出力します。ファイルが存在する場合は上書きされ、存在しない場合は新しく作成されます。</remarks>
        /// <param name="str">The input string to be written to the file.</param>
        /// <param name="filePath">The path of the file where the content will be written.</param>
        /// <param name="encoding">The encoding to be used for writing the file.</param>
        public static void WriteToFile(this string str, string filePath, Encoding encoding)
        {
            File.WriteAllText(filePath, str, encoding);
        }

        /// <summary>
        /// Appends the string content to a specified file with the specified encoding, creating the file if it does not exist.
        /// </summary>
        /// <remarks>エンコードを指定して指定されたファイルパスに文字列を追加します。ファイルが存在しない場合は新しく作成されます。</remarks>
        /// <param name="str">The input string to be appended to the file.</param>
        /// <param name="filePath">The path of the file where the content will be appended.</param>
        /// <param name="encoding">The encoding to be used for appending to the file.</param>
        public static void AppendToFile(this string str, string filePath, Encoding encoding)
        {
            File.AppendAllText(filePath, str, encoding);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;
using nmecc;

namespace SFC_PAR
{
    public class SFC_PAR : ICodePlugin
    {
        private const string CODE_FORMAT = "[0-9a-fA-F]{6} [0-9a-fA-F]{2}";
        private string _error = "";

        private List<Tuple<nint, ulong>> _codes;

        /// <summary>
        /// Whether the address can be obtained
        /// アドレスを取得可能か
        /// Used to determine if the memory display corresponding to the specified code is possible
        /// 指定されたコードに対応するメモリ表示が可能かどうかの判定に使用される
        /// </summary>
        /// <returns></returns>
        public bool CanGetAddress() => true;

        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// Execute code
        /// コードを実行する
        /// Called repeatedly at a set cycle from when Start is called until Stop is called.
        /// Startが呼び出されてからStopが呼び出されるまでの間、設定された周期で繰り返し呼び出される
        /// </summary>
        /// <param name="readFunc">
        /// A function that reads a memory value by specifying an address and size
        /// アドレスとサイズを指定してメモリの値を読み込む関数
        /// </param>
        /// <param name="writeFunc">
        /// Function to write to memory by specifying address, size and value
        /// アドレスとサイズと値を指定してメモリに書き込む関数
        /// </param>
        /// <returns></returns>
        public async Task Execute(Func<nint, nint, BigInteger> readFunc, Action<nint, nint, BigInteger> writeFunc)
        {
            await Task.Run(() =>
            {
                foreach (var code in _codes)
                {
                    writeFunc(code.Item1, 1, code.Item2);
                }
            });
        }

        /// <summary>
        /// Get the address corresponding to the specified code
        /// 指定されたコードに対応するアドレスを取得する
        /// Called when performing a memory display corresponding to the specified code
        /// 指定されたコードに対応してメモリ表示を実行する際に呼び出される
        /// </summary>
        /// <param name="code">
        /// The text entered in the code field
        /// コード欄に入力されているテキスト
        /// </param>
        /// <param name="encrypted">
        /// Whether "code" is encrypted
        /// codeが暗号化されているか
        /// </param>
        /// <returns>
        /// Address to display in memory
        /// メモリ表示するアドレス
        /// </returns>
        public nint GetAddress(string code, bool encrypted = false)
        {
            return Convert(code).FirstOrDefault().Item1;
        }

        /// <summary>
        /// Get the string of the code that sets the value to the specified address
        /// 指定されたアドレスに値を設定するコードの文字列を取得する
        /// </summary>
        /// <param name="memory">
        /// List of (address, size, value)
        /// (アドレス,サイズ,値)のリスト
        /// </param>
        /// <param name="encrypted">
        /// Whether "code" is encrypted
        /// codeが暗号化されているか
        /// </param>
        public string GetCodeString(IEnumerable<Tuple<nint, int, ulong>> memory, bool encrypted = false)
        {
            return memory
                .SelectMany(data => Enumerable.Range(0, data.Item2).Select(index => new Tuple<nint, ulong>(data.Item1 + index, (data.Item3 >> (8 * index)) & 0xff)))
                .Select(data => $"{data.Item1:X6} {data.Item2:X2}")
                .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
        }

        /// <summary>
        /// Get the error
        /// エラーを取得する
        /// </summary>
        /// <returns>
        /// Error string
        /// エラー文字列
        /// </returns>
        public string GetError() => _error;

        /// <summary>
        /// Get the display name at the time of selection
        /// 選択時の表示名を取得する
        /// </summary>
        /// <returns>
        /// display name
        /// 表示名
        /// </returns>
        public string GetName() => "SFC PAR";

        /// <summary>
        /// Start executing code
        /// コードの実行を開始する
        /// </summary>
        /// <param name="code">
        /// The text entered in the code field
        /// コード欄に入力されているテキスト
        /// </param>
        /// <param name="encrypted">
        /// Whether "code" is encrypted
        /// codeが暗号化されているか
        /// </param>
        public void Start(string code, bool encrypted = false)
        {
            _codes = Convert(code);
        }

        /// <summary>
        /// Stop code execution
        /// コードの実行を停止する
        /// </summary>
        public void Stop()
        {
            _codes?.Clear();
            _codes = null;
        }

        private List<Tuple<nint, ulong>> Convert(string code)
        {
            return code
                .Split(Environment.NewLine)
                .Where(line => Regex.IsMatch(line, CODE_FORMAT))
                .Select(line => line.Split(" "))
                .Select(lineSplit => new Tuple<nint, ulong>(
                    nint.TryParse(lineSplit.FirstOrDefault() ?? "0", NumberStyles.HexNumber, null, out nint address) ? address : 0,
                    ulong.TryParse(lineSplit.Skip(1).FirstOrDefault() ?? "0", NumberStyles.HexNumber, null, out ulong value) ? value : 0
                    )
                ).ToList();
        }

        /// <summary>
        /// Whether it can be encrypted
        /// 暗号可能か
        /// </summary>
        /// <returns></returns>
        public bool CanEncrypt() => false;

        /// <summary>
        /// Encrypt the specified code
        /// 指定されたコードを暗号化する
        /// </summary>
        /// <param name="code">
        /// The text entered in the code field (encrypted)
        /// コード欄に入力されているテキスト(暗号済み)
        /// </param>
        /// <returns>
        /// Encrypted code
        /// 暗号化されたコード
        /// </returns>
        public string Encrypt(string code) => code;

        /// <summary>
        /// Decrypt the specified code
        /// 指定されたコードを暗号化する
        /// </summary>
        /// <param name="code">
        /// The text entered in the code field (decrypted)
        /// コード欄に入力されているテキスト(復号済み)
        /// </param>
        /// <returns>
        /// Decrypted code
        /// 復号化されたコード
        /// </returns>
        public string Decrypt(string code) => code;
    }
}

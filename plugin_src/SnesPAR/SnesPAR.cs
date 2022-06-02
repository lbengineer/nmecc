using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;
using nmecc;

namespace SnesPAR
{
    public class SnesPAR : ICodePlugin
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
        /// <param name="readFunc">アドレスとサイズを指定してメモリの値を読み込む関数</param>
        /// <param name="writeFunc">アドレスとサイズと値を指定してメモリに書き込む関数</param>
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
        /// <returns>
        /// Address to display in memory
        /// メモリ表示するアドレス
        /// </returns>
        public nint GetAddress(string code)
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
        public string GetCodeString(IEnumerable<Tuple<nint, int, ulong>> memory)
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
        public string GetName() => "SnesPAR";

        /// <summary>
        /// Start executing code
        /// コードの実行を開始する
        /// </summary>
        /// <param name="code">
        /// The text entered in the code field
        /// コード欄に入力されているテキスト
        /// </param>
        public void Start(string code)
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
    }
}

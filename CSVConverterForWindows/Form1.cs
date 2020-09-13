using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateModelClass
{
    public partial class Form1 : Form
    {
        int DebugCount = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddFileButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            // ダイアログの説明文を指定する
            fbDialog.Description = "インポートするファイル";

            // デフォルトのフォルダを指定する
            fbDialog.SelectedPath = @"C:";

            // 「新しいフォルダーの作成する」ボタンを表示する
            fbDialog.ShowNewFolderButton = true;

            //フォルダを選択するダイアログを表示する
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                this.FileNameBox.Text = fbDialog.SelectedPath;
                Console.WriteLine(fbDialog.SelectedPath);
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }

            // オブジェクトを破棄する
            fbDialog.Dispose();
        }



        private void ExportButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            // ダイアログの説明文を指定する
            fbDialog.Description = "エクスポートするファイル";

            // デフォルトのフォルダを指定する
            fbDialog.SelectedPath = @"C:";

            // 「新しいフォルダーの作成する」ボタンを表示する
            fbDialog.ShowNewFolderButton = true;

            //フォルダを選択するダイアログを表示する
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                this.ExportPathBox.Text = fbDialog.SelectedPath;
                Console.WriteLine(fbDialog.SelectedPath);
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }

            // オブジェクトを破棄する
            fbDialog.Dispose();
        }

        /// <summary>
        /// 実行ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcuteButton_Click(object sender, EventArgs e)
        {

            try
            {
                CreateFileContent();
            }
            catch (Exception)
            {
                var error_result = "エラーが発生しました。";
                MessageBox.Show(error_result);
                throw;
            }

        }

        /// <summary>
        /// ファイルの生成
        /// </summary>
        private void CreateFileContent() 
        {
            var importPath = this.FileNameBox.Text;
            if (!Directory.Exists(importPath) || String.IsNullOrEmpty(importPath))
            {
                var error_result = "入力パスが間違っています。指定したフォルダは存在しません。";
                MessageBox.Show(error_result);
                return;
            };
            var exportPath = this.ExportPathBox.Text;
            if (!Directory.Exists(exportPath) || String.IsNullOrEmpty(exportPath))
            {
                var error_result = "出力パスが間違っています。指定したフォルダは存在しません。";
                MessageBox.Show(error_result);
                return;
            };
            //同じパスの時
            if (exportPath == importPath)
            {
                var error_result = "インポート元とエクスポート先が同じパスです。ファイルが書き換わる可能性があるため、実行できませんでした。エクスポート先は違うフォルダを指定して下さい。";
                MessageBox.Show(error_result);
                return;
            };
            //"C:\test"以下のファイルをすべて取得する
            //ワイルドカード"*"は、すべてのファイルを意味する
            string[] files = System.IO.Directory.GetFiles(importPath, "*.csv", System.IO.SearchOption.AllDirectories);
            if (files.Length == 0) 
            {
                var error_result = "インポートするファイルが存在しません。";
                MessageBox.Show(error_result);
                return;
            }
            foreach (var filefullname in files)
            {
                //ファイル名のみ
                var fileName = Path.GetFileNameWithoutExtension(filefullname);

                StreamReader sr = new StreamReader(filefullname, Encoding.GetEncoding("Shift_JIS"));
                StreamWriter writer = new StreamWriter($@"{exportPath}\{fileName}.csv", false, Encoding.GetEncoding("Shift_JIS"));

                var colums = sr.ReadLine();
                var line = sr.ReadToEnd();

                var answer = Calculate(line,colums);
                writer.WriteLine(answer);

                writer.Close();
                sr.Close();
            }
            if (files.Length > 0) 
            {
                var result = DebugCount.ToString()+"件 更新しました。";
                MessageBox.Show(result);
            }
        }

        /// <summary>
        /// ファイル整形
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string Calculate(string line,string colums) 
        {
            //初期化
            DebugCount = 0;
            //カラム集合体
            var colums_parts = colums.Split(',');
            //nameというカラムを対象とする
            var _name = "name";
            var target_colum = $"\"{_name}\"";

            //カラム数
            var columNum = colums_parts.Length; 
            //全データ
            var DataAllSet = line.Split(',');
            //制限文字数
            var maxChar = 75;
            //キーワード
            var key_word = this.KeyWordBox.Text;
            var dic_list = new List<Dictionary<string, string>>();
            for (int i = 0; i < DataAllSet.Length; i++)
            {
                
                if (i % (columNum - 1) == 0 || i == 0) 
                {
                    var data_dic = new Dictionary<string,string>();
                    for (int k = 0; k < columNum; k++)
                    {
                        if(DataAllSet.Length > i + k) data_dic.Add(colums_parts[k], DataAllSet[i + k]);
                    }
                    dic_list.Add(data_dic);
                }
            }
            foreach (var item in dic_list)
            {
                var targetData = "";
                if (item.ContainsKey(target_colum))
                {
                    targetData = item[target_colum];
                }
                else { continue; }

                var targetDataParts = targetData.Split(' ');
                targetDataParts[0] = targetDataParts[0].Remove(0, 1);
                //対象文字を含む商品名のみ更新
                if (this.SearchWordBox.Text.Length == 0 || SearchFlg(targetData))
                {
                    targetData = String.Join(" ", targetDataParts);
                    targetData = $@"{key_word} {targetData}";
                    if (targetData.Length - 1 > maxChar)
                    {
                        item[target_colum] = targetData.Remove(maxChar - 1, (targetData.Length - (maxChar - 1)));
                        DebugCount++;
                    }
                }
            }

            var strLine = String.Join(",", colums_parts);
            var strContentList = new List<string>();
            foreach (var item in dic_list)
            {
                strContentList.Add(String.Join(",", item.Values));
            }
            var strContent = String.Join(",", strContentList);

            var answer = strLine + strContent;
            return answer;
        }

        bool SearchFlg(string targetData) 
        {
            var isSearch = false;
            var search_parts = this.SearchWordBox.Text.Split(' ');

            var count = 0;
            foreach (var part in search_parts)
            {
                //全て含まれたら
                if (targetData.Contains(part))
                {
                    count++;
                }
            }
            isSearch = (count == search_parts.Length);
            return isSearch;
        }
    }
}

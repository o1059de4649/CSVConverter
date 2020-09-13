const fs = require('fs');
const iconv = require('iconv-lite');
const readline = require('readline');
const { BrowserWindow, dialog } = require('electron').remote;


//openFileボタンが押されたとき（ファイル名取得まで）
function openFile() {
    const win = BrowserWindow.getFocusedWindow();
    dialog.showOpenDialog(
        win,
        {
            properties: ['openFile'],
            filters: [
                {
                    name: 'Document',
                    extensions: ['csv', 'txt']
                }
            ]
        },
        (fileNames) => {
            if (fileNames) {
            readFile(fileNames[0]); //複数選択の可能性もあるので配列となる。
                // alert(fileNames[0]);
            }
        }
    ).then(result => {
        let in_obj = getObj('importFilePath');
        in_obj.innerText = result.filePaths[0];
      }).catch(err => {
        console.log(err)
      })
}

function openFolder()
{
    dialog.showOpenDialog(null, {
        properties: ['openDirectory'],
        title: 'フォルダ(単独選択)',
        defaultPath: '.'
    }, (folderNames) => {
        console.log(folderNames);
    }).then(result => {
        let ex_obj = getObj('exportFilePath');
        ex_obj.innerText = result.filePaths[0];
      }).catch(err => {
        console.log(err)
      })
}

//オブジェクトを取得
function getObj(id)
{
 return document.getElementById(id)
}

//指定したファイルを読み込む
function readFile(importPath) {
    
    let data_array = GetStream(importPath);

    let colums = data_array[1].toString();
    let _data = data_array[0].toString();
    let result_data = Calculate(_data,colums);
    return result_data;
}

function GetStream(importPath)
{
    let rs = fs.readFileSync(importPath).toString();
    let colums = fs.readFileSync(importPath).toString().split('\n')[0];
    let data_array = new Array();
    data_array.push(rs);
    data_array.push(colums);

    return data_array;
}

//fileを保存（Pathと内容を指定）
function writeFile(path, data) {
    fs.writeFile(path, data, (error) => {
        if (error != null) {
            alert("save error.");
            return;
        }
    })
}

/// <summary>
/// ファイル整形
/// </summary>
/// <param name="line"></param>
/// <returns></returns>
function Calculate(line,colums) 
{
    //初期化
    var DebugCount = 0;
    //カラム集合体
    var colums_parts = colums.split(',');
    colums_parts.push("");
    colums_parts.push("");
    //nameというカラムを対象とする
    var _name = "name";
    var target_colum = "\""+_name+"\"";

    //カラム数
    var columNum = colums_parts.length; 
    //全データ
    var DataAllSet = line.split('"');
    //データ整理
    for (var i = 0; i < DataAllSet.length; i++) {
        if (DataAllSet[i] == "," || i == 0)DataAllSet.splice(i, 1);
    }
    //制限文字数
    var maxChar = 75;
    //キーワード
    var key_word = getObj("KeyWordBox").value;
    var dic_list = new Array();
    for (var i = 0; i < DataAllSet.length; i++)
    {
        
        if (i % (columNum - 1) == 0 || i == 0) 
        {
            var data_dic =  new Array();
            for (var k = 0; k < columNum; k++)
            {
                var dataObj = {};
                dataObj["key"] = colums_parts[k];
                dataObj["value"] = DataAllSet[i + k];
                if (DataAllSet.length > i + k)
                {
                    data_dic.push(dataObj);
                }
            }
            data_dic.length--;
            dic_list.push(data_dic);
        }
    }

    dic_list.forEach(dic => {
        var targetData = "";
        dic.forEach(item => {
            if (item.key == target_colum)
            {
                targetData = item.value;
            }
        });


        var targetDataParts = targetData.split(' ');
        targetDataParts[0] = targetDataParts[0].slice(1);
        //対象文字を含む商品名のみ更新
        if (getObj("SearchWordBox").value.length == 0 || SearchFlg(targetData))
        {
            targetData = targetDataParts.join(" ");
            targetData = key_word + " " + targetData;
            if (targetData.length - 1 > maxChar)
            {
                var targetData = targetData.slice(0, maxChar);
                DebugCount++;
            }

            //答え代入
            dic.forEach(item => {
                if (item.key == target_colum) {
                    item[target_colum] = targetData;
                }
            });
        }
    });

    //データ単位
    var dataList = new Array();
    //子データ単位
    dic_list.forEach(dic => {
        var dicItemList = new Array();
        dic.length--;
        dic.forEach(item => {
            dicItemList.push('"'+item.value+'"');
        });
        dataList.push(dicItemList.join(','));
    });
    var strContent = dataList.join('\n').toString();

    var answer = strContent;
    return answer;
}

//探索
function SearchFlg(targetData) 
{
    var isSearch = false;
    var search_parts = this.SearchWordBox.Text.Split(' ');

    var count = 0;
    search_parts.forEach(part => {
        //全て含まれたら
        if (targetData.Contains(part))
        {
            count++;
        } 
    });
    isSearch = (count == search_parts.length);
    return isSearch;
}

//実行
function Excute()
{
    let inPath = getObj('importFilePath').innerText;
    let path_parts = inPath.split('\\');
    let file = path_parts[path_parts.length-1];
    let exPath = getObj('exportFilePath').innerText;
    let _result = readFile(inPath);
    fs.writeFileSync(exPath + '\\' + file, _result);
}
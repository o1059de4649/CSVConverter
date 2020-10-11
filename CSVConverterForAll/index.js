const fs = require('fs');
const iconv = require('iconv-lite');
const readline = require('readline');
const { BrowserWindow, dialog } = require('electron').remote;

//openFile�{�^���������ꂽ�Ƃ��i�t�@�C�����擾�܂Łj
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
            readFile(fileNames[0]); //�����I���̉\��������̂Ŕz��ƂȂ�B
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
        title: '�t�H���_(�P�ƑI��)',
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

//�I�u�W�F�N�g���擾
function getObj(id)
{
 return document.getElementById(id)
}

//�w�肵���t�@�C����ǂݍ���
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

//file��ۑ��iPath�Ɠ��e���w��j
function writeFile(path, data) {
    fs.writeFile(path, data, (error) => {
        if (error != null) {
            alert("save error.");
            return;
        }
    })
}

/// <summary>
/// �t�@�C�����`
/// </summary>
/// <param name="line"></param>
/// <returns></returns>
function Calculate(line,colums) 
{
    //������
    var DebugCount = 0;
    //�J�����W����
    var colums_parts = colums.split(',');
    colums_parts.push("");
    colums_parts.push("");
    //name�Ƃ����J������ΏۂƂ���
    var _name = "name";
    var target_colum = "\""+_name+"\"";

    //�J������
    var columNum = colums_parts.length; 
    //�S�f�[�^
    var DataAllSet = line.split('"');
    //�f�[�^����
    for (var i = 0; i < DataAllSet.length; i++) {
        if (DataAllSet[i] == "," || i == 0)DataAllSet.splice(i, 1);
    }
    //����������
    var maxChar = 75;
    //�L�[���[�h
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

    //�f�[�^�P��
    var dataList = new Array();

    dic_list.forEach(dic => {
        var targetData = "";
        dic.forEach(item => {
            if (item.key == target_colum)
            {
                targetData = item.value;
            }
        });

        var targetDataParts = targetData.split(' ');
        //�Ώە������܂ޏ��i���̂ݍX�V
        if (getObj("SearchWordBox").value.length == 0 || SearchFlg(targetData))
        {
            targetData = targetDataParts.join(" ");
            targetData = key_word + " " + targetData;
            if (targetData.length - 1 > maxChar)
            {
                var targetData = targetData.slice(0, maxChar);
                DebugCount++;
            }

            //�������
            dic.forEach(item => {
                if (item.key == target_colum) {
                    item["value"] = new String(targetData);
                }
            });
        }
        //TODO:�Ȃ����ł��Ȃ�
        //���ʍ쐬
        var itemList = new Array();
        dic.length--;
        dic.forEach(item => {
            itemList.push('"' + item.value + '"');
        });
        var str = new String(itemList.join(','));
        dataList.push(str);
    });

    var strContent = dataList.join('\n').toString();

    var answer = strContent;
    return answer;
}

//�T��
function SearchFlg(targetData) 
{
    var isSearch = false;
    var search_parts = this.SearchWordBox.value.split(' ');

    var count = 0;
    search_parts.forEach(part => {
        //�S�Ċ܂܂ꂽ��
        if (targetData.includes(part))
        {
            count++;
        } 
    });
    isSearch = (count == search_parts.length);
    return isSearch;
}

//���s
function Excute()
{
    let inPath = getObj('importFilePath').innerText;
    let path_parts = inPath.split('\\');
    let file = path_parts[path_parts.length-1];
    let exPath = getObj('exportFilePath').innerText;

    try {
        //�摜�\��
        var img = getObj('loading');
        img.classList.remove('loaded');
        //�t�@�C���쐬
        let _result = readFile(inPath);
        fs.writeFileSync(exPath + '\\' + file, _result);
        alert("変換が完了しました。");
        img.classList.add('loaded');
    } catch{
        alert("変換に失敗しました。");
        img.classList.add('loaded');
    }
}

window.onload = function() {
    const spinner = document.getElementById('loading');
    spinner.classList.add('loaded');
  }
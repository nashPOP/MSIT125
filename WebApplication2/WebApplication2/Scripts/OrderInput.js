//當網頁載入時
$(document).ready(function () {
    console.log(data);
    //加入事件
    $("#WineryData").on("change", op.setProductNames);
    $("#CategoryData").on("change", op.setProductNames);
    $("#AddButton").on("click", op.addToTable);
    $("#ConfirmButton").on("click", op.confirmRevise);
    $("#sendButton").on("click", op.sendTableData);

    //觸發事件
    $("#WineryData").change();

    //隱藏確認修改按鈕
    $("#ConfirmButton").hide();
});

var op = {};
//名稱陣列
op.rowCount = 0;
op.dataFieldName = ["Winery", "Category", "ProductId", "ProductName"];
//下列兩個的對應欄位的後墜需加Data
op.optionFieldId = ["WineryData", "CategoryData", "ProductNameData", "QuantityData", "CustomerNameData", "TelphoneData", "AddressData", "ShippedDateData", "NoteData"];
op.optionDataField = ["Winery", "Category", "ProductName", "Quantity", "CustomerName", "Telphone", "Address", "ShippedDate", "Note"];

//根據酒裝和分類的值 增加選項到PRODUCTNAME C
op.setProductNames = () => {
    //取得酒裝和分類的VALUE
    let wineryName = $("#WineryData").val();
    let categoryName = $("#CategoryData").val();

    //根據上面兩個值去篩出位於data物件內的適合資料
    let suitItem = [];
    data.forEach((e) => {
        if (e.Winery === wineryName && e.Category === categoryName) {
            suitItem.push(e);
            //console.log(`Push ${JSON.stringify(e)} to Array`)
        }
    });

    //先移除選項 再加入到ID=productNameData的SELECT
    $("#ProductNameData").children().remove();
    suitItem.forEach((item) => {
        $("#ProductNameData").append(`<option value='${item.ProductName}' data-productId='${item.ProductId}'> ${item.ProductName} </option>`)
    })

    //console.log(`winerNmae = ${wineryName}`);
    //console.log(`categoryName = ${categoryName}`);
    //console.log(JSON.stringify(suitItem));
};

//將選項的值加入到表格中
op.addToTable = () => {
    //取得資料
    let optionData = op.getOptionData();
    //驗證資料
    if (op.vaildOptionData(optionData)) {
        //開始加入到表格的準備工作
        //完成要加入的HTML字串
        let appendString = `<tr id="${op.rowCount}">`;
        for (var key in optionData) {
            appendString += `<td class="${key}">${optionData[key]}</td>`;
        }
        appendString += `<td><button class='btn btn-warning reviseButton' data-row='${op.rowCount}'>修改</button> 
                         <button class='btn btn-danger deleteButton' data-row='${op.rowCount}'>刪除</button></td>`
        appendString += `</tr>`;

        //加入到表格
        $("#AppendTarget").append(appendString);

        //為兩個按鈕加入事件
        $(".deleteButton").on("click", op.deleteRow);
        $(".reviseButton").on("click", op.reviseRow);
        op.rowCount++;
    } else {
        window.alert("資料有誤");
    }
};

//驗證輸入的資料 尚未實作
op.vaildOptionData = (OptionData) => {
    return true;
}

//取得訂單選項的值 C
op.getOptionData = () => {
    let optionData = {};
    op.optionDataField.forEach((name) => {
        optionData[name] = $(`#${name + "Data"}`).val();
    });
    //console.log(JSON.stringify(optionData));
    return optionData;
};

//設定訂單選項的值
op.setOptionData = (OptionData) => {
    //因為PRODUCTNAME可能不在選項中 因此單獨處理
    op.optionDataField.forEach((e) => {
        if (e !== "ProductName") {
            $(`#${e + "Data"}`).val(OptionData[e]);
        }
    });
    //觸發把產品名稱加入到選項的事件 確保PRODUCTNAME在選項中後修改
    op.setProductNames();
    $("#ProductNameData").val(OptionData["ProductName"]);
}

//取得已加入訂單的資料行資料 C
op.getTableRowData = (rowId) => {
    let returnOptionData = {};

    op.optionDataField.forEach((e) => {
        returnOptionData[e] = $(`#${rowId}`).children(`.${e}:first`).text();
    })

    console.log(JSON.stringify(returnOptionData));
    return returnOptionData;
}

//當該列的刪除按鈕按下後刪除該列 C
op.deleteRow = (e) => {
    let targetId = e.target.dataset.row;
    $(`#${targetId}`).remove();
}

//當該列的修改按鈕按下後，將該列的資料貼上到選項區後隱藏add按鈕，顯示修改確認紐，並儲存修改目標行的id資訊
op.reviseRow = (e) => {
    //取得要修改的ROWID
    let rowId = e.target.dataset.row;
    //取得ROW的資料貼到選項區
    op.setOptionData(op.getTableRowData(rowId));
    //顯示修改按鈕，並將修改按鈕的data-row設定為要更改的目標 隱藏加入按鈕
    $("#ConfirmButton").data("row", rowId);
    $("#ConfirmButton").show();
    $("#AddButton").hide();
}

//確認修改按鈕的韓式
op.confirmRevise = () => {
    //取得要修改的ROWID
    let rowId = $("#ConfirmButton").data("row");

    //移除要修改的ROW的CHILDREN然後把他加回原位
    $(`#${rowId}`).children().remove();
    //加回原位
    //完成要加入的HTML字串
    let data = op.getOptionData();
    console.log(JSON.stringify(data));
    let appendString = "";
    for (var key in data) {
        appendString += `<td class="${key}">${data[key]}</td>`;
    }
    appendString += `<td><button class='btn btn-warning reviseButton' data-row='${rowId}'>修改</button> 
                         <button class='btn btn-danger deleteButton' data-row='${rowId}'>刪除</button></td>`

    //加入到表格
    console.log(appendString);
    $(`#${rowId}`).append(appendString);

    //為兩個按鈕加入事件
    $(".deleteButton").on("click", op.deleteRow);
    $(".reviseButton").on("click", op.reviseRow);

    //隱藏確認修改 顯示加入紐
    $("#ConfirmButton").hide();
    $("#AddButton").show();
}

//利用AJAX送出資料的函示 ************************************還沒完成**************************
op.sendTableData = () => {
    let sendData = [];
    $("#AppendTarget").children("tr").each(function () {
        let data = {};
        op.optionDataField.forEach((key) => {
            data[key] = $(this).children(`.${key}`).text();
        })
        sendData.push(data);
    })

    console.log(JSON.stringify(sendData));
    console.log(urlToPost);
    $.ajax({
        type: "POST",
        url: urlToPost,
        dataType: "json",
        data: JSON.stringify(sendData),
        contentType: "application/json",
        success: function (res) {
            $("#testArea").text(JSON.stringify(res));
        },
        error: function (err) {
            $("#testArea").text(err);
        }
    })
}
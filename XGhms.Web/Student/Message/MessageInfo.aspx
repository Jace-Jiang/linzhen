<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageInfo.aspx.cs" Inherits="XGhms.Web.Student.Message.MessageInfo" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>站内消息 - XGhms</title>
    <link href="~/Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/site.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
    <script src="../../editor/kindeditor-all.js" type="text/javascript"></script>
    <script src="../../editor/lang/zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="content"]', {
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                items: [
						'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
						'insertunorderedlist', '|', 'emoticons', 'image', 'link']
            });
        });
		</script>
</head>
<body>
<%--<form id="form1" runat="server">--%>
<uctrols:stuTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:stuLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="row-fluid">
                <div class="page-header">
				    <h1> <span id="title_h1">发送消息</span><small><span id="title_h1small">站内信</span></small></h1>
			    </div>
                <h3>消息详情:</h3><br />
                <div id="addDivOfMes"></div>
                <div class="span11"><h3>回复消息:</h3></div>
                <textarea id="txtKindEditor" name="content" style="width:95%;height:230px;visibility:hidden;"></textarea>
                <div class="form-actions">
					<input id="hwEditId" onclick="RepeaterMsg()" name="hwEditId" type="button" value="回复" class="btn btn-success btn-large"/> <a class="btn" href="MyMessage.aspx">取消</a>
				</div>
            </div>
        </div>
    </div>
    <uctrols:downFooter ID="AllDownFooter" runat="server" />
</div>
<%--</form>--%>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#left_nav_ul li:eq(7)").attr('class', 'active'); //获取相应的li，设置class为active
        //获取传过来的参数值，然后根据相应的参数值来判断相应的值
        var mesid = getParam("id");
        if (mesid == "null" || mesid == null) {
            window.location.replace("../../Error.aspx?id=0");
            return;
        };
        var parm = "action=GetMessageBymesID&id=" + mesid;
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: parm,
            cache: false,
            async: true,
            success: function (data) {
                if (data == "false") {
                    window.location.replace("../../Error.aspx?id=0");
                    return;
                }
                else {
                    $.each(data.meslist, function (index, item) {
                        addMessage(unescape(item.mescontent), item.send_time, item.sender, item.mesStatus);
                    });
                }
            }
        });
    });

    function addMessage(messageVal, mesTime, mesUser, mesStatus) {
        if (mesStatus == 1) { //对方的消息
            $('#addDivOfMes').append('<div class="span11" style="margin-left:2.56%"><blockquote><p>' + messageVal + '</p><small>' + mesTime + '&nbsp; &nbsp;' + mesUser + '</small></blockquote></div>');
        }
        if (mesStatus == 0) { //你的消息
            $('#addDivOfMes').append('<div class="span11" style="margin-left:2.56%"><blockquote class="pull-right"><p>' + messageVal + '</p><small>' + mesTime + '&nbsp; &nbsp;' + mesUser + '</small></blockquote></div>');
        }
    }
    //回复该消息
    function RepeaterMsg() {
        var msgCon = editor.html();
        var mesid = getParam("id");
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=InsertMsgByOtherID&id=" + mesid + "&msgCon=" + escape(msgCon),
            cache: false,
            async: true,
            success: function (data) {
                alert(data.msg);
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }

    //获取URL参数
    var getParam = function (name) {
        var search = document.location.search;
        var pattern = new RegExp("[?&]" + name + "\=([^&]+)", "g");
        var matcher = pattern.exec(search);
        var items = null;
        if (null != matcher) {
            try {
                items = decodeURIComponent(decodeURIComponent(matcher[1]));
            } catch (e) {
                try {
                    items = decodeURIComponent(matcher[1]);
                } catch (e) {
                    items = matcher[1];
                }
            }
        }
        return items;
    };
</script>
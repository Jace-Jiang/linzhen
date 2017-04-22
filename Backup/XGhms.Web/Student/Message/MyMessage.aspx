<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyMessage.aspx.cs" Inherits="XGhms.Web.Student.Message.MyMessage" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的消息 - XGhms</title>
    <link href="../../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/site.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
</head>
<body>
<form id="form1" runat="server">
<uctrols:stuTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:stuLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="row-fluid">
                <div class="page-header">
				    <h1>我的消息 <small id="h1small"> 全部消息</small></h1>
			    </div>
                <div> 
                    选择条件:&nbsp; &nbsp;&nbsp;
                    <label class="radio inline"><input name="optionsRadios" type="radio" id="opsRec" checked="checked" value="Receive">接收到的消息</label>
                    <label class="radio inline"><input name="optionsRadios" type="radio" id="opsSed" value="Send">已发送的消息</label>&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label" for="msgType">选择类型</label>
                    <select id="msgType" onchange="typeChange()">
						<option selected="selected" value="all">全部</option>
                        <option value="student">学生消息</option>
                        <option value="teacher">教师消息</option>
                        <option value="admin">管理员消息</option>
                        <option value="system">系统通知</option>
					</select>
                </div>
                <h4 id="h4title" style="margin:7px 0 7px;">收到的消息</h4>
                <table class="table table-striped table-bordered table-condensed" style="width:95%">
                  <thead>
			        <tr>
				        <th>ID</th>
				        <th id="th_userTitle">发送者</th>
				        <th>内容</th>
				        <th>时间</th>
				        <th>是否阅读</th>
				        <th></th>
			        </tr>
		          </thead>
                  <tbody id="defaultAddTr">
                  </tbody>
                </table>
                <!-- 分页 -->
			    <div class="pagination">
				    <ul id="pageNum_nav">
				    </ul>
			    </div>
                <div class="form-actions">
					<a id="EditMessage" class="btn btn-success btn-large" href="NewMessage.aspx">发送消息</a> 
				</div>
            </div>
        </div>
    </div>
    <uctrols:downFooter ID="AllDownFooter" runat="server" />
</div>
</form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#left_nav_ul li:eq(7)").attr('class', 'active'); //获取相应的li，设置class为active
        var urlType = getParam("type"); //获取消息类型值
        if (urlType == "stu") {
            $('#msgType').val("student");
        }
        if (urlType == "ter") {
            $('#msgType').val("teacher");
        }
        if (urlType == "adm") {
            $('#msgType').val("admin");
        }
        if (urlType == "sys") {
            $('#msgType').val("system");
        }
        SelectUserListTotalBySelter();
    });
    //当选择 接收到的消息 时
    $("#opsRec").click(function () {
        $('#h4title').empty();
        $('#h4title').append('收到的消息');
        $('#th_userTitle').empty();
        $('#th_userTitle').append('发送者');
        SelectUserListTotalBySelter();
    });
    //当选择 已发送的消息 时
    $("#opsSed").click(function () {
        $('#h4title').empty();
        $('#h4title').append('发送的消息');
        $('#th_userTitle').empty();
        $('#th_userTitle').append('接收者');
        SelectUserListTotalBySelter();
    });
    //消息类型发生变化时
    function typeChange() {
        SelectUserListTotalBySelter();
    }
    //根据所有的条件进行查询，输出总数
    function SelectUserListTotalBySelter() {
        var msgSel = $('input:radio[name="optionsRadios"]:checked').val();
        var msgType = $('#msgType').val();
        var parms = "action=GetUserMsgListCountNum&msgCondition=" + msgSel + "&msgType=" + msgType;
        $.ajax({ //获取用户列表
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: parms,
            success: function (data) {
                $('#pageNum_nav').empty();
                pageNumShow(data.pagenum, 1);
            }
        });
    }
    //根据选项获取消息列表
    function GetUserMsgList(nowPageNum) {
        var msgSel = $('input:radio[name="optionsRadios"]:checked').val();
        var msgType = $('#msgType').val();
        var parms = "action=GetUserMsgList&msgCondition=" + msgSel + "&msgType=" + msgType + "&nowPageNum=" + nowPageNum;
        $.ajax({ //获取消息列表
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: parms,
            success: function (data) {
                $('#defaultAddTr').empty(); //先清除所有的内容
                $.each(data.msgList, function (index, item) {
                    AddTrList(item.id, item.userName, unescape(item.msgCon), item.msgTime, item.msgRead);
                });
            }
        });
    }
    //表格添加代码
    function AddTrList(ID, userName, msgCon, msgTime, msgRead) {
        var msgCont = "";
        if (msgCon.length > 20) {
            msgCont = '<abbr title="' + msgCon + '">' + msgCon.substr(0, 20) + '...</abbr>';
        }
        if (msgCon.length <= 20) {
            msgCont = msgCon;
        }
        var readCot = "";
        if (msgRead == 1) {
            readCot = '<td><span class="label label-success">已阅读</span></td>';
        }
        if (msgRead == 0) {
            readCot = '<td><span class="label label-important">未阅读</span></td>';
        }
        var statusSet = '<li><a href="MessageInfo.aspx?id=' + ID + '"><i class="icon-eye-open"></i> 查看消息</a></li><li><a href="javascript:deleteMsg(' + ID + ')"><i class="icon-remove"></i> 删除消息</a></li>';
        $('#defaultAddTr').append('<tr class="list-users"><td>' + ID + '</td><td>' + userName + '</td><td>' + msgCont + '</td><td>' + msgTime + '</td>' + readCot + '<td><div class="btn-group"><a class="btn btn-mini dropdown-toggle" data-toggle="dropdown" href="#">操作 <span class="caret"></span></a><ul class="dropdown-menu">' + statusSet + '</ul></div></td></tr>');
    }
    //根据ID删除消息
    function deleteMsg(id) {
        if (!confirm("你确信要删除该消息？")) {
            return;
        }
        $.ajax({ //获取消息列表
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=deleteMsg&id=" + id,
            success: function (data) {
                alert(data.msg);
                SelectUserListTotalBySelter();
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }
    //分页代码
    function pageNumShow(totalNum, nowPageNum) {
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="totalNum">总页码</param>
        /// <param name="nowPageNum">当前页码</param>
        /// <returns>添加到html中</returns>
        if (nowPageNum == $("#liactive").text()) {
            return;
        }
        else {
            GetUserMsgList(nowPageNum); //加载新的页码的内容
            $("#pageNum_nav").empty(); //先清除所有的内容
            $("#pageNum_nav").append('<li><a href="javascript:pageNumShow(' + totalNum + ',1);">首页</a></li>');
            if (totalNum <= 5) { //当总页码小于等于5的时候
                for (var i = 1; i < totalNum + 1; i++) {
                    if (i == nowPageNum) {
                        $("#pageNum_nav").append('<li id="liactive" class="active"><a href="javascript:void(0);">' + i + '</a></li>');
                    }
                    else {
                        $("#pageNum_nav").append('<li><a href="javascript:pageNumShow(' + totalNum + ',' + i + ');">' + i + '</a></li>');
                    }
                }
            }
            else { //当总页码大于5的时候
                if ((nowPageNum - 3) <= 0) { //当前页码小于等于3的时候
                    for (var i = 1; i < nowPageNum + 1; i++) {
                        if (i == nowPageNum) {
                            $("#pageNum_nav").append('<li id="liactive" class="active"><a href="javascript:void(0);">' + i + '</a></li>');
                        }
                        else {
                            $("#pageNum_nav").append('<li><a href="javascript:pageNumShow(' + totalNum + ',' + i + ');">' + i + '</a></li>');
                        }
                    }
                }
                if ((nowPageNum - 3) > 0) { //当前页码大于3的时候
                    $("#pageNum_nav").append('<li><a href="#">...</a></li>');
                    for (var i = nowPageNum - 2; i < nowPageNum + 1; i++) {
                        if (i == nowPageNum) {
                            $("#pageNum_nav").append('<li id="liactive" class="active"><a href="javascript:void(0);">' + i + '</a></li>');
                        }
                        else {
                            $("#pageNum_nav").append('<li><a href="javascript:pageNumShow(' + totalNum + ',' + i + ');">' + i + '</a></li>');
                        }
                    }
                }
                if ((nowPageNum + 3) >= totalNum) { //当前页码+3大于等于总页数的时候
                    for (var i = nowPageNum + 1; i < totalNum + 1; i++) {
                        $("#pageNum_nav").append('<li><a href="javascript:pageNumShow(' + totalNum + ',' + i + ');">' + i + '</a></li>');
                    }
                }
                if ((nowPageNum + 3) < totalNum) { //当前页码+3之后小于总页数的时候
                    for (var i = nowPageNum + 1; i < nowPageNum + 3; i++) {
                        $("#pageNum_nav").append('<li><a href="javascript:pageNumShow(' + totalNum + ',' + i + ');">' + i + '</a></li>');
                    }
                    $("#pageNum_nav").append('<li><a href="javascript:void(0);">...</a></li>');
                }
            }
            $("#pageNum_nav").append('<li><a href="javascript:pageNumShow(' + totalNum + ',' + totalNum + ');">尾页</a></li>');
        }
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
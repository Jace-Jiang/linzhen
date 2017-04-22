<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XGhms.Web.Admin.Default" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>课程作业管理系统 - XGhms</title>
    <link href="../../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/site.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
</head>
<body>
<form id="form1" runat="server">
<uctrols:admTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:admLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="well hero-unit">
                <h1><asp:Label ID="lab_MainTitle" runat="server" Text="欢迎使用课程作业管理系统"></asp:Label></h1>
                <p><asp:Label ID="lab_MainDescription" runat="server" Text="This is system descript"></asp:Label> </p>
                <p><a class="btn btn-success btn-large" href="UserControls/UserList.aspx">开始管理用户 &raquo;</a></p>
            </div>
            <div class="row-fluid">
                <div class="span3">
                  <h3>学生消息</h3>
                  <p><a id="stuMsgNum" href="Message/MyMessage.aspx?type=stu" class="badge badge-inverse"></a></p>
                </div>
                <div class="span3">
                  <h3>教师消息</h3>
                  <p><a id="terMsgNum" href="Message/MyMessage.aspx?type=ter" class="badge badge-inverse"></a></p>
                </div>
                <div class="span3">
                  <h3>管理员消息</h3>
                  <p><a id="admMsgNum" href="Message/MyMessage.aspx?type=adm" class="badge badge-inverse"></a></p>
                </div>
                <div class="span3">
                  <h3>系统通知</h3>
                  <p><a id="sysMsgNum" href="Message/MyMessage.aspx?type=sys" class="badge badge-inverse"></a></p>
                </div>
            </div>
            <div class="row-fluid">
        <div class="page-header">
          <h1>我的消息 <small>最近收到的消息预览</small></h1>
        </div>
        <table class="table table-bordered">
          <thead>
			<tr>
				<th>ID</th>
				<th>发送者</th>
				<th>内容</th>
				<th>时间</th>
				<th>是否阅读</th>
				<th></th>
			</tr>
		  </thead>
          <tbody id="defaultAddTr">
          </tbody>
        </table>
        <div id="testone"></div>
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
        var parm = "action=GetThreeMsgForDefault";
        $.ajax({ //获取消息的列表
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: parm,
            cache: false,
            async: true,
            success: function (data) {
                $.each(data.msgList, function (index, item) {
                    AddTrList(item.id, item.userName, unescape(item.msgCon), item.msgTime, item.msgRead);
                });
            }
        });
        $.ajax({ //获取学生的消息数目
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetStuMsgNumForDefault",
            cache: false,
            async: true,
            success: function (data) {
                $('#stuMsgNum').append(data.num);
            }
        });
        $.ajax({ //获取老师的消息数目
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetTerMsgNumForDefault",
            cache: false,
            async: true,
            success: function (data) {
                $('#terMsgNum').append(data.num);
            }
        });
        $.ajax({ //获取管理员的消息数目
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetAdmMsgNumForDefault",
            cache: false,
            async: true,
            success: function (data) {
                $('#admMsgNum').append(data.num);
            }
        });
        $.ajax({ //获取系统通知的数目
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetSysMsgNumForDefault",
            cache: false,
            async: true,
            success: function (data) {
                $('#sysMsgNum').append(data.num);
            }
        });
    });
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
        $('#defaultAddTr').append('<tr class="pending-user"><td>' + ID + '</td><td>' + userName + '</td><td>' + msgCont + '</td><td>' + msgTime + '</td>' + readCot + '<td><span class="user-actions"><a href="Message/MessageInfo.aspx?id=' + ID + '" class="label label-info"><i class="icon-search"></i> 查看消息</a></span></td></tr>');
    }
</script>
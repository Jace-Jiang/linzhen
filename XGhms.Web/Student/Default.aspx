<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XGhms.Web.Student.Default" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
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
<uctrols:stuTopNav ID="topNav" runat="server" />
<div class="container-fluid">
  <div class="row-fluid">
    <uctrols:stuLeftNav ID="leftNav" runat="server" />
    <div class="span9">
      <div class="well hero-unit">
        <h1><asp:Label ID="lab_MainTitle" runat="server" Text="欢迎使用课程作业管理系统"></asp:Label></h1>
        <p><asp:Label ID="lab_MainDescription" runat="server" Text="This is system descript"></asp:Label> </p>
        <p><a class="btn btn-success btn-large" href="HomeWorkManage/Default.aspx">开始我的作业 &raquo;</a></p>
      </div>

      <div class="row-fluid">
        <div class="span3">
          <h3>未完成</h3>
          <p><a id="noWorkNum" href="HomeWorkManage/Default.aspx" class="badge badge-inverse"></a></p>
        </div>
        <div class="span3">
          <h3>已完成</h3>
          <p><a id="WorkNum" href="HomeWorkManage/Default.aspx" class="badge badge-inverse"></a></p>
        </div>
        <div class="span3">
          <h3>消息</h3>
          <p><a id="nosysMsgNum" href="Message/MyMessage.aspx" class="badge badge-inverse"></a></p>
        </div>
        <div class="span3">
          <h3>系统通知</h3>
          <p><a id="sysMsgNum" href="Message/MyMessage.aspx?type=sys" class="badge badge-inverse"></a></p>
        </div>
      </div>
      <br />
      <div class="row-fluid">
        <div class="page-header">
          <h1>最近作业 <small>作业简单预览</small></h1>
        </div>
        <table class="table table-bordered">
          <thead>
			<tr>
				<th>ID</th>
				<th>课程</th>
				<th>作业题目</th>
				<th>时间</th>
				<th>老师</th>
				<th>状态</th>
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
        var parm = "action=GetAllLine";
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: parm,
            cache: false,
            async: true,
            success: function (data) {
                $.each(data.Homework, function (index, item) {
                    addtabletr(item.id, item.homeworkID, item.courseName, item.homeworkName, item.homeworkTime, item.teacher, item.status);
                });
            }
        });

        $.ajax({ //获取老师未完成批改的作业数目
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetStuNoWorkNum",
            cache: false,
            async: true,
            success: function (data) {
                $('#noWorkNum').append(data.num);
            }
        });
        $.ajax({ //获取老师已完成批改的作业数目
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetStuWorkNum",
            cache: false,
            async: true,
            success: function (data) {
                $('#WorkNum').append(data.num);
            }
        });
        $.ajax({ //获取用户的消息数目
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetNoSysMsgNum",
            cache: false,
            async: true,
            success: function (data) {
                $('#nosysMsgNum').append(data.num);
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

    function addtabletr(id, homeworkID, courseName, homeworkName, homeworkTime, teacher, status) {
        var statusName;
        if (status == 0 || status == 3) {
            statusName = '<span class="label label-important">未完成</span>';
        }
        else if (status == 4) {
            statusName = '<span class="label label-success">已完成</span>';
        }
        else {
            statusName = '<span class="label label-info">批阅中</span>';
        }
        $('#defaultAddTr').append('<tr class="pending-user"><td>' + homeworkID + '</td><td>' + courseName + '</td><td>' + homeworkName + '</td><td>' + homeworkTime + '</td><td>' + teacher + '</td><td>' + statusName + '</td><td><span class="user-actions"><a href="HomeWorkManage/Homework.aspx?id=' + homeworkID + '" class="label label-info"><i class="icon-search"></i> 查看详细</a></span></td></tr>');
    }
</script>
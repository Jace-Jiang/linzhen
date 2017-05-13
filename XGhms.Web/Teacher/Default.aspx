<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XGhms.Web.Teacher.Default" %>
<%@ Register TagPrefix="uctrols" TagName="terTopNav" Src="~/Teacher/MyControls/TerTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="terLeftNav" Src="~/Teacher/MyControls/TerLeftNav.ascx" %>
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
<uctrols:terTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:terLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="well hero-unit">
                <h1><asp:Label ID="lab_MainTitle" runat="server" Text="欢迎使用课程作业管理系统"></asp:Label></h1>
                <p><asp:Label ID="lab_MainDescription" runat="server" Text="This is system descript"></asp:Label> </p>
                <p><a class="btn btn-success btn-large" href="HomeWorkCheck/StudentWorkList.aspx">开始批改作业 &raquo;</a></p>
            </div>
            <div class="row-fluid">
                <div class="span3">
                  <h3>待审批</h3>
                  <p><a href="HomeWorkCheck/StudentWorkList.aspx?id=1" id="noCheckNum" class="badge badge-inverse"></a></p>
                </div>
                <div class="span3">
                  <h3>已完成</h3>
                  <p><a href="HomeWorkCheck/StudentWorkList.aspx?id=4" id="checkNum" class="badge badge-inverse"></a></p>
                </div>
                <div class="span3">
                  <h3>消息</h3>
                  <p><a href="Message/MyMessage.aspx" id="nosysMsgNum" class="badge badge-inverse"></a></p>
                </div>
                <div class="span3">
                  <h3>系统通知</h3>
                  <p><a href="Message/MyMessage.aspx?type=sys" id="sysMsgNum" class="badge badge-inverse"></a></p>
                </div>
            </div>
            <div class="row-fluid">
        <div class="page-header">
          <h1>学生作业 <small>最近收到的作业预览</small></h1>
        </div>
        <table class="table table-bordered">
          <thead>
			<tr>
				<th>ID</th>
			    <th>作业名称</th>
			    <th>课程名</th>
			    <th>学生姓名</th>
			    <th>作业状态</th>
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
        var parm = "action=GetThreeHWlistForDefault";
        $.ajax({ //获取消息的列表
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: parm,
            cache: false,
            async: true,
            success: function (data) {
                $.each(data.hwList, function (index, item) {
                    AddTrList(item.id, item.homework_name, item.course_name, item.student_name, item.homework_status, item.stuhwID);
                });
            }
        });
        $.ajax({ //获取老师未完成批改的作业数目
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetNoCheckWorkNum",
            cache: false,
            async: true,
            success: function (data) {
                $('#noCheckNum').append(data.num);
            }
        });
        $.ajax({ //获取老师已完成批改的作业数目
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetCheckWorkNum",
            cache: false,
            async: true,
            success: function (data) {
                $('#checkNum').append(data.num);
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
    //添加table代码
    function AddTrList(ID, hwName, courseName, stuName, hwStatus, stuhwID) {
        var homeworkstatus = '';
        var hwsn = '';
        if (hwStatus == 0) {
            homeworkstatus = '<td><span class="label label-info">未完成</span></td>';
            hwsn = '查看作业';
        }
        if (hwStatus == 1) {
            homeworkstatus = '<td><span class="label label-important">待批阅</span></td>';
            hwsn = '批阅作业';
        }
        if (hwStatus == 2) {
            homeworkstatus = '<td><span class="label label-warning">正在批阅</span></td>';
            hwsn = '批阅作业';
        }
        if (hwStatus == 3) {
            homeworkstatus = '<td><span class="label label-inverse">重做</span></td>';
            hwsn = '查看作业';
        }
        if (hwStatus == 4) {
            homeworkstatus = '<td><span class="label label-success">已完成</span></td>';
            hwsn = '查看作业';
        }
        var statusSet = '<li><a href="HomeWorkCheck/StudentWorkCheck.aspx?id=' + stuhwID + '"><i class="icon-eye-open"></i> ' + hwsn + '</a></li>';
        var str = '<td><div class="btn-group"><a class="btn btn-mini dropdown-toggle" data-toggle="dropdown" href="#">操作 <span class="caret"></span></a><ul class="dropdown-menu">' + statusSet + '</ul></div></td>';
        $('#defaultAddTr').append('<tr class="list-users"><td>' + ID + '</td><td>' + hwName + '</td><td>' + courseName + '</td><td>' + stuName + '</td>' + homeworkstatus + str + '</tr>');
    }
</script>
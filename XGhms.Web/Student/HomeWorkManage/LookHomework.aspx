<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LookHomework.aspx.cs" Inherits="XGhms.Web.Student.HomeWorkManage.LookHomework" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看已阅作业 - XGhms</title>
    <link href="~/Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/site.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
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
				    <h1> <span id="title_h1"></span> <small><span id="title_h1small"></span></small></h1>
			    </div>
                <h3>作业开始时间: <span id="hw_beginTime" style="font-weight:normal; margin-right:20px;"> </span>
                    作业结束时间: <span id="hw_endTime" style="font-weight:normal; margin-right:20px;"> </span> 
                    授课老师: <span id="hw_teacher" style="font-weight:normal;"></span></h3> <br />
                <h3>老师评语:</h3>
                <p id="hwInfos"></p>
                <h4>你的作业最后老师给分：<small id="txtCorse"></small> 分</h4>
                <div class="form-actions">
					 <a id="successchangelink" class="btn" href="Default.aspx">返回作业列表</a>
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
        $("#left_nav_ul li:eq(2)").attr('class', 'active'); //获取相应的li，设置class为active
        //获取传过来的参数值，然后根据相应的参数值来判断相应的值
        var hwid = getParam("id");
        if (hwid == "null" || hwid == null) {
            window.location.replace("../../Error.aspx?id=0");
            return;
        };
        var parm = "action=GetHomeworkByID&id=" + hwid;
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: parm,
            cache: false,
            async: true,
            success: function (data) {
                if (data.id == "0") {
                    window.location.replace("../../Error.aspx?id=0");
                    return;
                }
                else {
                    setHomeworkInfo(data.homework_name, data.course_name, data.hw_begtime, data.hw_endtime, data.course_teacher);
                }
            }
        });
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetHomeWorkInfoForTer&id=" + hwid,
            cache: false,
            async: true,
            success: function (data) {
                if (data.id == "0") {
                    return;
                }
                else {
                    $('#hwInfos').append(unescape(data.homework_comment));
                    $('#txtCorse').append(data.homework_score);
                }
            }
        });
    });

    //设置显示的数据
    function setHomeworkInfo(HWtitle, courseName, beginTime, endTime, teacherName) {
        $("#title_h1").append(HWtitle); //设置作业名称
        $("#title_h1small").append(courseName); //设置课程名称
        $("#hw_beginTime").append(beginTime); //设置开始时间
        $("#hw_endTime").append(endTime); //设置结束时间
        $("#hw_teacher").append(teacherName); //设置课程老师
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
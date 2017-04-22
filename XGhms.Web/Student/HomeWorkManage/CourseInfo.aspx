<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseInfo.aspx.cs" Inherits="XGhms.Web.Student.HomeWorkManage.CourseInfo" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>课程详情 - XGhms</title>
    <link href="~/Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/site.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
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
				    <h1> <span id="title_h1">课程名</span> <small><span id="title_h1small"></span></small></h1>
			    </div>
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseName">课程名</label>
						    <div class="controls">
                                <input id="tb_courseName" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseNum">课程标志号</label>
						    <div class="controls">
                                <input id="tb_courseNum" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseTrem">学期</label>
						    <div class="controls">
                                <input id="tb_courseTrem" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseCollege">开课学院</label>
						    <div class="controls">
                                <input id="tb_courseCollege" type="text" readonly="readonly"/>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseTeacher">课程老师</label>
						    <div class="controls">
                                <input id="tb_courseTeacher" type="text" readonly="readonly"/>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseOtherTeacher">助教</label>
						    <div class="controls">
                                <input id="tb_courseOtherTeacher" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_studentName">学生负责人</label>
						    <div class="controls">
                                <input id="tb_studentName" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="description">课程信息</label>
						    <div class="controls">
                                <span id="description"></span>
						    </div>
					    </div>
                        <div class="form-actions">
						     <a class="btn" href="MyCourse.aspx">返回课程列表</a>
					    </div>	
                    </fieldset>
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
        $("#left_nav_ul li:eq(1)").attr('class', 'active'); //获取相应的li，设置class为active
        var geturlID = getParam("id");
        if (geturlID == null || geturlID == "") {
            alert("请先选择课程！");
            window.location.replace("MyCourse.aspx");
            return;
        }
        else {
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Handles/StudentHandler.ashx",
                data: "action=GetCourseInfoByID&id=" + geturlID,
                cache: true,
                async: true,
                success: function (data) {
                    if (data.msgtype == 0) { //表示课程无法访问
                        window.location.replace("../../Error.aspx?id=1");
                        return;
                    } else if (data.msgtype == 1) { //表示改班级只可以查看
                        $('#tb_courseName').val(data.course_name);
                        $('#tb_courseNum').val(data.course_number);
                        $('#tb_courseTrem').val(data.term);
                        $('#title_h1small').append(data.course_name);
                        $('#tb_courseCollege').val(data.college);
                        $('#tb_courseTeacher').val(data.teacher);
                        $('#tb_courseOtherTeacher').val(data.other_teacher);
                        $('#tb_studentName').val(data.student);
                        $('#description').append(unescape(data.course_info));
                    }
                },
                error: function () {
                    alert("未知错误！");
                }
            });
        }
    }); 
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
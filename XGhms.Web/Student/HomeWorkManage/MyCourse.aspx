<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyCourse.aspx.cs" Inherits="XGhms.Web.Student.HomeWorkManage.MyCourse" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的课程 - XGhms</title>
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
				    <h1>我的课程 <small id="h1small"> </small></h1>
			    </div>
                <div id="oneDiv" class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
						    <label class="control-label" for="selTerm">选择学期</label>
						    <div class="controls">
							    <select onchange="SelectTerm()" id="selTerm"><option value="0">请选择</option></select>
						    </div>
					    </div>
                    </fieldset>
                </div>
                <table class="table table-bordered">
                  <thead>
			        <tr>
				        <th>ID</th>
				        <th>课程编号</th>
				        <th>课程名</th>
				        <th>授课教师</th>
				        <th>所在学院</th>
				        <th>查看课程</th>
                        <th>查看作业</th>
			        </tr>
		          </thead>
                  <tbody id="defaultAddTr">
                  </tbody>
                </table>
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
        $.ajax({ //添加学期
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetAllTermForNow",
            success: function (data) {
                $.each(data.termList, function (index, item) {
                    if (item.termCheck == 1) {
                        col_addSelect("#selTerm", item.id, item.termName, item.id);
                        $('#h1small').append(item.termName);
                        GetCourselistByTrem(item.id);
                    }
                    else {
                        col_add("#selTerm", item.id, item.termName);
                    }
                });
            }
        });
    });

    //当学期选项变化时候
    function SelectTerm() {
        if ($('#selTerm option:selected').val() == 0) {
            return;
        }
        $('#defaultAddTr').empty();
        $('#h1small').empty();
        $('#h1small').append($('#selTerm option:selected').text());
        GetCourselistAll();
    }
    //查询数据
    function GetCourselistByTrem(tremID) {
        $.ajax({ //获取当前的查询条件下面的条数
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetStuCourseByTrem&tremID=" + tremID,
            success: function (data) {
                $.each(data.courseList, function (index, item) {
                    AddTrList(item.id, item.course_number, item.course_name, item.teacher, item.college);
                });
            }
        });
    }
    //查询数据
    function GetCourselistAll() {
        var tremID = $('#selTerm option:selected').val();
        $.ajax({ //获取当前的查询条件下面的条数
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetStuCourseByTrem&tremID=" + tremID,
            success: function (data) {
                $('#defaultAddTr').empty();
                $.each(data.courseList, function (index, item) {
                    AddTrList(item.id, item.course_number, item.course_name, item.teacher, item.college);
                });
            }
        });
    }
    function AddTrList(ID, courseNum, courseName, teacher, collegeName) {
        var newHW = '<td><a href="Default.aspx?cid=' + ID + '" class="label label-success"><i class="icon-plus-sign"></i> 查看作业</a></td>';
        $('#defaultAddTr').append('<tr class="list-users"><td>' + ID + '</td><td>' + courseNum + '</td><td>' + courseName + '</td><td>' + teacher + '</td><td>' + collegeName + '</td><td><a href="CourseInfo.aspx?id=' + ID + '" class="label label-info"><i class="icon-search"></i> 查看课程</a></td>' + newHW + '</tr>');
    }
    // 添加下拉列表选项根据已选择的
    function col_addSelect(selectobj, value, text, selectValue) {
        var selObj = $(selectobj);
        if (value == selectValue) {
            selObj.append("<option selected='selected' value='" + value + "'>" + text + "</option>");
        } else {
            selObj.append("<option value='" + value + "'>" + text + "</option>");
        }
    }
    // 添加下拉列表选项
    function col_add(selectobj, value, text) {
        var selObj = $(selectobj);
        selObj.append("<option value='" + value + "'>" + text + "</option>");
    }
    // 删除
    function col_delete(selectobj) {
        var selOpt = $(selectobj + " option:selected");
        selOpt.remove();
    }
    // 清空下拉列表选项并且加一个全选的按钮
    function col_clear(colobject) {
        var selOpt = $(colobject + " option");
        selOpt.remove();
        col_add(colobject, 0, "请选择"); //默认添加有全部请选择的选项
    }
</script>

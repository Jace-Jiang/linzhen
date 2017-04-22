<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XGhms.Web.Student.HomeWorkManage.Default" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的作业 - XGhms</title>
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
				<h1>我的作业 <small id="h1small"> </small></h1>
			</div>
            <div class="control-group">
                <label style="display:inline" class="control-label" for="selTerm">选择学期</label>
				<select onchange="GetCourseListByTremID($('#selTerm').val())" id="selTerm"><option value="0">请选择</option></select>&nbsp; &nbsp;&nbsp;
                <label style="display:inline" class="control-label" for="courseList">选择课程</label>
				<select onchange="SelectCourse()" id="courseList"><option value="0">请选择</option></select>&nbsp; &nbsp;&nbsp;
                <label style="display:inline" class="control-label" for="selStatus">选择状态</label>
				<select onchange="SelectCourse()" id="selStatus"><option value="5">全部</option><option value="0">未完成</option><option value="1">待审批</option><option value="2">正在审批</option><option value="3">重做</option><option value="4">完成</option></select>
            </div>
            <table class="table table-striped table-bordered table-condensed" style="width:95%">
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
            <!-- 分页 -->
			<div class="pagination">
				<ul id="pageNum_nav">
				</ul>
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
    $(function () { //页面加载时执行
        $("#left_nav_ul li:eq(2)").attr('class', 'active'); //获取相应的li，设置class为active
        var geturlID = getParam("cid");
        if (geturlID == "" || geturlID == null) {
            $.ajax({ //添加学期
                type: "post",
                dataType: "json",
                url: "/Handles/StudentHandler.ashx",
                data: "action=GetAllTermForNow",
                success: function (data) {
                    $.each(data.termList, function (index, item) {
                        if (item.termCheck == 1) {
                            col_addSelect("#selTerm", item.id, item.termName, item.id);
                            GetCourseListByTremID(item.id);
                        }
                        else {
                            col_add("#selTerm", item.id, item.termName);
                        }
                    });
                }
            });
        } else { //根据课程ID获取列表信息
            $.ajax({ //添加学期
                type: "post",
                dataType: "json",
                url: "/Handles/StudentHandler.ashx",
                data: "action=GetAllTermForNow",
                async:false,
                success: function (data) {
                    $.each(data.termList, function (index, item) {
                        if (item.termCheck == 1) {
                            col_addSelect("#selTerm", item.id, item.termName, item.id);
                            GetCourseListByTremIDAndcid(item.id, geturlID);
                        }
                        else {
                            col_add("#selTerm", item.id, item.termName);
                        }
                    });
                }
            });
            $.ajax({ //获取信息
                type: "post",
                dataType: "json",
                url: "/Handles/StudentHandler.ashx",
                data: "action=GetHWListNumByCourseIdAndStuID&id=" + geturlID + "&status=5",
                cache: false,
                async: true,
                success: function (data) {
                    pageNumShow(data[0].pagenum, 1);
                }
            });
        }
    });
    //根据学期ID获取课程列表
    function GetCourseListByTremID(id) {
        if (id==0) {return;}
        col_clear("#courseList");//清除内容
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetCourseListByStuandTerm&tremID=" + id,
            cache: false,
            async: true,
            success: function (data) {
                $.each(data.courseList, function (index, item) { col_add("#courseList", item.id, item.course_name); });
            }
        });
    }
    //根据学期ID和课程ID获取课程列表
    function GetCourseListByTremIDAndcid(id,cid) {
        if (id == 0) { return; }
        col_clear("#courseList"); //清除内容
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetCourseListByStuandTerm&tremID=" + id,
            cache: false,
            async: true,
            success: function (data) {
                $.each(data.courseList, function (index, item) {
                    col_addSelect("#courseList", item.id, item.course_name, cid); 
                 });
            }
        });
    }
    //当课程选项改变时
    function SelectCourse() {
        var courseID = $('#courseList').val();
        var status = $('#selStatus').val(); //获取作业的状态
        $('#h1small').empty();
        $('#h1small').append($('#courseList option:selected').text());
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetHWListNumByCourseIdAndStuID&id=" + courseID + "&status=" + status,
            cache: false,
            async: true,
            success: function (data) {
                $('#pageNum_nav').empty();
                pageNumShow(data[0].pagenum, 1);
            }
        });
    }

    //根据不同的作业的状态来插入相应的内容
    function addtabletr(id, homeworkID, courseName, homeworkName, homeworkTime, teacher, status) {
        var statusName;
        var statusSet;
        if (status == 0) { //没有完成时显示的状态
            statusName = '<span class="label label-important">未完成</span>';
            statusSet = '<li><a href="Homework.aspx?id=' + homeworkID + '"><i class="icon-eye-open"></i> 查看作业</a></li><li><a href="EditHomework.aspx?id=' + homeworkID + '"><i class="icon-pencil"></i> 提交作业</a></li>';
        }
        else if (status == 1) { //已经提交时的状态（表示可以修改）
            statusName = '<span class="label label-warning">已提交</span>';
            statusSet = '<li><a href="Homework.aspx?id=' + homeworkID + '"><i class="icon-eye-open"></i> 查看作业</a></li><li><a href="EditHomework.aspx?id=' + homeworkID + '"><i class="icon-pencil"></i> 修改作业</a></li>';
        }
        else if (status == 2) { //正在批阅的状态
            statusName = '<span class="label label-info">批阅中</span>';
            statusSet = '<li><a href="Homework.aspx?id=' + homeworkID + '"><i class="icon-eye-open"></i> 查看作业</a></li><li><a href="EditHomework.aspx?id=' + homeworkID + '"><i class="icon-check"></i> 查看提交</a></li>';
        }
        else if (status == 3) { //重做的状态
            statusName = '<span class="label label-inverse">重做</span>';
            statusSet = '<li><a href="Homework.aspx?id=' + homeworkID + '"><i class="icon-eye-open"></i> 查看作业</a></li><li><a href="EditHomework.aspx?id=' + homeworkID + '"><i class="icon-pencil"></i> 修改作业</a></li><li><a href="LookHomework.aspx?id=' + homeworkID + '"><i class="icon-user" ></i> 查看批阅</a></li>';
        }
        else if (status == 4) { //已经完成的作业状态
            statusName = '<span class="label label-success">已完成</span>';
            statusSet = '<li><a href="Homework.aspx?id=' + homeworkID + '"><i class="icon-eye-open"></i> 查看作业</a></li><li><a href="EditHomework.aspx?id=' + homeworkID + '"><i class="icon-check"></i> 查看提交</a></li><li><a href="LookHomework.aspx?id=' + homeworkID + '"><i class="icon-user" ></i> 查看批阅</a></li>';
        }
        $('#defaultAddTr').append('<tr class="list-users"><td>' + homeworkID + '</td><td>' + courseName + '</td><td>' + homeworkName + '</td><td>' + homeworkTime + '</td><td>' + teacher + '</td><td>' + statusName + '</td><td><div class="btn-group"><a class="btn btn-mini dropdown-toggle" data-toggle="dropdown" href="#">操作 <span class="caret"></span></a><ul class="dropdown-menu">' + statusSet + '</ul></div></td></tr>');
    }

    //分页代码
    function pageNumShow(totalNum, nowPageNum) {
        if (nowPageNum == $("#liactive").text()) {
            return;
        }
        else {
            setHWlistForPage(nowPageNum);//加载新的页码的内容
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

    //点击了页码之后加载不同的数据
    function setHWlistForPage(pageNum) {
        $("#defaultAddTr").empty(); //先清除所有的内容
        var courseID = $('#courseList').val(); //获取课程ID
        var status = $('#selStatus').val(); //获取作业的状态
        var parm = "action=GetHWListByCourseIdAndStuID&page=" + pageNum + "&id=" + courseID + "&status=" + status;
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
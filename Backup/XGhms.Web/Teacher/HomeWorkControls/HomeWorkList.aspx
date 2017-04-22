<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeWorkList.aspx.cs" Inherits="XGhms.Web.Teacher.HomeWorkControls.HomeWorkList" %>
<%@ Register TagPrefix="uctrols" TagName="terTopNav" Src="~/Teacher/MyControls/TerTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="terLeftNav" Src="~/Teacher/MyControls/TerLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我发布的作业列表 - XGhms</title>
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
            <div class="row-fluid">
                <div class="page-header">
				    <h1><span id="title_h1">我发布的作业列表 </span> <small id="h1small"> </small></h1>
			    </div>
                <div class="control-group">
					<label style="display:inline" class="control-label" for="selTerm">选择学期</label>
					<select onchange="SelectTerm()" id="selTerm"><option value="0">请选择</option></select>&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label" for="selCourse">选择课程</label>
					<select onchange="SelectCourse()" id="selCourse"><option value="0">请选择</option></select>
				</div>
                <table class="table table-striped table-bordered table-condensed" style="width:95%">
				    <thead>
					    <tr>
						    <th>ID</th>
				            <th>作业名称</th>
				            <th>课程名</th>
				            <th>作业开始时间</th>
				            <th>作业结束时间</th>
                            <th></th>
					    </tr>
				    </thead>
                    <tbody id="defaultAddTr">
                    </tbody>
			    </table>
                <!-- 分页 -->
			    <div class="pagination">
				    <ul id="pageNum_nav"></ul>
			    </div>	
			
                <div class="form-actions">
					<a id="hwEditId" type="button" href="../CourseControls/MyCourse.aspx" class="btn btn-success btn-large">新建作业</a>
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
        $.ajax({ //添加学期
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetAllTermForNow",
            success: function (data) {
                $.each(data.termList, function (index, item) {
                    if (item.termCheck == 1) {
                        col_addSelect("#selTerm", item.id, item.termName, item.id);
                        $('#h1small').append(item.termName);
                        GetCourselistByTrem(item.id); //根据学期获取课程
                    }
                    else {
                        col_add("#selTerm", item.id, item.termName);
                    }
                });
            }
        });
    });
    //根据学期获取课程的列表
    function GetCourselistByTrem(tremID) {
        $.ajax({ //添加课程下拉列表
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetCourseListByTerandTerm&tremID=" + tremID,
            success: function (data) {
                $.each(data.courseList, function (index, item) {
                    col_add("#selCourse", item.id, item.course_name);
                });
            }
        });
    }
    function SelectTerm() {
        var termID = $('#selTerm').val();
        GetCourselistByTrem(termID);
    }
    function SelectCourse() {
        var courseID = $('#selCourse').val(); //获取课程ID
        if (courseID==0) {
            return;
        }
        $('#hwEditId').attr('href', 'HomeWorkManage.aspx?cid=' + courseID);
        $("#pageNum_nav").empty(); //先清除所有的内容
        GetHomeWorkTotalNumBytermAndCor();
        
    }
    //根据学期和课程来查询作业的总数，用于分页
    function GetHomeWorkTotalNumBytermAndCor() {
        var courseID = $('#selCourse').val(); //获取课程ID
        $.ajax({ //获取作业列表
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetHomeWorkListBytermAndCor&courseID=" + courseID,
            success: function (data) {
                pageNumShow(data.pagenum, 1);
            }
        });
    }
    //根据学期和课程来查询作业的列表
    function GetHomeWorkListBytermAndCor(nowPageNum) {
        var tremID=$('#selTerm').val(); //获取学期表
        var courseID=$('#selCourse').val(); //获取课程ID
        $.ajax({ //获取作业列表
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetHomeWorkTotalNumBytermAndCor&courseID=" + courseID + "&nowPageNum=" + nowPageNum,
            success: function (data) {
                $('#defaultAddTr').empty();
                $.each(data.hwList, function (index, item) {
                    AddTrList(item.id, item.homework_name, item.course_name, item.homework_beginTime, item.homework_endTime);
                });
            }
        });
    }
    //添加table代码
    function AddTrList(ID, hwName, courseName, hwBegin, hwEnd) {
        var statusSet = '<li><a href="HomeWorkManage.aspx?id=' + ID + '"><i class="icon-eye-open"></i> 查看作业</a></li><li><a href="javascript:deleteHW(' + ID + ')"><i class="icon-remove"></i> 删除作业</a></li>';
        var str = '<td><div class="btn-group"><a class="btn btn-mini dropdown-toggle" data-toggle="dropdown" href="#">操作 <span class="caret"></span></a><ul class="dropdown-menu">' + statusSet + '</ul></div></td>';
        $('#defaultAddTr').append('<tr class="list-users"><td>' + ID + '</td><td>' + hwName + '</td><td>' + courseName + '</td><td>' + hwBegin + '</td><td>' + hwEnd + '</td>' + str + '</tr>');
    }
    function deleteHW(id) {
        if (!confirm("确定要删除该作业？")) {
            return;
        }
        $.ajax({ //获取作业列表
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=deleteHomeWork&id=" + id,
            success: function (data) {
                alert(data.msg);
            }
        });
        SelectCourse();
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
            GetHomeWorkListBytermAndCor(nowPageNum); //加载新的页码的内容
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
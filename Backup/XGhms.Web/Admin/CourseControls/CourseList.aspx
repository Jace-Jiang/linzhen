<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseList.aspx.cs" Inherits="XGhms.Web.Admin.CourseControls.CourseList" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>班级列表 - XGhms</title>
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
            <div class="row-fluid">
                <div class="page-header">
				    <h1>课程列表 <small id="h1small"> </small></h1>
			    </div>
                <div class="control-group">
					<label style="display:inline" class="control-label" for="selTerm">选择学期</label>
					<select onchange="SelectTerm()" id="selTerm"><option value="0">请选择</option></select>&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label" for="collegeName">选择学院</label>
					<select onchange="SelectCollege()" id="collegeName"><option value="0">请选择</option></select>
				</div>
                <table class="table table-striped table-bordered table-condensed" style="width:95%">
				    <thead>
					    <tr>
						    <th>ID</th>
				            <th>课程编号</th>
				            <th>课程名</th>
				            <th>课程老师</th>
				            <th>所在学院</th>
                            <th>修改课程</th>
                            <th>删除课程</th>
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
					<a id="hwEditId" type="button" href="CourseManage.aspx" class="btn btn-success btn-large">新建课程</a>
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
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetAllTermForNow",
            success: function (data) {
                $.each(data.termList, function (index, item) {
                    if (item.termCheck == 1) {
                        col_addSelect("#selTerm", item.id, item.termName, item.id);
                        $('#h1small').append(item.termName);
                    }
                    else {
                        col_add("#selTerm", item.id, item.termName);
                    }
                });
            }
        });
        $.ajax({ //添加学院
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { col_add("#collegeName", item.id, item.collegeName); });
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
        $("#collegeName").val(0);
    }
    //当学院选项发生变化后
    function SelectCollege() {
        if ($('#collegeName option:selected').val() == 0) {
            return;
        }
        $('#defaultAddTr').empty();
        var tremID = $('#selTerm option:selected').val();
        var collegeID = $('#collegeName option:selected').val();
        $.ajax({ //获取当前的查询条件下面的条数
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetNumTotalByCourseList&tremID=" + tremID + "&collegeID=" + collegeID,
            success: function (data) {
                $('#pageNum_nav').empty();
                pageNumShow(data.pagenum, 1);
            }
        });
    }

    function SelectClassByCollegeID() {
        if ($('#collegeName option:selected').val() == 0) {
            return;
        }
        $('#defaultAddTr').empty();
        $('#h1small').empty();
        var collegeID = $('#collegeName option:selected').val(); //获取学院ID
        $('#h1small').append($('#collegeName option:selected').text());
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetAllClassByCollegeID&collegeID=" + collegeID,
            success: function (data) {
                $.each(data.classList, function (index, item) { AddTrList(item.id, item.className, item.collegeName, item.headerTer, item.stuLeader); });
            }
        });
    }

    function AddTrList(ID,courseNum, courseName,teacher ,collegeName) {
        $('#defaultAddTr').append('<tr class="list-users"><td>' + ID + '</td><td>' + courseNum + '</td><td>' + courseName + '</td><td>' + teacher + '</td><td>' + collegeName + '</td><td><a href="CourseManage.aspx?id=' + ID + '" class="label label-info"><i class="icon-pencil"></i> 修改课程</a></td><td><a href="javascript:deleteCourse(' + ID + ')" class="label label-important"><i class="icon-remove"></i> 删除课程</a></td></tr>');
    }
    //删除课程
    function deleteCourse(id) {
        if (confirm("你确信要删除该课程？\n删除该课程记得将学生的信息修改")) {
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Handles/AdminHandler.ashx",
                data: "action=DeleteCourseByID&id=" + id,
                success: function (data) {
                    alert(data.msg);
                },
                error: function () {
                    alert("未知错误！");
                }
            });
        }
        else {
            return;
        }
    }

    //根据页码来查询数据
    function setCourselistForPage(nowPageNum) {
        var tremID = $('#selTerm option:selected').val();
        var collegeID = $('#collegeName option:selected').val();
        $.ajax({ //获取当前的查询条件下面的条数
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=setCourselistForPage&tremID=" + tremID + "&collegeID=" + collegeID + "&page=" + nowPageNum,
            success: function (data) {
                $.each(data.courseList, function (index, item) {
                    AddTrList(item.id, item.course_number, item.course_name, item.teacher, item.college);
                });
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
            setCourselistForPage(nowPageNum); //加载新的页码的内容
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
</script>
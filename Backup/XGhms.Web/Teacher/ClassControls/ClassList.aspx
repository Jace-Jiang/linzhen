<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassList.aspx.cs" Inherits="XGhms.Web.Teacher.ClassControls.ClassList" %>
<%@ Register TagPrefix="uctrols" TagName="terTopNav" Src="~/Teacher/MyControls/TerTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="terLeftNav" Src="~/Teacher/MyControls/TerLeftNav.ascx" %>
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
<uctrols:terTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:terLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="row-fluid">
                <div class="page-header">
				    <h1><span id="title_h1">班级列表</span> <small id="h1small"> </small></h1>
			    </div>
                <div id="oneDiv" class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
						    <label class="control-label" for="collegeName">选择学院</label>
						    <div class="controls">
							    <select onchange="SelectClassByCollegeID()" id="collegeName"><option value="0">请选择</option></select>
						    </div>
					    </div>
                    </fieldset>
                </div>
                <table class="table table-striped table-bordered table-condensed" style="width:95%">
				    <thead>
					    <tr>
						    <th>ID</th>
				            <th>班级名称</th>
				            <th>所属学院</th>
				            <th>辅导员</th>
				            <th>班长</th>
                            <th>查看</th>
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
        $("#left_nav_ul li:eq(10)").attr('class', 'active'); //获取相应的li，设置class为active
        $.ajax({  //获取学院列表
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { col_add("#collegeName", item.id, item.collegeName); });
            }
        });
    });

    function SelectClassByCollegeID() {
        if ($('#collegeName option:selected').val() == 0) {
            return;
        }
        $('#defaultAddTr').empty();
        $('#h1small').empty();
        var collegeID = $('#collegeName option:selected').val(); //获取学院ID
        $('#h1small').append($('#collegeName option:selected').text());
        $.ajax({  //根据学院ID获取班级列表
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetAllClassByCollegeID&collegeID=" + collegeID,
            success: function (data) {
                $.each(data.classList, function (index, item) { AddTrList(item.id, item.className, item.collegeName, item.headerTer, item.stuLeader); });
            }
        });
    }

    function AddTrList(ID, className, collegeName, HeaderTer, ClsLeader) {
        $('#defaultAddTr').append('<tr class="list-users"><td>' + ID + '</td><td>' + className + '</td><td>' + collegeName + '</td><td>' + HeaderTer + '</td><td>' + ClsLeader + '</td><td><a href="ClassManage.aspx?id=' + ID + '" class="label label-info"><i class="icon-pencil"></i> 班级信息</a></td></tr>');
    }
    // 添加下拉列表选项
    function col_add(selectobj, value, text) {
        var selObj = $(selectobj);
        selObj.append("<option value='" + value + "'>" + text + "</option>");
    }
</script>
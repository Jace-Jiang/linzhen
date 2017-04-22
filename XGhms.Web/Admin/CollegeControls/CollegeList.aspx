<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeList.aspx.cs" Inherits="XGhms.Web.Admin.CollegeControls.CollegeList" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学院列表 - XGhms</title>
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
				    <h1>学院部门列表 <small id="h1small"> 全部列表</small></h1>
			    </div>
                <table class="table table-striped table-bordered table-condensed" style="width:95%">
				    <thead>
					    <tr>
						    <th>ID</th>
				            <th>学院名称</th>
				            <th>学院管理员</th>
				            <th>修改</th>
				            <th>删除</th>
					    </tr>
				    </thead>
                    <tbody id="defaultAddTr">
                    </tbody>
			    </table>
                <div class="form-actions">
					<a id="hwEditId" type="button" href="CollegeManage.aspx" class="btn btn-success btn-large">新建学院</a>
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
        GetAllCollegeList();
    });
    //获取学院列表
    function GetAllCollegeList() {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { addAllTr(item.id, item.collegeName, item.collegeAdmin) });
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }
    //添加table
    function addAllTr(collegeID, collegeName, collegeAdmin) {
        $('#defaultAddTr').append('<tr class="list-users"><td>' + collegeID + '</td><td>' + collegeName + '</td><td>' + collegeAdmin + '</td><td><a href="CollegeManage.aspx?id=' + collegeID + '" class="label label-info"><i class="icon-pencil"></i> 修改</a></td><td><a href="javascript:deleteCollege(' + collegeID + ')" class="label label-important"><i class="icon-remove"></i> 删除</a></td></tr>');
    }

    function deleteCollege(id) {
        if (confirm("你确信要删除该学院？")) {
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Handles/AdminHandler.ashx",
                data: "action=DeleteCollegeByID&id=" + id,
                success: function (data) {
                    alert(data.msg);
                    $('#defaultAddTr').empty();
                    GetAllCollegeList();
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
</script>
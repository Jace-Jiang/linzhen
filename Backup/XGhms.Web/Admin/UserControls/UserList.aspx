<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="XGhms.Web.Admin.UserControls.UserList" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户列表 - XGhms</title>
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
				    <h1>用户列表 <small id="h1small"> 学生</small></h1>
			    </div>
                <div class="control-group">
                    <label style="display:inline" class="control-label" for="role">选择角色</label>
                    <select id="role" onchange="roleChange()">
						<option selected="selected" value="Student">学生</option>
                        <option value="Teacher">老师</option>
                        <option value="HeadTeacher">辅导员</option>
                        <option value="CollegeAdmin">学院管理员</option>
                        <option value="Admin">系统管理员</option>
                        <option value="Administrator">超级管理员</option>
					</select>&nbsp; &nbsp;&nbsp;
					<label style="display:inline" class="control-label" for="selCollege">选择学院</label>
					<select onchange="SelectCollege()" id="selCollege"><option value="0">请选择</option></select>&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label" for="selClass">选择班级</label>
					<select onchange="SelectClass()" id="selClass"><option value="0">请选择</option></select>
				</div>
                <table class="table table-striped table-bordered table-condensed" style="width:95%">
				    <thead>
					    <tr>
						    <th>用户名</th>
				            <th>学号/工号</th>
				            <th>姓名</th>
				            <th>性别</th>
				            <th>专业</th>
                            <th>激活</th>
                            <th>修改</th>
                            <th>删除</th>
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
					<a id="hwEditId" type="button" href="AddUser.aspx" class="btn btn-success btn-large">新建用户</a>
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
        $("#left_nav_ul li:eq(10)").attr('class', 'active'); //获取相应的li，设置class为active
        $.ajax({ //添加学院
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { col_add("#selCollege", item.id, item.collegeName); });
            }
        });
        SelectUserListTotalBySelter();
    });

    //当角色选项发生变化时
    function roleChange() {
        var UserRole = $('#role').val(); //获取角色名
        if (UserRole != "Student") {
            $('#selClass').attr('disabled', 'disabled');
        }
        if (UserRole == "Student") {
            $("#selClass").removeAttr("disabled");
            $('#selCollege').removeAttr('disabled');
        }
        if (UserRole == "Admin" || UserRole == "Administrator" || UserRole == "CollegeAdmin") {
            $('#selCollege').attr('disabled', 'disabled');
        }
        if (UserRole != "Admin" && UserRole != "Administrator" && UserRole != "CollegeAdmin") {
            $('#selCollege').removeAttr('disabled');
        }
        $('#h1small').empty();
        $('#h1small').append($("#role option:selected").text());
        SelectUserListTotalBySelter();
    }
    //当学院选项发生变化后,更新班级列表
    function SelectCollege() {
        SelectUserListTotalBySelter();
        if ($('#selCollege option:selected').val() == 0) {
            return;
        }
        var collegeID = $('#selCollege option:selected').val();
        $.ajax({ //获取当前的查询条件下面的条数
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetAllClassByCollegeID&collegeID=" + collegeID,
            success: function (data) {
                col_clear('#selClass');
                $.each(data.classList, function (index, item) { col_add("#selClass", item.id, item.className); });
            }
        });
    }
    //当班级选择变化后
    function SelectClass() {
        SelectUserListTotalBySelter();
    }
    //根据所有的条件进行查询，输出总数
    function SelectUserListTotalBySelter() {
        var UserRole = $('#role').val(); //获取角色名
        var CollegeID = $('#selCollege').val(); //获取学院ID
        var ClassID = $('#selClass').val(); //获取班级ID
        var parms = "action=SelectUserListTotalBySelter&UserRole=" + UserRole + "&CollegeID=" + CollegeID + "&ClassID=" + ClassID;
        $.ajax({ //获取用户列表
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: parms,
            success: function (data) {
                $('#pageNum_nav').empty();
                pageNumShow(data.pagenum, 1);
            }
        });
    }
    //根据所有的条件进行查询并且显示在页面
    function SelectUserListBySelter(nowPageNum) {
        var UserRole = $('#role').val(); //获取角色名
        var CollegeID = $('#selCollege').val(); //获取学院ID
        var ClassID = $('#selClass').val(); //获取班级ID
        var parms = "action=SelectUserListForAdmin&UserRole=" + UserRole + "&CollegeID=" + CollegeID + "&ClassID=" + ClassID + "&nowPageNum=" + nowPageNum;
        $.ajax({ //获取用户列表
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: parms,
            success: function (data) {
                $('#defaultAddTr').empty(); //先清除所有的内容
                $.each(data.userList, function (index, item) {
                    AddTrList(item.id,item.userName, item.userNum, item.realName, item.userSex, item.userMajor, item.userLock);
                });
            }
        });
    }
    
    //表格添加代码
    function AddTrList(ID, userName, userNum, realName, userSex, userMajor, userLock) {
        var isLock = '';
        if (userLock==0) {
            isLock = '<td><span class="label label-success">已激活</span></td>';
        }
        if (userLock==1) {
            isLock = '<td><span class="label label-inverse">锁定</span></td>';
        }
        $('#defaultAddTr').append('<tr class="list-users"><td>' + userName + '</td><td>' + userNum + '</td><td>' + realName + '</td><td>' + userSex + '</td><td>' + userMajor + '</td>' + isLock + '<td><a href="AddUser.aspx?id=' + ID + '" class="label label-info"><i class="icon-pencil"></i> 修改</a></td><td><a href="javascript:deleteUser(' + ID + ')" class="label label-important"><i class="icon-remove"></i> 删除</a></td></tr>');
    }
    //删除用户
    function deleteUser(id) {
        if (confirm("你确信要删除该用户？")) {
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Handles/AdminHandler.ashx",
                data: "action=DeleteUserByID&id=" + id,
                success: function (data) {
                    alert(data.msg);
                    SelectUserListTotalBySelter();
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
            SelectUserListBySelter(nowPageNum); //加载新的页码的内容
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

    // 添加下拉列表选项
    function col_add(selectobj, value, text) {
        var selObj = $(selectobj);
        selObj.append("<option value='" + value + "'>" + text + "</option>");
    }
    // 清空下拉列表选项并且加一个全选的按钮
    function col_clear(colobject) {
        var selOpt = $(colobject + " option");
        selOpt.remove();
        col_add(colobject, 0, "请选择"); //默认添加有全部请选择的选项
    }
</script>
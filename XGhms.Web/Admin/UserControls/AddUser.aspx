<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="XGhms.Web.Admin.UserControls.AddUser" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理 - XGhms</title>
    <link href="../../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/site.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
</head>
<body>
<%--<form id="form1" runat="server">--%>
<uctrols:admTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:admLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="row-fluid">
                <div class="page-header">
				    <h1>用户管理 <small id="h1small"> </small></h1>
			    </div>
                <h4 id="h4title" style="margin:7px 0 7px;"></h4>
                <div id="addNewUserDiv"> 选择新建方式:
                <label class="radio inline"><input name="optionsRadios" type="radio" id="opsOne" value="单个">单个新建</label>
                <label class="radio inline"><input name="optionsRadios" type="radio" id="opsAll" value="批量">批量新建</label><br /><br /></div>
                <div style="margin:7px 0 7px; display:none;" id="oneDiv" class="form-horizontal">
                    <fieldset>
					    <div class="control-group">
						    <label class="control-label" for="userName">用户名(学号/工号)</label>
						    <div class="controls">
							    <input type="text" id="userName" value="" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="role">选择角色</label>
						    <div class="controls">
							    <select id="role" onchange="roleChange()">
								    <option value="Student">学生</option>
                                    <option value="Teacher">老师</option>
                                    <option value="HeadTeacher">辅导员</option>
                                    <option value="CollegeAdmin">学院管理员</option>
                                    <option value="Admin">系统管理员</option>
                                    <option value="Administrator">超级管理员</option>
							    </select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="realName">真实姓名</label>
						    <div class="controls">
							    <input type="text" id="realName" value="" />
						    </div>
					    </div>
                        <div class="control-group">
                            <label class="control-label" for="userSex">性别</label>
                            <div class="controls">
							    <label class="radio inline"><input checked="checked" name="userSex" type="radio" id="sexMan" value="1">男</label>
                                <label class="radio inline"><input name="userSex" type="radio" id="sexWoman" value="0">女</label>
						    </div>
                        </div>
                        <div class="control-group">
						    <label class="control-label" for="majorName">专业</label>
						    <div class="controls">
							    <input type="text" id="majorName" value="" />
						    </div>
					    </div>
					    <div class="control-group">
						    <label class="control-label" for="collegeName">学院</label>
						    <div class="controls">
							    <select id="collegeName" onchange="SelectCollegeForClassList()"><option value="0">请选择</option></select>
						    </div>
					    </div>
					    <div class="control-group">
						    <label class="control-label" for="selClassAll">班级</label>
						    <div class="controls">
							    <select id="selClassAll"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div id="resetPwdDiv" class="control-group">
						    <label class="control-label">重置密码</label>
						    <div class="controls">
							    <input id="resetPwd" onclick="resetPwd()" type="button" class="btn" value="重置用户密码" />   <small>重置后密码为123456</small>
						    </div>
					    </div>
					    <div class="control-group">
						    <label class="control-label" for="active">激活用户</label>
						    <div class="controls">
							    <input type="checkbox" id="active" value="1" checked="checked" />
						    </div>
					    </div>	
                        <div class="form-actions">
						    <input id="btnSave" type="button" onclick="ClickSaveUser()" class="btn btn-success btn-large" value="保存" /> <a id="successchangelink" class="btn" href="UserList.aspx">取消</a>
					    </div>		
				    </fieldset>
                </div>
                <div style="margin:7px 0 7px; display:none;" id="allDiv" class="form-horizontal">
                    <div id="selAlladd" style="margin:7px 0 7px;">
                        <h4 style="margin:7px 0 7px;">批量添加</h4>
                        <form class="form-search" id="Form2" method="post"><!--method="post"不能省略，在ie里面必不可少-->  
                            <input id="btnfile" name="btnfile" type="file" value="提交" class="input-medium search-query"/>
                            <input  id="btn" type="button"  value="导入" class="btn"/>
                        </form>批量添加学生的示例文件<a href="../../Upload/demo/批量添加学生示例.xls">点击下载</a><br />
                        <small>这里只能批量导入学生，默认密码为123456，请按照正确格式导入，每次建议导入不要超过100个。已经存在的用户会根据导入的信息修改他的信息</small>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uctrols:downFooter ID="AllDownFooter" runat="server" />
</div>
<%--</form>--%>
</body>
</html>
<script src="../../Script/MyAjaxForm.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("#left_nav_ul li:eq(11)").attr('class', 'active'); //获取相应的li，设置class为active
        var geturlID = getParam("id");
        if (geturlID == null || geturlID == "") {
            $('#h1small').append("添加用户");
            $('#h4title').append("新建用户:");
            $('#resetPwdDiv').css('display', 'none');
            GetAllCollegeListByAdd();
        }
        else {
            $('#userName').attr('readonly', 'readonly');
            $('#h4title').append("修改用户:");
            $('#addNewUserDiv').css('display', 'none');
            $("#oneDiv").css('display', 'block');
            setUserInfoByID(geturlID);
        }
    });

    //新建的状态下获取学院列表
    function GetAllCollegeListByAdd() {
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { col_add("#collegeName", item.id, item.collegeName); });
            }
        });
    }
    //修改状态下的获取用户信息
    function setUserInfoByID(id) {
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetUserInfoByID&id=" + id,
            success: function (data) {
                $('#h1small').append(data.userName);
                $('#userName').val(data.userName);
                $("#role").val(data.userRoleName);
                $('#realName').val(data.userRealName);
                $('#majorName').val(data.userMajor);
                if (data.userSex == 0) {
                    $('#sexWoman').attr('checked', 'checked');
                }
                if (data.userLock == 1) {
                    $('#active').removeAttr('checked');
                }
                if (data.userCollegeID == null || data.userCollegeID == "") {
                    GetAllCollegeListByChange(0);
                }
                else {
                    GetAllCollegeListByChange(data.userCollegeID);
                    if (data.userClassID == null || data.userClassID == "") {
                        return;
                    }
                    else {
                        GetClassListByCollege(data.userCollegeID, data.userClassID);
                    }
                }
            }
        });
    }
    //修改的状态下添加学院列表
    function GetAllCollegeListByChange(selCollegeID) {
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { col_addSelect("#collegeName", item.id, item.collegeName, selCollegeID); });
            }
        });
    }
    //修改状态下面根据学院ID添加班级列表
    function GetClassListByCollege(collegeID,classID) {
        col_clear("#selClassAll"); //清除已有的选项
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetAllClassByCollegeID&collegeID=" + collegeID,
            success: function (data) {
                $.each(data.classList, function (index, item) { col_addSelect("#selClassAll", item.id, item.className, classID); });
            }
        });
    }
    //点击学院的时候更新班级列表
    function SelectCollegeForClassList() {
        var collegeID = $('#collegeName').val();
        if (collegeID==0) {
            return;
        }
        col_clear("#selClassAll"); //清除已有的选项
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetAllClassByCollegeID&collegeID=" + collegeID,
            success: function (data) {
                $.each(data.classList, function (index, item) { col_add("#selClassAll", item.id, item.className); });
            }
        });
    }
    //当角色发生变化时
    function roleChange() {
        var UserRole = $('#role').val(); //获取角色名
        if (UserRole!="Student") {
            $('#majorName').attr('readonly', 'readonly');
            $('#selClassAll').attr('disabled', 'disabled');
        }
        if (UserRole == "Student") {
            $("#majorName").removeAttr("readonly");
            $('#selClassAll').removeAttr('disabled');
        }
        if (UserRole == "Admin" || UserRole == "Administrator" || UserRole == "CollegeAdmin") {
            $('#collegeName').attr('disabled', 'disabled');
        } 
        if (UserRole != "Admin" && UserRole != "Administrator" && UserRole != "CollegeAdmin") {
            $('#collegeName').removeAttr('disabled');
        }
    }
    //当点击保存按钮时触发事件
    function ClickSaveUser() {
        var collegeID = $('#collegeName').val(); //学院ID
        var classID = $("#selClassAll").val(); //班级ID
        var UserRealName = $('#realName').val(); //用户的真实姓名
        var UserName = $('#userName').val(); //用户名
        var UserRole = $('#role').val(); //获取角色名
        var UserSex = 1; //获取用户性别
        if ($('#sexWoman').is(':checked')) {
            UserSex = 0;
        }
        var IsLock = 1; //获取是否锁定用户
        if ($('#active').is(':checked')) {
            IsLock = 0;
        }
        var majorName = $('#majorName').val(); //获取专业名称
        if (UserName == "") {
            alert("用户名（学号/教工号）不能为空！");
            $('#userName').focus();
            return;
        }
        if (UserRealName == "") {
            alert("姓名不能为空！");
            $('#realName').focus();
            return;
        }
        if (UserRole == "Student") { //角色为学生时
            if (collegeID == 0) {
                alert("请选择学院！");
                $('#collegeName').focus();
                return;
            }
            if (classID == 0) {
                alert("请选择班级！");
                $('#selClassAll').focus();
                return;
            }
            if (majorName=="") {
                alert("请输入专业！");
                $('#majorName').focus();
                return;
            }
        }
        if (UserRole == "Teacher") { //角色为老师时
            if (collegeID == 0) {
                alert("请选择学院！");
                $('#collegeName').focus();
                return;
            }
        }
        if (UserRole == "HeadTeacher") { //角色为辅导员时
            if (collegeID == 0) {
                alert("请选择学院！");
                $('#collegeName').focus();
                return;
            }
        }
        var geturlID = getParam("id"); //获取学生ID 
        var parms = "";  //临时变量
        if (geturlID == null || geturlID == "") {
            parms = "action=AddNewUserByAdmin&UserRole=" + UserRole + "&UserName=" + UserName + "&UserRealName=" + UserRealName + "&collegeID=" + collegeID + "&classID=" + classID + "&UserSex=" + UserSex + "&isLock=" + IsLock + "&majorName=" + majorName;
        }
        else {
            parms = "action=UpdateOldUserByAdmin&UserRole=" + UserRole + "&id=" + geturlID + "&UserRealName=" + UserRealName + "&collegeID=" + collegeID + "&classID=" + classID + "&UserSex=" + UserSex + "&isLock=" + IsLock + "&majorName=" + majorName; 
        }
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: parms,
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回用户列表');
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }
    //点击重置密码之后触发事件
    function resetPwd() {
        if (!confirm("你确信要重置该用户的密码？\n重置后密码为123456")) {
            return;
        }
        var geturlID = getParam("id"); //获取学生ID
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=ResetPwdFromAdmin&id=" + geturlID,
            success: function (data) {
                alert(data.msg);
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }
    //批量保存
    $("#btn").click(function () { 
        $("#Form2").ajaxSubmit({
            url: "/Handles/AdminUploadHandler.ashx",
            type: "post",
            dataType: "json",
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回用户列表');
            }
        });
    });  

    $("#opsOne").click(function () {
        $("#oneDiv").css('display', 'block');
        $("#allDiv").css('display', 'none');
    });
    $("#opsAll").click(function () {
        $("#oneDiv").css('display', 'none');
        $("#allDiv").css('display', 'block');
    });
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
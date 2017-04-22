<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="XGhms.Web.Admin.Search.SearchUser" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户搜索 - XGhms</title>
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
				    <h1>查询用户 <small id="h1small"> 精确查询</small></h1>
			    </div>
                <h3>输入查询:</h3><br />
                <div class="form-search">
                  <input id="seachText" type="text" class="input-medium search-query"/>
                  <button id="searchBtn" data-toggle="modal" data-target="#myModal" type="button" onclick="showUserList()" class="btn">Search</button>  <small class="muted">支持学号或者姓名</small>
                </div>
                <!-- sample modal content -->
                <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 id="myModalLabel">查询到的结果如下</h3>
                    </div>
                    <div class="modal-body">
                        <h4 style="margin:7px 0 7px;">搜索结果:</h4> 
                        <table class="table table-bordered">
                          <thead>
			                <tr>
				                <th>ID</th>
				                <th>姓名</th>
				                <th>学院</th>
				                <th>查看详情</th>
                                <th>修改用户</th>
			                </tr>
		                  </thead>
                          <tbody id="userListTb">
                          </tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button class="btn" data-dismiss="modal">取消</button>
                        <button class="btn btn-primary" data-dismiss="modal" onclick="">确定</button>
                    </div>
                </div>
                <!--End model-->
                <br /><h3>用户信息:</h3><br />
                <div id="teacherDiv" style="display:none;">
                <div class="row-fluid">
                    <div class="span4" style="margin-left: 2.564102564%;"><h4>姓名: <span id="ter_name" style="font-weight:normal; margin-right:20px;"> </span></h4></div>
                    <div class="span4"><h4>教工号: <span id="ter_num" style="font-weight:normal; margin-right:20px;"> </span></h4></div>
                    <div class="span4"><h4>所在部门: <span id="ter_college" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>E-mail: <span id="ter_email" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>电话: <span id="ter_tel" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>生日: <span id="ter_bir" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>地址: <span id="ter_adress" style="font-weight:normal;"> </span></h4></div>
                </div>
                <h3>个人简介:</h3><br /><p id="ter_p"></p>
                </div>
                <div id="studentDiv" style="display:none;">
                <div class="row-fluid">
                    <div class="span4" style="margin-left: 2.564102564%;"><h4>姓名: <span id="stu_name" style="font-weight:normal; margin-right:20px;"> </span></h4></div>
                    <div class="span4"><h4>学号: <span id="stu_num" style="font-weight:normal; margin-right:20px;"> </span></h4></div>
                    <div class="span4"><h4>专业: <span id="stu_major" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>所在院系: <span id="stu_college" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>所在班级: <span id="stu_class" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>E-mail: <span id="stu_email" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>电话: <span id="stu_tel" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>生日: <span id="stu_bir" style="font-weight:normal;"> </span></h4></div>
                    <div class="span4"><h4>地址: <span id="stu_adress" style="font-weight:normal;"> </span></h4></div>
                </div>
                <h3>个人简介:</h3><br /><p id="stu_p"></p>
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
        $("#left_nav_ul li:eq(18)").attr('class', 'active'); //获取相应的li，设置class为active
    });
    function showUserList() {
        var str = $('#seachText').val();
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/SearchHandler.ashx",
            data: "action=SearchUserList&str=" + str,
            success: function (data) {
                if (data == false) {
                    userDontExists();
                }
                else {
                    $.each(data.user, function (index, item) { addUserListTb(item.id, item.user_number, item.real_name, item.college_name); });
                }
            },
            error: function () {
                $('#userListTb').empty(); //先清除
                $('#userListTb').append('<tr class="error"><td colspan="5">查询出现错误！</td></tr>');
            }
        });
    }
    function addUserListTb(id, userID, userName, collegeName) {
        $('#userListTb').empty(); //先清除
        $('#userListTb').append('<tr class="pending-user"><td>' + userID + '</td><td>' + userName + '</td><td>' + collegeName + '</td><td><span class="user-actions"><a href="javascript:showUserInfo(' + id + ');" class="label label-info"><i class="icon-search"></i> 查看详细</a></span></td><td><a href="../UserControls/AddUser.aspx?id=' + id + '" class="label label-warning"><i class="icon-pencil"></i> 修改</a></td></tr>');
    }
    function userDontExists() {
        $('#userListTb').empty(); //先清除
        $('#userListTb').append('<tr><td colspan="5">没有找到该用户</td></tr>');
    }

    function showUserInfo(id) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/SearchHandler.ashx",
            data: "action=SearchUserInfo&userID=" + id,
            success: function (data) {
                if (data.userinfo[0].role_name == "Student") {
                    showStudentInfo(data.userinfo[0].real_name, data.userinfo[0].user_number, data.userinfo[0].college_name, data.userinfo[0].telephone, data.userinfo[0].birthday, unescape(data.userinfo[0].address), unescape(data.userinfo[0].explain), unescape(data.userinfo[0].major), data.userinfo[0].class_name, data.userinfo[0].email);
                }
                else {
                    showTeacherInfo(data.userinfo[0].real_name, data.userinfo[0].user_number, data.userinfo[0].college_name, data.userinfo[0].telephone, data.userinfo[0].birthday, unescape(data.userinfo[0].address), unescape(data.userinfo[0].explain));
                }
            }
        });
    }

    function showTeacherInfo(ter_name, ter_num, ter_college, ter_tel, ter_bir, ter_adress, ter_p) {
        $('#studentDiv').css("display", "none"); //设置学生的为隐藏
        $('#teacherDiv').css("display", "block"); //设置老师的为显示
        /***清除原有的信息****/
        $('#ter_name').empty();
        $('#ter_num').empty();
        $('#ter_college').empty();
        $('#ter_tel').empty();
        $('#ter_bir').empty();
        $('#ter_adress').empty();
        $('#ter_p').empty();
        /***赋值****/
        $('#ter_name').append(ter_name);
        $('#ter_num').append(ter_num);
        $('#ter_college').append(ter_college);
        $('#ter_tel').append(ter_tel);
        $('#ter_bir').append(ter_bir);
        $('#ter_adress').append(ter_adress);
        $('#ter_p').append(ter_p);
    }

    function showStudentInfo(stu_name, stu_num, stu_college, stu_tel, stu_bir, stu_adress, stu_p, stu_major, stu_class, stu_email) {
        $('#studentDiv').css("display", "block"); //设置学生的为显示
        $('#teacherDiv').css("display", "none"); //设置老师的为隐藏
        /***清除原有的信息****/
        $('#stu_name').empty();
        $('#stu_num').empty();
        $('#stu_college').empty();
        $('#stu_tel').empty();
        $('#stu_bir').empty();
        $('#stu_adress').empty();
        $('#stu_p').empty();
        $('#stu_major').empty();
        $('#stu_class').empty();
        $('#stu_email').empty();
        /***赋值****/
        $('#stu_name').append(stu_name);
        $('#stu_num').append(stu_num);
        $('#stu_college').append(stu_college);
        $('#stu_tel').append(stu_tel);
        $('#stu_bir').append(stu_bir);
        $('#stu_adress').append(stu_adress);
        $('#stu_p').append(stu_p);
        $('#stu_major').append(stu_major);
        $('#stu_class').append(stu_class);
        $('#stu_email').append(stu_email);
    }

    $("body").keydown(function () {
        if (event.keyCode == "13") {//keyCode=13是回车键
            $('#searchBtn').click();
        }
    });
</script>
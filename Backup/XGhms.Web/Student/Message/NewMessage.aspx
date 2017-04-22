<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="XGhms.Web.Student.Message.NewMessage" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建站内消息 - XGhms</title>
    <link href="../../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/site.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
    <script src="../../editor/kindeditor-all.js" type="text/javascript"></script>
    <script src="../../editor/lang/zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="content"]', {
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                items: [
						'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
						'insertunorderedlist', '|', 'emoticons']
            });
        });
		</script>
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
				    <h1> <span id="title_h1">发送消息</span><small><span id="title_h1small">站内信</span></small></h1>
			    </div>
                <div class="form-horizontal">
				    <fieldset>
					    <div class="control-group">
						    <label class="control-label" for="role">收信人</label>
						    <div class="controls">
							    <input type="text" readonly="readonly" class="input-xlarge uneditable-input" id="role" /> 
                                <!-- sample modal content -->
                                <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                    <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                      <h3 id="myModalLabel">选择相应的用户</h3>
                                    </div>
                                    <div class="modal-body">
                                      <h4 style="margin:7px 0 7px;">选择用户类型:</h4> 
                                      <label class="radio inline"><input name="optionsRadios" type="radio" id="opsStu" value="学生">学生</label>
                                      <label class="radio inline"><input name="optionsRadios" type="radio" id="opsTer" value="老师">老师</label>
                                      <label class="radio inline"><input name="optionsRadios" type="radio" id="opsAdm" value="管理员">管理员</label>
                                      <label class="radio inline"><input name="optionsRadios" type="radio" id="opsCls" value="班级">班级</label>
                                      <!--当选择学生时-->
                                      <div id="selStudent" style="display:none;margin:7px 0 7px;">
                                          <h4 style="margin:7px 0 7px;">请选择相应学生</h4>
                                          <span>学院：</span><select id="selstucol" onchange="colChangeSetCls()" class="span3" style="display:inline;"><option value="0">请选择</option></select>
                                          <span>班级：</span><select id="selstuclass" onchange="clsChangeSetStu()" class="span4" style="display:inline;"><option value="0">请选择</option></select>    
                                          <span>用户：</span><select id="selstuuser" onchange="addNewUser($(this).val() ,$(this).find('option:selected').text())" class="span2" style="display:inline;"><option value="0">请选择</option></select>    
                                      </div>
                                      <!--当选择老师时-->
                                      <div id="selTeacher" style="display:none;margin:7px 0 7px;">
                                       <h4 style="margin:7px 0 7px;">请选择相应老师</h4>
                                       <span>学院：</span><select id="seltercol" onchange="colChangeSetTer()" class="span4" style="display:inline;"><option value="0">请选择</option><</select>
                                       <span>教师：</span><select id="selterter" onchange="addNewUser($(this).val() ,$(this).find('option:selected').text())" class="span4" style="display:inline;"><option value="0">请选择</option></select>   
                                      </div>
                                      <!--当选择管理员时-->
                                      <div id="selAdmin" style="margin:7px 0 7px;display:none;">
                                       <h4 style="margin:7px 0 7px;">请选择相应管理员</h4>
                                       <span>管理员：</span><select id="seladmadm" onchange="addNewUser($(this).val() ,$(this).find('option:selected').text())" class="span4" style="display:inline;"><option value="0">请选择</option></select>
                                      </div>
                                      <!--当选择班级时-->
                                      <div id="selClass" style="display:none;margin:7px 0 7px;">
                                       <h4 style="margin:7px 0 7px;">请选择相应班级</h4>
                                       <span>学院：</span><select id="selclscol" onchange="colChangeSetclsBycls()" class="span4" style="display:inline;"><option value="0">请选择</option></select>
                                       <span>班级：</span><select id="selclscls" onchange="addNewClass($(this).val())" class="span4" style="display:inline;"><option value="0">请选择</option></select>   
                                      </div>
                                      <h4 style="margin:7px 0 7px;">当前已选择的用户</h4>
                                      <select style="display:inline;" multiple="multiple" ondblclick="col_delete('#selUsers')" id="selUsers">
                                      </select> <small> 双击取消选定</small>
                                    </div>
                                    <div class="modal-footer">
                                      <button class="btn" data-dismiss="modal">取消</button>
                                      <button class="btn btn-primary" data-dismiss="modal" onclick="sureUser()">确定</button>
                                    </div>
                                </div>
                                <!--End model-->
                                <a data-toggle="modal" href="#myModal" class="btn">选择用户</a>
						    </div>
					    </div>
					    <div class="control-group">
						    <label class="control-label" for="description">内容</label>
						    <div class="controls">
							    <textarea name="content" style="width:95%;height:230px;visibility:hidden;"></textarea>
						    </div>
					    </div>
					    <div class="form-actions">
						    <input onclick="sendMsg()" type="button" class="btn btn-success btn-large" value="发送" /> <a class="btn" href="MyMessage.aspx">取消</a>
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
        $("#left_nav_ul li:eq(7)").attr('class', 'active'); //获取相应的li，设置class为active
        GetCollegeList(); //获取学院的下拉列表
        GetAllAdmin(); //获取管理员的下拉列表
    });

    $("#opsStu").click(function () {
        $("#selStudent").css('display', 'block');
        $("#selTeacher").css('display', 'none');
        $("#selAdmin").css('display', 'none');
        $("#selClass").css('display', 'none');
    });
    $("#opsTer").click(function () {
        $("#selTeacher").css('display', 'block');
        $("#selStudent").css('display', 'none');
        $("#selAdmin").css('display', 'none');
        $("#selClass").css('display', 'none');
    });
    $("#opsAdm").click(function () {
        $("#selAdmin").css('display', 'block');
        $("#selStudent").css('display', 'none');
        $("#selTeacher").css('display', 'none');
        $("#selClass").css('display', 'none');
    });
    $("#opsCls").click(function () {
        $("#selClass").css('display', 'block');
        $("#selTeacher").css('display', 'none');
        $("#selAdmin").css('display', 'none');
        $("#selStudent").css('display', 'none');
    });

    //获取学院信息
    function GetCollegeList() {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { col_add("#selstucol", item.id, item.collegeName); });
                $.each(data.collegeList, function (index, item) { col_add("#seltercol", item.id, item.collegeName); });
                $.each(data.collegeList, function (index, item) { col_add("#selclscol", item.id, item.collegeName); });
            }
        });
    }

    //根据学院获取班级信息
    function GetClassList(colID, selobj) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetAllClassByCollegeID&collegeID=" + colID,
            success: function (data) {
                col_clear(selobj);
                $.each(data.classList, function (index, item) { col_add(selobj, item.id, item.className); });
            }
        });
    }

    //根据学院获取老师名单
    function GetTeacherListByCol(colID) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetTerListByCollegeID&id=" + colID,
            success: function (data) {
                $.each(data.ter, function (index, item) { col_add("#selterter", item.id, item.value); });
            }
        });
    }

    //根据班级获取学生名单
    function GetStudentListByCls(clsID, selobj) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetStuListByClassID&id=" + clsID,
            success: function (data) {
                $.each(data.stu, function (index, item) { col_add(selobj, item.id, item.value); });

            }
        });
    }

    //获取所有管理员
    function GetAllAdmin() {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetAdminList",
            success: function (data) {
                $.each(data.adm, function (index, item) { col_add("#seladmadm", item.id, item.value); });
            }
        });
    }

    //当学院的选项发生改变时，更改班级列表
    function colChangeSetCls() {
        col_clear("#selstuclass"); //清除班级列表
        col_clear("#selstuuser"); //清除学生列表
        var colVal = $("#selstucol").val();
        if (colVal == 0) {
            return;
        }
        if (colVal != 0) {
            GetClassList(colVal, "#selstuclass");
        }
    }

    //当班级的选项发生改变时，更改学生列表
    function clsChangeSetStu() {
        col_clear("#selterter");
        var clsVal = $("#selstuclass").val();
        if (clsVal == 0) {
            return;
        }
        if (clsVal != 0) {
            GetStudentListByCls(clsVal, "#selstuuser");
        }
    }

    //当学院的选项发生改变时，更改老师列表
    function colChangeSetTer() {
        col_clear("#selterter");
        var colVal = $("#seltercol").val();
        if (colVal == 0) {
            return;
        }
        if (colVal != 0) {
            GetTeacherListByCol(colVal);
        }
    }

    //当学院的选项发生变化时，更改班级列表,班级列表专属
    function colChangeSetclsBycls() {
        col_clear("#selclscls"); //清除班级列表
        var colVal = $("#selclscol").val();
        if (colVal == 0) {
            return;
        }
        if (colVal != 0) {
            GetClassList(colVal, "#selclscls");
        }
    }

    //添加用户到已选择的选择框里面
    function addNewUser(addVal, addText) {
        //首先要确定是否包含该用户
        if (addVal == 0) {
            return;
        }
        var isAdd = true; //临时变量，设置是否添加
        var count = $("#selUsers option").length;
        for (var i = 0; i < count; i++) {
            if ($("#selUsers ").get(0).options[i].value == addVal) {
                isAdd = false; //已经存在不能添加
                break;
            }
        }
        if (isAdd == true) {
            col_add("#selUsers", addVal, addText);
        }
    }

    //添加班级到选择框里面
    function addNewClass(clsID) {
        $('#selUsers option').remove();
        GetStudentListByCls(clsID, "#selUsers");
    }

    //确定选择的用户
    function sureUser() {
        var userName = ""; //临时变量
        var count = $("#selUsers option").length;
        for (var i = 0; i < count; i++) {
            userName = userName + $("#selUsers ").get(0).options[i].text + ';';
        }
        $("#role").val(userName);
    }
    //检查用户列表和输入框中有什么区别
    function checkUser() {
        var userName = ""; //临时变量
        var count = $("#selUsers option").length;
        for (var i = 0; i < count; i++) {
            userName = userName + $("#selUsers ").get(0).options[i].text + ';';
        }
        if (userName == $('#role').val()) {
            return true;
        }
        else {
            return false;
        }
    }
    //发送消息
    function sendMsg() {
        if (!checkUser()) {
            alert("请重新确定发送用户！");
            $('#role').focus();
            return;
        }
        if ($('#role').val() == "") {
            alert("发送用户不能为空！");
            $('#role').focus();
            return;
        }
        var msgCon = editor.html(); //输入框内容
        if (msgCon == "") {
            alert("消息不能为空！");
            return;
        }
        if (!confirm("请文明用语，遵守相关法律法规\n确定要发送该消息？")) {
            return;
        }
        var userList = "";
        var count = $("#selUsers option").length;
        for (var i = 0; i < count; i++) {
            userList = userList + $("#selUsers ").get(0).options[i].value + ',';
        }
        var shtml = escape(msgCon);
        var re = new RegExp('\\+', 'g');
        var smsgCon = shtml.replace(re, '%2b');
        $.ajax({ //发送消息
            type: "post",
            dataType: "json",
            url: "/Handles/MessageHandler.ashx",
            data: "action=InsertNewMsg&msgCon=" + smsgCon + "&userList=" + userList,
            success: function (data) {
                alert(data.msg);
            },
            errpr: function () {
                alert("未知错误！");
            }
        });
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
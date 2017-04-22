<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeWorkManage.aspx.cs" Inherits="XGhms.Web.Teacher.HomeWorkControls.HomeWorkManage" %>
<%@ Register TagPrefix="uctrols" TagName="terTopNav" Src="~/Teacher/MyControls/TerTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="terLeftNav" Src="~/Teacher/MyControls/TerLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>作业管理 - XGhms</title>
    <link href="../../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/site.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
    
    <script src="../../editor/kindeditor-all.js" type="text/javascript"></script>
    <script src="../../editor/lang/zh-CN.js" type="text/javascript"></script>
    <link href="../../editor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="content"]', {
                uploadJson: '../../Handles/UploadJsonHandler.ashx',
                fileManagerJson: '../../Handles/FileManagerJsonHandler.ashx',
                allowFileManager: true,
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                }
            });
        });
		</script>
        <script src="../../editor/plugins/code/code.js" type="text/javascript"></script>
        <script src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
				    <h1><span id="title_h1">作业管理 </span> <small id="h1small"> </small></h1>
			    </div>
                <div class="control-group">
					<label style="display:inline" class="control-label">当前学期:</label>
                    <input id="termName" disabled="disabled" type="text" />&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label">所选课程:</label>
                    <input id="courseName" disabled="disabled" type="text" />
				</div>
                <div class="control-group">
                    <label style="display:inline" class="control-label"  for="hwBeginTime">作业开始时间:</label>
                    <input id="hwBeginTime" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'})" type="text" />&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label" for="hwEndTime">作业结束时间:</label>
                    <input id="hwEndTime" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'})" type="text" />
                </div>
                <div class="control-group">
                    <label style="display:inline" class="control-label" for="hwName">作业名称:</label>
                    <input id="hwName" type="text" />
                    <label style="display:inline" class="control-label">本次作业学生管理:</label>
                    <input type="button" id="btnmodelClick" data-toggle="modal" data-target="#myModal" class="btn" value="点击添加该作业学生" />
                </div>
                <!-- sample modal content -->
                <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 id="myModalLabel">选择学生</h3>
                    </div>
                    <div class="modal-body">
                        <h4 style="margin:7px 0 7px;">选择添加方式:</h4> 
                        <label class="radio inline"><input name="optionsRadios" type="radio" id="opsOne" value="单个添加">单个添加</label>
                        <label class="radio inline"><input name="optionsRadios" type="radio" id="opsRad" value="随机添加">随机添加</label>
                        <label class="radio inline"><input name="optionsRadios" type="radio" id="opsAll" value="全班学生">全班学生</label>
                        <!--当选择单个添加时-->
                        <div id="selOneAdd" style="margin:7px 0 7px; display:none;">
                        <h4 style="margin:7px 0 7px;">单个添加:</h4>
                            <div class="form-search">
                              <select id="selstuuser" onchange="addNewUser($(this).val() ,$(this).find('option:selected').text())" class="span2" style="display:inline;"><option value="0">请选择</option></select>
                            </div><br />
                        </div>
                        <!--当选择随机添加时-->
                        <div id="selRadAdd" style="margin:7px 0 7px; display:none;">
                        <h4 style="margin:7px 0 7px;">随机添加:</h4>
                            <div class="form-search">
                              <select id="selradstu" onchange="addRadUser($(this).val())" class="span2" style="display:inline;"><option value="0">请选择</option><option value="10">随机10人</option><option value="20">随机20人</option><option value="30">随机30人</option><option value="40">随机40人</option><option value="50">随机50人</option><option value="60">随机60人</option></select>
                            </div><br />
                        </div>
                        <h4 style="margin:7px 0 7px;">当前已选择的用户</h4>
                        <select style="display:inline;" multiple="multiple" ondblclick="col_delete('#selUsers')" id="selUsers">
                        </select> <small> 双击取消选定</small>
                    </div>
                    <div class="modal-footer">
                        <%--<button class="btn" data-dismiss="modal">取消</button>--%>
                        <button class="btn btn-primary" data-dismiss="modal">关闭</button>
                    </div>
                </div>
                <!--End model-->
                <div class="control-group">
					<h4>作业内容:</h4>
					<div class="controls">
						<textarea id="txetInfo" name="content" style="width:95%;height:230px;visibility:hidden;"></textarea>
					</div>
				</div>
                <div class="form-actions">
					<button id="hwEditId" type="button" onclick="btnClickSave()" class="btn btn-success btn-large">保存</button> <a id="successchangelink" class="btn" href="HomeWorkList.aspx">取消</a>
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
        $("#left_nav_ul li:eq(8)").attr('class', 'active'); //获取相应的li，设置class为active
        var id = getParam("id"); //作业ID
        var cid = getParam("cid"); //课程ID
        if (id == null || id == "") {
            if (cid == null || cid == "") {
                alert("请选择相应课程");
                window.location.replace("../CourseControls/MyCourse.aspx");
            } else {
                GetCourseInfoBycid(cid);
                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: "/Handles/TeacherHandler.ashx",
                    data: "action=GetStuListByCourseID&id=" + cid,
                    success: function (data) {
                        $.each(data.stulist, function (index, item) { col_add("#selstuuser", item.id, item.value); });
                    }
                });
                $('#h1small').append('新建作业');
            }
        } else {
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Handles/TeacherHandler.ashx",
                data: "action=GetStuListByCourseID&hwid=" + id,
                success: function (data) {
                    $.each(data.stulist, function (index, item) { col_add("#selstuuser", item.id, item.value); });
                }
            });
            $('#h1small').append('修改作业');
            GetHWInfoByid(id);
        }
    });
    /*新建作业*/
    //获取课程信息
    function GetCourseInfoBycid(cid) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetCourseInfoBycid&cid=" + cid,
            success: function (data) {
                if (data.msgtype == 0) {
                    alert(data.msg);
                    window.location.replace("../CourseControls/MyCourse.aspx");
                } else if (data.msgtype == 1) {
                    $('#termName').val(data.term);
                    $('#courseName').val(data.course_name);
                }
            }
        });
    }
    /*修改作业*/
    function GetHWInfoByid(id) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetHWInfoByid&id=" + id,
            success: function (data) {
                if (data.msgtype == 0) {
                    alert(data.msg);
                    window.location.replace("../CourseControls/MyCourse.aspx");
                } else if (data.msgtype == 1) {
                    $('#termName').val(data.term);
                    $('#courseName').val(data.course_name);
                    $('#hwBeginTime').val(data.homework_beginTime);
                    $('#hwEndTime').val(data.homework_endTime);
                    //$('#txetInfo').append(unescape(data.homework_info));
                    editor.html(unescape(data.homework_info));
                    $('#hwName').val(unescape(data.homework_name));
                }
            }
        });
    }



    /*点击保存按钮时，触发新建或者修改的事件*/
    function btnClickSave() {
        var id = getParam("id"); //作业ID
        var cid = getParam("cid"); //课程ID
        var hwName = $('#hwName').val();
        if (hwName == null || hwName=="") {
            alert("请输入作业标题");
            $('#hwName').focus();
            return;
        }
        var html = escape(editor.html()); //输入框内容
        if (html == null || html=="") {
            alert("请输入作业的内容");
            return;
        }
        var beginTime = $('#hwBeginTime').val(); // 开始时间
        if (beginTime == null || beginTime == "") {
            alert("请输入作业开始时间");
            $('#hwBeginTime').focus();
            return;
        }
        var endTime = $('#hwEndTime').val(); // 结束时间
        if (endTime == null || endTime == "") {
            alert("请输入作业结束时间");
            $('#hwEndTime').focus();
            return;
        }
        if (beginTime >= endTime) {
            alert("开始时间不能在结束时间后面");
            $('#hwBeginTime').focus();
            return;
        }
        var parm;
        var userList = "";
        var count = $("#selUsers option").length;
        for (var i = 0; i < count; i++) {
            userList = userList + $("#selUsers ").get(0).options[i].value + ',';
        }
        if (id == null || id == "") {
            if (cid == null || cid == "") {
                return;
            } else {
                //新建的代码
                parm = "action=addnewHW&id=" + cid + "&hwName=" + escape(hwName) + "&hwCon=" + html + "&userList=" + userList + "&beginTime=" + beginTime + "&endTime=" + endTime;
            }
        } else {
            //修改的代码
            parm = "action=updoldHW&id=" + id + "&hwName=" + escape(hwName) + "&hwCon=" + html + "&userList=" + userList + "&beginTime=" + beginTime + "&endTime=" + endTime;
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: parm,
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回作业列表');
            }
        });
    }
    $('#opsOne').click(function () {
        $('#selOneAdd').css('display', 'block');
        $('#selRadAdd').css('display', 'none');
        $('#selUsers').empty();
    });
    $('#opsRad').click(function () {
        $('#selOneAdd').css('display', 'none');
        $('#selRadAdd').css('display', 'block');
        $('#selUsers').empty();
    });
    $('#opsAll').click(function () {
        $('#selOneAdd').css('display', 'none');
        $('#selRadAdd').css('display', 'none');
        $('#selUsers').empty();
        var id = getParam("id"); //作业ID
        var cid = getParam("cid"); //课程ID
        if (id == null || id == "") {
            if (cid == null || cid == "") {
                return;
            } else {
                //课程ID
                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: "/Handles/TeacherHandler.ashx",
                    data: "action=GetStuListByCourseID&id=" + cid,
                    success: function (data) {
                        $.each(data.stulist, function (index, item) { col_add("#selUsers", item.id, item.value); });
                    }
                });
            }
        } else {
            //作业ID
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Handles/TeacherHandler.ashx",
                data: "action=GetStuListByCourseID&hwid=" + id,
                success: function (data) {
                    $.each(data.stulist, function (index, item) { col_add("#selUsers", item.id, item.value); });
                }
            });
        }
    });
    //随机添加用户
    function addRadUser(addVal) {
        //首先要确定是否包含该用户
        if (addVal == 0) {
            return;
        }
        $("#selUsers").empty();
        var id = getParam("id"); //作业ID
        var cid = getParam("cid"); //课程ID
        if (id == null || id == "") {
            if (cid == null || cid == "") {
                return;
            } else {
                //课程ID
                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: "/Handles/TeacherHandler.ashx",
                    data: "action=GetStuListRandomByCourseID&id=" + cid + "&value=" + addVal,
                    success: function (data) {
                        $.each(data.stulist, function (index, item) { col_add("#selUsers", item.id, item.value); });
                    }
                });
            }
        } else {
            //作业ID
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Handles/TeacherHandler.ashx",
                data: "action=GetStuListRandomByCourseID&hwid=" + id + "&value=" + addVal,
                success: function (data) {
                    $.each(data.stulist, function (index, item) { col_add("#selUsers", item.id, item.value); });
                }
            });
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
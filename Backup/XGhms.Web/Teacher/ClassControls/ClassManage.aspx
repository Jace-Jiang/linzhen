<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassManage.aspx.cs" Inherits="XGhms.Web.Teacher.ClassControls.ClassManage" %>
<%@ Register TagPrefix="uctrols" TagName="terTopNav" Src="~/Teacher/MyControls/TerTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="terLeftNav" Src="~/Teacher/MyControls/TerLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>班级信息 - XGhms</title>
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
				    <h1><span id="title_h1">班级信息</span> <small><span id="title_h1small"> </span></small></h1>
			    </div>
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
						    <label class="control-label" for="ip_className">班级名</label>
						    <div class="controls">
                                <input id="ip_className" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="ip_collegeName">所属学院</label>
						    <div class="controls">
                                <input id="ip_collegeName" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="ip_headTeacher">辅导员</label>
						    <div class="controls">
                                <input id="ip_headTeacher" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="sel_leader">班长</label>
						    <div class="controls">
                                <select id="sel_leader" disabled="disabled"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="sel_squadLeader">副班长</label>
						    <div class="controls">
                                <select id="sel_squadLeader" disabled="disabled"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="sel_groupSecretary">团支书</label>
						    <div class="controls">
                                <select id="sel_groupSecretary" disabled="disabled"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="sel_stduySecretary">学习委员</label>
						    <div class="controls">
                                <select id="sel_stduySecretary" disabled="disabled"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="sel_lifeSecretary">生活委员</label>
						    <div class="controls">
                                <select id="sel_lifeSecretary" disabled="disabled"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label">学生列表</label>
						    <div class="controls">
                                <a id="returnExcel" type="button" class="btn">导出学生列表</a>
						    </div>
					    </div>
                        <div class="form-actions">
                            <input id="btnSave" type="button" class="btn btn-success btn-large" value="保存" disabled="disabled" /> <a id="successchangelink" class="btn" href="ClassList.aspx">取消</a>
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
        $("#left_nav_ul li:eq(11)").attr('class', 'active'); //获取相应的li，设置class为active
        var geturlID = getParam("id");
        if (geturlID == null || geturlID == "") {
            alert("请先选择班级！");
            window.location.replace("ClassList.aspx");
            return;
        }
        else {
            $('#returnExcel').attr('href', '../../Handles/TeacherHandler.ashx?action=CreateExcelByClassID&classID=' + geturlID);
            GetClassInfoByID(geturlID);
        }
    });
    //修改班级信息
    $('#btnSave').click(function () {
        var geturlID = getParam("id");
        var parm = "action=SaveClassInfo&id=" + geturlID + "&sel_leader=" + $('#sel_leader').val() + "&sel_squadLeader=" + $('#sel_squadLeader').val() + "&sel_groupSecretary=" + $('#sel_groupSecretary').val() + "&sel_stduySecretary=" + $('#sel_stduySecretary').val() + "&sel_lifeSecretary=" + $('#sel_lifeSecretary').val();
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: parm,
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回班级列表');
            }
        });
    });
    //根据ID获取班级信息
    function GetClassInfoByID(geturlID) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetClassInfoByTerIDClsID&id=" + geturlID,
            cache: true,
            async: true,
            success: function (data) {
                if (data.msgtype == 0) { //表示改班级不存在
                    window.location.replace("../../Error.aspx?id=0");
                    return;
                } else if (data.msgtype == 1) { //表示改班级只可以查看
                    $('#ip_className').val(data.class_name);
                    $('#ip_collegeName').val(data.college);
                    $('#ip_headTeacher').val(data.head_teacher);
                    col_addSelect('#sel_leader', 1, data.class_leader, 1);
                    col_addSelect('#sel_squadLeader', 1, data.squad_leader, 1);
                    col_addSelect('#sel_groupSecretary', 1, data.class_group_secretary, 1);
                    col_addSelect('#sel_stduySecretary', 1, data.study_secretary, 1);
                    col_addSelect('#sel_lifeSecretary', 1, data.life_secretary, 1);
                } else if (data.msgtype == 2) { //表示该班级可以修改
                    $('#btnSave').removeAttr('disabled'); //可以选择修改
                    $('#sel_leader').removeAttr('disabled'); //可以选择修改
                    $('#sel_squadLeader').removeAttr('disabled'); //可以选择修改
                    $('#sel_groupSecretary').removeAttr('disabled'); //可以选择修改
                    $('#sel_stduySecretary').removeAttr('disabled'); //可以选择修改
                    $('#sel_lifeSecretary').removeAttr('disabled'); //可以选择修改
                    $('#ip_className').val(data.class_name);
                    $('#ip_collegeName').val(data.college);
                    $('#ip_headTeacher').val(data.head_teacher);
                    addSelStuList(geturlID,'#sel_leader',data.class_leader);
                    addSelStuList(geturlID,'#sel_squadLeader',data.squad_leader);
                    addSelStuList(geturlID,'#sel_groupSecretary',data.class_group_secretary);
                    addSelStuList(geturlID,'#sel_stduySecretary',data.study_secretary);
                    addSelStuList(geturlID,'#sel_lifeSecretary',data.life_secretary);
                }
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }
    //获取学生列表并且添加学生
    function addSelStuList(geturlID,selectobj, selectValue) {
        $.ajax({ 
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/MessageHandler.ashx",
            data: "action=GetStuListByClassID&id=" + geturlID,
            success: function (data) {
                $.each(data.stu, function (index, item) { col_addSelect(selectobj, item.id, item.value,selectValue); });
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
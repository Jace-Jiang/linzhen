<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeManage.aspx.cs" Inherits="XGhms.Web.Admin.CollegeControls.CollegeManage" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学院部门管理 - XGhms</title>
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
				    <h1>学院部门管理 <small id="h1small"> </small></h1>
			    </div>
                <h4 id="h4title" style="margin:7px 0 7px;"></h4> 
                <br />
                <div id="oneDiv" class="form-horizontal">
                    <fieldset>
					    <div class="control-group">
						    <label class="control-label" for="collegeName">学院名称</label>
						    <div class="controls">
							    <input type="text" class="input-xlarge" id="collegeName" value="" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="seladmadm">添加管理员</label>
						    <div class="controls">
							    <select id="seladmadm" onchange="addNewUser($(this).val() ,$(this).find('option:selected').text())"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="selUsers">选择管理员</label>
						    <div class="controls">
							    <select multiple="multiple" ondblclick="col_delete('#selUsers')" id="selUsers">
                                </select> <small> 双击取消选定</small>
						    </div>
					    </div>
					    <div class="form-actions">
						    <input id="btnSave" type="button" onclick="ClickSaveCollege()" class="btn btn-success btn-large" value="保存" /> <a id="successchangelink" class="btn" href="CollegeList.aspx">取消</a>
					    </div>					
				    </fieldset>
                </div>
                <div id="allDiv" class="form-horizontal">
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
        var geturlID = getParam("id");
        $("#left_nav_ul li:eq(2)").attr('class', 'active'); //获取相应的li，设置class为active
        if (geturlID == null || geturlID == "") {
            $('#h1small').append("新增学院");
            $('#h4title').append("新建学院部门:");
        }
        else {
            $('#h4title').append("修改学院部门:");
            setcollegeInfoByID(geturlID);
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=SelectCollegeAdminAll",
            success: function (data) {
                $.each(data.userList, function (index, item) { col_add("#seladmadm", item.id, item.value); });
            }
        });
    });
    //保存
    function ClickSaveCollege() {
        if ($('#collegeName').val()=="") {
            alert("学院名称不能为空！");
            $('#collegeName').focus();
            return;
        }
        var geturlID = getParam("id");
        var collegeName = $('#collegeName').val();
        var userName = ""; var parms = "";  //临时变量
        var count = $("#selUsers option").length;
        for (var i = 0; i < count; i++) {
            userName = userName + $("#selUsers ").get(0).options[i].value + ',';
        }
        if (geturlID == null || geturlID == "") {
            parms = "action=AddNewCollege&collegeName=" + collegeName + "&userName=" + userName;
        }
        else {
            parms = "action=UpdateOldCollege&collegeName=" + collegeName + "&userName=" + userName + "&collegeID=" + geturlID;
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: parms,
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回学院列表');
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }

    //如果是修改，获取学院的信息
    function setcollegeInfoByID(collegeID) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCollegeInfoByID&id=" + collegeID,
            success: function (data) {
                $('#collegeName').val(data.collegeName);
                $.each(data.userList, function (index, item) { col_add("#selUsers", item.id, item.value); });
                $('#h1small').append(data.collegeName);
            },
            error: function () {
                alert("没有找到该学院部门！");
                $('#btnSave').attr('disabled', 'disabled');
            }
        });
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
<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ClassManage.aspx.cs" Inherits="XGhms.Web.Admin.ClassControls.ClassManage" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>班级管理 - XGhms</title>
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
				    <h1>班级管理 <small id="h1small"> </small></h1>
			    </div>
                <h4 id="h4title" style="margin:7px 0 7px;"></h4> 
                <br />
                <div id="oneDiv" class="form-horizontal">
                    <fieldset>
					    <div class="control-group">
						    <label class="control-label" for="className">班级名称</label>
						    <div class="controls">
							    <input type="text" class="input-xlarge" id="className" value="" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="collegeName">所属学院</label>
						    <div class="controls">
							    <select id="collegeName" onchange="changeCollegeList()"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="seladmadm">班级辅导员</label>
						    <div class="controls">
							    <select id="seladmadm"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="selUsers">班级学生管理</label>
						    <div class="controls">
							    <input type="button" id="btnmodelClick" data-toggle="modal" data-target="#myModal" class="btn" value="点击管理班级学生" />
						    </div>
					    </div>
					    <div class="form-actions">
						    <input id="btnSave" type="button" onclick="ClickSaveClass()" class="btn btn-success btn-large" value="保存" /> <a id="successchangelink" class="btn" href="ClassList.aspx">取消</a>
					    </div>					
				    </fieldset>
                </div>
                <!-- sample modal content -->
                <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 id="myModalLabel">管理班级学生</h3>
                    </div>
                    <div class="modal-body">
                        <h4 style="margin:7px 0 7px;">选择添加方式:</h4> 
                        <label class="radio inline"><input name="optionsRadios" type="radio" id="opsStu" value="单个添加">单个添加</label>
                        <label class="radio inline"><input name="optionsRadios" type="radio" id="opsTer" value="批量添加">批量添加</label>
                        <!--当选择单个添加时-->
                        <div id="selOneadd" style="margin:7px 0 7px; display:none;">
                        <h4 style="margin:7px 0 7px;">单个添加:</h4>
                            <div class="form-search">
                              <input id="InsertNewStuTxt" placeholder="输入学号添加" type="text" class="input-medium search-query"/>
                              <button type="button" onclick="InsertNewStu()" class="btn"> 添加 </button>
                            </div><br />
                        </div>
                        <!--当选择批量添加时-->
                        <div id="selAlladd" style="margin:7px 0 7px; display:none;">
                            <h4 style="margin:7px 0 7px;">批量添加</h4>
                            <form class="form-search" id="Form1" method="post"><!--method="post"不能省略，在ie里面必不可少-->  
                              <input id="btnfile" name="btnfile" type="file" value="提交" class="input-medium search-query"/>
                              <input  id="btn" type="button"  value="上传" class="btn"/>
                            </form>批量添加班级学生的示例文件<a href="../../Upload/demo/批量添加班级学生示例.xls">点击下载</a><br />
                        </div>
                        <!--删除班级学生-->
                        <div id="selClass" style="margin:7px 0 7px;">
                            <h4 style="margin:7px 0 7px;">删除班级学生:</h4>
                            <form class="form-search" id="fm1" ENCTYPE ="multipart/form-data" method="post" runat="server"><!--method="post"不能省略，在ie里面必不可少--> 
                              <input id="DeleteStuTxt" placeholder="输入学号删除" type="text" class="input-medium search-query"/>
                              <button id="searchBtn" type="button" onclick="DeleteStuFromClass()" class="btn"> 删除 </button>
                            </form> <div id="divimg"></div>  
                        </div>
                        <!--导出班级学生-->
                        <div id="Div1" style="margin:7px 0 7px;">
                            <h4 style="margin:7px 0 10px;">导出班级所有学生:</h4>
                            <a id="returnExcel" type="button" class="btn" href="../../Handles/AdminHandler.ashx">导出学生列表</a>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <%--<button class="btn" data-dismiss="modal">取消</button>--%>
                        <button class="btn btn-primary" data-dismiss="modal">关闭</button>
                    </div>
                </div>
                <!--End model-->
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
        $("#left_nav_ul li:eq(5)").attr('class', 'active'); //获取相应的li，设置class为active
        var geturlID = getParam("id");
        if (geturlID == null || geturlID == "") {
            $('#h1small').append("新增班级");
            $('#h4title').append("新建班级:");
            addcollegeandadmin();
            $('#btnmodelClick').attr('disabled', 'disabled');
        }
        else {
            $('#h4title').append("修改班级:");
            setClassInfoByID(geturlID);
        }
        $('#returnExcel').attr('href', '../../Handles/AdminHandler.ashx?action=CreateExcelByClassID&classID=' + geturlID);
    });

    //添加学院和学院管理员
    function addcollegeandadmin() {
        $.ajax({ //获取学院列表
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
    //学院列表发生改变之后
    function changeCollegeList() {
        var selcol=$('#collegeName').val();
        if (selcol==0) {
            return;
        } else {
        col_clear('#seladmadm');
            GetHeadTeacherListBycolid(selcol);
        }

    }
    //如果是修改，获取班级的信息
    function setClassInfoByID(classID) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetClassInfoByID&id=" + classID,
            success: function (data) {
                $('#className').val(data.className);
                $('#h1small').append(data.className);
                if (data.collegeID != null || data.collegeID != "") {
                    GetAllColegeBySelectID(data.collegeID);
                }
                if (data.adminID != null || data.adminID != "") {
                    GetAllAdminBySelectID(data.collegeID,data.adminID);
                }
            },
            error: function () {
                alert("没有找到该班级！");
                $('#btnSave').attr('disabled', 'disabled');
                $('#btnmodelClick').attr('disabled', 'disabled');
            }
        });
    }
    //获取所有的学院列表，并且显示已选择的学院
    function GetAllColegeBySelectID(selectVal) {
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { col_addSelect("#collegeName", item.id, item.collegeName,selectVal); });
            }
        });
    }
    //获取所有的辅导员，并且显示已选择的辅导员
    function GetAllAdminBySelectID(collegeID,selectVal) {
        $.ajax({
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetClassAdminByCollegeID&id=" + collegeID,
            success: function (data) {
                $.each(data.userList, function (index, item) { col_addSelect("#seladmadm", item.id, item.value,selectVal); });
            }
        });
    }
    //根据学院ID获取辅导员
    function GetHeadTeacherListBycolid(id) {
        $.ajax({ //根据学院ID获取辅导员
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetClassAdminByCollegeID&id="+id,
            success: function (data) {
                $.each(data.userList, function (index, item) { col_add("#seladmadm", item.id, item.value); });
            }
        });
    }
    //点击保存按钮保存班级信息
    function ClickSaveClass() {
        var collegeName = $('#className').val(); //获取班级名称
        var collegeID = $('#collegeName option:selected').val(); //获取学院ID
        var userName = $('#seladmadm option:selected').val(); //获取辅导员ID
        if (collegeName == "") {
            alert("班级名称不能为空！");
            $('#className').focus();
            return;
        }
        if (collegeID == 0) {
            alert("请选择所属学院！");
            $('#collegeName').focus();
            return;
        }
        if (userName == 0) {
            alert("请选择班级的辅导员！");
            $('#seladmadm').focus();
            return;
        }
        var geturlID = getParam("id"); //获取班级ID 
        var parms = "";  //临时变量
        if (geturlID == null || geturlID == "") {
            parms = "action=AddNewClass&className=" + collegeName + "&HeadTeacher=" + userName + "&collegeID=" + collegeID;
        }
        else {
            parms = "action=UpdateOldClass&className=" + collegeName + "&HeadTeacher=" + userName + "&collegeID="+collegeID + "&classID=" + geturlID;
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: parms,
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回班级列表');
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }

    $("#opsStu").click(function () {
        $("#selOneadd").css('display', 'block');
        $("#selAlladd").css('display', 'none');
    });
    $("#opsTer").click(function () {
        $("#selOneadd").css('display', 'none');
        $("#selAlladd").css('display', 'block');
    });
    //给班级单个添加学生，根据学号添加
    function InsertNewStu() {
        if ($('#InsertNewStuTxt').val() == "") {
            alert("学号不能为空！");
            $('#InsertNewStuTxt').focus();
            return;
        }
        var geturlID = getParam("id"); //获取班级ID
        var getstuNum=$('#InsertNewStuTxt').val();
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=UpdateUserClassByUserID&classID=" + geturlID + "&userNum=" + getstuNum,
            success: function (data) {
                alert(data.msg);
            },
            error: function () {
                alert("保存失败！");
            }
        });
    }
    //根据学号从班级里面删除学生
    function DeleteStuFromClass() {
        if ($('#DeleteStuTxt').val() == "") {
            alert("学号不能为空！");
            $('#DeleteStuTxt').focus();
            return;
        }
        var geturlID = getParam("id"); //获取班级ID
        var getstuNum = $('#DeleteStuTxt').val();
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=DeleteStuFromClass&classID=" + geturlID + "&userNum=" + getstuNum,
            success: function (data) {
                alert(data.msg);
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }
    $("#btn").click(function () { //批量保存
        $("#Form1").ajaxSubmit({
            url: "/Handles/AdminUploadHandler.ashx",
            type: "post",
            dataType: "json",
            success: function (data) {
                alert(data.msg);
            }
        });
    });  

    // 添加下拉列表选项根据已选择的
    function col_addSelect(selectobj, value, text,selectValue) {
        var selObj = $(selectobj);
        if (value==selectValue) {
            selObj.append("<option selected='selected' value='" + value + "'>" + text + "</option>");
        }else {
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
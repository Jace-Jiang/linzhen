<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseManage.aspx.cs" Inherits="XGhms.Web.Admin.CourseControls.CourseManage" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>课程管理 - XGhms</title>
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
				    <h1>课程管理 <small id="h1small"> </small></h1>
			    </div>
                <h4 id="h4title" style="margin:7px 0 7px;"></h4> 
                <br />
                <div id="oneDiv" class="form-horizontal">
                    <fieldset>
					    <div class="control-group">
						    <label class="control-label" for="courseName">课程名称</label>
						    <div class="controls">
							    <input type="text" class="input-xlarge" id="courseName" value="" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="courseNum">课程编号</label>
						    <div class="controls">
							    <input type="text" class="input-xlarge" id="courseNum" value="" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="selTerm">学期选择</label>
						    <div class="controls">
							    <select id="selTerm"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="courseTer">课程老师</label>
						    <div class="controls">
							    <input type="text" placeholder="输入教工号" id="courseTer" /> <input type="button" onclick="checkUserID()" class="btn" value="验证" /> <span id="span_yz"></span>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="collegeName">所属学院</label>
						    <div class="controls">
							    <select id="collegeName"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="selUsers">课程学生管理</label>
						    <div class="controls">
							    <input type="button" id="btnmodelClick" data-toggle="modal" data-target="#myModal" class="btn" value="点击管理课程学生" />
						    </div>
					    </div>
					    <div class="form-actions">
						    <input id="btnSave" type="button" onclick="ClickSaveCollege()" class="btn btn-success btn-large" value="保存" /> <a id="successchangelink" class="btn" href="CourseList.aspx">取消</a>
					    </div>					
				    </fieldset>
                </div>
                <!-- sample modal content -->
                <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 id="myModalLabel">管理课程学生</h3>
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
                            </form>批量添加的示例文件<a href="../../Upload/demo/批量添加课程学生示例.xls">点击下载</a>
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
                            <h4 style="margin:7px 0 10px;">导出课程所有学生:</h4>
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
        var geturlID = getParam("id");
        $("#left_nav_ul li:eq(8)").attr('class', 'active'); //获取相应的li，设置class为active
        if (geturlID == null || geturlID == "") {
            $('#h1small').append("新增课程");
            $('#h4title').append("新建增加课程:");
            $('#btnmodelClick').attr('disabled', 'disabled');
            AddStatusInfo();
        }
        else {
            $('#h4title').append("修改课程:");
            setcourseInfoByID(geturlID);
        }
        $('#returnExcel').attr('href', '../../Handles/AdminHandler.ashx?action=CreateExcelByCourseID&courseID=' + geturlID);
    });
    //新增状态下的起始数据
    function AddStatusInfo() {
        $.ajax({ //添加学期
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetAllTermForNow",
            success: function (data) {
                $.each(data.termList, function (index, item) {
                    if (item.termCheck == 1) {
                        col_addSelect("#selTerm", item.id, item.termName, item.id);
                    }
                    else {
                        col_add("#selTerm", item.id, item.termName);
                    }
                });
            }
        });
        $.ajax({ //添加学院
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
    //点击验证
    function checkUserID() {
        var userID = $('#courseTer').val(); //获取教师编号
        $.ajax({ //检测用户名
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=CheckUserIDExist&userID=" + userID,
            success: function (data) {
                if (data.msg == 2) {
                    alert("用户不存在！");
                } else if (data.msg == 0) {
                    alert("该老师的姓名没找到！");
                } else if (data.msg == 1) {
                    $('#span_yz').empty();
                    $('#span_yz').append(data.userName);
                }
            }
        });
    }
    //保存
    function ClickSaveCollege() {
        var courseName=$('#courseName').val();
        var courseNum = $('#courseNum').val();
        var courseTer = $('#courseTer').val();
        var collegeNameID = $('#collegeName option:selected').val(); //获取学院ID
        var selTermID = $('#selTerm option:selected').val(); //获取学期ID
        if ( courseName== "") {
            alert("课程名称不能为空！");
            $('#courseName').focus();
            return;
        }
        if (courseNum == "") {
            alert("课程编号不能为空！");
            $('#courseNum').focus();
            return;
        }
        if (courseTer == "") {
            alert("授课教师不能为空！");
            $('#courseTer').focus();
            return;
        }
        if (collegeNameID == 0) {
            alert("请选择学院！");
            $('#collegeName').focus();
            return;
        }
        if (selTermID == 0) {
            alert("请选择学期！");
            $('#selTerm').focus();
            return;
        }
        var geturlID = getParam("id");
        var parms = "";  //临时变量
        if (geturlID == null || geturlID == "") {
            parms = "action=AddNewCourse&courseName=" + courseName + "&courseNum=" + courseNum + "&courseTer=" + courseTer + "&collegeNameID="+collegeNameID+"&selTermID="+selTermID;
        }
        else {
            parms = "action=UpdateOldCourse&courseName=" + courseName + "&courseNum=" + courseNum + "&courseTer=" + courseTer + "&collegeNameID=" + collegeNameID + "&selTermID=" + selTermID + "&courseID=" + geturlID;
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: parms,
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回课程列表');
            },
            error: function () {
                alert("未知错误！");
            }
        });
    }

    //如果是修改，获取学院的信息
    function setcourseInfoByID(courseID) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCourseInfoByID&id=" + courseID,
            success: function (data) {
                $('#courseName').val(data.courseName);
                $('#courseNum').val(data.courseNum);
                $('#courseTer').val(data.courseTer);
                addTermByCourseID(data.termID);
                addCollegeByCourseID(data.collegeID);
                $('#h1small').append(data.courseName);
            },
            error: function () {
                alert("没有找到该课程！");
                $('#btnSave').attr('disabled', 'disabled');
            }
        });
    }
    function addTermByCourseID(termID) {
        $.ajax({ //添加学期列表
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetAllTermForNow",
            success: function (data) {
                $.each(data.termList, function (index, item) {
                    col_addSelect("#selTerm", item.id, item.termName, termID);
                });
            }
        });
    }
    function addCollegeByCourseID(collegeID) {
        $.ajax({ //添加学院列表
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/AdminHandler.ashx",
            data: "action=GetCollegeList",
            success: function (data) {
                $.each(data.collegeList, function (index, item) { col_addSelect("#collegeName", item.id, item.collegeName, collegeID); });
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
    //给课程单个添加学生，根据学号添加
    function InsertNewStu() {
        if ($('#InsertNewStuTxt').val() == "") {
            alert("学号不能为空！");
            $('#InsertNewStuTxt').focus();
            return;
        }
        var geturlID = getParam("id"); //获取课程ID
        var getstuNum = $('#InsertNewStuTxt').val();
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/AdminHandler.ashx",
            data: "action=UpdateUserCourseByUserID&courseID=" + geturlID + "&userNum=" + getstuNum,
            success: function (data) {
                alert(data.msg);
            },
            error: function () {
                alert("保存失败！");
            }
        });
    }
    //根据学号从课程里面删除学生
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
            data: "action=DeleteStuFromCourse&courseID=" + geturlID + "&userNum=" + getstuNum,
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
            url: "/Handles/AdminUploadForCourseHandler.ashx",
            type: "post",
            dataType: "json",
            success: function (data) {
                alert(data.msg);
            }
        });
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
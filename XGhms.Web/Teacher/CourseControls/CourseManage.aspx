<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseManage.aspx.cs" Inherits="XGhms.Web.Teacher.CourseControls.CourseManage" %>
<%@ Register TagPrefix="uctrols" TagName="terTopNav" Src="~/Teacher/MyControls/TerTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="terLeftNav" Src="~/Teacher/MyControls/TerLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的课程管理 - XGhms</title>
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
				    <h1><span id="title_h1">课程信息</span> <small><span id="title_h1small"> </span></small></h1>
			    </div>
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseName">课程名</label>
						    <div class="controls">
                                <input id="tb_courseName" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseNum">课程标志号</label>
						    <div class="controls">
                                <input id="tb_courseNum" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseTrem">学期</label>
						    <div class="controls">
                                <input id="tb_courseTrem" type="text" readonly="readonly" />
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseCollege">开课学院</label>
						    <div class="controls">
                                <input id="tb_courseCollege" type="text" readonly="readonly"/>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseTeacher">课程老师</label>
						    <div class="controls">
                                <input id="tb_courseTeacher" type="text" readonly="readonly"/>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_courseOtherTeacher">助教</label>
						    <div class="controls">
                                <input id="tb_courseOtherTeacher" type="text" /> <input type="button" onclick="checkUserID()" class="btn" value="验证" /> <span id="span_yz"></span>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="sel_leadStu">学生负责人</label>
						    <div class="controls">
                                <select id="sel_leadStu"><option value="0">请选择</option></select>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="description">课程信息</label>
						    <div class="controls">
                                <textarea class="input-xlarge" id="description" rows="5"></textarea>
						    </div>
					    </div>
                        <div class="form-actions">
                            <input id="btnSave" type="button" class="btn btn-success btn-large" value="保存" /> <a id="successchangelink" class="btn" href="MyCourse.aspx">取消</a>
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
        $("#left_nav_ul li:eq(5)").attr('class', 'active'); //获取相应的li，设置class为active
        var geturlID = getParam("id");
        if (geturlID == null || geturlID == "") {
            alert("请先选择课程！");
            window.location.replace("MyCourse.aspx");
            return;
        }
        else {
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Handles/TeacherHandler.ashx",
                data: "action=GetCourseInfoByID&id=" + geturlID,
                cache: true,
                async: true,
                success: function (data) {
                    if (data.msgtype == 0) { //表示课程无法访问
                        window.location.replace("../../Error.aspx?id=1");
                        return;
                    } else if (data.msgtype == 1) { //表示改班级只可以查看
                        $('#tb_courseName').val(data.course_name);
                        $('#tb_courseNum').val(data.course_number);
                        $('#tb_courseTrem').val(data.term);
                        $('#tb_courseCollege').val(data.college);
                        $('#tb_courseTeacher').val(data.teacher);
                        $('#tb_courseOtherTeacher').val(data.other_teacher);
                        addSelStuList(geturlID, '#sel_leadStu', data.student);
                        $('#description').append(unescape(data.course_info));
                    }
                },
                error: function () {
                    alert("未知错误！");
                }
            });
        }
    });

    function addSelStuList(geturlID, selectobj, selectValue) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetStuListByCourseID&id=" + geturlID,
            success: function (data) {
                $.each(data.stulist, function (index, item) { col_addSelect(selectobj, item.id, item.value, selectValue); });
            }
        });
    }
    //点击验证
    function checkUserID() {
        var userID = $('#tb_courseOtherTeacher').val(); //获取教师编号
        $.ajax({ //检测用户名
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/TeacherHandler.ashx",
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
    $('#btnSave').click(function () {
        var geturlID = getParam("id");
        var otherTeacher = $('#tb_courseOtherTeacher').val();
        var student = $('#sel_leadStu').val();
        var courseinfo = $('#description').html();
        $.ajax({ //保存课程信息
            type: "post",
            dataType: "json",
            async: false,
            url: "/Handles/TeacherHandler.ashx",
            data: "action=SaveCourseInfo&id=" + geturlID + "&otherTeacher=" + otherTeacher + "&student=" + student + "&courseinfo=" + escape(courseinfo),
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回课程列表');
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentWorkCheck.aspx.cs" Inherits="XGhms.Web.Teacher.HomeWorkCheck.StudentWorkCheck" %>
<%@ Register TagPrefix="uctrols" TagName="terTopNav" Src="~/Teacher/MyControls/TerTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="terLeftNav" Src="~/Teacher/MyControls/TerLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批阅作业 - XGhms</title>
    <link href="../../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/site.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
    <link href="../../editor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script src="../../editor/kindeditor-all.js" type="text/javascript"></script>
    <script src="../../editor/lang/zh-CN.js" type="text/javascript"></script>
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
				    <h1><span id="title_h1">批阅作业 </span> <small id="h1small"> </small></h1>
			    </div>
                <div class="control-group">
					<label style="display:inline" class="control-label" for="stuName">姓名:</label>
                    <input id="stuName" class="span1" disabled="disabled" type="text"  style="display:inline"/>&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label" for="stuNum">学号:</label>
                    <input id="stuNum" class="span2" disabled="disabled" type="text"  style="display:inline"/>&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label" for="subTime">提交时间:</label>
                    <input id="subTime" class="span2" disabled="disabled" type="text"  style="display:inline"/>&nbsp; &nbsp;&nbsp;
                    <label style="display:inline" class="control-label" for="courseName">课程名:</label>
                    <input id="courseName" class="span2" disabled="disabled" type="text" style="display:inline"/>
				</div>
                <h4>详细内容:</h4><br />
                <div id="stuhwCon"></div>
                <h4>作业附件:</h4><br />
                <a id="stu_file" href="#">该学生作业附件</a>
                <h4>对该次作业的评价:</h4><br />
                <textarea id="txetInfo" name="content" style="width:95%;height:230px;visibility:hidden;"></textarea><br />
                <div class="control-group">
                    <label style="display:inline" class="control-label">评分:</label>
                    <input id="txt_score" class="span1" type="text" onchange="check(this.value);" onkeyup="check(this.value);" style="display:inline"/>
                </div>
                <div class="form-actions">
					<input type="button" onclick="saveClick()" class="btn btn-success btn-large" value="保存"/> <a id="successchangelink" class="btn" href="StudentWorkList.aspx">取消</a>
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
        $("#left_nav_ul li:eq(2)").attr('class', 'active'); //获取相应的li，设置class为active
        var id = getParam("id"); //该学生的作业ID
        if (id == null || id == "") {
            alert("请先选择相应学生的作业");
            window.location.replace("StudentWorkList.aspx");
        } else {
            GetStuHWInfoByID(id);
        }
    });
    //获取该学生作业的详细信息
    function GetStuHWInfoByID(id) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=GetStuHWInfoByID&id=" + id,
            success: function (data) {
                //根据该学生作业的id获取学生作业的：课程名、作业名、学生姓名、学生学号、提交时间、作业提交的内容、作业的提交的附件
                if (data.msgtype == 0) {
                    alert(data.msg);
                    window.location.replace("../../Error.aspx");
                } else {
                    $('#h1small').append(data.homework_name);
                    $('#stuName').val(data.student_name);
                    $('#stuNum').val(data.student_num);
                    $('#subTime').val(data.submit_time);
                    $('#courseName').val(data.course_name);
                    $('#stuhwCon').append(unescape(data.homework_con));
                    $('#stu_file').attr('href', '../' + unescape(data.submit_file)); //设置下载链接
                    editor.html(unescape(data.homework_comment));
                    $('#txt_score').val(data.homework_score);
                }
            }
        });
    }
    //检查输入分数的正确性
    function check(v) {
        if (!/^(100|[1-9]\d|\d)$/.test(v)) return alert('请输入0~100的数字！');
        $('#txt_score').focus();
    }
    //点击保存触发
    function saveClick() {
        var shtml = escape(editor.html()); //输入框内容
        var re=new RegExp('\\+','g');
        var html=shtml.replace(re, '%2b');
        if (html == null || html == "") {
            alert("请输入作业的内容");
            return;
        }
        var score = $('#txt_score').val();
        if (score == null || score == "" || score>100||score<0) {
            alert("该作业的分数输入有误吧，请重新输入！");
            return;
        }
        var id = getParam("id"); //该学生的作业ID
        var parm = "action=CheckStudentWork&id=" + id + "&con=" + html + "&score=" + score;
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
    //点击重做作业触发
    function ReformWork() {
        var id = getParam("id"); //该学生的作业ID
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/TeacherHandler.ashx",
            data: "action=ReformWork&id="+id,
            success: function (data) {
                alert(data.msg);
                window.location.replace("StudentWorkList.aspx");
            }
        });
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
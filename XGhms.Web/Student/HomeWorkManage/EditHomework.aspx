<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditHomework.aspx.cs" Inherits="XGhms.Web.Student.HomeWorkManage.EditHomework" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>提交作业 - XGhms</title>
    <link href="~/Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/site.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <script src="../../editor/kindeditor-all.js" type="text/javascript"></script>
    <script src="../../editor/lang/zh-CN.js" type="text/javascript"></script>
    <script>
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="content"]', {
                resizeType: 1,
                items: [
        'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
        'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
        'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
        'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
        'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image', 'multiimage',
        'table', 'hr', 'emoticons', 'pagebreak','anchor', 'link', 'unlink', '|', 'about'
]
            });
//            K('input[name=hwEditId]').click(function (e) {
//                alert(editor.html());
//            });
//            K('#insertfile').click(function () {
//                editor.loadPlugin('insertfile', function () {
//                    editor.plugin.fileDialog({
//                        fileUrl: K('#url').val(),
//                        clickFn: function (url, title) {
//                            K('#url').val(url);
//                            editor.hideDialog();
//                        }
//                    });
//                });
//            });
        });
    </script>
</head>
<body>
<%--<form id="form1" runat="server">--%>
<uctrols:stuTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:stuLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="row-fluid">
                <div class="page-header">
				    <h1> <span id="title_h1"></span> <small><span id="title_h1small"></span></small></h1>
			    </div>
                <h4>作业开始时间: <span id="hw_beginTime" style="font-weight:normal; margin-right:20px;"> </span>
                    作业结束时间: <span id="hw_endTime" style="font-weight:normal; margin-right:20px;"> </span> 
                    授课老师: <span id="hw_teacher" style="font-weight:normal;"></span></h4> <br />
                <h3>编写作业:</h3><br />
                <p id="hwInfos">
                    <textarea id="editor_id" name="content" style="width:90%;height:400px;">
                    </textarea>
                    <br />
                    <strong>上传作业压缩包:&nbsp;</strong>
                    <form class="form-search" id="Form2" method="post"><!--method="post"不能省略，在ie里面必不可少-->  
                        <input id="btnfile" name="btnfile" type="file" value="提交" class="input-medium search-query"/>
                        <input  id="btn" type="button" value="上传" class="btn"/>&nbsp; &nbsp;&nbsp;<small style="color:Red;">请将自己的作业打包压缩后上传</small>
                    </form>
                </p>
                <div id="stuLink" style=" display:none;"><strong>作业附件下载:&nbsp;</strong><a id="stuDown">点击下载我上传的作业附件</a></div>
                <div class="form-actions">
					<input id="hwEditId" name="hwEditId" type="button" class="btn btn-success btn-large"/>  <a id="successchangelink" class="btn" href="Default.aspx">取消</a>
				</div>
            </div>
        </div>
    </div>
    <uctrols:downFooter ID="AllDownFooter" runat="server" />
</div>
    <script src="../../Script/fileControl.js" type="text/javascript"></script>
    <script src="../../Script/MyAjaxForm.js" type="text/javascript"></script>
<%--</form>--%>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#left_nav_ul li:eq(2)").attr('class', 'active'); //获取相应的li，设置class为active
        //获取传过来的参数值，然后根据相应的参数值来判断相应的值
        var hwid = getParam("id");
        if (hwid == "null" || hwid == null) {
            window.location.replace("../../Error.aspx?id=0");
            return;
        };
        var parm = "action=GetHomeworkByID&id=" + hwid;
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: parm,
            cache: false,
            async: true,
            success: function (data) {
                if (data.id == "0") {
                    alert(data.msg);
                    return;
                }
                else {
                    setHomeworkInfo(data.homework_name, data.course_name, data.hw_begtime, data.hw_endtime, data.course_teacher);
                    setButtonSta(data.hw_status, hwid);
                }
            }
        });
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetHomeworkEditByID&id=" + hwid,
            cache: false,
            async: true,
            success: function (data) {
                if (data.id == "0") {
                    return;
                }
                else {
                    editor.html(unescape(data.submit_content)); //设置编辑器内容
                    $('#stuLink').css('display', 'block'); //显示下载链接
                    $('#stuDown').attr('href', '../' + unescape(data.submit_file)); //设置下载链接
                }
            }
        });
        $('#btnfile').checkFileTypeAndSize({
            allowedExtensions: ['jpg', 'rar', 'zip', '7z', 'doc', 'docx', 'xls', 'xlsx', 'pdf', 'txt', 'jpg', 'png', 'gif'],
            maxSize: 1000,
            success: function () {
            },
            extensionerror: function () {
                alert('允许的格式为：.rar .zip .7z格式的压缩文件\n.doc .docx .xls .xlsx .pdf .txt格式的文档\n.jpg .png .gif 格式的图片');
                return;
            },
            sizeerror: function () {
                alert('最大尺寸1M');
                return;
            }
        });
    });
    //上传附件
    $("#btn").click(function () { //上传作业
        if (!confirm("确定要上传作业？\n本次上传的作业会覆盖以前的作业")) {
            return;
        }
        $("#Form2").ajaxSubmit({
            url: "/Handles/StudentUploadHandler.ashx",
            type: "post",
            dataType: "json",
            success: function (data) {
                alert(data.msg);
            },
            error: function () {
                alert("上传失败！\n未知错误请联系管理员");
            }
        });
    });  
    //设置显示的数据
    function setHomeworkInfo(HWtitle, courseName, beginTime, endTime, teacherName) {
        $("#title_h1").append(HWtitle); //设置作业名称
        $("#title_h1small").append(courseName); //设置课程名称
        $("#hw_beginTime").append(beginTime); //设置开始时间
        $("#hw_endTime").append(endTime); //设置结束时间
        $("#hw_teacher").append(teacherName); //设置课程老师
    }

    //设置按钮的数据
    function setButtonSta(hw_status, hwid) {
        if (hw_status == 0) {
            $("#hwEditId").val("提交作业");
        }
        if (hw_status == 1) {
            $("#hwEditId").val("提交修改");
        }
        if (hw_status == 2) {
            $("#hwEditId").val("无法提交");
            $("#hwEditId").attr('disabled', 'disabled');
        }
        if (hw_status == 3) {
            $("#hwEditId").val("提交修改");
        }
        if (hw_status == 4) {
            $("#hwEditId").val("无法提交");
            $("#hwEditId").attr('disabled', 'disabled');
        }
    }
    $('#hwEditId').click(function () {
        var htmlCon = editor.html(); //输入框内容
        if (htmlCon == "" || htmlCon == null) {
            alert("请输入提交内容");
            return;
        }
        var hwid = getParam("id");
        var shtml=escape(htmlCon);
        var re = new RegExp('\\+', 'g');
        var smsgCon = shtml.replace(re, '%2b');
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=StuSubmitHomeWork&id=" + hwid + "&htmlCon=" + smsgCon,
            cache: false,
            async: true,
            success: function (data) {
                alert(data.msg);
                $('#successchangelink').empty();
                $('#successchangelink').append('返回作业列表');
            }
        });
    });
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
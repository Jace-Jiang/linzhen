<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemSetting.aspx.cs" Inherits="XGhms.Web.Student.Setting.SystemSetting" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统设置 - XGhms</title>
    <link href="~/Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/site.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/login.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
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
				    <h1> <span id="title_h1">修改密码 </span><small> <span id="title_h1small"> </span></small></h1>
			    </div>
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
						    <label class="control-label" for="stuOldPwd">原密码</label>
						    <div class="controls">
							    <input type="password" class="input-xlarge" id="stuOldPwd" value="" />
                                <span class="fcCF261F info username_msg"></span>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="stuNewPwdOne">新密码</label>
						    <div class="controls">
							    <input type="password" class="input-xlarge" id="stuNewPwdOne" value="" />
                                <span class="fcCF261F info password_msg"></span>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="stuNewPwdTwo">再输一次</label>
						    <div class="controls">
							    <input type="password" class="input-xlarge" id="stuNewPwdTwo" value="" />
                                <span class="fcCF261F info verifycode_msg"></span>
						    </div>
					    </div>
                        <div class="form-actions">
						    <input id="btnChange" type="submit" class="btn btn-success btn-large" value="修改密码" /> <a class="btn" href="#">取消</a>
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
        $("#left_nav_ul li:eq(13)").attr('class', 'active'); //获取相应的li，设置class为active
        UserInfo();
        var navWidthFuck = $("#div_setnavwidth").width() + 30;
        if ($("#div_setnavwidth").height() > 50) {  //个别浏览器存在样式问题，这里修改样式
            $('#div_setnavwidth').css("width", navWidthFuck + "px");
        } 
    });

    //网站后台登录
    adminLogin = function () {
        //旧密码验证
        if ($('#stuOldPwd').attr("value") == '') {
            $(".username_msg").html('请输入您的旧密码');
            $("#stuOldPwd").focus();
            setTimeout(function () { $(".username_msg").html(''); }, 3000);
            return false;
        }
        //密码验证
        if ($('#stuNewPwdOne').attr("value") == '') {
            $(".password_msg").html('请输入您的新密码');
            setTimeout(function () { $(".password_msg").html(''); }, 3000);
            $("#stuNewPwdOne").focus();
            return false;
        }
        //2次密码验证
        if ($('#stuNewPwdTwo').attr("value") == '') {
            $(".verifycode_msg").html('请再次输入新密码');
            setTimeout(function () { $(".verifycode_msg").html(''); }, 3000);
            $("#stuNewPwdTwo").focus();
            return false;
        }
        //密码一致性验证
        if ($('#stuNewPwdTwo').attr("value") != $('#stuNewPwdOne').attr("value")) {
            $(".verifycode_msg").html('两次密码不相同');
            setTimeout(function () { $(".verifycode_msg").html(''); }, 3000);
            $("#stuNewPwdTwo").focus();
            return false;
        }
        return true;
    };

    //点击修改时提交数据
    $("#btnChange").click(function () {
        if (!adminLogin()) { return false; } else {
            var name = $("#stuOldPwd").val();
            var pwd = $("#stuNewPwdTwo").val();
            var parm = "action=ResetPasswordBySelf&oldpwd=" + escape(name) + "&newpwd=" + escape(pwd);
            $.ajax({
                type: 'post',
                dataType: "text",
                url: '/Handles/SystemSetHandler.ashx',
                data: parm,
                cache: false,
                async: false,
                success: function (rs) {
                    if (parseInt(rs) == 2) {
                        $(".username_msg").html('密码错误');
                        $("#stuOldPwd").focus();
                        setTimeout(function () { $(".username_msg").html(''); }, 3000);
                        return;
                    } else if (parseInt(rs) == 3) {
                        alert("修改失败！");
                    }
                    else if (parseInt(rs) == 1) {
                        alert("修改成功，请重新登录！");
                        window.location.replace(document.URL);
                    } else {
                        alert("未知错误！");
                    }
                }
            });
        }
    });
</script>
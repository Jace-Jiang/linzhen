<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XGhms.Web.Login" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录系统 - XGhms</title>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script src="/Script/bootstrap.min.js" type="text/javascript"></script>
    <link href="Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Style/site.css" rel="stylesheet" type="text/css" />
    <link href="Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <link href="Style/login.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<!-- 导航条部分 -->
<div class="navbar navbar-fixed-top">
    <div class="navbar-inner">
        <div class="container-fluid"> 
            <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"> 
                <span class="icon-bar"></span> 
                <span class="icon-bar"></span> 
                <span class="icon-bar"></span> 
            </a> 
            <a class="brand" href="#">课程作业管理系统</a>
            <div id="div_setnavwidth" class="btn-group pull-right"> 
                <a class="btn" href="#">
                    <i class="icon-user"></i> 
                    <asp:Label ID="lab_userName" runat="server" Text="请登录"></asp:Label>
                </a> 
                <a class="btn dropdown-toggle" data-toggle="dropdown" href="#"> 
                    <span class="caret"></span> 
                </a>
                <ul id="ui_islogin" class="dropdown-menu">
                    <li><a href="#" onclick="logout()">注销登录</a></li>
                </ul>
            </div>
            <div class="nav-collapse">
                <ul class="nav">
                    <li><asp:HyperLink ID="hl_zhuye" runat="server" NavigateUrl="~/Default.aspx">主页</asp:HyperLink></li>
                </ul>
            </div>
        </div>
    </div>
</div>
<div class="container-fluid">
    <div class="hr tp-div-nexthr" style="margin-top: 0;margin-bottom: 0;padding: 0;"></div>
	<!-- Example row of columns -->
    <div class="container pb-15" style="width:1260px;">
		<div class="label-div t-15 border-all" style="padding: 0; margin:0 auto;">
			<div class="clearfix label-public">
				<!-- 左边的界面 -->
                <div class="pull-left third-party l-30 pt-40" style="width:600px;min-height: 350px;">
					<div class="fc333"></div>
					<div class="clearfix t-25">
                        <div class="pull-left sina-div r-10 b-10" style="_width: 110px;">
                            <img alt="课程作业管理系统" src="Img/guat.png" width="110px" height="110px" />
						</div>
                        <div style=" width:380px; font-size:30px; margin-top:20px; text-align:center; line-height:35px;">航天工业大学<br />课程作业管理系统</div>
					</div>
					<p class="fc999" style="max-width: 470px;font-size:14px;">欢迎使用课程作业管理系统，请先登录系统然后根据您的角色进入相应的管理界面，如果在使用当中有任何问题，请联系管理员。</p>
				</div>
                <!-- 当没有登录的时候显示以下登录界面 -->
				<div id="div_login" class="pull-left border-l public-login pt-40" style="min-height: 350px; width:540px;">
					<div class="t-25 pl-30 fc333">请用学号或者教工号登录系统</div>
					<div class="clearfix pt-25">
						<div class="title pull-left fc999" style="width: 60px;">用户名</div>
						<div class="pull-left l-20" style="_margin-left: 0;width: 300px;">
                            <input type="text" style="width: 260px;" id="username" name="Login[username]" value="" class="span4"/>
                        </div>
						<span class="pull-left fcCF261F info username_msg"></span>
					</div>
					<div class="clearfix pt-25">
						<div class="title pull-left fc999" style="width: 60px;">密码</div>
						<div class="pull-left l-20" style="_margin-left: 0;width: 300px;">
                            <input type="password" style="width: 260px;" id="password" name="Login[password]" value="" class="span4"/>
                        </div>
						<span class="pull-left fcCF261F info password_msg"></span>
					</div>
                    <div class="clearfix pt-25">
						<div class="title pull-left fc999" style="width: 60px;">验证码</div>
						<div class="pull-left l-20" style="_margin-left: 0;width: 300px;">
                            <input type="text" id="verifycode" name="Login[verifycode]" value="" class="span2"/>&nbsp;&nbsp;
                            <img src="/Handles/Verify_code.ashx" id="Verify_codeImag" class="dib_vt login_code" alt="点击切换验证码" title="点击切换验证码" style="padding-bottom: 3px; vertical-align: middle;cursor: pointer;" onclick="ToggleCode(this.id, '/Handles/Verify_code.ashx');return false;" />
                        </div>
						<span class="pull-left fcCF261F info verifycode_msg"></span>
					</div>
					<div class="clearfix pt-25">
						<div class="title pull-left" style="margin-left: 6px;_margin-left: 3px;">&nbsp;</div>
						<a id="btnLogin" href="javascript:void(0);" title="登录" class="pull-left btn btn-success btn-large">登&nbsp;录</a>
						<div class="pull-left l-20 t-8" style="_width: auto;_margin-left: 15px;font-size:14px;">
                            <input id="loginCheckbox" type="checkbox" checked="checked" style="_width: 20px;_margin: 0;"/>
                        </div>
						<span class="pull-left l-5 info fc999" style="_width: auto;_margin: 0; font-size:14px;">下次自动登录</span>
					</div>
				</div>
                <!-- 当已经登录的时候显示以下界面 -->
                <div id="div_logout" class="pull-left border-l public-login pt-40" style="min-height: 350px; width:540px; display:none; ">
                    <div class="t-25 pl-30 fc333 fs18">您已登录系统，点击下面的按钮注销登录</div>
                    <div class="clearfix pt-25 l-40"><a class="pull-left btn btn-success btn-large" onclick="logout()">注销登录</a></div>
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
        var isLogin=<%=aaa %>;
        var navWidthFuck=$("#div_setnavwidth").width()+30; 
        if (isLogin=="0") {
            $('#ui_islogin').css("display","none");
        }
        if (isLogin=="1") {
            $('#div_logout').css("display","inline");
            $('#div_login').css("display","none");
        }
        if ($("#div_setnavwidth").height()>50) {  //个别浏览器存在样式问题，这里修改样式
            $('#div_setnavwidth').css("width",navWidthFuck+"px");
        } 
    });

    $('input').keydown(function (event) {
        if (event.keyCode == 13) submit_form('.login-register-btn');
    });

    var submit_form = function (obj) {
        adminLogin();
    };

    //网站后台登录
    adminLogin = function () {
        //用户名验证
        if ($('#username').attr("value") == '') {
            $(".username_msg").html('请输入您的用户名');
            $("#username").focus();
            setTimeout(function () { $(".username_msg").html(''); }, 3000);
            return false;
        }
        //密码验证
        if ($('#password').attr("value") == '') {
            $(".password_msg").html('请输入您的密码');
            setTimeout(function () { $(".password_msg").html(''); }, 3000);
            $("#password").focus();
            return false;
        }
        //验证码验证
        if ($('#verifycode').attr("value") == '') {
            $(".verifycode_msg").html('请输入验证码');
            setTimeout(function () { $(".verifycode_msg").html(''); }, 3000);
            $("#verifycode").focus();
            return false;
        }
        return true;
    };
    /**切换验证码**/
    function ToggleCode(obj, codeurl) {
        $("#txtCode").val("");
        $("#" + obj).attr("src", codeurl + "?time=" + Math.random());
    }
    //登陆按钮回车事件
    document.onkeydown = function (e) {
        if (!e) {
            e = window.event;   //火狐中是window.event
        }
        if ((e.keyCode || e.which) == 13) {
            var btnSave = document.getElementById("btnLogin");
            btnSave.focus();
            btnSave.click();
        }
    }

    //点击登录时提交数据
    $("#btnLogin").click(function () {
        if (!adminLogin()) { return false; } else {
            var name = $("#username").val();
            var pwd = $("#password").val();
            var code = $("#verifycode").val();
            var ischecked = $("#loginCheckbox").is(':checked');
            var parm = "action=login&userName=" + escape(name) + "&userPwd=" + escape(pwd) + "&Code=" + escape(code) + "&isChecked=" + escape(ischecked) + "";
            $.ajax({
                type: 'post',
                dataType: "text",
                url: '/Handles/LoginHandler.ashx',
                data: parm,
                cache: false,
                async: false,
                success: function (rs) {
                    if (parseInt(rs) == 2) {
                        alert("该用户被锁定, 请联系管理员！");
                        window.location.href = window.location.href.replace('#', '');
                        ToggleCode("Verify_codeImag", '/Handles/Verify_code.ashx');
                        return false;
                    } else if (parseInt(rs) == 3) {
                        $(".password_msg").html('密码错误！');
                        setTimeout(function () { $(".password_msg").html(''); }, 3000);
                        $("#password").focus();
                        ToggleCode("Verify_codeImag", '/Handles/Verify_code.ashx');
                        return false;
                    } else if (parseInt(rs) == 4) {
                        $(".verifycode_msg").html('验证码错误！');
                        setTimeout(function () { $(".verifycode_msg").html(''); }, 3000);
                        $("#verifycode").focus();
                        ToggleCode("Verify_codeImag", '/Handles/Verify_code.ashx');
                        return false;
                    } else if (parseInt(rs) == 1) {
                        var newUrl = getParam('ReturnUrl');
                        if (newUrl==null) {
                            window.location.replace("Default.aspx");
                        }
                        else {
                            window.location.replace(newUrl);
                        }
                    } else {
                        $(".username_msg").html('用户名不存在！');
                        $("#username").focus();
                        setTimeout(function () { $(".username_msg").html(''); }, 3000);
                        ToggleCode("Verify_codeImag", '/Handles/Verify_code.ashx');
                        return false;
                    }
                }
            });
        }
    });
    //注销登录
    function logout() {
        $.ajax({
            type: 'post',
            dataType: "text",
            url: '/Handles/LoginHandler.ashx',
            data: "action=logout",
            cache: false,
            async: false,
            success: function (rs) {
                if (parseInt(rs) == 5) {
                    alert("注销成功！");
                    window.location.replace("Login.aspx");
                }
            }
        });
    };

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
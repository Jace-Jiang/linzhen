<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TerTopNav.ascx.cs" Inherits="XGhms.Web.Teacher.MyControls.TerTopNav" %>
<div class="navbar navbar-fixed-top">
  <div class="navbar-inner">
    <div class="container-fluid"> <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </a> <a class="brand" href="#">作业管理系统</a>
      <div class="btn-group pull-right"> <asp:HyperLink ID="HyperLink_myprofile" runat="server" NavigateUrl="~/Teacher/User/MyInfo.aspx" CssClass="btn"><i class="icon-user"></i> 
          <asp:Label ID="lab_userName" runat="server" Text="用户"></asp:Label></asp:HyperLink> <a class="btn dropdown-toggle" data-toggle="dropdown" href="#"> <span class="caret"></span> </a>
        <ul class="dropdown-menu">
          <li><asp:HyperLink ID="HyperLink_syssetting" runat="server" NavigateUrl="~/Teacher/Setting/ChangePassword.aspx">修改密码</asp:HyperLink></li>
          <li class="divider"></li>
          <li><a href="#" onclick="logout()">注销登录</a></li>
        </ul>
      </div>
      <div class="nav-collapse">
        <ul class="nav">
          <li><asp:HyperLink ID="hl_zhuye" runat="server" NavigateUrl="~/Teacher/Default.aspx">主页</asp:HyperLink></li>
          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">作业 <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><asp:HyperLink ID="HyperLink_myhw" runat="server" NavigateUrl="~/Teacher/HomeWorkCheck/StudentWorkList.aspx">批改作业</asp:HyperLink></li>
              <li class="divider"></li>
              <li><asp:HyperLink ID="HyperLink_mycour" runat="server" NavigateUrl="~/Teacher/HomeWorkControls/HomeWorkList.aspx">管理作业</asp:HyperLink></li>
            </ul>
          </li>
          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">查看 <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><asp:HyperLink ID="HyperLink_kectj" runat="server" NavigateUrl="~/Teacher/CourseControls/MyCourse.aspx">查看课程</asp:HyperLink></li>
              <li class="divider"></li>
              <li><asp:HyperLink ID="HyperLink_hwtj" runat="server" NavigateUrl="~/Teacher/ClassControls/ClassList.aspx">查看班级</asp:HyperLink></li>
            </ul>
          </li>
          <li><asp:HyperLink ID="HyperLink_message" runat="server" NavigateUrl="~/Teacher/Message/MyMessage.aspx">消息</asp:HyperLink></li>
        </ul>
      </div>
    </div>
  </div>
</div>
<script src="/Script/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="/Script/bootstrap.min.js" type="text/javascript"></script>
<script type="text/javascript">
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
                    window.location.replace(document.URL);
                }
            }
        });
    };
</script>
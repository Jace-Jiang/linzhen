<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdmTopNav.ascx.cs" Inherits="XGhms.Web.Admin.MyControls.AdmTopNav" %>
<div class="navbar navbar-fixed-top">
  <div class="navbar-inner">
    <div class="container-fluid"> <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </a> <a class="brand" href="#">课程作业管理系统</a>
      <div class="btn-group pull-right"> <asp:HyperLink ID="HyperLink_myprofile" runat="server" NavigateUrl="~/Admin/User/MyInfo.aspx" CssClass="btn"><i class="icon-user"></i> 
          <asp:Label ID="lab_userName" runat="server" Text="用户"></asp:Label></asp:HyperLink> <a class="btn dropdown-toggle" data-toggle="dropdown" href="#"> <span class="caret"></span> </a>
        <ul class="dropdown-menu">
          <li><asp:HyperLink ID="HyperLink_syssetting" runat="server" NavigateUrl="~/Admin/Setting/SiteSettings.aspx">系统设置</asp:HyperLink></li>
          <li class="divider"></li>
          <li><a href="#" onclick="logout()">注销登录</a></li>
        </ul>
      </div>
      <div class="nav-collapse">
        <ul class="nav">
          <li><asp:HyperLink ID="hl_zhuye" runat="server" NavigateUrl="~/Admin/Default.aspx">主页</asp:HyperLink></li>
          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">学院 <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><asp:HyperLink ID="HyperLink_newCollege" runat="server" NavigateUrl="~/Admin/CollegeControls/CollegeManage.aspx">新建学院</asp:HyperLink></li>
              <li class="divider"></li>
              <li><asp:HyperLink ID="HyperLink_collegeList" runat="server" NavigateUrl="~/Admin/CollegeControls/CollegeList.aspx">学院列表</asp:HyperLink></li>
            </ul>
          </li>
          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">班级 <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><asp:HyperLink ID="HyperLink_newClass" runat="server" NavigateUrl="~/Admin/ClassControls/ClassManage.aspx">新建班级</asp:HyperLink></li>
              <li class="divider"></li>
              <li><asp:HyperLink ID="HyperLink_classList" runat="server" NavigateUrl="~/Admin/ClassControls/ClassList.aspx">班级列表</asp:HyperLink></li>
            </ul>
          </li>
          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">用户 <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><asp:HyperLink ID="HyperLink_newUser" runat="server" NavigateUrl="~/Admin/UserControls/AddUser.aspx">新建用户</asp:HyperLink></li>
              <li class="divider"></li>
              <li><asp:HyperLink ID="HyperLink_userList" runat="server" NavigateUrl="~/Admin/UserControls/UserList.aspx">用户列表</asp:HyperLink></li>
            </ul>
          </li>
          <li><asp:HyperLink ID="HyperLink_message" runat="server" NavigateUrl="~/Admin/Message/MyMessage.aspx">消息</asp:HyperLink></li>
        </ul>
      </div>
    </div>
  </div>
</div>
<script src="/Script/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="/Script/jquery.min.js" type="text/javascript"></script>
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
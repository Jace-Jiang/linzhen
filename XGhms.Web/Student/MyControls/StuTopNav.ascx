<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StuTopNav.ascx.cs" Inherits="XGhms.Web.Student.MyControls.StuTopNav" %>
<div class="navbar navbar-fixed-top">
  <div class="navbar-inner">
    <div class="container-fluid"> <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </a> <a class="brand" href="#">作业管理系统</a>
      <div class="btn-group pull-right"> <asp:HyperLink ID="HyperLink_myprofile" runat="server" NavigateUrl="~/Student/User/Default.aspx" CssClass="btn"><i class="icon-user"></i> 
          <asp:Label ID="lab_userName" runat="server" Text="用户"></asp:Label></asp:HyperLink> <a class="btn dropdown-toggle" data-toggle="dropdown" href="#"> <span class="caret"></span> </a>
        <ul class="dropdown-menu">
          <li><asp:HyperLink ID="HyperLink_syssetting" runat="server" NavigateUrl="~/Student/Setting/SystemSetting.aspx">修改密码</asp:HyperLink></li>
          <li class="divider"></li>
          <li><a href="#" onclick="logout()">注销登录</a></li>
        </ul>
      </div>
      <div class="nav-collapse">
        <ul class="nav">
          <li><asp:HyperLink ID="hl_zhuye" runat="server" NavigateUrl="~/Student/Default.aspx">主页</asp:HyperLink></li>
          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">作业 <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><asp:HyperLink ID="HyperLink_myhw" runat="server" NavigateUrl="~/Student/HomeWorkManage/Default.aspx">我的作业</asp:HyperLink></li>
              <li class="divider"></li>
              <li><asp:HyperLink ID="HyperLink_mycour" runat="server" NavigateUrl="~/Student/HomeWorkManage/MyCourse.aspx">我的课程</asp:HyperLink></li>
            </ul>
          </li>
          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">统计 <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><asp:HyperLink ID="HyperLink_kectj" runat="server" NavigateUrl="~/Student/MyStatistics/Default.aspx">课程统计</asp:HyperLink></li>
              <li class="divider"></li>
              <li><asp:HyperLink ID="HyperLink_hwtj" runat="server" NavigateUrl="~/Student/MyStatistics/HomeWorkList.aspx">作业统计</asp:HyperLink></li>
            </ul>
          </li>
          <li><asp:HyperLink ID="HyperLink_message" runat="server" NavigateUrl="~/Student/Message/MyMessage.aspx">消息</asp:HyperLink></li>
        </ul>
      </div>
    </div>
  </div>
</div>
<script src="/Script/jquery.js" type="text/javascript"></script>
<script src="/Script/bootstrap.min.js" type="text/javascript"></script>
<script type="text/javascript">
function logout(){
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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdmLeftNav.ascx.cs" Inherits="XGhms.Web.Admin.MyControls.AdmLeftNav" %>
<div class="span3">
    <div class="well sidebar-nav">
        <ul id="left_nav_ul" class="nav nav-list">
            <li class="nav-header"><i class="icon-wrench"></i> 学院设置</li>
            <li><asp:HyperLink ID="hl_01" runat="server" NavigateUrl="~/Admin/CollegeControls/CollegeList.aspx">学院列表</asp:HyperLink></li>
            <li><asp:HyperLink ID="hl_02" runat="server" NavigateUrl="~/Admin/CollegeControls/CollegeManage.aspx">学院管理</asp:HyperLink></li>   
            <li class="nav-header"><i class="icon-wrench"></i> 班级设置</li>
            <li><asp:HyperLink ID="hl_04" runat="server" NavigateUrl="~/Admin/ClassControls/ClassList.aspx">班级列表</asp:HyperLink></li>    
            <li><asp:HyperLink ID="hl_05" runat="server" NavigateUrl="~/Admin/ClassControls/ClassManage.aspx">班级管理</asp:HyperLink></li>      
            <li class="nav-header"><i class="icon-wrench"></i> 课程设置</li>
            <li><asp:HyperLink ID="hl_07" runat="server" NavigateUrl="~/Admin/CourseControls/CourseList.aspx">课程列表</asp:HyperLink></li>    
            <li><asp:HyperLink ID="hl_08" runat="server" NavigateUrl="~/Admin/CourseControls/CourseManage.aspx">课程管理</asp:HyperLink></li>     
            <li class="nav-header"><i class="icon-wrench"></i> 用户设置</li>
            <li><asp:HyperLink ID="hl_10" runat="server" NavigateUrl="~/Admin/UserControls/UserList.aspx">用户列表</asp:HyperLink></li>     
            <li><asp:HyperLink ID="hl_11" runat="server" NavigateUrl="~/Admin/UserControls/AddUser.aspx">用户管理</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-envelope"></i> 消息管理</li>
            <li><asp:HyperLink ID="hl_13" runat="server" NavigateUrl="~/Admin/Message/MyMessage.aspx">我的消息</asp:HyperLink></li>     
            <li><asp:HyperLink ID="hl_14" runat="server" NavigateUrl="~/Admin/Message/NewMessage.aspx">发送消息</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-user"></i> 用户资料</li>
            <li><asp:HyperLink ID="hl_16" runat="server" NavigateUrl="~/Admin/User/MyInfo.aspx">个人中心</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-search"></i> 查询</li>
            <li><asp:HyperLink ID="hl_18" runat="server" NavigateUrl="~/Admin/Search/SearchUser.aspx">用户查询</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-cog"></i> 设置</li>
            <li><asp:HyperLink ID="hl_20" runat="server" NavigateUrl="~/Admin/Setting/SiteSettings.aspx">系统设置</asp:HyperLink></li>
            <li><asp:HyperLink ID="hl_21" runat="server" NavigateUrl="~/Admin/Setting/ChangePassword.aspx">修改密码</asp:HyperLink></li>
            <%--<li><a href="#" onclick="logout()">注销登录</a></li>--%>
        </ul>
    </div>
</div>
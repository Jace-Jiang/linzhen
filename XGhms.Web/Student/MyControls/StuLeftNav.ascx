<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StuLeftNav.ascx.cs" Inherits="XGhms.Web.Student.MyControls.StuLeftNav" %>
<div class="span3">
    <div class="well sidebar-nav">
        <ul id="left_nav_ul" class="nav nav-list">
            <li class="nav-header"><i class="icon-wrench"></i> 作业管理</li>
            <li><asp:HyperLink ID="hl_00" runat="server" NavigateUrl="~/Student/HomeWorkManage/MyCourse.aspx">我的课程</asp:HyperLink></li>
            <li><asp:HyperLink ID="hl_01" runat="server" NavigateUrl="~/Student/HomeWorkManage/Default.aspx">我的作业</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-signal"></i> 作业统计</li>
            <li><asp:HyperLink ID="hl_02" runat="server" NavigateUrl="~/Student/MyStatistics/Default.aspx">课程统计</asp:HyperLink></li> 
            <li><asp:HyperLink ID="hl_03" runat="server" NavigateUrl="~/Student/MyStatistics/HomeWorkList.aspx">详细统计</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-envelope"></i> 消息管理</li>
            <li><asp:HyperLink ID="hl_04" runat="server" NavigateUrl="~/Student/Message/MyMessage.aspx">我的消息</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-search"></i> 查询</li>
            <li><asp:HyperLink ID="hl_05" runat="server" NavigateUrl="~/Student/Search/Default.aspx">用户查询</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-user"></i> 用户资料</li>
            <li><asp:HyperLink ID="hl_07" runat="server" NavigateUrl="~/Student/User/Default.aspx">个人中心</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-cog"></i> 设置</li>
            <li><asp:HyperLink ID="hl_08" runat="server" NavigateUrl="~/Student/Setting/SystemSetting.aspx">修改密码</asp:HyperLink></li>
            <li><a href="#" onclick="logout()">注销登录</a></li>
        </ul>
    </div>
</div>
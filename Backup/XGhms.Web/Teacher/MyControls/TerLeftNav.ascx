<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TerLeftNav.ascx.cs" Inherits="XGhms.Web.Teacher.MyControls.TerLeftNav" %>
<div class="span3">
    <div class="well sidebar-nav">
        <ul id="left_nav_ul" class="nav nav-list">
            <li class="nav-header"><i class="icon-wrench"></i> 批改作业</li>
            <li><asp:HyperLink ID="hl_01" runat="server" NavigateUrl="~/Teacher/HomeWorkCheck/StudentWorkList.aspx">作业列表</asp:HyperLink></li>
            <li><asp:HyperLink ID="hl_02" runat="server" NavigateUrl="~/Teacher/HomeWorkCheck/StudentWorkCheck.aspx">批改作业</asp:HyperLink></li>   
            <li class="nav-header"><i class="icon-wrench"></i> 课程管理</li>
            <li><asp:HyperLink ID="hl_04" runat="server" NavigateUrl="~/Teacher/CourseControls/MyCourse.aspx">我的课程</asp:HyperLink></li>    
            <li><asp:HyperLink ID="hl_05" runat="server" NavigateUrl="~/Teacher/CourseControls/CourseManage.aspx">课程设置</asp:HyperLink></li>      
            <li class="nav-header"><i class="icon-wrench"></i> 作业管理</li>
            <li><asp:HyperLink ID="hl_07" runat="server" NavigateUrl="~/Teacher/HomeWorkControls/HomeWorkList.aspx">所有作业</asp:HyperLink></li>    
            <li><asp:HyperLink ID="hl_08" runat="server" NavigateUrl="~/Teacher/HomeWorkControls/HomeWorkManage.aspx">作业管理</asp:HyperLink></li>     
            <li class="nav-header"><i class="icon-wrench"></i> 班级查看</li>
            <li><asp:HyperLink ID="hl_10" runat="server" NavigateUrl="~/Teacher/ClassControls/ClassList.aspx">班级列表</asp:HyperLink></li>     
            <li><asp:HyperLink ID="hl_11" runat="server" NavigateUrl="~/Teacher/ClassControls/ClassManage.aspx">查看班级</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-envelope"></i> 消息管理</li>
            <li><asp:HyperLink ID="hl_13" runat="server" NavigateUrl="~/Teacher/Message/MyMessage.aspx">我的消息</asp:HyperLink></li>     
            <li><asp:HyperLink ID="hl_14" runat="server" NavigateUrl="~/Teacher/Message/NewMessage.aspx">发送消息</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-user"></i> 用户资料</li>
            <li><asp:HyperLink ID="hl_16" runat="server" NavigateUrl="~/Teacher/User/MyInfo.aspx">个人中心</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-search"></i> 查询</li>
            <li><asp:HyperLink ID="hl_18" runat="server" NavigateUrl="~/Teacher/Search/SearchUser.aspx">用户查询</asp:HyperLink></li>
            <li class="nav-header"><i class="icon-cog"></i> 设置</li>
            <li><asp:HyperLink ID="hl_20" runat="server" NavigateUrl="~/Teacher/Setting/ChangePassword.aspx">修改密码</asp:HyperLink></li>
            <li><a href="#" onclick="logout()">注销登录</a></li>
        </ul>
    </div>
</div>
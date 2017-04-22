<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteSettings.aspx.cs" Inherits="XGhms.Web.Admin.Setting.SiteSettings" validateRequest="false" %>
<%@ Register TagPrefix="uctrols" TagName="admTopNav" Src="~/Admin/MyControls/AdmTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="admLeftNav" Src="~/Admin/MyControls/AdmLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>网站设置 - XGhms</title>
    <link href="../../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/site.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
</head>
<body>
<form id="form1" runat="server">
<uctrols:admTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:admLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="row-fluid">
                <div class="page-header">
				    <h1>系统设置 <small id="h1small"> 相关描述设置</small></h1>
			    </div>
                <div id="oneDiv" class="form-horizontal">
                    <fieldset>
					    <div class="control-group">
						    <label class="control-label" for="stuTitle">学生主页标题</label>
						    <div class="controls">
                                <asp:TextBox ID="stuTitle" CssClass="input-xxlarge" runat="server"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="stuDescription">学生主页描述</label>
						    <div class="controls">
                                <asp:TextBox ID="stuDescription" CssClass="input-xxlarge" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="terTitle">老师主页标题</label>
						    <div class="controls">
                                <asp:TextBox ID="terTitle" CssClass="input-xxlarge" runat="server"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="terDescription">老师主页描述</label>
						    <div class="controls">
                                <asp:TextBox ID="terDescription" CssClass="input-xxlarge" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="admTitle">管理员主页标题</label>
						    <div class="controls">
                                <asp:TextBox ID="admTitle" CssClass="input-xxlarge" runat="server"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="admDescription">管理员主页描述</label>
						    <div class="controls">
                                <asp:TextBox ID="admDescription" CssClass="input-xxlarge" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="FooterInfo">页脚显示代码</label>
						    <div class="controls">
                                <asp:TextBox ID="FooterInfo" CssClass="input-xxlarge" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
						    </div>
					    </div>
					    <div class="form-actions">
                            <asp:Button ID="btnSave" CssClass="btn btn-success btn-large" OnClientClick="return saveValue();" runat="server" Text="保存" onclick="btnSave_Click" />
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
        $("#left_nav_ul li:eq(20)").attr('class', 'active'); //获取相应的li，设置class为active
    });

    var saveValue = function () {
        $('#stuTitle').val();
        if ($('#stuTitle').val() == '') {
            alert("标题不能为空");
            $("#stuTitle").focus();
            return false;
        }
        if ($('#stuDescription').val() == '') {
            alert("描述不能为空");
            $("#stuDescription").focus();
            return false;
        }
        if ($('#terTitle').val() == '') {
            alert("标题不能为空");
            $("#terTitle").focus();
            return false;
        }
        if ($('#terDescription').val() == '') {
            alert("描述不能为空");
            $("#terDescription").focus();
            return false;
        }
        if ($('#admTitle').val() == '') {
            alert("标题不能为空");
            $("#admTitle").focus();
            return false;
        }
        if ($('#admDescription').val() == '') {
            alert("描述不能为空");
            $("#admDescription").focus();
            return false;
        }
        if (!confirm("你确信要保存修改？")) {
            return false;
        }
    };

</script>
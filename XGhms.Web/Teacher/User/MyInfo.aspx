<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="XGhms.Web.Teacher.User.MyInfo" %>
<%@ Register TagPrefix="uctrols" TagName="terTopNav" Src="~/Teacher/MyControls/TerTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="terLeftNav" Src="~/Teacher/MyControls/TerLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>个人中心 - XGhms</title>
    <link href="../../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/site.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.min.js"></script>
    <![endif]-->
    <script src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
<form id="form1" runat="server">
<uctrols:terTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:terLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="row-fluid">
                <div class="page-header">
				    <h1> <span id="title_h1">个人信息 </span><small><span id="title_h1small"> 修改</span></small></h1>
			    </div>
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
						    <label class="control-label" for="tb_UserName">用户名</label>
						    <div class="controls">
                                <asp:TextBox ID="tb_UserName" CssClass="input-xlarge" runat="server" ReadOnly="True"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_UserNum">编号</label>
						    <div class="controls">
                                <asp:TextBox ID="tb_UserNum" CssClass="input-xlarge" runat="server" ReadOnly="True"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_RealName">姓名</label>
						    <div class="controls">
                                <asp:TextBox ID="tb_RealName" CssClass="input-xlarge" runat="server"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_UserBir">生日</label>
						    <div class="controls">
                                <asp:TextBox ID="tb_UserBir" CssClass="input-xlarge" onClick="WdatePicker()" runat="server"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_UserEmail">E-mail</label>
						    <div class="controls">
                                <asp:TextBox ID="tb_UserEmail" CssClass="input-xlarge" runat="server"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_UserPhone">联系电话</label>
						    <div class="controls">
                                <asp:TextBox ID="tb_UserPhone" CssClass="input-xlarge" runat="server"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_UserAddress">联系地址</label>
						    <div class="controls">
                                <asp:TextBox ID="tb_UserAddress" CssClass="input-xlarge" runat="server"></asp:TextBox>
						    </div>
					    </div>
                        <div class="control-group">
						    <label class="control-label" for="tb_UserIntro">个人简介</label>
						    <div class="controls">
                                <asp:TextBox ID="tb_UserIntro" CssClass="input-xlarge" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
						    </div>
					    </div>
                        <div class="form-actions">
                            <asp:Button ID="btn_save" CssClass="btn btn-success btn-large" 
                                OnClientClick="return saveValue();" runat="server" Text="保存" 
                                onclick="btn_save_Click" />&nbsp; &nbsp;&nbsp;<asp:Label ID="lab_infoMsg" 
                                runat="server" ForeColor="Red"></asp:Label>
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
        $("#left_nav_ul li:eq(16)").attr('class', 'active'); //获取相应的li，设置class为active
    });
    var saveValue = function () {
        $('#tb_RealName').val();
        if ($('#tb_RealName').val() == '') {
            alert("姓名不能为空");
            $("#tb_RealName").focus();
            return false;
        }
        if (!confirm("你确信要保存修改？")) {
            return false;
        }
    };
</script>
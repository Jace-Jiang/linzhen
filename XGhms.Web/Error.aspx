<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="XGhms.Web.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>您访问的页面出错</title>
    <script src="Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var id = getParam('id');
            if (id == 0) { //表示该网页无法找到
                alert("抱歉，该网页无法找到！");
            }
            else if (id == 1) { //表示该用户没有权限访问
                alert("抱歉，您没有权限访问该网页！");
            }
            else if (id == 2) { //未知错误
                alert("抱歉，访问该网页时出现未知错误！");
            }
            else if (id == 3) { //无法访问
                alert("抱歉，您暂时无法访问该页面！");
            }
        });

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
</head>
<body>
<form id="form1" runat="server">
    <script type="text/javascript" src="http://www.qq.com/404/search_children.js" charset="utf-8" homePageUrl="/Default.aspx" homePageName="回到我的主页"></script>
</form>
</body>
</html>


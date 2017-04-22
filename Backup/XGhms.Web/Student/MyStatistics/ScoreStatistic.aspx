<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScoreStatistic.aspx.cs" Inherits="XGhms.Web.Student.MyStatistics.ScoreStatistic" %>
<%@ Register TagPrefix="uctrols" TagName="stuTopNav" Src="~/Student/MyControls/StuTopNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="stuLeftNav" Src="~/Student/MyControls/StuLeftNav.ascx" %>
<%@ Register TagPrefix="uctrols" TagName="downFooter" Src="~/MyControls/DownFooter.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的成绩统计 - XGhms</title>
    <link href="~/Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/site.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
</head>
<body>
<form id="form1" runat="server">
<uctrols:stuTopNav ID="topNav" runat="server" />
<div class="container-fluid">
    <div class="row-fluid">
        <uctrols:stuLeftNav ID="leftNav" runat="server" />
        <div class="span9">
            <div class="row-fluid">
                <div class="page-header">
				    <h1>我的成绩统计 <small id="h1small"></small></h1>
			    </div>
                <div id="placeholder" style="width:80%;height:300px;"></div>
            </div>
        </div>
    </div>
    <uctrols:downFooter ID="AllDownFooter" runat="server" />
</div>
<script src="/Script/jquery.flot.js" type="text/javascript"></script>
<script src="/Script/jquery.flot.resize.js" type="text/javascript"></script>	
</form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#left_nav_ul li:eq(4)").attr('class', 'active'); //获取相应的li，设置class为active
        //获取传过来的参数值，然后根据相应的参数值来判断相应的值
        var hwid = getParam("id");
        if (hwid == "null" || hwid == null) {
            window.location.replace("../../Error.aspx?id=0");
            return;
        };
        $.ajax({ //获取班级数据
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetCourseInfoByID&type=all&id=" + hwid,
            cache: false,
            async: true,
            success: function (data3) {
                $('#h1small').append(data3.course_name+'   最近20条数据统计');
            }
        });
        $.ajax({ //获取个人数据
            type: "post",
            dataType: "json",
            url: "/Handles/StudentHandler.ashx",
            data: "action=GetTwentyScoreByCourseID&type=stu&id=" + hwid,
            cache: false,
            async: true,
            success: function (data1) {
                if (data1 == "false") {
                    window.location.replace("../../Error.aspx?id=0");
                    return;
                }
                else {
                    $.ajax({ //获取班级数据
                        type: "post",
                        dataType: "json",
                        url: "/Handles/StudentHandler.ashx",
                        data: "action=GetTwentyScoreByCourseID&type=all&id=" + hwid,
                        cache: false,
                        async: true,
                        success: function (data2) {
                            if (data2 == "false") {
                                window.location.replace("../../Error.aspx?id=0");
                                return;
                            }
                            else {
                                buding(data1, data2);
                            }
                        }
                    });
                }
            }
        });
    });
    //绑定数据
    function buding(dataStu, dataAll) {
        var pl = $.plot($("#placeholder"),
                [
                    { label: "我的分数", data: dataStu },
                    { label: "班级平均分", data: dataAll }
                ],
                {
                    series: {//控制线的填充、点的显示
                        lines: { show: true },
                        points: { show: true }
                    },
                    //开启鼠标移动和点击事件  折线图外框颜色 和 外框的宽度
                    grid: { hoverable: true, clickable: true, borderColor: '#000', borderWidth: 1 },
                    xaxis: {//x轴的最大最小范围 和 刻度自定义。
                        min: 0,
                        max: 21,
                        ticks: [1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21]
                    },
                    yaxis: {//y轴的最小范围
                        min: 0,
                        max: 100
                    }
                }
            );
        function showTooltip(x, y, contents) {//浮动块信息
            $('<div id="tooltip">' + contents + '</div>').css({
                position: 'absolute',
                display: 'none',
                top: y + 5,
                left: x + 5,
                border: '1px solid #fdd',
                padding: '2px',
                'background-color': '#fee',
                opacity: 0.80
            }).appendTo("body").fadeIn(200);
        }
        var previousPoint = null;
        $("#placeholder").bind("plothover", function (event, pos, item) {
            if (item) {
                if (previousPoint != item.dataIndex) {
                    previousPoint = item.dataIndex;
                    $("#tooltip").remove();
                    var x = item.datapoint[0], //x y 轴位置
                            y = item.datapoint[1],
                            n = x - 1;
                    var changci = item.series['data'][n][2], //作业名称 获取当前焦点的 数据信息 通过n（即x轴位置获取数据）
                  	        shijian = item.series['data'][n][3]; //同上 作业名称获取原理。 作业时间
                    var contents = "作业名称：" + changci + "<br />分数（均分）：" + y + "<br />作业时间：" + shijian;
                    showTooltip(item.pageX, item.pageY, contents);
                }
            }
            else {
                $("#tooltip").remove();
                previousPoint = null;
            }
        });
    }
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
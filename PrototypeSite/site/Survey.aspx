<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="site.Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            color: #675E5B;
            font: 12px/18px Arial,Helvetica,sans-serif;
        }
        
        .content {
            position: relative;
            width: 100%;
        }
        
        .headerbox {
            position: relative;
        }
        .headerboxBg {
            background-color: #9FBE3F;
            height: 353px;
            left: 0;
            position: absolute;
            top: 0;
            width: 100%;
        }
        .headerboxBg div {
            border-bottom: 2px solid #FFFFFF;
            border-top: 2px solid #FFFFFF;
            bottom: 4px;
            font-size: 0;
            height: 1px;
            line-height: 0;
            overflow: hidden;
            position: absolute;
            width: 100%;
        }
        .mainBox {
            margin: 0;
            padding: 0;
        }
        .wrap {
            margin: 0 auto;
            position: relative;
            width: 971px;
            z-index: 0;
        }
        .topSearchBox {
            padding: 9px 0 4px;
        }
               
        .footerBox {
            background: url("/Images/gapLineBg.gif") repeat scroll 0 0 transparent;
            text-align: center;
            width: 100%;
        }
        .gapLineEdge {
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="content">
        <div class="headerbox">
            <div class="headerboxBg">
                <div></div>
            </div>
        </div>
        <div class="mainBox">
            <div class="wrap">
                <div class="topSearchBox">
                    
                </div>
            </div>
        </div>
        <div class="footerBox">
            <div class="gapLineEdge"></div>
        </div>
    </div>
    </form>
</body>
</html>

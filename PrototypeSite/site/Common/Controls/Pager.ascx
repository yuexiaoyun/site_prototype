<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="site.Common.Controls.Pager1" %>
<div id="pagerTable" runat="server" style="padding: 5px">
    <div style="float: left;">
        <asp:Label ID="lblResults" runat="server" Text="Showing 1 - 24 of 48 Results"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp; Page&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lbtnFirst" ToolTip="First Page" runat="server" OnCommand="Pager_Command"
            CommandName="First"></asp:LinkButton>
        <asp:LinkButton ID="lbtnPrevious" ToolTip="Previous Page" runat="server" OnCommand="Pager_Command"
            CommandName="Previous"></asp:LinkButton>
        <asp:LinkButton ID="lbtnPrevious10" ToolTip="Previous Screen" runat="server" OnCommand="Pager_Command"
            CommandName="Previous10"></asp:LinkButton>
    </div>
    <div style="float: left;">
        <asp:Panel ID="panelPageNumber" runat="server">
        </asp:Panel>
    </div>
    <div style="float: left;">
        <asp:LinkButton ID="lbtnNext10" ToolTip="Next Screen" runat="server" OnCommand="Pager_Command"
            CommandName="Next10"></asp:LinkButton>
        <asp:LinkButton ID="lbtnNext" ToolTip="Next Page" runat="server" OnCommand="Pager_Command"
            CommandName="Next"></asp:LinkButton>
        <asp:LinkButton ID="lbtnLast" ToolTip="Last Page" runat="server" OnCommand="Pager_Command"
            CommandName="Last"></asp:LinkButton>
        <asp:Label ID="lblCurrentPageNumber" runat="server" Visible="false">0</asp:Label><asp:Label
            ID="lblCurrentFirstPageNumber" runat="server" Visible="false">0</asp:Label>
    </div>
</div>
<br />
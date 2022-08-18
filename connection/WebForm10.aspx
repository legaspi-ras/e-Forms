<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WebForm10.aspx.vb" Inherits="connection.WebForm10" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div>

            <p> For Approval </p>
            <table style="width: 100%;">
                <tr>
                    <td class="auto-style1">&nbsp;  <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                    <td class="auto-style2">&nbsp; <asp:Button ID="Button1" runat="server" Text="Button" /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1" style="height: 240px">&nbsp;  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1195px">
                <Columns>
                    <asp:BoundField DataField="formTitle" HeaderText="Form Title" SortExpression="formTitle" />
                    <asp:BoundField DataField="formControlnum" HeaderText="Form Control Number" SortExpression="applicableSpecs" />
                    <asp:BoundField DataField="modifdate" DataFormatString="{0:d}" HeaderText="Modification Date" SortExpression="modifdate" />
                    <asp:BoundField DataField="filename" HeaderText="Filename" SortExpression="filename" />
                    <asp:BoundField DataField="requestorName" HeaderText="Requestor Name" SortExpression="requestorName" />
                    <asp:BoundField DataField="requestorDepartment" HeaderText="Requestor Department" SortExpression="requestorDepartment" />
                    <asp:BoundField DataField="requestStatus" HeaderText="Request Status" SortExpression="requestStatus" />
                    <asp:BoundField DataField="requestDate" DataFormatString="{0:d}" HeaderText="Request Date" SortExpression="requestDate" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" Text="view" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:eformsConnectionString %>" ProviderName="<%$ ConnectionStrings:eformsConnectionString.ProviderName %>" SelectCommand="SELECT tblmasterlist.formTitle, tblapprovalrequest.formControlnum, tblapprovalrequest.modifdate, tblapprovalrequest.filename, tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment, tblapprovalrequest.requestStatus, tblapprovalrequest.requestDate FROM tblmasterlist INNER JOIN tblapprovalrequest ON tblmasterlist.formControlnum = tblapprovalrequest.formControlnum"></asp:SqlDataSource>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></td>
                    <td class="auto-style2" style="height: 240px">&nbsp; <asp:HyperLink ID="HyperLink1" runat="server" Visible="False">Your selected pdf file will appear here.</asp:HyperLink></td>
                    <td style="height: 240px"></td>
                </tr>
                <tr>
                    <td class="auto-style1">
        
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style2">
        <asp:Button ID="Button2" runat="server" Text="Button" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <p> 
               
                
            </p>

 
        </div>
        
        <br />
       
  
        <br />
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Approver Name" SortExpression="approverName">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("approverName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("approverName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="emailAddress" HeaderText="Email Address" SortExpression="emailAddress" />
                <asp:TemplateField HeaderText="Approver Status" SortExpression="approverStatus">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("approverStatus") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlApproverstatus" runat="server">
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>Approved</asp:ListItem>
                            <asp:ListItem>Disapproved</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="approvDate" HeaderText="Approved Date" SortExpression="approvDate" />
                <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button3" runat="server" Text="save" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="Button4" runat="server" Text="logout" />
        <br />

</asp:Content>

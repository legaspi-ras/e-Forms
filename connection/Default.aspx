<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="connection._Default" %>


        <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
            
            <br />

           <div class="container-fluid">
        <div class = "row">
            <div class ="col">

                <div class="card">
                 <div class="card-body">
                    <h5 class="card-title">List of e-forms</h5>
                    <h6 class="card-subtitle mb-2 text-muted">Search the e-form your looking for</h6>
     
                     <div class="input-group mb-3">
                        <%-- <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>--%>
                       <%-- <button class="btn btn-outline-secondary" type="button" id="button-addon2">Button</button>--%>
                         <input type="text" id="txtsearch" runat="server"  class ="form-control" placeholder="" aria-label="Recipient's username" aria-describedby="button-addon2">
                         <asp:Button ID="btnsearch" runat="server" Text="Search"  class="btn btn-outline-secondary" />
                    </div>

                     <asp:GridView ID="gvfiles" class="table table-bordered table-condensed table-responsive table-hover " runat="server"  AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="OnPaging">
                         <Columns>
                             <asp:BoundField HeaderText="Form Control Number" DataField="formControlnum" SortExpression="formControlnum" />
                             <asp:BoundField HeaderText="Form Title" DataField="formTitle" SortExpression="formTitle" />
                             <asp:TemplateField HeaderText="Action">
                                 <ItemTemplate>
                                     <asp:Button ID="Button1" runat="server" class="btn btn-primary" CausesValidation="false" CommandName="Select" Text="View" />
                                 </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                     </asp:GridView>

                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:eformsConnectionString %>" ProviderName="<%$ ConnectionStrings:eformsConnectionString.ProviderName %>" SelectCommand="SELECT formControlnum, formTitle FROM tblform_masterlist"></asp:SqlDataSource>

                    </div>
                </div>

               </div>

           <div class ="col">

                <div class="card" >
                    <div class="card-body">
                        <h5 class="card-title">View File</h5>        
                                                     
                        <asp:HyperLink ID="HyperLink1" runat="server">Your selected file will be viewed here</asp:HyperLink> 

                        <asp:Literal ID="ltEmbed" runat="server" />

                    </div>
                </div>

           </div>

        </div>


    </div>


</asp:Content>

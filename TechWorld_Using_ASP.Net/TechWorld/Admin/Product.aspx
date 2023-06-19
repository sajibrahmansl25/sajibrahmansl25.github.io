<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="TechWorld.Admin.Product" %>
<%@ Import Namespace="TechWorld" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        /*For disappearing alert message*/
        window.onload = function (){
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID%>").style.display = "none";
            }, seconds * 1000);
        };

    </script>

    <script>

        function ImagePreview(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    $('#<%=imgProduct.ClientID %>').prop('src', e.target.result)

                        .width(200)

                        .height(200);
                };

                reader.readAsDataURL(input.files[0]);

            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content pt-0">
        <div class="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">

                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header">
                                </div>
                                <div class="card-block">
                                    <div class="row">
                                        <div class="col-sm-6 col-md-4 col-lg-4">
                                            <h4 class="sub-title">Product</h4>
                                            <div>

                                                <div class="form-group">

                                                    <label>Product Name</label>

                                                    <div>

                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"
                                                            placeholder="Enter Product Name" required></asp:TextBox>
                                                        <asp:HiddenField ID="hdnId" runat="server" Value="0" />

                                                    </div>

                                                </div>

                                                 <div class="form-group">

                                                    <label>Product Description</label>

                                                    <div>

                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"
                                                            placeholder="Enter Product Description" TextMode="MultiLine" required></asp:TextBox>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />

                                                    </div>

                                                </div>

                                                <div class="form-group">

                                                    <label>Product Image</label>
                                                    <div>

                                                        <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control"
                                                            onchange="ImagePreview(this);" />

                                                    </div>

                                                </div>

                                                 <div class="form-group">

                                                    <label>Product Price($)</label>

                                                    <div>

                                                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"
                                                            placeholder="Enter Product Price" required></asp:TextBox>
                                                        

                                                    </div>

                                                </div>

                                                <div class="form-group">

                                                    <label>Product Quantity</label>

                                                    <div>

                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"
                                                            placeholder="Enter Product Quantity" required></asp:TextBox>
                                                        

                                                    </div>

                                                </div>

                                                 <div class="form-group">

                                                    <label>Product Category</label>

                                                    <div>
                                                        <asp:DropDownList ID="ddlCategories" CssClass="form-control" runat="server" required DataTextField="Name" DataValueField="CategoryId" DataSourceID="SqlDataSource1" AppendDataBoundItems="true">
                                                            <asp:ListItem value="0">Select Category</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CategoryId], [Name] FROM [Categories]"></asp:SqlDataSource>

                                                    </div>

                                                </div>

                                                <div class="form-check pl-4">

                                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive"
                                                        CssClass="form-check-input" />
                                                </div>

                                                <div class="pb-5">

                                                    <asp:Button ID="btnAddOrUpdate" runat="server" Text="Add" CssClass="btn btn-primary"
                                                        OnClick="btnAddOrUpdate_Click"/>

                                                    &nbsp;
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary"
                                                        CausesValidation="false" OnClick="btnClear_Click" />

                                                </div>

                                                <div>

                                                    <asp:Image ID="imgProduct" runat="server" CssClass="img-thumbnail" />

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                            <h4 class="sub-title">Category Lists</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">

                                                    <asp:Repeater Id="rProduct" runat="server" OnItemCommand="rProduct_ItemCommand"
                                                        OnItemDataBound="rProduct_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">Name</th>
                                                                        <th>Image</th>
                                                                        <th>Price($)</th>
                                                                        <th>Quantity</th>
                                                                        <th>Category</th>
                                                                        <th>IsActive</th>
                                                                        <th>Description</th>
                                                                        <th>CreatedDate</th>
                                                                        <th class="datatable-nosort">Action</th>
                                                                    </tr>
                                                               </thead>
                                                            <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("Name") %></td>
                                                                <td>
                                                                    <img width="40px" alt="" src="<%# TechWorld.Utils.GetImageUrl(Eval("ImageUrl")) %>"" />
                                                                </td>
                                                                <td><%# Eval("Price") %></td>
                                                                <td>
                                                                   <asp:Label ID="lblQuantity" runat="server" text='<%# Eval("Quantity") %>'></asp:Label> 
                                                                </td>
                                                                <td><%# Eval("CategoryName") %></td>
                                                                
                                                                <td>
                                                                   <asp:Label ID="lblIsActive" runat="server" text='<%# Eval("IsActive") %>'></asp:Label> 
                                                                </td>
                                                                <td><%# Eval("Description") %></td>
                                                                <td>
                                                                    <%# Eval("CreatedDate") %>

                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server"  CssClass="badge badge-primary" CausesValidation="false"
                                                                        Commandargument='<%# Eval("ProductId")%>' CommandName="edit">
                                                                        <i class="ti-pencil"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDelete" Text="Edit" runat="server" CommandName="delete"  CssClass="badge bg-danger"
                                                                        Commandargument='<%# Eval("ProductId")%>'
                                                                        OnClientClick="return confirm('Do you want to delete this category?');" CausesValidation="false">
                                                                        <i class="ti-trash"></i>
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>

                                        <div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

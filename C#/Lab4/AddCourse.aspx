<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="AddCourse.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h1>Add New Courses</h1>
        <div class="top-margin vertical-margin ">
                  
        <div class=" ">
             <div class="emphsis">Course Number:</div><asp:TextBox ID="courseNum" cssclass="input form-control" runat="server"></asp:TextBox>
            
            <asp:CustomValidator runat="server"  cssclass="error" ID="lblErrNum" ControlToValidate="courseNum" Display="Dynamic" ErrorMessage="Course with this code already exist" OnServerValidate="ExistanceValidator"/>
            <asp:RequiredFieldValidator runat="server"  cssclass="error" ID="rqdNum" ControlToValidate="courseNum" ErrorMessage="Required" Display="Dynamic" />
             <br />
            <div class="emphsis">Course Name: </div><asp:TextBox ID="courseName" cssclass="input form-control" runat="server"></asp:TextBox>
                
            <asp:RequiredFieldValidator runat="server"  cssclass="error" ID="rqdName" ControlToValidate="courseName" ErrorMessage="Required" Display="Dynamic" />
             
        
        </div>

        <br /><asp:Button runat="server" ID="btnCrsInfo" cssclass="btn btn-primary" Text="Submit Course Information" OnClick="btnAddCrsInfo"/>
    
        </div>

         <div>
            <p class="top-margin vertical-margin ">Following courses are currently in the system:</p>

              <asp:Table runat="server" ID="tblCourseInfo" CssClass="table">
                <asp:TableRow>
                    <asp:TableHeaderCell><a href="AddCourse.aspx?sort=code">Code</a></asp:TableHeaderCell>
                    <asp:TableHeaderCell><a href="AddCourse.aspx?sort=title">Course Title</a></asp:TableHeaderCell>  
                </asp:TableRow>
            </asp:Table>
        </div>


</asp:Content>

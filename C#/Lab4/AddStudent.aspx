<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h1>Course Information</h1>
     <div class="top-margin vertical-margin ">
          <div class="emphsis">Courses: </div>
            <asp:DropDownList OnSelectedIndexChanged="drpCourseSelection_SelectedIndexChanged" ID="drpCourseSelection" runat="server" CssClass="dropdown-toggle input form-control" 
             AutoPostBack="true" ViewStateMode="Inherit">
            <asp:ListItem Value="-1">Select a Course ... </asp:ListItem>
        </asp:DropDownList><br />

             <div class="emphsis">StudentID: </div>
                 <asp:TextBox ID="stdId" cssclass="input form-control" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server"  cssclass="error" ID="rqdId" ControlToValidate="stdId" ErrorMessage="Required" Display="Dynamic" />
              <asp:CustomValidator runat="server"  cssclass="error" ID="lblErrStdId" ControlToValidate="stdId" Display="Dynamic" ErrorMessage="The system already has a record of this student for the selected course" OnServerValidate="ExistanceValidator"/>
            <br /><div class="emphsis">Student Name: </div>
                <asp:TextBox ID="stdName" cssclass="input form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server"  cssclass="error" ID="rqdName" ControlToValidate="stdName" ErrorMessage="Required" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="RegularExpValidator" ValidationExpression="[a-zA-Z]+\s+[a-zA-Z]+" ControlToValidate="stdName" CssClass="error" Display="Dynamic" ErrorMessage="Must be in first_name last_name!" runat="server"/>
            <br /><div class="emphsis">Grade (0-100):</div> 
                <asp:TextBox ID="stdGrade" cssclass="input form-control" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server"  cssclass="error" ID="rqdGrade" ControlToValidate="stdGrade" ErrorMessage="Required" Display="Dynamic" />
        <asp:RangeValidator runat="server" ID="rngGrade" cssclass="error" ControlToValidate="stdGrade" MaximumValue="100" MinimumValue="0" Type="Integer" ErrorMessage="Must be an from 0 to 100"  Display="Dynamic" />
        
        </div>
          <div class="top-margin vertical-margin ">
            <asp:Button runat="server"  ID="btnAdd" Text="Add to Course Record" cssclass="btn btn-primary" OnClick="btnAdd_Click"/> &nbsp; &nbsp;
            
        </div>
   
        <div class="top-margin vertical-margin ">
            <p>The selected course has the following student records:</p>
            
            <asp:Table runat="server" ID="tblStudentInfo" CssClass="table">
                <asp:TableRow>
                    <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Name</asp:TableHeaderCell>    
                    <asp:TableHeaderCell>Grade</asp:TableHeaderCell> 
                </asp:TableRow>
            </asp:Table>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>


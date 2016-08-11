<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ContentPage.aspx.cs" Inherits="Sample.ContentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <!-- Main component for a primary marketing message or call to action -->
    <div class="jumbotron">
        <h1>File Converter</h1>
        <p>
            This is a test project that allows you to convert your only Doc. and Docx file into multiple formates. It will use a REST API to convert you
            file. The REST API will convert given file into to required formate by using aspos.words for .NET
            Aspos.Word For .Net
        </p>
        <p>
            &nbsp;</p>
        <div>
            <form id="form1" runat="server">
                <asp:FileUpload ID="uplFileUploader" runat="server" />
                <br />
                <br />
                <br>
                <asp:Label ID="Label1" runat="server" Text="Convert into"></asp:Label>
                <br>
                <br />
                <asp:RadioButton Text="html" ID="html" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="bmp" ID="bmp" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="doc" ID="doc"  GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="docm" ID="docm" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="docx" ID="docx" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="dot" ID="dot" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="dotm" ID="dotm" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="dotx" ID="dotx" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="emf" ID="emf" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="epub" ID="epub" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="flatopc" ID="flatopc" GroupName="formate" runat="server" />
                <br />
                <asp:RadioButton Text="jpeg" ID="jpeg" runat="server" GroupName="formate"/>
                <br />
                <asp:RadioButton Text="pdf" ID="pdf" runat="server" GroupName="formate"/>
                <br />
                <br />
                <br />
                <br>

                <asp:Button ID="btnUploadToDb" runat="server" Text="Convert" OnClick="btnUploadToDb_Click" />
                <br>
                <asp:Label ID="lblUploadResult" runat="server" Text=""></asp:Label>
            </form>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>

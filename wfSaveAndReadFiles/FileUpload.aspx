<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="wfSaveAndReadFiles.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us">
<head runat="server">
    <title>File Upload</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
</head>

<script type="text/javascript">
    //activy click select files
    function EnableInputClick() {
        document.getElementById("FileUpload1").click()
    }

    //listen event change from fileUpload
    function load() {
        var el = document.getElementById("FileUpload1");
        el.addEventListener("change", () => document.getElementById("lbChooseFile").innerText = el.value.substring(12), false);
    }

    //listen event change from fileUpload
    document.addEventListener("DOMContentLoaded", load, false);
</script>

<style type="text/css">

    .form-control {
        padding: 0;
    }

    .form-control .btn {
        border-radius: 0;
    }
    
    #FileUpload1 {
        display: none;
    }
</style>

<body>
    <form id="form1" runat="server">
        <div>
            <h1>File Upload</h1>
            <div>
                <div class="row">                    
                    <div class="col-md-6">   
                       <div class="form-control">
                           <asp:HyperLink ID="hlChooseFile" runat="server" CssClass="btn btn-secondary" onClick="EnableInputClick()">Choose File</asp:HyperLink>
                           <asp:Label ID="lbChooseFile" runat="server" Text="Label" class="form-label">No file selected.</asp:Label>
                       </div>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-label form-control"/>
                    </div>
                    <div class="col-md-3">                    
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="218px" OnClick="btnUpload_Click" CssClass="btn btn-secondary"/>
                    </div>
                </div>
                <br />
                <br />
                <asp:Label ID="lbMessage" runat="server" Text="Label" Visible="False" class="alert alert-danger"></asp:Label>
                <br />
                <br />
                <asp:Table ID="Table1" runat="server" CssClass="table table-striped">
                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>

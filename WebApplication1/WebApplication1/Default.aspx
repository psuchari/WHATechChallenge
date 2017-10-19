<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Risk Analysis</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Settled Bets</h2>
            <p>
                Upload a csv file containing settled bet data.
            </p>
            <p>
                <input class="btn btn-default" type=file id=SettledBetsFile name=SettledBetsFile runat="server" onchange="ValidateSettledFile(this.value);">
            </p>
        </div>
        <div class="col-md-4">
            <h2>Unsettled Bets</h2>
            <p>
                Upload a csv file containing unsettled bet data.
            </p>
            <p>
                <input class="btn btn-default" type=file id=UnsettledBetsFile name=UnsettledBetsFile runat="server" onchange="ValidateUnsettledFile(this.value);">
            </p>
        </div>
        <div class="col-md-4">
            <h2>Analyse Bets</h2>
            <p>
                <span style="background-color:yellow;">Risky</span> - When a customer wins more than 60% of their bets<br />
                <span style="color:orange;">Unusual</span> - When a customer stake is 10 times higher than their average bet<br />
                <span style="color:red;">Highly Unusual</span> - When a customer stake is 30 times higher than their average bet<br />
                <span style="font-weight:700">Large Payout</span> - When a bet will win more than $1000
            </p>
            <p>                
                <asp:Button ID="AnalyseBets" runat="server" Text="Analyse" OnClick="AnalyseBets_Click" disabled="true"/>
            </p>
        </div>
    </div>

    <div class="row">
        <asp:GridView ID="GridViewSettledBets" runat="server" OnRowDataBound="GridViewSettledBets_OnRowDataBound"></asp:GridView>
    </div>

    <div class="row">
        <asp:GridView ID="GridViewUnsettledBets" runat="server" OnRowDataBound="GridViewUnsettledBets_OnRowDataBound"></asp:GridView>
    </div>

    <div class="row">
        <asp:Label ID="LabelError" runat="server" /> 
    </div>

    <script type="text/javascript">
        function ValidateSettledFile(fileName) {
            if (ValidateCSVFile(fileName)) {
                if (ValidateCSVFile(document.getElementById('<%=UnsettledBetsFile.ClientID%>').value, true)) document.getElementById('<%=AnalyseBets.ClientID%>').disabled = false;
                return true;
            }
            document.getElementById('<%=AnalyseBets.ClientID%>').disabled = true;
            return false;
        }

        function ValidateUnsettledFile(fileName) {
            if (ValidateCSVFile(fileName)) {
                if (ValidateCSVFile(document.getElementById('<%=SettledBetsFile.ClientID%>').value, true)) document.getElementById('<%=AnalyseBets.ClientID%>').disabled = false;
                return true;
            }
            document.getElementById('<%=AnalyseBets.ClientID%>').disabled = true;
            return false;
        }

        function ValidateCSVFile(fileName, SuppressPromptOnError = false) {
            if (fileName.split('.').pop() == "csv") return true;   
            if (!SuppressPromptOnError) alert("Please select a csv file.");
            return false;
        }
    </script>

</asp:Content>

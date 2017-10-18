<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Risk Analysis</h1>
        <p class="lead">
            
        </p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Settled Bets</h2>
            <p>
                Upload a csv file containing settled bet data.
            </p>
            <p>
                <input class="btn btn-default" type=file id=SettledBetsFile name=SettledBetsFile runat="server" >
            </p>
        </div>
        <div class="col-md-4">
            <h2>Unsettled Bets</h2>
            <p>
                Upload a csv file containing unsettled bet data.
            </p>
            <p>
                <input class="btn btn-default" type=file id=UnsettledBetsFile name=UnsettledBetsFile runat="server" >
            </p>
        </div>
        <div class="col-md-4">
            <h2>Analyse Bets</h2>
            <p>
                Risky - When a customer wins more than 60% of their bets<br />
                Unusual - When a customer stake is 10 times higher than their average bet<br />
                Highly Unusual - When a customer stake is 30 times higher than their average bet<br />
                Large Win - When a bet will win more than $1000
            </p>
            <p>
                <input class="btn btn-default" type="submit" id="AnalyseBets" value="Analyse" runat="server" NAME="AnalyseBets">
            </p>
        </div>
    </div>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="NewsFlashUI.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/slideshow.css" rel="stylesheet" />
    <title>NewsFlash</title>
    <script type="text/javascript">
        var appInsights = window.appInsights || function (config) {
            function s(config) { t[config] = function () { var i = arguments; t.queue.push(function () { t[config].apply(t, i) }) } } var t = { config: config }, r = document, f = window, e = "script", o = r.createElement(e), i, u; for (o.src = config.url || "//az416426.vo.msecnd.net/scripts/a/ai.0.js", r.getElementsByTagName(e)[0].parentNode.appendChild(o), t.cookie = r.cookie, t.queue = [], i = ["Event", "Exception", "Metric", "PageView", "Trace"]; i.length;) s("track" + i.pop()); return config.disableExceptionTracking || (i = "onerror", s("_" + i), u = f[i], f[i] = function (config, r, f, e, o) { var s = u && u(config, r, f, e, o); return s !== !0 && t["_" + i](config, r, f, e, o), s }), t
        }({
            instrumentationKey: "6555cf60-7127-4e13-b048-b9e51e84687f"
        });

        window.appInsights = appInsights;
        appInsights.trackPageView();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%">
            <tr id="header" style="height: 50px; background-color: white; text-align: left;">
                <td style="font-family: 'Segoe UI'; color: darkgray; font-weight: bold; text-align: left; vertical-align: middle">
                    <img src="Content/NewsFlash.png" height="50px" />&nbsp;NewsFlash
                </td>
                <td style="width: 30%; font-family: 'Segoe UI'; color: darkgray; text-wrap: avoid; text-align: right">Hello, Ilana!</td>
            </tr>
            <tr id="trCategories" style="background-color: lightgray">
                <td colspan="2">
                    <asp:Table runat="server" ID="tblCategories" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                                    <asp:Button runat="server" ID="btnBusiness" Width="150px" Height="150px" ForeColor="White" BackColor="RoyalBlue" Text="Business" OnClick="btnBusiness_Click" />
                            </asp:TableCell>
                            <asp:TableCell>&nbsp;&nbsp;
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button runat="server" ID="btnfashion" Text="Fashion" OnClick="btnBusiness_Click" Width="150px" Height="150px" ForeColor="White" BackColor="Teal" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button runat="server" ID="Button1" Text="Sports" OnClick="btnBusiness_Click" Width="150px" Height="150px" ForeColor="White" BackColor="Magenta" />

                            </asp:TableCell>
                            <asp:TableCell>&nbsp;&nbsp;
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button runat="server" ID="Button2" Text="Technology" OnClick="btnBusiness_Click" Width="150px" Height="150px" ForeColor="White" BackColor="OrangeRed" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button runat="server" ID="Button3" Text="World News" OnClick="btnBusiness_Click" Width="150px" Height="150px" ForeColor="White" BackColor="PaleTurquoise" />

                            </asp:TableCell>
                            <asp:TableCell>&nbsp;&nbsp;
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button runat="server" ID="Button4" Text="Food" OnClick="btnBusiness_Click" Width="150px" Height="150px" ForeColor="White" BackColor="RoyalBlue" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <dx:ASPxImageSlider ID="ASPxImageSlider1" runat="server" ClientInstanceName="ImageSlider" SettingsImageArea-AnimationType="None" Visible="false"
                                            EnableViewState="False" EnableTheming="False" NavigateUrlFormatString="javascript:void({0});" ShowNavigationBar="false">
                                            <SettingsImageArea NavigationButtonVisibility="None" ImageSizeMode="FitProportional" ItemTextVisibility="Always" EnableLoopNavigation="true" />
                                            <SettingsSlideShow AutoPlay="true" StopPlayingWhenPaging="false" PausePlayingWhenMouseOver="false" PlayPauseButtonVisibility="Faded" Interval="250"></SettingsSlideShow>
                                            <Styles>
                                                <ImageArea Width="100%" Height="600" BackColor="white" />
                                            </Styles>
                                            <ItemTextTemplate>
                                                <h3 class='isdemoH3'><%# Eval("Text") %></a></h3>
                                            </ItemTextTemplate>
                                        </dx:ASPxImageSlider>
                </td>
            </tr>
            <tr id="footer" style="height: 40px; background-color: white">
                <td colspan="2" style="color: darkgray; font-family: 'Segoe UI'">Copyright NewsFlash 2015. All rights reserved.</td>
            </tr>
        </table>
    </form>
</body>
</html>

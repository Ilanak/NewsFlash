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
            <tr id="header" style="height: 50px; background-color: white; text-align:left;">
                <td style="font-family: 'Segoe UI'; color: darkgray; font-weight: bold; text-align: left; vertical-align:middle">
                    <img src="Content/NewsFlash.png" height="50px" />&nbsp;NewsFlash
                </td>
                <td style="width: 30%; font-family: 'Segoe UI'; color: darkgray; text-wrap:avoid;text-align:right">Hello, Ilana!</td>
            </tr>
            <%--<tr><td>&nbsp;</td></tr>--%>
            <tr id="content" style="background-color: lightgray">
                <td colspan="2">
                    <dx:ASPxImageSlider ID="ASPxImageSlider1" runat="server" ClientInstanceName="ImageSlider" SettingsImageArea-AnimationType="None"
                        EnableViewState="False" EnableTheming="False" NavigateUrlFormatString="javascript:void({0});" ShowNavigationBar="false">
                        <SettingsImageArea ImageSizeMode="FitProportional" ItemTextVisibility="Always" EnableLoopNavigation="true" />
                        <SettingsSlideShow AutoPlay="true" StopPlayingWhenPaging="false" PausePlayingWhenMouseOver="false" PlayPauseButtonVisibility="Faded" Interval="200"></SettingsSlideShow>
                        <Styles>
                            <ImageArea Width="70%" Height="600" />
                        </Styles>
                        <ItemTextTemplate>
                            <h3 class='isdemoH3'><a tabindex="-1" href='<%# Eval("NavigateUrl") %>' target="_blank"><%# Eval("Text") %></a></h3>
                            <p><%# Eval("Name") %></p>
                        </ItemTextTemplate>
                    </dx:ASPxImageSlider>
                </td>
            </tr>
            <%--<tr><td>&nbsp;</td></tr>--%>
            <tr id="footer" style="height: 40px; background-color: white">
                <td colspan="2" style="color: darkgray; font-family: 'Segoe UI'">Copyright NewsFlash 2015. All rights reserved.</td>
            </tr>
        </table>
    </form>
</body>
</html>

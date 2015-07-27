<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="NewsFlashUI.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/slideshow.css" rel="stylesheet" />
    <title>NewsFlash</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxImageSlider ID="ASPxImageSlider1" runat="server" ClientInstanceName="ImageSlider"
        EnableViewState="False" EnableTheming="False" NavigateUrlFormatString="javascript:void({0});" >
        <SettingsImageArea ImageSizeMode="FitProportional" NavigationButtonVisibility="None"
            ItemTextVisibility="Always" EnableLoopNavigation="true" />
        <SettingsNavigationBar Mode="Dots" />
        <SettingsSlideShow AutoPlay="true" StopPlayingWhenPaging="false" PausePlayingWhenMouseOver="false" PlayPauseButtonVisibility="Faded" Interval="1000"></SettingsSlideShow>
        <Styles>
            <ImageArea Width="1000" Height="600" />
        </Styles>
            <ItemTextTemplate>
                <h3 class='isdemoH3'><%# Eval("Text") %></h3>
                    <p><%# Eval("Name") %>. <a tabindex="-1" href='<%# Eval("NavigateUrl") %>'>»</a></p>
            </ItemTextTemplate>
    </dx:ASPxImageSlider>
    </div>
    </form>
</body>
</html>
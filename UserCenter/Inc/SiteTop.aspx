<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteTop.aspx.cs" Inherits="UserCenter.Inc.SiteTop" %>
<%@ OutputCache CacheProfile="cache1" %>
function exit(){if (confirm("确认退出平台吗？"))return true;else return false;}
function gameshow(){document.getElementById('youxiliebiao').style.display= 'block';}
function gamehidden(){document.getElementById('youxiliebiao').style.display = 'none';}
function setHomepage(){if(document.all){document.body.style.behavior='url(#default#homepage)';document.body.setHomePage('http://www.dao50.com');}else if(window.sidebar){if(window.netscape){try{netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");}catch (e){alert("该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入about:config,然后将项signed.applets.codebase_principal_support值该为true");}} var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components. interfaces.nsIPrefBranch);prefs.setCharPref('browser.startup.homepage','http://www.dao50.com');}}

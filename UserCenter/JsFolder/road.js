/* 本文件用于与应用程序交互，若无必要，切勿改动！ */
/* 将以下代码插入到<head>和</head>直接即可调用这些函数
   <script src="road.js"></script>
*/

//选服完毕，告诉应用程序游戏的swf文件完整url，包括参数
function LoadGame(flashurl)
{
    location.href='app://loadgame|'+flashurl;
}


//页面加载完毕，告诉应用程序显示界面
function LoadFinished()
{
    location.href='app://finished';
}
//最小化
function Minimize()
{
    location.href='app://minimize|';
}
//重新运行程序
function ReloadMe()
{
    location.href='app://reloadme|';
}
//关闭程序
function ExitGame()
{
    location.href='app://exitgame|';
}
//LoadFinished();
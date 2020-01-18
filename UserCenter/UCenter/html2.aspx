<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTML2.aspx.cs" Inherits="HTML2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type ="text/css">
        table {
            width: 500px;
            border: 1px solid gray;
            /*合并文本框使之无缝隙*/
            border-collapse: collapse;
        }
            table th, table td {
                line-height:30px;
                text-align:center;
                border:1px solid gray;

            }
        /*创建一个表格的示例包括各种细节等*/
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
 <%  for (int i = 0; i < dt.Rows.Count; i++){%>
       <br> <% = dt.Rows[i][0] %>  </br>
       <%--<input type ="button" value = "del<% =i %>" />--%>
        <button type="button" value ="<% =i %>"">删除</button>
        <button type="button" value ="<% =i %>"">增加</button>
        <button type="button" value ="<% =i %>"">修改</button>


        <%} %>

  <%--=================================================--%>
        <script type ="text/javascript">

          //  创建一个表格对象
  
            var tab = document.createElement('table');//table制作完毕
            //标题制作完毕
            var cap = document.createElement('caption');
            cap.innerHTML = '信息表';
            tab.appendChild(cap);
            var tr = document.createElement('tr');
            var th1 = document.createElement('th');
            th1.innerHTML = '编号';
            var th2 = document.createElement('th');
            th2.innerHTML = '姓名';;
            var th3 = document.createElement('th');
            th3.innerHTML = '年龄';
            var th4 = document.createElement('th');
            th4.innerHTML = '操作';
            tr.appendChild(th1);
            tr.appendChild(th2);
            tr.appendChild(th3);
            tr.appendChild(th4);
     
            
            tab.appendChild(tr);
          
            var tb_tr1 = document.createElement('tr');




            var tr1_td1 = document.createElement('td');//一列一空
            var tr1_td2 = document.createElement('td');//一列二空
            var tr1_td3 = document.createElement('td');//一列三空
            var tr1_td4 = document.createElement('td');//一列四空



            var _link = document.createElement('a');
            _link.href = '#';


            tr1_td1.innerHTML = '1';//编号
            tr1_td2.innerHTML = 'jack';//姓名
            tr1_td3.innerHTML = '20';//年龄



            //   //将列添加到行内
            tb_tr1.appendChild(tr1_td1);
            tb_tr1.appendChild(tr1_td2);
            tb_tr1.appendChild(tr1_td3);
            tb_tr1.appendChild(tr1_td4);

            _link.innerHTML = '删除';
            tr1_td4.appendChild(_link);

            //行加表内
            tab.appendChild(tb_tr1);

            // 将表格添加到页面中
            document.body.appendChild(tab);
            
            //$(function(){for (var i = 0 ; i < tab.rows.length; i++)
            //    {

               
            //}
            //})

            
              
         

      
        </script>     


    </div>
    </form>
</body>
</html>

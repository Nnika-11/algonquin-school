<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title></title>
    </head>
    <body>
        <?php
        // put your code here
        date_default_timezone_get("America/Toronto");
        $today=date("Y-m-d");
        ?>
        <h2>Welcome to PHP!</h2>
        Today is <?php print( $today); ?>
 
    </body>
</html>


<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
  <title>Validation</title>
</head>

<body>
  <div class="container m-auto border-left">
    <div class="pl-3 m-5">
        <h2 class="mb-3">Thank you<?php if(!empty ($_GET["inputName"])) echo ", ", trim($_GET["inputName"]),","; ?> for using  our deposit calculator!</h2>
    <?php
        extract( $_GET );
        
        $errorMsg = "  <p>However, we can not process your request because following following invalid data:</p>"
                . "<ul class=text-danger>";
        $MSG = $errorMsg;
//       &inputPost=K2L4A4&inputTel=6138795798&inputEmail=sherr.veronika%40gmail.com
        if (empty($inputAmount)){
        $errorMsg .= "<li>Pricincipal Amount field can not be blank.</li>";
        } elseif (!is_numeric($inputAmount)||$inputAmount <= 0) {
             $errorMsg .= "<li>Pricincipal Amount must be a numeric and greater than 0</li>";
        }
        if (empty($inputRate)) {
            $errorMsg .= "<li>Interest Rate field can not be blank.</li>";
        } elseif (!is_numeric($inputRate)||$inputRate <= 0) {
             $errorMsg .= "<li>Interest Rate must be a numeric and non-negative</li>";
        } if($depYears=="0"){
            $errorMsg .= "<li>Must select number of years to deposit.</li>";
        } if (empty($inputName)) {
             $errorMsg .= "<li>Name field can not be blank.</li>";
        } if (empty($inputPost)) {
             $errorMsg .= "<li>Postal Code can not be blank.</li>";
        } if (empty($inputTel)) {
             $errorMsg .= "<li>Phone Number can not be blank.</li>";
        } if (empty($inputEmail)) {
             $errorMsg .= "<li>Email Address can not be blank.</li>";
        } if($Contact==="Phone" && !(isset($morningCall)||isset($dayCall)||isset($evenCall))){
            $errorMsg .= "<li>When contact method is phone, you have to select one or more contact times.</li>";
        }
       
        if($MSG != $errorMsg){
            echo "$errorMsg </ul>";
        } else{
print <<<Datatbl
            <p> Our customer service department will contact you within 2 days</p>
            <p> The following is the result of the calculation:</p>
            <table class="table mt-2">
          <thead class="thead-dark">
            <tr>
              <th scope="col">Year</th>
              <th scope="col">Principal at Year Start</th>
              <th scope="col">Interest for the Year</th>
            </tr>
          </thead>
          <tbody>
Datatbl;

            $runningPrincipal = $inputAmount;
            for($i = 1; $i <= $depYears; ++$i)
	{
		$interest = $runningPrincipal * $inputRate * 0.01;
                printf ("<tr><th scope=\"row\">%s</th><td>\$%.2f</td><td>\$%.2f</td></tr>", $i, $runningPrincipal, $interest);
                $runningPrincipal += $interest;
	}


print <<<Datatbl
          </tbody>
        </table>
Datatbl;
        }
    ?>
      
     
      
        
        <button type="submit" onclick="history.back()" class="btn btn-primary"> Return </button>
    </div>
  </div>
</body>

</html>

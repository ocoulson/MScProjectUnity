<?
//Based on Tutorial at http://forum.unity3d.com/threads/tutorial-unity-and-php-login-script-simple-but-useful.24721/
// CONNECTIONS =========================================================
$host = "localhost"; //put your host here
$user = "ocoulson"; //in general is root
$password = "Ch4rml3ss"; //use your password here
$dbname = "BioDomeDB"; //your database
$connect = mysql_connect($host, $user, $password) or die("Cant connect to mysql database".mysql_error());
mysql_select_db($dbname, $connect)or die("Cant connect to database" . mysql_error());
// =============================================================================
// PROTECT AGAINST SQL INJECTION and CONVERT PASSWORD INTO MD5 formats
function anti_injection_senha($sql, $formUse = true)
{
$sql = preg_replace("/(from|select|insert|delete|where|drop table|show tables|,|'|#|\*|--|\\\\)/i","",$sql);
$sql = trim($sql);
$sql = strip_tags($sql);
if(!$formUse || !get_magic_quotes_gpc())
  $sql = addslashes($sql);
  $sql = md5(trim($sql));
return $sql;
}
// THIS ONE IS JUST FOR THE NICKNAME PROTECTION AGAINST SQL INJECTION
function anti_injection($sql, $formUse = true)
{
$sql = preg_replace("/(from|select|insert|delete|where|drop table|show tables|,|'|#|\*|--|\\\\)/i","",$sql);
$sql = trim($sql);
$sql = strip_tags($sql);
if(!$formUse || !get_magic_quotes_gpc())
  $sql = addslashes($sql);
return $sql;
}
// =============================================================================
$unityHash = anti_injection($_POST["myform_hash"]);
$phpHash = "BioDomeUnity16062908"; // same code in here as in your Unity game
 
$playerUsername = anti_injection($_POST["myform_username"]); 
$playerPassword = anti_injection_senha($_POST["myform_password"]);
$playerQuestion = anti_injection($_POST["myform_question"]);
$playerAnswer = anti_injection($_POST["myform_answer"]);

if(!$playerUsername || !$playerPassword || !$playerQuestion || !$playerAnswer) {
    echo "Registration fields cant be empty. '$playerUsername', '$playerPassword', '$playerQuestion', '$playerAnswer'";
} else {
    if ($unityHash != $phpHash){
        echo "HASH codes don't match.";
    } else {
        $SQL = "SELECT * FROM BioDomeUsers WHERE 'Username' = '$playerUsername' LIMIT 1";
        $result = mysql_query($SQL) or die ('Query failed: ' . mysql_error());

        if(mysql_num_rows($result) > 0) {
            
            echo "Username taken, choose another";
        } else {
            $SQL = "INSERT INTO BioDomeUsers VALUES ('$playerUsername','$playerPassword', '$playerQuestion', '$playerAnswer', NULL);";
            $result = mysql_query($SQL) or die ('Query failed: ' . mysql_error());
            echo "Player registered, return and login.";
        }
    }
}

mysql_close();
?>
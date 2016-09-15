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
function anti_injection_login_senha($sql, $formUse = true)
{
$sql = preg_replace("/(from|select|insert|delete|where|drop table|show tables|,|'|#|\*|--|\\\\)/i","",$sql);
$sql = trim($sql);
$sql = strip_tags($sql);
$sql = mysql_real_escape_string($sql);
if(!$formUse || !get_magic_quotes_gpc())
  $sql = addslashes($sql);
  $sql = md5(trim($sql));
return $sql;
}
// THIS ONE IS JUST FOR THE NICKNAME PROTECTION AGAINST SQL INJECTION
function anti_injection_login($sql, $formUse = true)
{
$sql = preg_replace("/(from|select|insert|delete|where|drop table|show tables|,|'|#|\*|--|\\\\)/i","",$sql);
$sql = trim($sql);
$sql = strip_tags($sql);
$sql = mysql_real_escape_string($sql);
if(!$formUse || !get_magic_quotes_gpc())
  $sql = addslashes($sql);
return $sql;
}
// =============================================================================
$unityHash = anti_injection_login($_POST["myform_hash"]);
$phpHash = "BioDomeUnity16062908"; // same code in here as in your Unity game
 
$playerUsername = anti_injection_login($_POST["myform_username"]); 
$playerPassword = anti_injection_login_senha($_POST["myform_password"]);

if(!$playerUsername || !$playerPassword || $saveGameData) {
    echo "Username, password or save game data cant be empty.";
} else {
    if ($unityHash != $phpHash){
        echo "HASH codes don't match.";
    } else {
        $SQL = "SELECT * FROM BioDomeUsers WHERE Username = '" . $playerUsername . "'";
        $result_id = mysql_query($SQL) or die("DATABASE ERROR!: " . mysql_error());
        $total = mysql_num_rows($result_id);
        if($total) {
            $datas = mysql_fetch_array($result_id);
            if(!strcmp($playerPassword, $datas["Password"])) {
                if ($datas["SaveGameData"] != null) {
                    echo $datas["SaveGameData"];
                } else {
                    echo "No Saved Data found";
                }
            } else {
                echo "Username or password is wrong.";
            }
        } else {
            echo "Username not found.";
        }
    }
}

mysql_close();
?>